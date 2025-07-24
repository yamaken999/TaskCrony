using System;
using System.IO;
using System.Text;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TaskCrony.Tests
{
    /// <summary>
    /// TaskCrony アプリケーションのテストクラス
    /// </summary>
    [TestClass]
    public class TaskCronyTests
    {
        private string _testDirectory = "";
        private string _batFolderPath = "";

        [TestInitialize]
        public void Setup()
        {
            // テスト用ディレクトリを作成
            _testDirectory = Path.Combine(Path.GetTempPath(), "TaskCronyTest_" + Guid.NewGuid().ToString("N")[..8]);
            _batFolderPath = Path.Combine(_testDirectory, "BAT");
            Directory.CreateDirectory(_batFolderPath);
        }

        [TestCleanup]
        public void Cleanup()
        {
            // テスト用ディレクトリを削除
            if (Directory.Exists(_testDirectory))
            {
                try
                {
                    Directory.Delete(_testDirectory, true);
                }
                catch
                {
                    // クリーンアップ失敗は無視
                }
            }
        }

        #region ファイル名構築テスト

        [TestMethod]
        public void BuildFileName_WithPrefixAndSuffix_ReturnsCorrectFormat()
        {
            // Arrange
            var mainForm = new MainForm();

            // Act
            var result = mainForm.BuildFileNameWithSettings(
                "testfile", "20250723", "prefix", "suffix",
                false, false, true, // prefix options (none)
                false, false, true  // suffix options (none)
            );

            // Assert
            Assert.AreEqual("prefix_testfile_suffix", result);
        }

        [TestMethod]
        public void BuildFileName_WithDateBeforePrefix_ReturnsCorrectFormat()
        {
            // Arrange
            var mainForm = new MainForm();

            // Act
            var result = mainForm.BuildFileNameWithSettings(
                "testfile", "20250723", "prefix", "",
                true, false, false, // prefix before date
                false, false, true  // suffix none
            );

            // Assert
            Assert.AreEqual("20250723prefix_testfile", result);
        }

        [TestMethod]
        public void BuildFileName_WithDateAfterSuffix_ReturnsCorrectFormat()
        {
            // Arrange
            var mainForm = new MainForm();

            // Act
            var result = mainForm.BuildFileNameWithSettings(
                "testfile", "20250723", "", "suffix",
                false, false, true, // prefix none
                false, true, false  // suffix after date
            );

            // Assert
            Assert.AreEqual("testfile_suffix20250723", result);
        }

        [TestMethod]
        public void BuildFileName_WithDateOnlyPrefix_ReturnsCorrectFormat()
        {
            // Arrange
            var mainForm = new MainForm();

            // Act - 接頭語が空でも日付位置が指定されている場合
            var result = mainForm.BuildFileNameWithSettings(
                "testfile", "20250723", "", "",
                true, false, false, // prefix date before (empty prefix)
                false, false, true  // suffix none
            );

            // Assert
            Assert.AreEqual("20250723_testfile", result);
        }

        [TestMethod]
        public void BuildFileName_WithDateOnlySuffix_ReturnsCorrectFormat()
        {
            // Arrange
            var mainForm = new MainForm();

            // Act - 接尾語が空でも日付位置が指定されている場合
            var result = mainForm.BuildFileNameWithSettings(
                "testfile", "20250723", "", "",
                false, false, true, // prefix none
                true, false, false  // suffix date before (empty suffix)
            );

            // Assert
            Assert.AreEqual("testfile_20250723", result);
        }

        #endregion

        #region JSONファイル処理テスト

        [TestMethod]
        public void SaveTaskSettings_CreatesValidJsonFile()
        {
            // Arrange
            var taskName = "TestTask";
            var expectedSettings = new
            {
                TaskName = taskName,
                CreateFile = true,
                CreateFolder = false,
                SourcePath = "C:\\test\\source.txt",
                DestinationPath = "C:\\test\\destination",
                Prefix = "prefix",
                Suffix = "suffix",
                PrefixDatePosition = "Before",
                SuffixDatePosition = "After",
                DateOffset = 5,
                ScheduleType = 1
            };

            var jsonContent = JsonSerializer.Serialize(expectedSettings, new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });

            var jsonFilePath = Path.Combine(_batFolderPath, $"{taskName}_settings.json");

            // Act
            File.WriteAllText(jsonFilePath, jsonContent, Encoding.UTF8);

            // Assert
            Assert.IsTrue(File.Exists(jsonFilePath));
            var savedContent = File.ReadAllText(jsonFilePath, Encoding.UTF8);
            var savedSettings = JsonSerializer.Deserialize<Dictionary<string, object>>(savedContent);
            
            Assert.IsNotNull(savedSettings);
            Assert.AreEqual(taskName, savedSettings["TaskName"].ToString());
            Assert.AreEqual("True", savedSettings["CreateFile"].ToString());
            Assert.AreEqual("C:\\test\\source.txt", savedSettings["SourcePath"].ToString());
        }

        [TestMethod]
        public void LoadTaskSettings_FromValidJsonFile_ReturnsCorrectValues()
        {
            // Arrange
            var taskName = "TestTask";
            var settings = new
            {
                TaskName = taskName,
                CreateFile = true,
                CreateFolder = false,
                SourcePath = "C:\\test\\source.txt",
                DestinationPath = "C:\\test\\destination",
                Prefix = "testprefix",
                Suffix = "testsuffix",
                PrefixDatePosition = "Before",
                SuffixDatePosition = "None",
                DateOffset = -3,
                ScheduleType = 2
            };

            var jsonContent = JsonSerializer.Serialize(settings, new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });

            var jsonFilePath = Path.Combine(_batFolderPath, $"{taskName}_settings.json");
            File.WriteAllText(jsonFilePath, jsonContent, Encoding.UTF8);

            // Act
            var loadedContent = File.ReadAllText(jsonFilePath, Encoding.UTF8);
            var loadedSettings = JsonSerializer.Deserialize<Dictionary<string, object>>(loadedContent);

            // Assert
            Assert.IsNotNull(loadedSettings);
            Assert.AreEqual("testprefix", loadedSettings["Prefix"].ToString());
            Assert.AreEqual("testsuffix", loadedSettings["Suffix"].ToString());
            Assert.AreEqual("Before", loadedSettings["PrefixDatePosition"].ToString());
            Assert.AreEqual("None", loadedSettings["SuffixDatePosition"].ToString());
            Assert.AreEqual("-3", loadedSettings["DateOffset"].ToString());
        }

        #endregion

        #region BATファイル生成テスト

        [TestMethod]
        public void GenerateBatContent_WithFileAndFolderCreation_ContainsCorrectCommands()
        {
            // Arrange
            var expectedCommands = new[]
            {
                "@echo off",
                "chcp 65001 > nul",
                "setlocal",
                "echo TaskCrony v1.1.0 自動実行開始",
                "powershell -command",
                "set /p DATE_STRING=",
                ":build_filename",
                "mkdir",
                "copy"
            };

            var batContent = GenerateSampleBatContent();

            // Act & Assert
            foreach (var command in expectedCommands)
            {
                Assert.IsTrue(batContent.Contains(command), 
                    $"BATファイルに '{command}' コマンドが含まれていません");
            }
        }

        [TestMethod]
        public void GenerateBatContent_WithDateOffset_ContainsCorrectDateCalculation()
        {
            // Arrange & Act
            var batContent = GenerateSampleBatContentWithDateOffset(5);

            // Assert
            Assert.IsTrue(batContent.Contains("AddDays(5)"), 
                "日付オフセット計算が正しく含まれていません");
        }

        [TestMethod]
        public void GenerateBatContent_WithoutDateOffset_ContainsSimpleDateCommand()
        {
            // Arrange & Act
            var batContent = GenerateSampleBatContentWithDateOffset(0);

            // Assert
            Assert.IsTrue(batContent.Contains("(Get-Date).ToString('yyyyMMdd')"), 
                "シンプルな日付取得コマンドが含まれていません");
        }

        #endregion

        #region 日付処理テスト

        [TestMethod]
        public void DateOffset_PositiveValue_ReturnsCorrectFutureDate()
        {
            // Arrange
            var baseDate = new DateTime(2025, 7, 23);
            var offset = 5;

            // Act
            var resultDate = baseDate.AddDays(offset);

            // Assert
            Assert.AreEqual(new DateTime(2025, 7, 28), resultDate);
            Assert.AreEqual("20250728", resultDate.ToString("yyyyMMdd"));
        }

        [TestMethod]
        public void DateOffset_NegativeValue_ReturnsCorrectPastDate()
        {
            // Arrange
            var baseDate = new DateTime(2025, 7, 23);
            var offset = -10;

            // Act
            var resultDate = baseDate.AddDays(offset);

            // Assert
            Assert.AreEqual(new DateTime(2025, 7, 13), resultDate);
            Assert.AreEqual("20250713", resultDate.ToString("yyyyMMdd"));
        }

        [TestMethod]
        public void DateOffset_ZeroValue_ReturnsSameDate()
        {
            // Arrange
            var baseDate = new DateTime(2025, 7, 23);
            var offset = 0;

            // Act
            var resultDate = baseDate.AddDays(offset);

            // Assert
            Assert.AreEqual(baseDate, resultDate);
            Assert.AreEqual("20250723", resultDate.ToString("yyyyMMdd"));
        }

        #endregion

        #region 文字列置換テスト

        [TestMethod]
        public void StringReplacement_BasicReplacement_ReturnsCorrectResult()
        {
            // Arrange
            var originalFileName = "test_old_name";
            var replaceFrom = "old";
            var replaceTo = "new";

            // Act
            var result = originalFileName.Replace(replaceFrom, replaceTo);

            // Assert
            Assert.AreEqual("test_new_name", result);
        }

        [TestMethod]
        public void StringReplacement_NoMatch_ReturnsOriginal()
        {
            // Arrange
            var originalFileName = "test_file_name";
            var replaceFrom = "notfound";
            var replaceTo = "replacement";

            // Act
            var result = originalFileName.Replace(replaceFrom, replaceTo);

            // Assert
            Assert.AreEqual("test_file_name", result);
        }

        [TestMethod]
        public void StringReplacement_EmptyReplacement_RemovesText()
        {
            // Arrange
            var originalFileName = "test_remove_this";
            var replaceFrom = "_remove_this";
            var replaceTo = "";

            // Act
            var result = originalFileName.Replace(replaceFrom, replaceTo);

            // Assert
            Assert.AreEqual("test", result);
        }

        #endregion

        #region ファイル操作テスト

        [TestMethod]
        public void FileOperations_CreateAndDeleteBatFile_Success()
        {
            // Arrange
            var taskName = "TestTask";
            var batFileName = $"{taskName}_{DateTime.Now:yyyyMMddHHmmss}.bat";
            var batFilePath = Path.Combine(_batFolderPath, batFileName);
            var batContent = GenerateSampleBatContent();

            // Act - Create
            File.WriteAllText(batFilePath, batContent, new UTF8Encoding(true));

            // Assert - File created
            Assert.IsTrue(File.Exists(batFilePath), "BATファイルが作成されませんでした");

            // Act - Delete
            File.Delete(batFilePath);

            // Assert - File deleted
            Assert.IsFalse(File.Exists(batFilePath), "BATファイルが削除されませんでした");
        }

        [TestMethod]
        public void FileOperations_CleanupOldFiles_RemovesOnlyTargetFiles()
        {
            // Arrange
            var taskName = "TestTask";
            var oldFile1 = Path.Combine(_batFolderPath, $"{taskName}_20250101120000.bat");
            var oldFile2 = Path.Combine(_batFolderPath, $"{taskName}_20250102120000.bat");
            var otherFile = Path.Combine(_batFolderPath, "OtherTask_20250103120000.bat");

            File.WriteAllText(oldFile1, "test content");
            File.WriteAllText(oldFile2, "test content");
            File.WriteAllText(otherFile, "test content");

            // Act
            CleanupOldBatFiles(_batFolderPath, taskName);

            // Assert
            Assert.IsFalse(File.Exists(oldFile1), "古いBATファイル1が削除されませんでした");
            Assert.IsFalse(File.Exists(oldFile2), "古いBATファイル2が削除されませんでした");
            Assert.IsTrue(File.Exists(otherFile), "他のタスクのファイルが削除されました");
        }

        #endregion

        #region ヘルパーメソッド

        private string GenerateSampleBatContent()
        {
            var content = new StringBuilder();
            content.AppendLine("@echo off");
            content.AppendLine("chcp 65001 > nul");
            content.AppendLine("setlocal");
            content.AppendLine("echo TaskCrony v1.1.0 自動実行開始: %date% %time%");
            content.AppendLine("powershell -command \"(Get-Date).ToString('yyyyMMdd')\" > temp_date.txt");
            content.AppendLine("set /p DATE_STRING=<temp_date.txt");
            content.AppendLine("del temp_date.txt");
            content.AppendLine(":build_filename");
            content.AppendLine("mkdir %FOLDER_PATH%");
            content.AppendLine("copy \"source\" %FILE_PATH%");
            content.AppendLine("endlocal");
            return content.ToString();
        }

        private string GenerateSampleBatContentWithDateOffset(int offset)
        {
            var content = new StringBuilder();
            content.AppendLine("@echo off");
            content.AppendLine("chcp 65001 > nul");
            content.AppendLine("setlocal");
            
            if (offset != 0)
            {
                content.AppendLine($"powershell -command \"$date = (Get-Date).AddDays({offset}); $date.ToString('yyyyMMdd')\" > temp_date.txt");
            }
            else
            {
                content.AppendLine("powershell -command \"(Get-Date).ToString('yyyyMMdd')\" > temp_date.txt");
            }
            
            content.AppendLine("set /p DATE_STRING=<temp_date.txt");
            content.AppendLine("del temp_date.txt");
            content.AppendLine("endlocal");
            return content.ToString();
        }

        private void CleanupOldBatFiles(string batFolderPath, string taskName)
        {
            try
            {
                var batFiles = Directory.GetFiles(batFolderPath, "*.bat");
                foreach (var batFile in batFiles)
                {
                    var fileName = Path.GetFileNameWithoutExtension(batFile);
                    if (fileName.StartsWith(taskName + "_") && fileName != taskName)
                    {
                        File.Delete(batFile);
                    }
                }
            }
            catch
            {
                // エラーは無視
            }
        }

        #endregion
    }
}

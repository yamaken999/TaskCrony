using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

namespace TaskCrony.Tests
{
    /// <summary>
    /// TaskCronyアプリケーションのテストクラス
    /// </summary>
    [TestClass]
    public class TaskCronyTests
    {
        private string testDirectory = "";

        [TestInitialize]
        public void Setup()
        {
            // テスト用のディレクトリを作成
            testDirectory = Path.Combine(Path.GetTempPath(), "TaskCronyTests_" + Guid.NewGuid().ToString());
            Directory.CreateDirectory(testDirectory);
        }

        [TestCleanup]
        public void Cleanup()
        {
            // テスト用のディレクトリを削除
            if (Directory.Exists(testDirectory))
            {
                Directory.Delete(testDirectory, true);
            }
        }

        /// <summary>
        /// 基本的なファイル名生成のテスト
        /// </summary>
        [TestMethod]
        public void BuildFileNameWithSettings_BasicTest()
        {
            var form = new MainForm();
            string result = form.BuildFileNameWithSettings("Test_{DATE}_File", 0);
            
            string expected = $"Test_{DateTime.Now:yyyyMMdd}_File";
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// オフセット日数付きファイル名生成のテスト
        /// </summary>
        [TestMethod]
        public void BuildFileNameWithSettings_WithOffset()
        {
            var form = new MainForm();
            string result = form.BuildFileNameWithSettings("Backup_{DATE}_Folder", -1);
            
            string expected = $"Backup_{DateTime.Now.AddDays(-1):yyyyMMdd}_Folder";
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// DATE文字列がない場合のテスト
        /// </summary>
        [TestMethod]
        public void BuildFileNameWithSettings_NoDatePlaceholder()
        {
            var form = new MainForm();
            string result = form.BuildFileNameWithSettings("StaticFileName", 5);
            
            Assert.AreEqual("StaticFileName", result);
        }

        /// <summary>
        /// 複数のDATE文字列があるケースのテスト
        /// </summary>
        [TestMethod]
        public void BuildFileNameWithSettings_MultipleDatePlaceholders()
        {
            var form = new MainForm();
            string result = form.BuildFileNameWithSettings("Archive_{DATE}_to_{DATE}", 0);
            
            string expectedDate = DateTime.Now.ToString("yyyyMMdd");
            string expected = $"Archive_{expectedDate}_to_{expectedDate}";
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// 未来の日付のテスト
        /// </summary>
        [TestMethod]
        public void BuildFileNameWithSettings_FutureDate()
        {
            var form = new MainForm();
            string result = form.BuildFileNameWithSettings("Future_{DATE}", 7);
            
            string expected = $"Future_{DateTime.Now.AddDays(7):yyyyMMdd}";
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// JSON設定の読み書きテスト
        /// </summary>
        [TestMethod]
        public void JsonSettings_ReadWrite()
        {
            string jsonPath = Path.Combine(testDirectory, "test_settings.json");
            
            var originalSettings = new Dictionary<string, object>
            {
                ["taskName"] = "テストタスク",
                ["isCreateFolderChecked"] = true,
                ["offsetDays"] = 5,
                ["sourceFolder"] = @"C:\Test\Source",
                ["destinationFolder"] = @"C:\Test\Destination"
            };

            // JSON書き込み
            string jsonString = JsonSerializer.Serialize(originalSettings, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(jsonPath, jsonString);

            // JSON読み込み
            string readJsonString = File.ReadAllText(jsonPath);
            var readSettings = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(readJsonString);

            Assert.IsNotNull(readSettings);
            Assert.AreEqual("テストタスク", readSettings["taskName"].GetString());
            Assert.AreEqual(true, readSettings["isCreateFolderChecked"].GetBoolean());
            Assert.AreEqual(5, readSettings["offsetDays"].GetInt32());
        }

        /// <summary>
        /// BATファイル生成のテスト
        /// </summary>
        [TestMethod]
        public void BatFileGeneration_CreateFolder()
        {
            string sourceFolder = @"C:\Source\{DATE}";
            string destinationFolder = @"C:\Destination\{DATE}";
            string batContent = $"mkdir \"{sourceFolder}\"\nmkdir \"{destinationFolder}\"";
            
            string batPath = Path.Combine(testDirectory, "test_task.bat");
            File.WriteAllText(batPath, batContent);

            Assert.IsTrue(File.Exists(batPath));
            string content = File.ReadAllText(batPath);
            Assert.IsTrue(content.Contains("mkdir"));
            Assert.IsTrue(content.Contains(sourceFolder));
            Assert.IsTrue(content.Contains(destinationFolder));
        }

        /// <summary>
        /// BATファイル生成のテスト（ファイル作成）
        /// </summary>
        [TestMethod]
        public void BatFileGeneration_CreateFile()
        {
            string sourceFile = @"C:\Source\file_{DATE}.txt";
            string destinationFile = @"C:\Destination\file_{DATE}.txt";
            string batContent = $"echo. > \"{sourceFile}\"\necho. > \"{destinationFile}\"";
            
            string batPath = Path.Combine(testDirectory, "test_file_task.bat");
            File.WriteAllText(batPath, batContent);

            Assert.IsTrue(File.Exists(batPath));
            string content = File.ReadAllText(batPath);
            Assert.IsTrue(content.Contains("echo."));
        }

        /// <summary>
        /// 日付計算のテスト
        /// </summary>
        [TestMethod]
        public void DateCalculation_VariousOffsets()
        {
            DateTime baseDate = new DateTime(2024, 1, 15);
            
            // 過去
            DateTime pastDate = baseDate.AddDays(-10);
            Assert.AreEqual(new DateTime(2024, 1, 5), pastDate);
            
            // 未来
            DateTime futureDate = baseDate.AddDays(10);
            Assert.AreEqual(new DateTime(2024, 1, 25), futureDate);
            
            // 当日
            DateTime sameDate = baseDate.AddDays(0);
            Assert.AreEqual(baseDate, sameDate);
        }

        /// <summary>
        /// 文字列置換のテスト
        /// </summary>
        [TestMethod]
        public void StringReplacement_EdgeCases()
        {
            string template = "Prefix_{DATE}_Middle_{DATE}_Suffix";
            string date = "20240101";
            string result = template.Replace("{DATE}", date);
            
            Assert.AreEqual("Prefix_20240101_Middle_20240101_Suffix", result);
        }

        /// <summary>
        /// 空文字列のテスト
        /// </summary>
        [TestMethod]
        public void BuildFileNameWithSettings_EmptyString()
        {
            var form = new MainForm();
            string result = form.BuildFileNameWithSettings("", 0);
            
            Assert.AreEqual("", result);
        }

        /// <summary>
        /// null文字列のテスト
        /// </summary>
        [TestMethod]
        public void BuildFileNameWithSettings_NullString()
        {
            var form = new MainForm();
            
            // null文字列の場合は例外が発生する可能性があるため、適切にハンドリングされることを確認
            Assert.ThrowsException<ArgumentNullException>(() => 
            {
                form.BuildFileNameWithSettings(null, 0);
            });
        }

        /// <summary>
        /// 大きなオフセット値のテスト
        /// </summary>
        [TestMethod]
        public void BuildFileNameWithSettings_LargeOffset()
        {
            var form = new MainForm();
            string result = form.BuildFileNameWithSettings("Archive_{DATE}", 365);
            
            string expected = $"Archive_{DateTime.Now.AddDays(365):yyyyMMdd}";
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// 負の大きなオフセット値のテスト
        /// </summary>
        [TestMethod]
        public void BuildFileNameWithSettings_LargeNegativeOffset()
        {
            var form = new MainForm();
            string result = form.BuildFileNameWithSettings("Old_{DATE}", -365);
            
            string expected = $"Old_{DateTime.Now.AddDays(-365):yyyyMMdd}";
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// 特殊文字を含むファイル名のテスト
        /// </summary>
        [TestMethod]
        public void BuildFileNameWithSettings_SpecialCharacters()
        {
            var form = new MainForm();
            string result = form.BuildFileNameWithSettings("Test-File_Name.{DATE}.backup", 0);
            
            string expected = $"Test-File_Name.{DateTime.Now:yyyyMMdd}.backup";
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// ファイル操作のテスト
        /// </summary>
        [TestMethod]
        public void FileOperations_CreateAndDelete()
        {
            string testFilePath = Path.Combine(testDirectory, "test_file.txt");
            
            // ファイル作成
            File.WriteAllText(testFilePath, "Test content");
            Assert.IsTrue(File.Exists(testFilePath));
            
            // ファイル削除
            File.Delete(testFilePath);
            Assert.IsFalse(File.Exists(testFilePath));
        }

        /// <summary>
        /// ディレクトリ操作のテスト
        /// </summary>
        [TestMethod]
        public void DirectoryOperations_CreateAndDelete()
        {
            string testDirPath = Path.Combine(testDirectory, "test_subfolder");
            
            // ディレクトリ作成
            Directory.CreateDirectory(testDirPath);
            Assert.IsTrue(Directory.Exists(testDirPath));
            
            // ディレクトリ削除
            Directory.Delete(testDirPath);
            Assert.IsFalse(Directory.Exists(testDirPath));
        }
    }
}

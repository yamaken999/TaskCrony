using System;
using System.IO;

namespace TaskCrony.Tests
{
    /// <summary>
    /// TaskCronyの基本機能を確認するテストプログラム
    /// </summary>
    class BasicTestRunner
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== TaskCrony 基本機能テスト ===");
            Console.WriteLine();

            int passedTests = 0;
            int totalTests = 0;

            // 1. ファイル名生成テスト（基本）
            Console.WriteLine("1. ファイル名生成テスト（基本）");
            totalTests++;
            try
            {
                var form = new MainForm();
                string result = form.BuildFileNameWithSettings(
                    "TestFile", 
                    DateTime.Now.ToString("yyyyMMdd"), 
                    "", 
                    "",
                    false, false, true, // prefix: なし
                    false, false, true); // suffix: なし
                
                if (result == "TestFile")
                {
                    Console.WriteLine("   ✓ PASS: 基本的なファイル名生成");
                    passedTests++;
                }
                else
                {
                    Console.WriteLine($"   ✗ FAIL: 期待値: TestFile, 実際: {result}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"   ✗ ERROR: {ex.Message}");
            }

            // 2. 接頭語付きファイル名テスト
            Console.WriteLine("2. 接頭語付きファイル名テスト");
            totalTests++;
            try
            {
                var form = new MainForm();
                string dateString = DateTime.Now.ToString("yyyyMMdd");
                string result = form.BuildFileNameWithSettings(
                    "TestFile", 
                    dateString, 
                    "Prefix_", 
                    "",
                    true, false, false, // prefix: 日付の前
                    false, false, true); // suffix: なし
                
                string expected = $"Prefix_{dateString}_TestFile";
                if (result == expected)
                {
                    Console.WriteLine("   ✓ PASS: 接頭語付きファイル名生成");
                    passedTests++;
                }
                else
                {
                    Console.WriteLine($"   ✗ FAIL: 期待値: {expected}, 実際: {result}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"   ✗ ERROR: {ex.Message}");
            }

            // 3. 接尾語付きファイル名テスト
            Console.WriteLine("3. 接尾語付きファイル名テスト");
            totalTests++;
            try
            {
                var form = new MainForm();
                string dateString = DateTime.Now.ToString("yyyyMMdd");
                string result = form.BuildFileNameWithSettings(
                    "TestFile", 
                    dateString, 
                    "", 
                    "_Suffix",
                    false, false, true, // prefix: なし
                    true, false, false); // suffix: 日付の前
                
                string expected = $"TestFile_{dateString}_Suffix";
                if (result == expected)
                {
                    Console.WriteLine("   ✓ PASS: 接尾語付きファイル名生成");
                    passedTests++;
                }
                else
                {
                    Console.WriteLine($"   ✗ FAIL: 期待値: {expected}, 実際: {result}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"   ✗ ERROR: {ex.Message}");
            }

            // 4. 日付計算テスト
            Console.WriteLine("4. 日付計算テスト");
            totalTests++;
            try
            {
                DateTime baseDate = DateTime.Now;
                DateTime futureDate = baseDate.AddDays(7);
                DateTime pastDate = baseDate.AddDays(-7);
                
                bool testPassed = futureDate > baseDate && pastDate < baseDate;
                
                if (testPassed)
                {
                    Console.WriteLine("   ✓ PASS: 日付計算");
                    passedTests++;
                }
                else
                {
                    Console.WriteLine("   ✗ FAIL: 日付計算");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"   ✗ ERROR: {ex.Message}");
            }

            // 5. ファイル操作テスト
            Console.WriteLine("5. ファイル操作テスト");
            totalTests++;
            try
            {
                string testDir = Path.Combine(Path.GetTempPath(), "TaskCronyTest_" + Guid.NewGuid().ToString());
                Directory.CreateDirectory(testDir);
                
                string testFile = Path.Combine(testDir, "test.txt");
                File.WriteAllText(testFile, "Test content");
                
                bool fileExists = File.Exists(testFile);
                string content = File.ReadAllText(testFile);
                
                File.Delete(testFile);
                Directory.Delete(testDir);
                
                if (fileExists && content == "Test content")
                {
                    Console.WriteLine("   ✓ PASS: ファイル操作");
                    passedTests++;
                }
                else
                {
                    Console.WriteLine("   ✗ FAIL: ファイル操作");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"   ✗ ERROR: {ex.Message}");
            }

            // 6. 接頭語と接尾語両方のテスト
            Console.WriteLine("6. 接頭語と接尾語両方のテスト");
            totalTests++;
            try
            {
                var form = new MainForm();
                string dateString = DateTime.Now.ToString("yyyyMMdd");
                string result = form.BuildFileNameWithSettings(
                    "TestFile", 
                    dateString, 
                    "Pre_", 
                    "_Suf",
                    false, true, false, // prefix: 日付の後
                    false, true, false); // suffix: 日付の後
                
                string expected = $"Pre_TestFile_{dateString}_Suf";
                if (result == expected)
                {
                    Console.WriteLine("   ✓ PASS: 接頭語と接尾語両方");
                    passedTests++;
                }
                else
                {
                    Console.WriteLine($"   ✗ FAIL: 期待値: {expected}, 実際: {result}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"   ✗ ERROR: {ex.Message}");
            }

            // 7. 過去の日付テスト
            Console.WriteLine("7. 過去の日付テスト");
            totalTests++;
            try
            {
                var form = new MainForm();
                DateTime pastDate = DateTime.Now.AddDays(-30);
                string dateString = pastDate.ToString("yyyyMMdd");
                string result = form.BuildFileNameWithSettings(
                    "OldFile", 
                    dateString, 
                    "Archive_", 
                    "",
                    true, false, false, // prefix: 日付の前
                    false, false, true); // suffix: なし
                
                string expected = $"Archive_{dateString}_OldFile";
                if (result == expected)
                {
                    Console.WriteLine("   ✓ PASS: 過去の日付処理");
                    passedTests++;
                }
                else
                {
                    Console.WriteLine($"   ✗ FAIL: 期待値: {expected}, 実際: {result}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"   ✗ ERROR: {ex.Message}");
            }

            // 結果表示
            Console.WriteLine();
            Console.WriteLine("=== テスト結果 ===");
            Console.WriteLine($"合格: {passedTests}/{totalTests}");
            Console.WriteLine($"成功率: {(double)passedTests / totalTests * 100:F1}%");
            
            if (passedTests == totalTests)
            {
                Console.WriteLine("✓ すべてのテストが合格しました！");
            }
            else
            {
                Console.WriteLine("✗ 一部のテストが失敗しました。");
            }

            Console.WriteLine();
            Console.WriteLine("何かキーを押してください...");
            Console.ReadKey();
        }
    }
}

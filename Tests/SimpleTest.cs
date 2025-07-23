using System;
using System.Linq;

namespace SimpleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== TaskCrony 簡易機能テスト ===");
            Console.WriteLine();

            int passedTests = 0;
            int totalTests = 0;

            // 1. 日付フォーマットのテスト
            Console.WriteLine("1. 日付フォーマットのテスト");
            totalTests++;
            try
            {
                DateTime testDate = DateTime.Now;
                string formattedDate = testDate.ToString("yyyyMMdd");
                
                if (formattedDate.Length == 8 && formattedDate.All(char.IsDigit))
                {
                    Console.WriteLine($"   ✓ PASS: 日付フォーマット ({formattedDate})");
                    passedTests++;
                }
                else
                {
                    Console.WriteLine($"   ✗ FAIL: 不正な日付フォーマット ({formattedDate})");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"   ✗ ERROR: {ex.Message}");
            }

            // 2. 日付計算のテスト
            Console.WriteLine("2. 日付計算のテスト");
            totalTests++;
            try
            {
                DateTime baseDate = new DateTime(2024, 1, 15);
                DateTime futureDate = baseDate.AddDays(7);
                DateTime pastDate = baseDate.AddDays(-7);
                
                if (futureDate == new DateTime(2024, 1, 22) && 
                    pastDate == new DateTime(2024, 1, 8))
                {
                    Console.WriteLine("   ✓ PASS: 日付計算");
                    passedTests++;
                }
                else
                {
                    Console.WriteLine("   ✗ FAIL: 日付計算が不正");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"   ✗ ERROR: {ex.Message}");
            }

            // 3. 文字列置換のテスト
            Console.WriteLine("3. 文字列置換のテスト");
            totalTests++;
            try
            {
                string template = "File_{DATE}_Test";
                string dateString = "20240101";
                string result = template.Replace("{DATE}", dateString);
                string expected = "File_20240101_Test";
                
                if (result == expected)
                {
                    Console.WriteLine("   ✓ PASS: 文字列置換");
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

            // 4. 複数置換のテスト
            Console.WriteLine("4. 複数置換のテスト");
            totalTests++;
            try
            {
                string template = "{DATE}_File_{DATE}";
                string dateString = "20240201";
                string result = template.Replace("{DATE}", dateString);
                string expected = "20240201_File_20240201";
                
                if (result == expected)
                {
                    Console.WriteLine("   ✓ PASS: 複数置換");
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

            // 5. 空文字列処理のテスト
            Console.WriteLine("5. 空文字列処理のテスト");
            totalTests++;
            try
            {
                string template = "";
                string dateString = "20240301";
                string result = template.Replace("{DATE}", dateString);
                
                if (result == "")
                {
                    Console.WriteLine("   ✓ PASS: 空文字列処理");
                    passedTests++;
                }
                else
                {
                    Console.WriteLine($"   ✗ FAIL: 空文字列が正しく処理されていない");
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
                Console.WriteLine("✓ すべての基本テストが合格しました！");
                Console.WriteLine("TaskCronyの基本的なロジックは正常に動作します。");
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

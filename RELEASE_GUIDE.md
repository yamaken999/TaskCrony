# GitHub Release 作成手順

## TaskCrony v0.1.0 リリース作成

### 1. 配布パッケージの準備
```powershell
# リリースビルド作成
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true

# 配布フォルダ作成
mkdir dist
Copy-Item -Path "bin\Release\net8.0-windows\win-x64\publish\*" -Destination "dist\" -Recurse

# ドキュメント追加
# - インストールガイド.txt
# - README.md

# ZIPパッケージ作成
Compress-Archive -Path "dist\*" -DestinationPath "TaskCrony_v0.1.0_Distribution.zip" -Force
```

### 2. GitHub Release作成手順

1. **GitHubリポジトリにアクセス**
   - https://github.com/yamaken999/TaskCrony

2. **Releaseページへ移動**
   - 「Releases」タブをクリック
   - 「Create a new release」ボタンをクリック

3. **リリース情報入力**
   ```
   Tag version: v0.1.0
   Release title: TaskCrony v0.1.0 - Initial Release
   Target: master branch
   ```

4. **リリースノート記載**
   ```markdown
   # TaskCrony v0.1.0 - Initial Release

   Windows Task Scheduler と連携したファイル・フォルダ操作自動化ツールの初回リリースです。

   ## ✨ 主な機能
   - 日付オフセットシステム（実行日±365日）
   - ファイル/フォルダの自動作成・コピー
   - Windows Task Scheduler完全統合
   - JSON設定永続化
   - プレビュー機能

   ## 📥 インストール方法
   1. `TaskCrony_v0.1.0_Distribution.zip` をダウンロード
   2. ZIPファイルを任意のフォルダに解凍
   3. `TaskCrony.exe` を右クリック → 「管理者として実行」

   ## 🖥️ システム要件
   - Windows 10/11 (64-bit)
   - 管理者権限
   - PowerShell実行ポリシー設定

   ## 📋 含まれるファイル
   - TaskCrony.exe (自己完結型実行ファイル)
   - インストールガイド.txt (日本語)
   - README.md (英語)
   - 依存DLLファイル群

   ## ⚠️ 注意事項
   - ウイルス対策ソフトで誤検知される場合があります
   - TaskCrony.exeは移動しないでください
   - タスクの編集・削除は必ずTaskCronyから行ってください
   ```

5. **ファイルアップロード**
   - `TaskCrony_v0.1.0_Distribution.zip` をドラッグ&ドロップ

6. **リリース公開**
   - 「Publish release」ボタンをクリック

### 3. 配布案内

リリース作成後、以下の方法で配布を案内できます：

**ダウンロードリンク:**
```
https://github.com/yamaken999/TaskCrony/releases/tag/v0.1.0
```

**README.mdの案内:**
- [Releases ページ](https://github.com/yamaken999/TaskCrony/releases)からダウンロード

### 4. 今後のバージョン管理

**次回更新時:**
1. バージョン番号を更新（v0.1.1, v0.2.0など）
2. 同様の手順でリリース作成
3. 変更点をリリースノートに記載

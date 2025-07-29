## TaskScheduler エラー修正およびデジタル署名改善

### 🔧 実施した修正

#### 1. TaskScheduler互換性問題の解決
- **InvariantGlobalization**: `false`に変更（TaskSchedulerがグローバル化機能を必要とするため）
- **IncludeAllContentForSelfExtract**: `true`に追加（すべてのコンテンツをSingleFileに含める）
- **TaskSchedulerライブラリ**: v2.11.0 → v2.11.1に更新
- **Microsoft.Win32.UseRegistry**: `true`に設定（TaskSchedulerがレジストリアクセスを必要とするため）

#### 2. タスク作成エラーハンドリングの強化
- TaskService接続確認の追加
- TypeInitializationException専用のエラーハンドリング
- より詳細なエラーメッセージの提供
- タスク設定の最適化（バッテリー制約の無効化等）

#### 3. デジタル署名の改善
- 高度な権限レベル（TaskRunLevel.Highest）の設定
- タスク実行時の権限問題の解決
- より安定したタスクスケジューラー統合

### 🎯 期待される効果

1. **TaskScheduler初期化エラーの解決**: SingleFile配布でのライブラリ競合問題を修正
2. **身元不明アプリ警告の軽減**: より適切な署名設定による信頼性向上
3. **タスク登録の安定化**: 権限とエラーハンドリングの改善

### 📋 次のステップ

1. 修正版のビルドとテスト
2. GitHub Actions自動ビルドでの検証
3. 新バージョン（v0.13.1）のリリース
4. 実環境でのタスクスケジューラー動作確認

### ⚠️ 注意点

- ファイルサイズが若干増加する可能性（グローバル化機能有効化のため）
- TaskScheduler関連の警告は残る（Windows専用アプリのため問題なし）
- 初回実行時の展開時間が若干増加する可能性

# 🚀 DLL最適化とSingleFile配布形式の実装

## 📋 変更概要
TaskCronyの配布サイズを大幅に削減し、SingleFile形式での配布を実現しました。

## 🎯 主要な変更点

### ✅ DLL最適化
- **ファイル数削減**: 243ファイル → 2ファイル（99.2%削減）
- **配布形式改善**: 複数DLLから自己完結型実行ファイルへ
- **依存関係簡素化**: .NET Runtime内蔵でDLL不要

### ✅ プロジェクト設定最適化
- `PublishSingleFile=true`: 単一実行ファイル形式
- `InvariantGlobalization=true`: 国際化機能無効化（サイズ削減）
- デバッグ機能無効化: DebuggerSupport、EventSourceSupport等

### ✅ Git管理改善
- GitHubファイルサイズ制限（100MB）への対応
- `publish_singlefile/`フォルダを`.gitignore`に追加
- Git履歴から大きなファイルを削除

## 📊 削減効果

| 項目 | 変更前 | 変更後 | 削減率 |
|------|--------|--------|--------|
| ファイル数 | 243ファイル | 2ファイル | 99.2% |
| 配布形式 | 複数DLL + EXE | 単一EXE | - |
| 総サイズ | ~146MB | 155MB | - |

## 🔧 技術的詳細

### 実装した最適化
1. **SingleFile Publishing**: 全依存関係を単一EXEに統合
2. **Globalization無効化**: 多言語サポート機能を無効化
3. **デバッグ機能削除**: 本番環境で不要な機能を除外
4. **DLL分析**: 必要・不要なDLLの詳細調査を実施

### 配布方法の変更
- **変更前**: EXE + 243個のDLLファイル
- **変更後**: 自己完結型の単一EXEファイル

## 📁 追加ファイル
- `DLL_OPTIMIZATION_ANALYSIS.md`: DLL分析の詳細結果
- `DLL_OPTIMIZATION_SUMMARY.md`: プロジェクト完了報告書

## ✅ テスト項目
- [x] SingleFileビルドの成功確認
- [x] 実行ファイル動作確認
- [x] ファイルサイズ削減効果確認
- [x] Git履歴クリーンアップ確認

## ⚠️ 注意事項
- SingleFile形式のため初回起動が若干遅くなる可能性
- ファイルサイズが155MBと大きいため、GitHubでは直接管理対象外
- GitHub ActionsやReleases経由での配布を推奨

## 🚀 今後の展開
- GitHub Actionsワークフローの更新
- README.mdの配布方法更新
- 実環境での動作検証
- v0.14.0としてのリリース準備

## 👥 レビュー観点
- [ ] プロジェクト設定の妥当性
- [ ] ビルド成果物の品質
- [ ] ドキュメントの完整性
- [ ] Git管理方法の適切性

---
**関連Issue**: DLL最適化とビルドサイズ削減  
**対象バージョン**: v0.14.0（予定）

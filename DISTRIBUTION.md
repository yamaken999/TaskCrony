# TaskCrony v0.1.0 配布パッケージ作成手順

## 概要
GitHubのファイルサイズ制限（100MB）により、自己完結型実行ファイル（146MB）は直接リポジトリに含められません。

## 配布用ファイル作成手順

### 1. リリースビルド作成
```powershell
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
```

### 2. 配布フォルダ作成
```powershell
mkdir dist
Copy-Item -Path "bin\Release\net8.0-windows\win-x64\publish\*" -Destination "dist\" -Recurse
```

### 3. ドキュメント追加
- `インストールガイド.txt` (日本語版)
- `README.md` (英語版)

### 4. ZIPパッケージ作成
```powershell
Compress-Archive -Path "dist\*" -DestinationPath "TaskCrony_v0.1.0_Distribution.zip" -Force
```

## 配布方法

### GitHub Release（推奨）
1. GitHubのリポジトリページで「Releases」→「Create a new release」
2. Tag version: `v0.1.0`
3. Release title: `TaskCrony v0.1.0 - Initial Release`
4. ZIPファイルをアップロード
5. リリースノートを記載

### ファイル構成
```
TaskCrony_v0.1.0_Distribution.zip
├── TaskCrony.exe (146MB - 自己完結型)
├── TaskCrony.pdb (デバッグ情報)
├── README.md (英語版ガイド)
├── インストールガイド.txt (日本語版ガイド)
└── 依存DLLファイル群
    ├── D3DCompiler_47_cor3.dll
    ├── PenImc_cor3.dll
    ├── PresentationNative_cor3.dll
    ├── vcruntime140_cor3.dll
    └── wpfgfx_cor3.dll
```

## 注意事項
- 実行ファイルは .gitignore で除外済み
- ソースコードのみをバージョン管理
- 配布は GitHub Releases を使用

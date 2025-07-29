# TaskCrony DLL最適化分析レポート v0.13.0

## 🎯 最適化対象の特定

### 現在の状況
- 総DLLファイル数: 243個
- 総サイズ: 約146MB
- メインアプリケーション: TaskCrony.dll (0.1MB) + TaskCrony.exe (0.14MB)

### 🔥 削除候補DLL（重要度順）

#### 1. WPF関連DLL（約35MB削除可能）
**理由**: TaskCronyはWindows Formsアプリケーションであり、WPFは使用していない
```
PresentationFramework.dll - 15.38 MB ❌
PresentationCore.dll - 8.15 MB ❌
System.Xaml.dll - 1.36 MB ❌
System.Windows.Controls.Ribbon.dll - 1.39 MB ❌
WindowsBase.dll - 2.15 MB ❌
System.Windows.Presentation.dll - 0.03 MB ❌
WindowsFormsIntegration.dll - 0.2 MB ❌
PresentationUI.dll - 1.23 MB ❌
ReachFramework.dll - 1.53 MB ❌
PresentationFramework-* (すべて) - 約1MB ❌
PresentationNative_cor3.dll - 1.18 MB ❌
wpfgfx_cor3.dll - 1.86 MB ❌
```

#### 2. 不要なUIライブラリ（約5MB削除可能）
```
System.Windows.Forms.Design.dll - 5.31 MB ❌ (デザイナー用)
System.Windows.Forms.Design.Editors.dll - 0.02 MB ❌
UIAutomationClient.dll - 0.4 MB ❌ (アクセシビリティ自動化)
UIAutomationClientSideProviders.dll - 0.83 MB ❌
UIAutomationProvider.dll - 0.06 MB ❌
UIAutomationTypes.dll - 0.3 MB ❌
Accessibility.dll - 0.02 MB ❌
```

#### 3. Visual Basic関連（約1.5MB削除可能）
**理由**: TaskCronyはC#のみ使用
```
Microsoft.VisualBasic.Core.dll - 1.19 MB ❌
Microsoft.VisualBasic.dll - 0.02 MB ❌
Microsoft.VisualBasic.Forms.dll - 0.24 MB ❌
```

#### 4. 不要なネットワーク/Web関連（約5MB削除可能）
**理由**: TaskCronyは基本的なHTTPリクエスト（バージョンチェック）のみ使用
```
System.Net.HttpListener.dll - 0.53 MB ❌ (HTTPサーバー機能)
System.Net.Mail.dll - 0.41 MB ❌ (メール送信)
System.Net.Quic.dll - 0.27 MB ❌ (QUIC プロトコル)
System.Net.WebSockets.Client.dll - 0.1 MB ❌
System.Net.WebSockets.dll - 0.18 MB ❌
System.ServiceModel.Web.dll - 0.02 MB ❌ (WCF)
msquic.dll - 0.5 MB ❌
```

#### 5. デバッグ/開発関連（約7MB削除可能）
**理由**: リリースビルドでは不要
```
Microsoft.DiaSymReader.Native.amd64.dll - 2.09 MB ❌
mscordaccore.dll - 1.28 MB ❌
mscordaccore_amd64_amd64_8.0.1725.26602.dll - 1.28 MB ❌
mscordbi.dll - 1.18 MB ❌
createdump.exe - 0.07 MB ❌
System.Diagnostics.* (一部) - 約1MB ❌
```

#### 6. DirectX/グラフィックス関連（約5MB削除可能）
**理由**: TaskCronyは基本的なWindows Formsコントロールのみ使用
```
D3DCompiler_47_cor3.dll - 4.69 MB ❌
DirectWriteForwarder.dll - 0.48 MB ❌
PenImc_cor3.dll - 0.15 MB ❌ (タブレット入力)
```

#### 7. 高度な暗号化/証明書関連（約1.5MB削除可能）
**理由**: TaskCronyは基本的なHTTPS通信のみ
```
System.Security.Cryptography.Pkcs.dll - 0.72 MB ❌
System.Security.Cryptography.Xml.dll - 0.44 MB ❌
System.Security.Cryptography.OpenSsl.dll - 0.01 MB ❌
System.Security.Cryptography.ProtectedData.dll - 0.05 MB ❌
System.Security.Permissions.dll - 0.18 MB ❌
```

#### 8. 印刷関連（約1MB削除可能）
**理由**: TaskCronyは印刷機能なし
```
System.Printing.dll - 0.93 MB ❌
```

#### 9. その他の不要なライブラリ（約3MB削除可能）
```
System.DirectoryServices.dll - 1 MB ❌ (Active Directory)
System.ServiceProcess.dll - 0.02 MB ❌ (Windowsサービス管理)
System.CodeDom.dll - 0.47 MB ❌ (動的コード生成)
System.ComponentModel.DataAnnotations.dll - 0.02 MB ❌
System.Configuration.dll - 0.02 MB ❌
System.Design.dll - 0.02 MB ❌
System.Web.dll - 0.01 MB ❌
System.Web.HttpUtility.dll - 0.06 MB ❌
```

### 📋 必要DLL（保持対象）

#### コアランタイム
- System.Private.CoreLib.dll - 12.56 MB ✅
- coreclr.dll - 4.78 MB ✅
- clrjit.dll - 1.7 MB ✅
- hostfxr.dll - 0.35 MB ✅
- hostpolicy.dll - 0.39 MB ✅

#### Windows Forms (必須)
- System.Windows.Forms.dll - 12.94 MB ✅
- System.Windows.Forms.Primitives.dll - 2.87 MB ✅
- System.Drawing.Common.dll - 1.46 MB ✅

#### タスクスケジューラ (必須)
- Microsoft.Win32.TaskScheduler.dll - 0.32 MB ✅

#### 基本ライブラリ (必須)
- System.Text.Json.dll - 1.43 MB ✅ (設定ファイル)
- System.Net.Http.dll - 1.65 MB ✅ (バージョンチェック)
- System.Private.Xml.dll - 7.63 MB ✅ (設定/ログ)

## 🎯 最適化実装計画

### Phase 1: プロジェクトファイル最適化
```xml
<PropertyGroup>
  <UseWPF>false</UseWPF>
  <UseWindowsForms>true</UseWindowsForms>
  <TrimMode>partial</TrimMode>
  <PublishTrimmed>true</PublishTrimmed>
  <InvariantGlobalization>true</InvariantGlobalization>
</PropertyGroup>

<ItemGroup>
  <!-- WPF関連を明示的に除外 -->
  <TrimmerRootAssembly Remove="PresentationCore" />
  <TrimmerRootAssembly Remove="PresentationFramework" />
  <TrimmerRootAssembly Remove="System.Xaml" />
  <TrimmerRootAssembly Remove="WindowsBase" />
</ItemGroup>
```

### Phase 2: 不要な機能の無効化
- グローバリゼーション無効化 (InvariantGlobalization)
- 部分トリミング有効化 (TrimMode=partial)
- 使用していないアセンブリの明示的除外

### 📊 予想削減効果
- **削除可能サイズ**: 約65-70MB
- **最終サイズ予想**: 75-80MB (現在の146MBから約50%削減)
- **起動時間**: 大幅改善予想
- **メモリ使用量**: 削減予想

### ⚠️ 注意事項
1. 段階的に実装してテストを実施
2. 必要な機能が削除されていないか確認
3. 多言語対応が必要な場合はInvariantGlobalizationを無効化
4. デバッグ時はTrimを無効化してテスト効率化

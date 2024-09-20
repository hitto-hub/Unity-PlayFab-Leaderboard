# BreadcrumbsUnity-PlayFab-Leaderboard

このプロジェクトは、UnityとPlayFabを使用したログインシステム、スコア送信機能、リーダーボード表示機能を実装したサンプルです。ユーザーはカスタムIDを使用してログインし、スコアをPlayFabに送信し、そのスコアを基にしたリーダーボードを表示することができます。

## 機能

- **PlayFabを使用したログイン**  
  ユーザーは、入力されたカスタムIDを使用してPlayFabにログインします。ログイン時にアカウントが存在しない場合、新規アカウントが自動的に作成されます。

- **スコア送信**  
  プレイヤーのスコアは、PlayFabに統計データとして送信されます。

- **リーダーボード表示**  
  送信されたスコアを基に、リーダーボードを表示します。ユーザーはボタンを押してリーダーボードを更新することができます。

## セットアップ

### 1. PlayFab、 Unityプロジェクのセットアップ

1. [クイックスタート: Unity の C# 用の PlayFab クライアント ライブラリ](https://learn.microsoft.com/ja-jp/gaming/playfab/sdks/unity3d/quickstart)

2. タイトルIDを取得し、UnityプロジェクトのPlayFab設定で `PlayFabSettings.staticSettings.TitleId` に設定します。

   ```csharp
   PlayFabSettings.staticSettings.TitleId = "あなたのタイトルID";
   ```

## 参考

- [PlayFab Documentation](https://docs.microsoft.com/en-us/gaming/playfab/)

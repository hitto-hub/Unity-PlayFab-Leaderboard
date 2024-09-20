using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSender : MonoBehaviour
{
    // TMP_InputFieldへの参照をエディタで設定
    [SerializeField] private TMP_InputField scoreInputField;

    // Buttonへの参照をエディタで設定
    [SerializeField] private Button submitButton;

    private void Start()
    {
        // ボタンにクリックイベントを追加
        submitButton.onClick.AddListener(OnSubmitButtonClicked);
    }

    private void OnSubmitButtonClicked()
    {
        // InputFieldからスコアを取得
        string scoreText = scoreInputField.text;

        // スコアを送信するメソッドを呼び出し
        SendScore(scoreText);
    }

    private void SendScore(string scoreText)
    {
        // クライアント側のバリデーション：空文字やnullのチェック、および数値かどうか
        if (string.IsNullOrEmpty(scoreText) || !int.TryParse(scoreText, out int score))
        {
            Debug.LogError("スコアは有効な整数でなければなりません。");
            return;
        }

        var statisticUpdate = new StatisticUpdate
        {
            StatisticName = "Count", // 統計情報名を指定
            Value = score                // スコアをPlayFabに送信
        };

        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                statisticUpdate
            }
        };

        // PlayFab APIを呼び出してスコアを更新
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnSuccess, OnError);
    }

    private void OnSuccess(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("スコアが正常に更新されました！");
        // 必要に応じて、UIを更新するなどの処理を追加
        // 例: UpdateScoreUI(scoreInputField.text);
    }

    private void OnError(PlayFabError error)
    {
        Debug.LogError($"スコアの更新に失敗しました: {error.GenerateErrorReport()}");

        // エラーに応じた処理を追加
        switch (error.Error)
        {
            case PlayFabErrorCode.InvalidParams:
                Debug.LogError("無効なスコアが指定されました。");
                break;
            case PlayFabErrorCode.ConnectionError:
                Debug.LogError("接続エラーが発生しました。ネットワークを確認してください。");
                break;
            default:
                Debug.LogError($"予期しないエラーが発生しました: {error.ErrorMessage}");
                break;
        }

        // 必要に応じて、ユーザーにエラーメッセージを表示
        // 例: ShowErrorMessageToUser("スコアの更新に失敗しました。後ほど再試行してください。");
    }
}

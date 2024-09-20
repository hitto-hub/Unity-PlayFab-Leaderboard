using UnityEngine;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using System.Collections.Generic;

public class LeaderboardDisplay : MonoBehaviour
{
    // ランキング表示用のTextMeshPro UI参照
    [SerializeField] private TMP_Text leaderboardText;

    // 更新ボタン参照
    [SerializeField] private Button updateLeaderboardButton;

    private void Start()
    {
        // 更新ボタンが押された時にリーダーボードを更新
        updateLeaderboardButton.onClick.AddListener(GetLeaderboard);
    }

    // リーダーボードを取得して表示するメソッド
    public void GetLeaderboard()
    {
        // ログインしているかを確認
        if (PlayFabClientAPI.IsClientLoggedIn())
        {
            var request = new GetLeaderboardRequest
            {
                StatisticName = "Count", // 統計情報の名前
                StartPosition = 0,       // ランキングの開始位置（0でトップから）
                MaxResultsCount = 10     // 取得するランキング数
            };

            // PlayFab APIを使用してリーダーボードを取得
            PlayFabClientAPI.GetLeaderboard(request, OnGetLeaderboardSuccess, OnGetLeaderboardFailure);
        }
        else
        {
            Debug.LogError("ユーザーがログインしていません。リーダーボードを表示するにはログインが必要です。");
        }
    }

    // リーダーボード取得成功時の処理
    private void OnGetLeaderboardSuccess(GetLeaderboardResult result)
    {
        Debug.Log("リーダーボード取得成功");
        leaderboardText.text = "";  // 初期化

        // リーダーボードの結果をテキストに表示
        foreach (var entry in result.Leaderboard)
        {
            leaderboardText.text += $"{entry.Position + 1}. {entry.DisplayName} - {entry.StatValue}\n";
        }
    }

    // リーダーボード取得失敗時の処理
    private void OnGetLeaderboardFailure(PlayFabError error)
    {
        Debug.LogError($"リーダーボード取得失敗: {error.GenerateErrorReport()}");
        leaderboardText.text = "リーダーボードの取得に失敗しました";
    }
}

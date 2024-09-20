using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;

public class PlayFabLoginHandler : MonoBehaviour
{
    // TMP_InputField への参照をエディタで設定
    [SerializeField] private TMP_InputField customIdInputField;

    // Button への参照をエディタで設定
    [SerializeField] private Button loginButton;

    private void Start()
    {
        // ボタンにクリックイベントを追加
        loginButton.onClick.AddListener(OnLoginButtonClicked);
    }

    private void OnLoginButtonClicked()
    {
        // InputFieldからユーザーが入力したCustomIdを取得
        string customId = customIdInputField.text;

        // 入力値のバリデーション
        if (string.IsNullOrEmpty(customId))
        {
            Debug.LogError("CustomIdは空にできません。");
            return;
        }

        // PlayerPrefsにCustomIdを保存して再利用できるようにする
        PlayerPrefs.SetString("CustomId", customId);

        // PlayFabのログインリクエストを作成
        var request = new LoginWithCustomIDRequest
        {
            CustomId = customId,
            CreateAccount = true  // アカウントがない場合は新規作成
        };

        // PlayFab APIを使ってログインを試みる
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }

    // ログイン成功時の処理
    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("ログイン成功！");
        // ログイン成功後に必要な処理を追加
        // 例: メインメニューに遷移
        // SceneManager.LoadScene("MainMenu");
    }

    // ログイン失敗時の処理
    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogError($"ログイン失敗: {error.GenerateErrorReport()}");
        // 必要に応じてエラーハンドリングを追加
    }
}

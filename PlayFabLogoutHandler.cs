using UnityEngine;
using UnityEngine.UI;
using PlayFab;

public class PlayFabLogoutHandler : MonoBehaviour
{
    // Buttonへの参照をエディタで設定
    [SerializeField] private Button logoutButton;

    private void Start()
    {
        // ボタンにクリックイベントを追加
        logoutButton.onClick.AddListener(OnLogoutButtonClicked);
    }

    private void OnLogoutButtonClicked()
    {
        // 1. CustomIdを削除
        if (PlayerPrefs.HasKey("CustomId"))
        {
            PlayerPrefs.DeleteKey("CustomId");
            Debug.Log("CustomIdが削除されました。次回ログイン時に新しいCustomIdが生成されます。");
        }
        else
        {
            Debug.LogWarning("CustomIdは既に存在しません。");
        }

        // 2. PlayFabのセッション情報やキャッシュをクリア
        PlayFabClientAPI.ForgetAllCredentials();
        Debug.Log("PlayFabのセッション情報とキャッシュがクリアされました。");

        // 必要に応じて、ログアウト後の処理を実行
        // 例: メインメニューに遷移するなど
        // SceneManager.LoadScene("MainMenu");

        // 例: ログイン画面に戻す
        // ShowLoginScreen();
    }
}

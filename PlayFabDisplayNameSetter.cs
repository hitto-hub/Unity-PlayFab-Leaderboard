using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayFabDisplayNameSetter : MonoBehaviour
{
    // TMP_InputFieldへの参照をエディタで設定
    [SerializeField] private TMP_InputField userNameInputField;

    // Buttonへの参照をエディタで設定
    [SerializeField] private Button submitButton;

    private void Start()
    {
        // ボタンにクリックイベントを追加
        submitButton.onClick.AddListener(OnSubmitButtonClicked);
    }

    private void OnSubmitButtonClicked()
    {
        // InputFieldからユーザー名を取得
        string userName = userNameInputField.text;

        // ユーザー名を設定するメソッドを呼び出し
        SetUserName(userName);
    }

    private void SetUserName(string userName)
    {
        // クライアント側のバリデーション：空文字やnullのチェック
        if (string.IsNullOrEmpty(userName))
        {
            Debug.LogError("ユーザー名は空にできません。");
            return;
        }

        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = userName
        };

        // PlayFab APIを呼び出してディスプレイネームを更新
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnSuccess, OnError);
    }

    private void OnSuccess(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("ディスプレイネームが正常に更新されました！");
        // 必要に応じて、UIを更新するなどの処理を追加
        // 例: UpdateUserNameUI(userNameInputField.text);
    }

    private void OnError(PlayFabError error)
    {
        Debug.LogError($"ディスプレイネームの更新に失敗しました: {error.GenerateErrorReport()}");

        // エラーに応じた処理を追加
        switch (error.Error)
        {
            case PlayFabErrorCode.InvalidParams:
                Debug.LogError("無効なディスプレイネームが指定されました。");
                break;
            case PlayFabErrorCode.ConnectionError:
                Debug.LogError("接続エラーが発生しました。ネットワークを確認してください。");
                break;
            default:
                Debug.LogError($"予期しないエラーが発生しました: {error.ErrorMessage}");
                break;
        }

        // 必要に応じて、ユーザーにエラーメッセージを表示
        // 例: ShowErrorMessageToUser("ディスプレイネームの更新に失敗しました。後ほど再試行してください。");
    }
}

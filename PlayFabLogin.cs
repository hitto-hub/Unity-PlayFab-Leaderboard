using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class PlayFabLogin : MonoBehaviour
{
    public void Start()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
        {
            /*
                ??? titleId ? PlayFab ??? ??????????? titleId ??????????
                ????????????????????????????????????
            */
            PlayFabSettings.staticSettings.TitleId = "";
        }
        // CustomIdを生成し、ローカルに保存する例
        string customId = PlayerPrefs.GetString("CustomId", System.Guid.NewGuid().ToString());
        PlayerPrefs.SetString("CustomId", customId);
        // ログイン処理
        var request = new LoginWithCustomIDRequest
        {
            CustomId = customId,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }
    // ログイン成功
    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Congratulations, you made your first successful API call!");
    }
    // ログイン失敗
    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogWarning("Something went wrong with your first API call.  :(");
        Debug.LogError("Here's some debug information:");
        Debug.LogError(error.GenerateErrorReport());
    }
}
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
        // CustomId�𐶐����A���[�J���ɕۑ������
        string customId = PlayerPrefs.GetString("CustomId", System.Guid.NewGuid().ToString());
        PlayerPrefs.SetString("CustomId", customId);
        // ���O�C������
        var request = new LoginWithCustomIDRequest
        {
            CustomId = customId,
            CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }
    // ���O�C������
    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Congratulations, you made your first successful API call!");
    }
    // ���O�C�����s
    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogWarning("Something went wrong with your first API call.  :(");
        Debug.LogError("Here's some debug information:");
        Debug.LogError(error.GenerateErrorReport());
    }
}
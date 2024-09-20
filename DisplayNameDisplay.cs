using UnityEngine;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
public class DisplayNameDisplay : MonoBehaviour
{
    // Reference to the TextMeshPro UI to display the DisplayName
    [SerializeField] private TMP_Text displayNameText;

    // Reference to the Button that triggers DisplayName display
    [SerializeField] private Button displayNameButton;

    private void Start()
    {
        // Add a listener to the button that will call ShowDisplayName when clicked
        displayNameButton.onClick.AddListener(GetDisplayName);
    }

    // Method to get and display the player's DisplayName
    private void GetDisplayName()
    {
        // Ensure the user is logged in to PlayFab
        if (PlayFabClientAPI.IsClientLoggedIn())
        {
            // Make a request to get the player's account info, which includes DisplayName
            PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest(), OnGetAccountInfoSuccess, OnError);
        }
        else
        {
            Debug.LogError("User is not logged in. Cannot display DisplayName.");
            displayNameText.text = "User is not logged in.";
        }
    }

    // Callback for when the GetAccountInfo request is successful
    private void OnGetAccountInfoSuccess(GetAccountInfoResult result)
    {
        string displayName = result.AccountInfo.TitleInfo.DisplayName;
        if (string.IsNullOrEmpty(displayName))
        {
            displayName = "No Display Name Set";
        }

        // Update the TextMeshPro UI with the DisplayName
        displayNameText.text = "Display Name: " + displayName;
    }

    // Callback for when the GetAccountInfo request fails
    private void OnError(PlayFabError error)
    {
        Debug.LogError("Failed to retrieve DisplayName: " + error.GenerateErrorReport());
        displayNameText.text = "Error retrieving DisplayName.";
    }
}

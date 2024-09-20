using UnityEngine;
using UnityEngine.UI;
using PlayFab;

public class PlayFabLogoutHandler : MonoBehaviour
{
    // Button�ւ̎Q�Ƃ��G�f�B�^�Őݒ�
    [SerializeField] private Button logoutButton;

    private void Start()
    {
        // �{�^���ɃN���b�N�C�x���g��ǉ�
        logoutButton.onClick.AddListener(OnLogoutButtonClicked);
    }

    private void OnLogoutButtonClicked()
    {
        // 1. CustomId���폜
        if (PlayerPrefs.HasKey("CustomId"))
        {
            PlayerPrefs.DeleteKey("CustomId");
            Debug.Log("CustomId���폜����܂����B���񃍃O�C�����ɐV����CustomId����������܂��B");
        }
        else
        {
            Debug.LogWarning("CustomId�͊��ɑ��݂��܂���B");
        }

        // 2. PlayFab�̃Z�b�V��������L���b�V�����N���A
        PlayFabClientAPI.ForgetAllCredentials();
        Debug.Log("PlayFab�̃Z�b�V�������ƃL���b�V�����N���A����܂����B");

        // �K�v�ɉ����āA���O�A�E�g��̏��������s
        // ��: ���C�����j���[�ɑJ�ڂ���Ȃ�
        // SceneManager.LoadScene("MainMenu");

        // ��: ���O�C����ʂɖ߂�
        // ShowLoginScreen();
    }
}

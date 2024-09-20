using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;

public class PlayFabLoginHandler : MonoBehaviour
{
    // TMP_InputField �ւ̎Q�Ƃ��G�f�B�^�Őݒ�
    [SerializeField] private TMP_InputField customIdInputField;

    // Button �ւ̎Q�Ƃ��G�f�B�^�Őݒ�
    [SerializeField] private Button loginButton;

    private void Start()
    {
        // �{�^���ɃN���b�N�C�x���g��ǉ�
        loginButton.onClick.AddListener(OnLoginButtonClicked);
    }

    private void OnLoginButtonClicked()
    {
        // InputField���烆�[�U�[�����͂���CustomId���擾
        string customId = customIdInputField.text;

        // ���͒l�̃o���f�[�V����
        if (string.IsNullOrEmpty(customId))
        {
            Debug.LogError("CustomId�͋�ɂł��܂���B");
            return;
        }

        // PlayerPrefs��CustomId��ۑ����čė��p�ł���悤�ɂ���
        PlayerPrefs.SetString("CustomId", customId);

        // PlayFab�̃��O�C�����N�G�X�g���쐬
        var request = new LoginWithCustomIDRequest
        {
            CustomId = customId,
            CreateAccount = true  // �A�J�E���g���Ȃ��ꍇ�͐V�K�쐬
        };

        // PlayFab API���g���ă��O�C�������݂�
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }

    // ���O�C���������̏���
    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("���O�C�������I");
        // ���O�C��������ɕK�v�ȏ�����ǉ�
        // ��: ���C�����j���[�ɑJ��
        // SceneManager.LoadScene("MainMenu");
    }

    // ���O�C�����s���̏���
    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogError($"���O�C�����s: {error.GenerateErrorReport()}");
        // �K�v�ɉ����ăG���[�n���h�����O��ǉ�
    }
}

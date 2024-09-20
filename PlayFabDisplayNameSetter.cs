using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayFabDisplayNameSetter : MonoBehaviour
{
    // TMP_InputField�ւ̎Q�Ƃ��G�f�B�^�Őݒ�
    [SerializeField] private TMP_InputField userNameInputField;

    // Button�ւ̎Q�Ƃ��G�f�B�^�Őݒ�
    [SerializeField] private Button submitButton;

    private void Start()
    {
        // �{�^���ɃN���b�N�C�x���g��ǉ�
        submitButton.onClick.AddListener(OnSubmitButtonClicked);
    }

    private void OnSubmitButtonClicked()
    {
        // InputField���烆�[�U�[�����擾
        string userName = userNameInputField.text;

        // ���[�U�[����ݒ肷�郁�\�b�h���Ăяo��
        SetUserName(userName);
    }

    private void SetUserName(string userName)
    {
        // �N���C�A���g���̃o���f�[�V�����F�󕶎���null�̃`�F�b�N
        if (string.IsNullOrEmpty(userName))
        {
            Debug.LogError("���[�U�[���͋�ɂł��܂���B");
            return;
        }

        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = userName
        };

        // PlayFab API���Ăяo���ăf�B�X�v���C�l�[�����X�V
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnSuccess, OnError);
    }

    private void OnSuccess(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("�f�B�X�v���C�l�[��������ɍX�V����܂����I");
        // �K�v�ɉ����āAUI���X�V����Ȃǂ̏�����ǉ�
        // ��: UpdateUserNameUI(userNameInputField.text);
    }

    private void OnError(PlayFabError error)
    {
        Debug.LogError($"�f�B�X�v���C�l�[���̍X�V�Ɏ��s���܂���: {error.GenerateErrorReport()}");

        // �G���[�ɉ�����������ǉ�
        switch (error.Error)
        {
            case PlayFabErrorCode.InvalidParams:
                Debug.LogError("�����ȃf�B�X�v���C�l�[�����w�肳��܂����B");
                break;
            case PlayFabErrorCode.ConnectionError:
                Debug.LogError("�ڑ��G���[���������܂����B�l�b�g���[�N���m�F���Ă��������B");
                break;
            default:
                Debug.LogError($"�\�����Ȃ��G���[���������܂���: {error.ErrorMessage}");
                break;
        }

        // �K�v�ɉ����āA���[�U�[�ɃG���[���b�Z�[�W��\��
        // ��: ShowErrorMessageToUser("�f�B�X�v���C�l�[���̍X�V�Ɏ��s���܂����B��قǍĎ��s���Ă��������B");
    }
}

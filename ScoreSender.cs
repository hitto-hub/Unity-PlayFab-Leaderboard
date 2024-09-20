using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSender : MonoBehaviour
{
    // TMP_InputField�ւ̎Q�Ƃ��G�f�B�^�Őݒ�
    [SerializeField] private TMP_InputField scoreInputField;

    // Button�ւ̎Q�Ƃ��G�f�B�^�Őݒ�
    [SerializeField] private Button submitButton;

    private void Start()
    {
        // �{�^���ɃN���b�N�C�x���g��ǉ�
        submitButton.onClick.AddListener(OnSubmitButtonClicked);
    }

    private void OnSubmitButtonClicked()
    {
        // InputField����X�R�A���擾
        string scoreText = scoreInputField.text;

        // �X�R�A�𑗐M���郁�\�b�h���Ăяo��
        SendScore(scoreText);
    }

    private void SendScore(string scoreText)
    {
        // �N���C�A���g���̃o���f�[�V�����F�󕶎���null�̃`�F�b�N�A����ѐ��l���ǂ���
        if (string.IsNullOrEmpty(scoreText) || !int.TryParse(scoreText, out int score))
        {
            Debug.LogError("�X�R�A�͗L���Ȑ����łȂ���΂Ȃ�܂���B");
            return;
        }

        var statisticUpdate = new StatisticUpdate
        {
            StatisticName = "Count", // ���v��񖼂��w��
            Value = score                // �X�R�A��PlayFab�ɑ��M
        };

        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                statisticUpdate
            }
        };

        // PlayFab API���Ăяo���ăX�R�A���X�V
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnSuccess, OnError);
    }

    private void OnSuccess(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("�X�R�A������ɍX�V����܂����I");
        // �K�v�ɉ����āAUI���X�V����Ȃǂ̏�����ǉ�
        // ��: UpdateScoreUI(scoreInputField.text);
    }

    private void OnError(PlayFabError error)
    {
        Debug.LogError($"�X�R�A�̍X�V�Ɏ��s���܂���: {error.GenerateErrorReport()}");

        // �G���[�ɉ�����������ǉ�
        switch (error.Error)
        {
            case PlayFabErrorCode.InvalidParams:
                Debug.LogError("�����ȃX�R�A���w�肳��܂����B");
                break;
            case PlayFabErrorCode.ConnectionError:
                Debug.LogError("�ڑ��G���[���������܂����B�l�b�g���[�N���m�F���Ă��������B");
                break;
            default:
                Debug.LogError($"�\�����Ȃ��G���[���������܂���: {error.ErrorMessage}");
                break;
        }

        // �K�v�ɉ����āA���[�U�[�ɃG���[���b�Z�[�W��\��
        // ��: ShowErrorMessageToUser("�X�R�A�̍X�V�Ɏ��s���܂����B��قǍĎ��s���Ă��������B");
    }
}

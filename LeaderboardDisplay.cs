using UnityEngine;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using System.Collections.Generic;

public class LeaderboardDisplay : MonoBehaviour
{
    // �����L���O�\���p��TextMeshPro UI�Q��
    [SerializeField] private TMP_Text leaderboardText;

    // �X�V�{�^���Q��
    [SerializeField] private Button updateLeaderboardButton;

    private void Start()
    {
        // �X�V�{�^���������ꂽ���Ƀ��[�_�[�{�[�h���X�V
        updateLeaderboardButton.onClick.AddListener(GetLeaderboard);
    }

    // ���[�_�[�{�[�h���擾���ĕ\�����郁�\�b�h
    public void GetLeaderboard()
    {
        // ���O�C�����Ă��邩���m�F
        if (PlayFabClientAPI.IsClientLoggedIn())
        {
            var request = new GetLeaderboardRequest
            {
                StatisticName = "Count", // ���v���̖��O
                StartPosition = 0,       // �����L���O�̊J�n�ʒu�i0�Ńg�b�v����j
                MaxResultsCount = 10     // �擾���郉���L���O��
            };

            // PlayFab API���g�p���ă��[�_�[�{�[�h���擾
            PlayFabClientAPI.GetLeaderboard(request, OnGetLeaderboardSuccess, OnGetLeaderboardFailure);
        }
        else
        {
            Debug.LogError("���[�U�[�����O�C�����Ă��܂���B���[�_�[�{�[�h��\������ɂ̓��O�C�����K�v�ł��B");
        }
    }

    // ���[�_�[�{�[�h�擾�������̏���
    private void OnGetLeaderboardSuccess(GetLeaderboardResult result)
    {
        Debug.Log("���[�_�[�{�[�h�擾����");
        leaderboardText.text = "";  // ������

        // ���[�_�[�{�[�h�̌��ʂ��e�L�X�g�ɕ\��
        foreach (var entry in result.Leaderboard)
        {
            leaderboardText.text += $"{entry.Position + 1}. {entry.DisplayName} - {entry.StatValue}\n";
        }
    }

    // ���[�_�[�{�[�h�擾���s���̏���
    private void OnGetLeaderboardFailure(PlayFabError error)
    {
        Debug.LogError($"���[�_�[�{�[�h�擾���s: {error.GenerateErrorReport()}");
        leaderboardText.text = "���[�_�[�{�[�h�̎擾�Ɏ��s���܂���";
    }
}

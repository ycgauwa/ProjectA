using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CI.QuickSave;
using CI.QuickSave.Core.Storage;

public class UserData : MonoBehaviour
{
    //�ۑ�����̂� Player�̃|�W�V���� �X�g�[���[�̉���t���O�BEnding�̉����B�A�C�e����Bool�
    //���[�U�[��
    Vector3 playerPosition;
    ItemDateBase itemDate;
    string characterName;

    //�x�X�g�X�R�A
    int stageNumber;

    //�Z�[�u�ݒ�
    QuickSaveSettings m_saveSettings;

    public void Start()
    {
        // QuickSaveSettings�̃C���X�^���X���쐬
        m_saveSettings = new QuickSaveSettings();
        // �Í����̕��@ 
        m_saveSettings.SecurityMode = SecurityMode.Aes;
        // Aes�̈Í����L�[
        m_saveSettings.Password = "Password";
        // ���k�̕��@
        m_saveSettings.CompressionMode = CompressionMode.Gzip;
    }

    /// <summary>
    /// �Z�[�u�f�[�^�ǂݍ���
    /// </summary>
    public void LoadUserData()
    {
        //�t�@�C����������Ζ���
        if(FileAccess.Exists("SaveData") == false)
        {
            return;
        }

        // QuickSaveReader�̃C���X�^���X���쐬
        QuickSaveReader reader = QuickSaveReader.Create("SaveData", m_saveSettings);

        // �f�[�^��ǂݍ���
        playerPosition�@= reader.Read<Vector3>("PlayerPosition");
        characterName = reader.Read<string>("CharacterName");
        stageNumber = reader.Read<int>("StageNumber");
    }

    /// <summary>
    /// �f�[�^�Z�[�u
    /// </summary>
    public void SaveUserData()
    {
        Debug.Log("�Z�[�u�f�[�^�ۑ���:" + Application.persistentDataPath);

        // QuickSaveWriter�̃C���X�^���X���쐬
        QuickSaveWriter writer = QuickSaveWriter.Create("SaveData", m_saveSettings);

        // �f�[�^����������
        writer.Write("PlayerPosition", playerPosition);
        writer.Write("CharacterName", characterName);
        writer.Write("StageNumber", stageNumber);

        // �ύX�𔽉f
        writer.Commit();
    }
}

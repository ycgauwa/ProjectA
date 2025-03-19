using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using CI.QuickSave;
using CI.QuickSave.Core.Storage;
using RPGM.Gameplay;
using UnityEngine.TextCore.Text;
//using System.IO;

public class UserData : MonoBehaviour
{
    //�ۑ�����̂� Player�̃|�W�V���� �X�g�[���[�̉���t���O�BEnding�̉����B�A�C�e����Bool�
    //���[�U�[��
    public Vector3 playerPosition;
    public Vector3 enemyPosition;
    public Vector3 enemy2Position;
    public Vector3 haruPosition;
    public Vector3 seiitirouPosition;
    public string characterName;
    public string playTime;
    public string gameMode;
    private string locationName;
    private string destinationName;
    public int totalTime;
    public int chapterNumber;
    public int getedEndTotalCount;
    private int callNumber;
    // �A�C�e�����X�g�������ĂȂ��c�c
    public List<bool> itemsCheckPossession;
    public List<bool> itemsCheckGeted;
    public List<bool> gameProgressFlags;
    public List<bool> seiitirouProgressFlags;
    public List<bool> haruProgressFlags;
    public List<bool> endingFlags;
    public SaveDate saveDate;
    //�Z�[�u�ݒ�
    QuickSaveSettings m_saveSettings;
    public void Awake()
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
    public void LoadUserData(int saveIndex)
    {
        Debug.Log("���[�h");
        // QuickSaveReader�̃C���X�^���X���쐬
        QuickSaveReader reader = QuickSaveReader.Create("SaveData" + saveIndex, m_saveSettings);
        // �f�[�^��ǂݍ���(�f�[�^�ɉ�������񂪓����Ă���B)
        playerPosition�@= reader.Read<Vector3>("PlayerPosition");
        enemyPosition = reader.Read<Vector3>("EnemyPosition");
        enemy2Position = reader.Read<Vector3>("Enemy2Position");
        haruPosition = reader.Read<Vector3>("HaruPosition");
        seiitirouPosition = reader.Read < Vector3 > ("SeiitirouPosition");
        playTime = reader.Read<string>("PlayTime");
        gameMode = reader.Read<string>("GameMode");
        locationName = reader.Read<string>("LocationName");
        destinationName = reader.Read<string>("DestinationName");
        chapterNumber = reader.Read<int>("ChapterNumber");
        characterName = reader.Read<string>("CharacterName");
        totalTime = reader.Read<int>("TotalTime");
        getedEndTotalCount = reader.Read<int>("GetedEndTotalCount");
        callNumber = reader.Read<int>("CallNumber");
        itemsCheckPossession = reader.Read<List<bool>>("ItemsCheckPossession");
        itemsCheckGeted = reader.Read<List<bool>>("ItemsCheckGeted");
        gameProgressFlags = reader.Read<List<bool>>("GameProgressFlags");
        seiitirouProgressFlags = reader.Read<List<bool>>("SeiitirouProgressFlags");
        haruProgressFlags = reader.Read<List<bool>>("HaruProgressFlags");
        endingFlags = reader.Read<List<bool>>("EndingFlags");


        saveDate.playerPosition = playerPosition;
        saveDate.enemyPosition = enemyPosition;
        saveDate.enemy2Position = enemy2Position;
        saveDate.haruPosition = haruPosition;
        saveDate.seiitirouPosition = seiitirouPosition;
        saveDate.playTime = playTime;
        saveDate.gameModeString = gameMode;
        saveDate.locationName = locationName;
        saveDate.destinationName = destinationName;
        saveDate.chapterNumber = chapterNumber;
        saveDate.characterName = characterName;
        saveDate.totalSeconds = totalTime;
        saveDate.getedEndTotalCount = getedEndTotalCount;
        saveDate.callNumber = callNumber;
        saveDate.itemsPossessionList = itemsCheckPossession;
        saveDate.itemsGetedList = itemsCheckGeted;
        saveDate.gameProgressFlagsList = gameProgressFlags;
        saveDate.seiitirouProgressFlagsList = seiitirouProgressFlags;
        saveDate.haruProgressFlagsList= haruProgressFlags;
        saveDate.endingFlagsList = endingFlags;

        switch(saveIndex)
        {
            case 0:
                SaveSlotsManager.save_Instance.saveState.loadIndex = 99;
                break;
            case 1:
                SaveSlotsManager.save_Instance.saveState.loadIndex = 1;
                break;
            case 2:
                SaveSlotsManager.save_Instance.saveState.loadIndex = 2;
                break;
            case 3:
                SaveSlotsManager.save_Instance.saveState.loadIndex = 3;
                break;
        }
        SceneManager.LoadScene("Game");
    }

    /// <summary>
    /// �f�[�^�Z�[�u
    /// </summary>
    public void SaveUserData(int saveIndex)
    {
        Debug.Log("�Z�[�u�f�[�^�ۑ���:" + Application.persistentDataPath);
        // QuickSaveWriter�̃C���X�^���X���쐬
        QuickSaveWriter writer = QuickSaveWriter.Create("SaveData" + saveIndex, m_saveSettings);
        saveDate.KeepDateMethod();
        playerPosition = saveDate.playerPosition;
        enemyPosition = saveDate.enemyPosition;
        enemy2Position = saveDate.enemy2Position;
        haruPosition = saveDate.haruPosition;
        seiitirouPosition = saveDate.seiitirouPosition;
        playTime = saveDate.playTime;
        gameMode = saveDate.gameModeString;
        locationName = saveDate.locationName;
        destinationName = saveDate.destinationName;
        chapterNumber = saveDate.chapterNumber;
        characterName = saveDate.characterName;
        totalTime = saveDate.totalSeconds;
        getedEndTotalCount = saveDate.getedEndTotalCount;
        callNumber = saveDate.callNumber;
        itemsCheckPossession = saveDate.itemsPossessionList;
        itemsCheckGeted = saveDate.itemsGetedList;
        gameProgressFlags = saveDate.gameProgressFlagsList;
        seiitirouProgressFlags = saveDate.seiitirouProgressFlagsList;
        haruProgressFlags = saveDate.haruProgressFlagsList;
        endingFlags = saveDate.endingFlagsList;

        // �f�[�^���������� Json�t�@�C��
        writer.Write("PlayerPosition", playerPosition);
        writer.Write("EnemyPosition", enemyPosition);
        writer.Write("Enemy2Position", enemy2Position);
        writer.Write("HaruPosition", haruPosition);
        writer.Write("SeiitirouPosition", seiitirouPosition);
        writer.Write("PlayTime",playTime);
        writer.Write("GameMode",gameMode);
        writer.Write("LocationName", locationName);
        writer.Write("DestinationName", destinationName);
        writer.Write("ChapterNumber", chapterNumber);
        writer.Write("CharacterName", characterName);
        writer.Write("TotalTime",totalTime);
        writer.Write("GetedEndTotalCount", getedEndTotalCount);
        writer.Write("CallNumber", callNumber);
        writer.Write("ItemsCheckPossession",itemsCheckPossession);
        writer.Write("ItemsCheckGeted", itemsCheckGeted);
        writer.Write("GameProgressFlags",gameProgressFlags);
        writer.Write("SeiitirouProgressFlags", seiitirouProgressFlags);
        writer.Write("HaruProgressFlags", haruProgressFlags);
        writer.Write("EndingFlags",endingFlags);

        // �ύX�𔽉f
        writer.Commit();
    }
    public void UpdateSaveData(ref string[] times,ref string[] modes,ref string[] chars,ref int[] chapterNum)
    {
        string[] playTimes = new string[3];
        string[] gameModes = new string[3];
        string[] characters = new string[3];
        int[] Chapters = new int[3];
        //�@�����SaveDate��UI���A�b�v�f�[�g���Ă��郁�\�b�h�ŁA�����Ă����ϐ��̒��Ƀf�[�^�����Ă�����

        for(int i = 0; i < 3; i++)
        {

            string filePath = Path.Combine(Application.persistentDataPath, "QuickSave", $"SaveData{i+1}.json");

            if(File.Exists(filePath))
            {
                Debug.Log($"���[�h�t�@�C��{i+1}�����݂��܂��B");
            }
            else break;

            QuickSaveReader loadReader = QuickSaveReader.Create($"SaveData{i+1}", m_saveSettings);
            playTime = loadReader.Read<string>("PlayTime");
            gameMode = loadReader.Read<string>("GameMode");
            chapterNumber = loadReader.Read<int>("ChapterNumber");
            characterName = loadReader.Read<string>("CharacterName");

            playTimes[i] = playTime;
            gameModes[i] = gameMode;
            chapterNum[i] = chapterNumber;
            characters[i] = characterName;
        }
        times = playTimes;
        modes = gameModes;
        chars = characters;
        Chapters = chapterNum;
        Debug.Log("GameStart�N���X�ɖ߂�܂�");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CI.QuickSave;
using CI.QuickSave.Core.Storage;

public class UserData : MonoBehaviour
{
    //保存するのは Playerのポジション ストーリーの回収フラグ。Endingの回収具合。アイテムのBool具合
    //ユーザー名
    Vector3 playerPosition;
    ItemDateBase itemDate;
    string characterName;
    string playTime;
    int chapterNumber;

    //セーブ設定
    QuickSaveSettings m_saveSettings;

    public void Start()
    {
        // QuickSaveSettingsのインスタンスを作成
        m_saveSettings = new QuickSaveSettings();
        // 暗号化の方法 
        m_saveSettings.SecurityMode = SecurityMode.Aes;
        // Aesの暗号化キー
        m_saveSettings.Password = "Password";
        // 圧縮の方法
        m_saveSettings.CompressionMode = CompressionMode.Gzip;
    }

    /// <summary>
    /// セーブデータ読み込み
    /// </summary>
    public void LoadUserData()
    {
        // QuickSaveReaderのインスタンスを作成
        QuickSaveReader reader = QuickSaveReader.Create("SaveData", m_saveSettings);
        Debug.Log("Load完了");
        // データを読み込む
        //playerPosition　= reader.Read<Vector3>("PlayerPosition");
        //characterName = reader.Read<string>("CharacterName");
        playTime = reader.Read<string>("PlayTime");
        chapterNumber = reader.Read<int>("ChapterNumber");
        FlagsManager.flag_Instance.chapterNum = 2;
        Debug.Log("playTime" + playTime + "chapterNumber" + chapterNumber);
    }

    /// <summary>
    /// データセーブ
    /// </summary>
    public void SaveUserData()
    {
        Debug.Log("セーブデータ保存先:" + Application.persistentDataPath);

        // QuickSaveWriterのインスタンスを作成
        QuickSaveWriter writer = QuickSaveWriter.Create("SaveData", m_saveSettings);

        playTime = FlagsManager.flag_Instance.playTime;
        chapterNumber = FlagsManager.flag_Instance.chapterNum;
        chapterNumber = FlagsManager.flag_Instance.chapterNum;

        // データを書き込む
        //writer.Write("PlayerPosition", playerPosition);
       // writer.Write("CharacterName", characterName);
        writer.Write("PlayTime",playTime);
        writer.Write("ChapterNumber", chapterNumber);

        // 変更を反映
        writer.Commit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="SaveDate", menuName = "ScriptableObject/SaveDate")]
public class SaveDate : ScriptableObject
{
    //こいつの役割はゲームシーンのデータを一時的に保存しておくこと。
    //こいつが保存しているデータをセーブ時にはいろんな場所からJsonに入れる前段階の場所
    //ロード時もこいつを呼び出す関数を作っておく

    //セーブデータとして保存しておくのはキャラクターのポジション。
    //シナリオのフラグ管理。カメラの位置
    public Vector3 playerPosition;
    public Vector3 cameraPosition;
    public Vector3 seiitirouPosition;
    public Vector3 haruPosition;
    public Vector3 mistukiPosition;
    public Vector3 reiPosition;
    public Vector3 eijiPosition;
    public Vector3 enemyPosition;
    public Vector3 enemy2Position;
    public string characterName;
    public string playTime;
    public string gameModeString;
    public string locationName;
    public string destinationName;
    public List<bool> itemsPossessionList;
    public List<bool> itemsGetedList;
    public List<bool> gameProgressFlagsList;
    public List<bool> seiitirouProgressFlagsList;
    public List<bool> haruProgressFlagsList;
    public List<bool> endingFlagsList;
    public DifficultyLevelManager difficultyLevelManager;
    public int chapterNumber;
    public int totalSeconds;
    public int getedEndTotalCount;
    public int callNumber;

    public void KeepDateMethod()
    {
        //セーブの際にJsonファイルに書き込む前に各所からデータを取得しておく。
        playerPosition = GameManager.m_instance.player.transform.position;
        haruPosition = SecondHouseManager.secondHouse_instance.haru.transform.position;
        enemyPosition = GameManager.m_instance.enemy.transform.position;
        enemy2Position = SecondHouseManager.secondHouse_instance.ajure.transform.position;
        seiitirouPosition = GameManager.m_instance.seiitirou.transform.position;
        playTime = SaveSlotsManager.save_Instance.saveState.playTime;
        gameModeString = SaveSlotsManager.save_Instance.saveState.gameModeString;
        locationName = FlagsManager.flag_Instance.locationText.text;
        destinationName = FlagsManager.flag_Instance.destinationText.text;
        chapterNumber = SaveSlotsManager.save_Instance.saveState.chapterNum;
        characterName = SaveSlotsManager.save_Instance.saveState.characterName;
        totalSeconds = (int)GameManager.m_instance.nowTime;
        getedEndTotalCount = EndingGalleryManager.m_gallery.getedEndTotalNumber;

        itemsPossessionList.Clear();
        itemsGetedList.Clear();
        gameProgressFlagsList.Clear();
        seiitirouProgressFlagsList.Clear();
        haruProgressFlagsList.Clear();
        endingFlagsList.Clear();
        foreach(Item items in ItemDateBase.itemDate_instance.items)
        {
            itemsPossessionList.Add(items.checkPossession);
            itemsGetedList.Add(items.geted);
        }
        foreach(bool flags in FlagsManager.flag_Instance.flagBools)
        {
            gameProgressFlagsList.Add(flags);
        }
        foreach(bool flags in FlagsManager.flag_Instance.seiitirouFlagBools)
        {
            seiitirouProgressFlagsList.Add(flags);
        }
        foreach(bool flags in FlagsManager.flag_Instance.haruFlagBools)
        {
            haruProgressFlagsList.Add(flags);
        }
        foreach(bool endflags in EndingGalleryManager.m_gallery.endingFlag)
        {
            endingFlagsList.Add(endflags);
        }
    }
    public void LoadDateMethod()
    {
        //ロードの際にJsonファイルから読み取ったデータを一時的に保管しておく(代入してゲーム内に反映させる)。
        GameManager.m_instance.player.transform.position = playerPosition;
        SecondHouseManager.secondHouse_instance.haru.transform.position = haruPosition;
        GameManager.m_instance.enemy.transform.position = enemyPosition;
        SecondHouseManager.secondHouse_instance.ajure.transform.position = enemy2Position;
        GameManager.m_instance.seiitirou.transform.position = seiitirouPosition;
        SaveSlotsManager.save_Instance.saveState.playTime = playTime;
        SaveSlotsManager.save_Instance.saveState.gameModeString = gameModeString;
        FlagsManager.flag_Instance.locationText.text = locationName;
        FlagsManager.flag_Instance.destinationText.text = destinationName;
        SaveSlotsManager.save_Instance.saveState.chapterNum = chapterNumber;
        SaveSlotsManager.save_Instance.saveState.characterName = characterName;
        GameManager.m_instance.nowTime = totalSeconds;
        EndingGalleryManager.m_gallery.getedEndTotalNumber = getedEndTotalCount;
        // 上はリストを削除したりしてるからforeachだけどこっちはデータを入れるだけだから左の方が確定でIndexが多い
        for(int i = 0; i < itemsPossessionList.Count; i++)
        {
            ItemDateBase.itemDate_instance.items[i].checkPossession = itemsPossessionList[i];
            ItemDateBase.itemDate_instance.items[i].geted = itemsGetedList[i];
        }
        for(int i = 0; i < gameProgressFlagsList.Count; i++)
        {
            FlagsManager.flag_Instance.flagBools[i] = gameProgressFlagsList[i];
        }
        for(int i = 0; i < seiitirouProgressFlagsList.Count; i++)
        {
            FlagsManager.flag_Instance.seiitirouFlagBools[i] = seiitirouProgressFlagsList[i];
        }
        for(int i = 0; i < haruProgressFlagsList.Count; i++)
        {
            FlagsManager.flag_Instance.haruFlagBools[i] = haruProgressFlagsList[i];
        }
        for(int i = 0; i < endingFlagsList.Count; i++)
        {
            EndingGalleryManager.m_gallery.endingFlag[i] = endingFlagsList[i];
        }
        switch(SaveSlotsManager.save_Instance.saveState.gameModeString)
        {
            case "Easy":
                GameManager.m_instance.difficultyLevelManager.difficultyLevel = DifficultyLevelManager.DifficultyLevel.Easy;
                GameManager.m_instance.difficultyLevelManager.addEnemySpeed = 0;
                GameManager.m_instance.difficultyLevelManager.hideDifficultyFactor = 0;
                break;
            case "Normal":
                GameManager.m_instance.difficultyLevelManager.difficultyLevel = DifficultyLevelManager.DifficultyLevel.Normal;
                GameManager.m_instance.difficultyLevelManager.addEnemySpeed = 1;
                GameManager.m_instance.difficultyLevelManager.hideDifficultyFactor = 10;
                break;
            case "Hard":
                GameManager.m_instance.difficultyLevelManager.difficultyLevel = DifficultyLevelManager.DifficultyLevel.Hard;
                GameManager.m_instance.difficultyLevelManager.addEnemySpeed = 3;
                GameManager.m_instance.difficultyLevelManager.hideDifficultyFactor = 20;
                break;
        }
        FlagsManager.flag_Instance.LoadFlagsData();
    }
}

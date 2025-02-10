using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SaveSlotsManager : MonoBehaviour
{
    public Sprite[] characterSaveImages = new Sprite[6];
    public Button[] saveSlots = new Button[3];
    public Text[] playTimes = new Text[3];
    public Text[] gameModes = new Text[3];
    public Text[] characters = new Text[3];
    public Text[] Chapters = new Text[3];
    
    private void Start()
    {
        
    }
    public void SaveButton(int i)
    {
        playTimes[i].text = FlagsManager.flag_Instance.playTime.ToString();
        gameModes[i].text = FlagsManager.flag_Instance.gameMode.ToString();
        characters[i].text = FlagsManager.flag_Instance.characterName.ToString();
        Chapters[i].text = FlagsManager.flag_Instance.chapterNum.ToString();
    }

}

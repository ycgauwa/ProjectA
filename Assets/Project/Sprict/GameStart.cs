using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public Sprite[] stageImages = new Sprite[6];
    public Image[] displayImages = new Image[3];
    public Text[] displayTimes = new Text[3];
    public Text[] displayModes = new Text[3];
    public Text[] displayChars = new Text[3];
    public Text[] displayChapters = new Text[3];
    string[] playTimes = new string[3];
    string[] gameModes = new string[3];
    string[] characters = new string[3];
    private int[] chaptersNumber = new int[3];
    public Button[] saveButtons = new Button[3];
    public Button toTitleButton;
    public SaveDate saveDate;
    public Canvas saveCanvas;
    public Canvas optionCanvas;
    public UserData userData;
    public AudioClip decision;
    public AudioClip titleBgm;

    void Start()
    {
        var saveData = SaveSlotsManager.save_Instance;
        SoundManager.sound_Instance.PlayBgm(titleBgm);
        //ロード画面で保存されている情報が色々書きこまれている。
        //別のメソッドを用意したほうがよさそう。
        userData.UpdateSaveData(ref playTimes,ref gameModes,ref characters,ref chaptersNumber);
        if(saveData != null)
        {
            for(int i = 0; i < displayImages.Length; i++)
            {
                displayImages[i].gameObject.SetActive(true);
                displayImages[i].sprite = stageImages[chaptersNumber[i]];
                if(chaptersNumber[i] == 0)
                    displayImages[i].gameObject.SetActive(false);
                displayTimes[i].text = playTimes[i];
                displayModes[i].text = gameModes[i];
                displayChars[i].text = characters[i];
                displayChapters[i].text = chaptersNumber[i].ToString();
            }
            for(int i = 0; i < saveButtons.Length; i++)
            {
                int index = i;
                saveButtons[i].onClick.AddListener(() => userData.LoadUserData(index + 1));  // 各ボタンにクリックイベントを登録
            }
            toTitleButton.onClick.AddListener(ClickToTitleButton);
        }
    }
    public void ClickStartButton()
    {
        SaveSlotsManager.save_Instance.saveState.loadIndex = 0;
        SoundManager.sound_Instance.PlaySe(decision);
        SceneManager.LoadScene("Game");
    }
    public void ClickLoadButton()
    {
        saveCanvas.gameObject.SetActive(true);
        SoundManager.sound_Instance.PlaySe(decision);
    }
    public void ClickToTitleButton()
    {
        if(saveCanvas.gameObject.activeSelf)
            saveCanvas.gameObject.SetActive(false);
        else if(optionCanvas.gameObject.activeSelf)
            optionCanvas.gameObject.SetActive(false);
        SoundManager.sound_Instance.PlaySe(decision);
    }
    public void ClickToOptionButton()
    {
        optionCanvas.gameObject.SetActive(true);
        SoundManager.sound_Instance.PlaySe(decision);
    }
    public void ClickToExitButton()
    {
        SoundManager.sound_Instance.PlaySe(decision);
        #if UNITY_EDITOR
        // Unityエディターでの動作
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        // 実際のゲーム終了処理
        Application.Quit();
        #endif
    }
    // URLを開く関数
    public void OpenWebsite(string url)
    {
        Application.OpenURL(url);
        SoundManager.sound_Instance.PlaySe(decision);
    }
    public void SaveSoundValues()
    {
        PlayerPrefs.Save();
        SoundManager.sound_Instance.PlaySe(decision);
    }
}

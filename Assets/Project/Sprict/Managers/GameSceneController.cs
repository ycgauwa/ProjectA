using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class GameSceneController : MonoBehaviour
{
    public List<string> saveMessages;
    public List<string> saveNames;
    public List<Sprite> saveImages;
    public Canvas Selectwindow;
    public Image saveConfilmPanel;
    public GameObject firstSelect;
    public Sprite[] stageImages = new Sprite[6];
    public Image[] images = new Image[3];
    public Text[] times = new Text[3];
    public Text[] modes = new Text[3];
    public Text[] chars = new Text[3];
    public Text[] chapters = new Text[3];
    string[] playTimes = new string[3];
    string[] gameModes = new string[3];
    string[] characters = new string[3];
    private int[] chapterNumber = new int[3];
    private RectTransform rectTransform;
    public Image savedMessageImage;
    public Vector2 targetPosition;
    public UserData userdata;

    void Start()
    {
        rectTransform = savedMessageImage.GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(1, 0); // �E���A���J�[
        rectTransform.anchorMax = new Vector2(1, 0); // �E���A���J�[
        rectTransform.anchoredPosition = new Vector2(Screen.width, 70); // ��ʊO
        //rectTransform.anchoredPosition = new Vector3(900, -230, 0);
        userdata.UpdateSaveData(ref playTimes, ref gameModes, ref characters, ref chapterNumber);
        for(int i = 0; i < images.Length; i++)
        {
            images[i].gameObject.SetActive(true);
            images[i].sprite = stageImages[chapterNumber[i]];
            if(chapterNumber[i] == 0)
                images[i].gameObject.SetActive(false);
            times[i].text = playTimes[i];
            modes[i].text = gameModes[i];
            chars[i].text = characters[i];
            chapters[i].text = chapterNumber[i].ToString();
        }
        switch(SaveSlotsManager.save_Instance.saveState.loadIndex)
        {
            case 0:
                SaveSlotsManager.save_Instance.saveState.characterName = "�K�l";
                ToEvent1.one = false;
                SaveSlotsManager.save_Instance.saveState.chapterNum = 1;
                GameManager.m_instance.stopSwitch = true;
                break;
            case > 0:
                userdata.saveDate.LoadDateMethod();
                break;
        }
    }
    private void SavedateLoading(int i)
    {
        //�����̃C���f�b�N�X�̓e�L�X�g�ȂǃA�^�b�`���Ă���v�f�̂���0�X�^�[�g
        //�E���̃C���f�b�N�X�̓Z�[�u�f�[�^�̂���0�̓f�o�b�N�p�̃f�[�^�������Ă��邩��1�X�^�[�g
        images[i-1].gameObject.SetActive(true);
        images[i-1].sprite = stageImages[userdata.chapterNumber];
        if(userdata.chapterNumber == 0)
            images[i - 1].gameObject.SetActive(false);
        times[i - 1].text = userdata.playTime;
        modes[i - 1].text = userdata.saveDate.gameModeString;
        chars[i - 1].text = userdata.characterName;
        chapters[i - 1].text = userdata.chapterNumber.ToString();

        GameManager.m_instance.player.transform.position = userdata.playerPosition;
        GameManager.m_instance.enemy.transform.position = userdata.enemyPosition;
        SecondHouseManager.secondHouse_instance.haru.transform.position = userdata.haruPosition;
        GameManager.m_instance.seiitirou.transform.position = userdata.seiitirouPosition;
        SaveSlotsManager.save_Instance.saveState.playTime = userdata.playTime;
        SaveSlotsManager.save_Instance.saveState.gameModeString = userdata.saveDate.gameModeString;
        GameManager.m_instance.nowTime = userdata.saveDate.totalSeconds;
        GameManager.m_instance.gameTimerCountHour += userdata.saveDate.totalSeconds / 3600;
        userdata.saveDate.totalSeconds %= 3600;
        GameManager.m_instance.gameTimerCountMinute += userdata.saveDate.totalSeconds / 60;
        GameManager.m_instance.gameTimerCountSecond += userdata.saveDate.totalSeconds % 60;
        GameManager.m_instance.stopSwitch = false;
    }
    //�@�Z�[�u�������Ƃ��ɕ\�ʏ�̕\�L��ς��邽�߂̃{�^��
    public void SaveButton(int i)
    {
        Debug.Log(i);
        rectTransform.DOAnchorPos(targetPosition, 1f).SetEase(Ease.OutCubic).SetUpdate(true); ;//�Z�[�u�A�j���[�V����
        times[i].text = SaveSlotsManager.save_Instance.saveState.playTime.ToString();
        modes[i].text = SaveSlotsManager.save_Instance.saveState.gameModeString.ToString();
        chars[i].text = SaveSlotsManager.save_Instance.saveState.characterName;
        chapters[i].text = SaveSlotsManager.save_Instance.saveState.chapterNum.ToString();
        images[i].sprite = stageImages[SaveSlotsManager.save_Instance.saveState.chapterNum];
        if(!images[i].gameObject.activeSelf)
            images[i].gameObject.SetActive(true);
    }
    public void AcceptSaveRequest()
    {
        saveConfilmPanel.gameObject.SetActive(false);
        Selectwindow.gameObject.SetActive(false);
        MessageManager.message_instance.isOpenSelect = false;
        GameManager.m_instance.notSaveSwitch = false;
        GameManager.m_instance.saveCanvas.gameObject.SetActive(true);
    }
    public void IgnoreSaveRequest()
    {
        MessageManager.message_instance.isOpenSelect = false;
        saveConfilmPanel.gameObject.SetActive(false);
        Selectwindow.gameObject.SetActive(false);
    }
    public void ResetImagePosition()
    {
        rectTransform.anchoredPosition = new Vector2(Screen.width, 70); // ��ʊO
    }
}
public class SaveState
{
    //������̓Z�[�u�{�^�������������ɃQ�[���V�[���R���g���[���[���Q�Ƃ���邽�߂̕ϐ��B
    //�Z�[�u�X���b�g�ɕۊǂ��Ă������ƃX���b�g���ꂼ��ɓ���Ă����Ȃ��Ⴞ����Q�[���V�[��
    //�ŊǗ����Ă������߂̃C���X�^���g�ȕϐ��B
    public Vector3 playerPosition;
    public int chapterNum;
    public string characterName;
    public string playTime;
    public string gameModeString;
    public int loadIndex;
    public Sprite playerImage;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using System.Threading;

public class EndingGalleryManager : MonoBehaviour
{
    public Image[] endingGallerys  = new Image[100];
    public Sprite[] endingSprites = new Sprite[100];
    public bool[] endingFlag = new bool[100];
    public Image[] endingGalleryPages = new Image[16];
    private int activePageNum = 0;
    public int getedEndTotalNumber;
    public Sprite testSprite;
    public EndingCase1 endingCase1;
    public Case1Object case1Object;
    public EndingCase2 endingCase2;
    public EndingCase3 endingCase3;
    public GameObject endingDetail;
    public Image endingDetailImage;
    public Image endedUIPanel;
    public Image noiseImage;
    public Text endingDetailText;
    public Text endCountNumber;
    public Text endCountNextNumber;
    public AudioClip ending5Bgm;
    public AudioClip freezeSound;
    public AudioClip blizzardSound;
    public AudioClip noiseSound;
    public AudioClip endingCountSound;
    public string explainText;
    public static EndingGalleryManager m_gallery;

    private void Awake()
    {
        if(m_gallery == null)
            m_gallery = this;
        else
        {
            Destroy(this);
        }
    }
    private void Start()
    {
        //ƒ[ƒh‚µ‚½Žž‚ÉƒMƒƒƒ‰ƒŠ[‰æ–Ê‚ÌUI‚ðXV‚Ü‚½AƒGƒ“ƒfƒBƒ“ƒO‰ñŽû‚É‚æ‚Á‚Ä‚©‚í‚é—v‘f‚ð‚±‚±‚Å‰Á‚¦‚Ä‚¨‚­
        if(SaveSlotsManager.save_Instance.saveState.loadIndex > 0)
        {
            for(int i = 0 ; i <  endingFlag.Length; i++)
            {
                if(endingFlag[i] == true)
                {
                    m_gallery.endingGallerys[i].sprite = endingSprites[i];
                    LoadEndingChanges(i);
                }
            }
        }
    }
    public void EndingToDetail(int i)
    {
        //ƒ{ƒ^ƒ“‚ð‰Ÿ‚µ‚½‚Æ‚«‚É‰ð•ú‚µ‚Ä‚È‚¢ƒGƒ“ƒh‚ÍŠJ‚¯‚È‚¢‚æ‚¤‚É‚µ‚½‚¢B
        if(endingGallerys[i].sprite == testSprite) return;
        endingDetail.gameObject.SetActive(true);
        endingDetailImage.sprite = endingGallerys[i].sprite;
        if(i == 0) 
            explainText = "End1 l¶Å‘å‚Ì‘I‘ð\nyŽæ“¾ðŒz\nÅ‰‚Ì‘I‘ð‚ÅŠwZ‚És‚©‚¸Q‰ß‚²‚·";
        else if(i == 1)
            explainText = "End2 “¦–S‚ðŽŽ‚Ý‚½“V”±\nyŽæ“¾ðŒz\n”ò‚Î‚³‚ê‚½êŠ‚ÉŒºŠÖ‚©‚çŠO‚Öo‚æ‚¤‚Æ‚·‚é";
        else if(i == 2)
            explainText = "End3 ˜M‚Î‚ê‚½¬“®•¨\nyŽæ“¾ðŒz\nƒNƒ[ƒ[ƒbƒg‚É‰B‚ê‚½Œã‚ÉŽ_Œ‡‡V‚Æ‚È‚Á‚ÄŽ€–S‚·‚é";
        else if(i == 3)
            explainText = "End4 ‹÷‚É’Ç‚¢‚â‚ç‚ê‚½‘l\nyŽæ“¾ðŒz\nƒNƒ[ƒ[ƒbƒg‚É‰B‚ê‚Ä‚©‚çˆÀ‘S‚ðŠm”F‚¹‚¸ŠO‚Éo‚æ‚¤‚Æ‚·‚é";
        else if(i == 4)
            explainText = "End5 Žô‚í‚ê‚½¶–½‚Ì‹~Ï\nyŽæ“¾ðŒz\n“ä‚Ì’j‚ðŒ©ŽE‚µ‚É‚µ‚ÄŽ©•ª‚Ì‚½‚ß‚É—Fl‚Ì°‚à¶æÑ‚É‚·‚é";
        else if(i == 5)
            explainText = "End6 ŒŒ‚É‚Ü‚Ý‚ê‚½”ÓŽ`‰ï\nyŽæ“¾ðŒz\neØ‚È‚Ê‚¢‚®‚é‚Ý‚½‚¿‚Éì‚Á‚½—¿—‚ð“n‚³‚È‚¢";
        else if(i == 6)
            explainText = "End7 •ß‚ç‚í‚ê‚½Šl•¨‚ÌœÔšL\nyŽæ“¾ðŒz\nˆÙŠE‚Ì‚QŒ¬–Ú‚Ì–¯‰Æ‚É‚ÄŒºŠÖ‚©‚ço‚æ‚¤‚Æ‚·‚é";//–¼‘O•ÏXƒAƒŠ
        else if(i == 7)
            explainText = "End8 “€‚Ä‚Â‚­¢ŠE‚Ì’†‚Å\nyŽæ“¾ðŒz\n¶g‚Ìó‘Ô‚ÅŒºŠÖ‚ÌƒhƒA‚ðŠJ‚¯‚éB";
        else if(i == 8)
            explainText = "End9 –S—ì‚Æ‘žˆ«‚Ì–ÚŠo‚ß\nyŽæ“¾ðŒz\nªˆê˜Y‚ªKl‚ð¶æÑ‚É‚µ‚½ŒãA°‚ðŒ©ŽE‚µ‚É‚·‚éB";
        else if(i == 9)
            explainText = "End6 ŒŒ‚É‚Ü‚Ý‚ê‚½”ÓŽ`‰ï\nyŽæ“¾ðŒz\neØ‚È‚Ê‚¢‚®‚é‚Ý‚½‚¿‚Éì‚Á‚½—¿—‚ð“n‚³‚È‚¢";
        endingDetailText.text = explainText;
    }
    public  async UniTask GetedEndings(int getedEndingCount, CancellationToken ct = default)
    {
        SecondHouseManager.secondHouse_instance.light2D.intensity = 0f;
        endCountNumber.text = getedEndTotalNumber.ToString();
        getedEndTotalNumber++;
        endCountNextNumber.text = getedEndTotalNumber.ToString();
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: ct);
        //‚±‚ÌŠÖ”‚ðŒÄ‚Ño‚µ‚½‚çŽæ“¾Œã‚Ì”Žš‚ª•Ï‚í‚é‰‰o‚ðo‚·
        //‹ï‘Ì“I‚ÉƒGƒ“ƒfƒBƒ“ƒO‚ÌŽæ“¾ŒÂ”‚ª•Ï‚í‚é‚Ì‚¾‚ª‚»‚ê‚Í¬‚³‚¢”Žš‚Ì“§–¾“x‚ª”¼•ª‚ðØ‚Á‚½’iŠK
        //‚ÅV‚µ‚¢‚Ù‚¤‚Ì”Žš‚ª™X‚É•\‹L‚³‚ê‚éB‚ÅÅ‰‚ÆÅŒã‚Å‚¢‚¶‚é•Ï”‚ÍƒZ[ƒu‚ÆƒXƒgƒbƒv‚ÌƒXƒCƒbƒ`
        //‚ ‚Æ‚Í‚Q‚Â‚Ì•¶Žš‚Ì“§–¾“x‚ðÅŒã‚Í0‚É•Ï‚¦‚éB—~‚ðŒ¾‚¦‚Î•|‚¢SE‚à—~‚µ‚¢B
        GameManager.m_instance.notSaveSwitch = true;
        GameManager.m_instance.stopSwitch = true;
        endedUIPanel.gameObject.SetActive(true);
        noiseImage.gameObject.SetActive(true);
        SecondHouseManager.secondHouse_instance.light2D.intensity = 1f;
        SoundManager.sound_Instance.PlaySe(noiseSound);
        await UniTask.Delay(TimeSpan.FromSeconds(1f), cancellationToken: ct);
        SoundManager.sound_Instance.StopSe(noiseSound);
        SoundManager.sound_Instance.PlaySe(endingCountSound);
        noiseImage.gameObject.SetActive(false);
        Color color = endCountNumber.GetComponent<Text>().color;
        Color nextColor = endCountNextNumber.GetComponent<Text>().color;

        while (color.a > 0f)
        {
            color.a -= 0.01f;
            endCountNumber.color = color;
            await UniTask.Delay(1);
            if(color.a < 0.5f)
            {
                nextColor.a += 0.007f;
                endCountNextNumber.color = nextColor;
            }
        }
        while (nextColor.a < 1f)
        {
            nextColor.a += 0.007f;
            endCountNextNumber.color = nextColor;
            await UniTask.Delay(1);
        }
        noiseImage.gameObject.SetActive(true);
        SoundManager.sound_Instance.PlaySe(noiseSound);
        await UniTask.Delay(TimeSpan.FromSeconds(1f), cancellationToken: ct);
        SoundManager.sound_Instance.StopSe(noiseSound);

        color.a = 1f;
        nextColor.a = 0f;
        endCountNumber.color = color;
        endCountNextNumber.color = nextColor;
        noiseImage.gameObject.SetActive(false);

        GameManager.m_instance.notSaveSwitch = false;
        GameManager.m_instance.stopSwitch = false;
        endedUIPanel.gameObject.SetActive(false);
    }
    public void LoadEndingChanges(int endNumber)
    {
        switch(endNumber)
        {
            case 0:
                endingCase1.gameObject.transform.position = new Vector3(-35, 29, 0);
                endingCase1.answerNum = 3;
                Destroy(endingCase1.faliedSelect.gameObject);
                break;
            case 1:
                endingCase2.entrance.gameObject.SetActive(false);
                endingCase2.wall.gameObject.SetActive(true);
                endingCase2.gameObject.SetActive(false);
                break;
            case 2:
                endingCase3.gameObject.SetActive(false);
                break;
            case 3:
                endingCase3.gameObject.SetActive(false);
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                break;
            case 9:
                break;
            case 10:
                break;
            case 11:
                break;
            case 12:
                break;
            case 13:
                break;
            case 14:
                break;
            case 15:
                break;
            case 16:
                break;
            case 17:
                break;
            case 18:
                break;
            case 19:
                break;                   
            case 20:
                break;
            default
                : break;
        }
    }
    public void CloseEndingDetail()
    {
        endingDetail.gameObject.SetActive(false);
        explainText = "";
    }
    public void NextGalleryPage()
    {
        if(endingGalleryPages[activePageNum].gameObject.activeSelf)
        {
            SoundManager.sound_Instance.PlaySe(GameManager.m_instance.decision);
            endingGalleryPages[activePageNum].gameObject.SetActive(false);
            endingGalleryPages[activePageNum + 1].gameObject.SetActive(true);
            activePageNum++;
        }
    }
    public void BackGalleryButton()
    {
        if(endingGalleryPages[activePageNum].gameObject.activeSelf)
        {
            SoundManager.sound_Instance.PlaySe(GameManager.m_instance.decision);
            endingGalleryPages[activePageNum].gameObject.SetActive(false);
            endingGalleryPages[activePageNum - 1].gameObject.SetActive(true);
            activePageNum--;
        }
    }
}

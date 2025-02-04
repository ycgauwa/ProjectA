using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class EndingGalleryManager : MonoBehaviour
{
    public Image[] endingGallerys  = new Image[100];
    public bool[] endingFlag = new bool[100];
    public Image[] endingGalleryPages = new Image[16];
    private int activePageNum = 0;
    public Sprite testSprite;
    public EndingCase1 endingCase1;
    public Case1Object case1Object;
    public EndingCase2 endingCase2;
    public EndingCase3 endingCase3;
    public GameObject endingDetail;
    public Image endingDetailImage;
    public Text endingDetailText;
    public AudioClip ending5Bgm;
    public AudioClip freezeSound;
    public AudioClip blizzardSound;
    public string explainText;
    public static EndingGalleryManager m_gallery;

    private void Start()
    {
        m_gallery = this;
    }
    public void EndingToDetail(int i)
    {
        //ƒ{ƒ^ƒ“‚ğ‰Ÿ‚µ‚½‚Æ‚«‚É‰ğ•ú‚µ‚Ä‚È‚¢ƒGƒ“ƒh‚ÍŠJ‚¯‚È‚¢‚æ‚¤‚É‚µ‚½‚¢B
        if(endingGallerys[i].sprite == testSprite) return;
        endingDetail.gameObject.SetActive(true);
        endingDetailImage.sprite = endingGallerys[i].sprite;
        if(i == 0) 
            explainText = "End1 l¶Å‘å‚Ì‘I‘ğ\nyæ“¾ğŒz\nÅ‰‚Ì‘I‘ğ‚ÅŠwZ‚És‚©‚¸Q‰ß‚²‚·";
        else if(i == 1)
            explainText = "End2 “¦–S‚ğ‚İ‚½“V”±\nyæ“¾ğŒz\n”ò‚Î‚³‚ê‚½êŠ‚ÉŒºŠÖ‚©‚çŠO‚Öo‚æ‚¤‚Æ‚·‚é";
        else if(i == 2)
            explainText = "End3 ˜M‚Î‚ê‚½¬“®•¨\nyæ“¾ğŒz\nƒNƒ[ƒ[ƒbƒg‚É‰B‚ê‚½Œã‚É_Œ‡‡V‚Æ‚È‚Á‚Ä€–S‚·‚é";
        else if(i == 3)
            explainText = "End4 ‹÷‚É’Ç‚¢‚â‚ç‚ê‚½‘l\nyæ“¾ğŒz\nƒNƒ[ƒ[ƒbƒg‚É‰B‚ê‚Ä‚©‚çˆÀ‘S‚ğŠm”F‚¹‚¸ŠO‚Éo‚æ‚¤‚Æ‚·‚é";
        else if(i == 4)
            explainText = "End5 ô‚í‚ê‚½¶–½‚Ì‹~Ï\nyæ“¾ğŒz\n“ä‚Ì’j‚ğŒ©E‚µ‚É‚µ‚Ä©•ª‚Ì‚½‚ß‚É—Fl‚Ì°‚à¶æÑ‚É‚·‚é";
        else if(i == 5)
            explainText = "End6 ŒŒ‚É‚Ü‚İ‚ê‚½”Ó`‰ï\nyæ“¾ğŒz\neØ‚È‚Ê‚¢‚®‚é‚İ‚½‚¿‚Éì‚Á‚½—¿—‚ğ“n‚³‚È‚¢";
        else if(i == 6)
            explainText = "End7 •ß‚ç‚í‚ê‚½Šl•¨‚ÌœÔšL\nyæ“¾ğŒz\nˆÙŠE‚Ì‚QŒ¬–Ú‚Ì–¯‰Æ‚É‚ÄŒºŠÖ‚©‚ço‚æ‚¤‚Æ‚·‚é";//–¼‘O•ÏXƒAƒŠ
        else if(i == 7)
            explainText = "End8 “€‚Ä‚Â‚­¢ŠE‚Ì’†‚Å\nyæ“¾ğŒz\n¶g‚Ìó‘Ô‚ÅŒºŠÖ‚ÌƒhƒA‚ğŠJ‚¯‚éB";
        else if(i == 8)
            explainText = "End9 –S—ì‚Æ‘ˆ«‚Ì–ÚŠo‚ß\nyæ“¾ğŒz\nªˆê˜Y‚ªKl‚ğ¶æÑ‚É‚µ‚½ŒãA°‚ğŒ©E‚µ‚É‚·‚éB";
        else if(i == 9)
            explainText = "End6 ŒŒ‚É‚Ü‚İ‚ê‚½”Ó`‰ï\nyæ“¾ğŒz\neØ‚È‚Ê‚¢‚®‚é‚İ‚½‚¿‚Éì‚Á‚½—¿—‚ğ“n‚³‚È‚¢";
        endingDetailText.text = explainText;
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

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
    public Sprite testSprite;
    public EndingCase1 endingCase1;
    public Case1Object case1Object;
    public EndingCase2 endingCase2;
    public EndingCase3 endingCase3;
    public GameObject endingDetail;
    public Image endingDetailImage;
    public Text endingDetailText;
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
        endingDetailText.text = explainText;
    }
    public void CloseEndingDetail()
    {
        endingDetail.gameObject.SetActive(false);
        explainText = "";
    }
}

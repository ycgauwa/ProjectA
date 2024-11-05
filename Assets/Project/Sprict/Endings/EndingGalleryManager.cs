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
        //�{�^�����������Ƃ��ɉ�����ĂȂ��G���h�͊J���Ȃ��悤�ɂ������B
        if(endingGallerys[i].sprite == testSprite) return;
        endingDetail.gameObject.SetActive(true);
        endingDetailImage.sprite = endingGallerys[i].sprite;
        if(i == 0) 
            explainText = "End1 �l���ő�̑I��\n�y�擾�����z\n�ŏ��̑I���Ŋw�Z�ɍs�����Q�߂���";
        else if(i == 1)
            explainText = "End2 ���S�����݂��V��\n�y�擾�����z\n��΂��ꂽ�ꏊ�Ɍ��ւ���O�֏o�悤�Ƃ���";
        else if(i == 2)
            explainText = "End3 �M�΂ꂽ������\n�y�擾�����z\n�N���[�[�b�g�ɉB�ꂽ��Ɏ_���V�ƂȂ��Ď��S����";
        else if(i == 3)
            explainText = "End4 ���ɒǂ����ꂽ�l\n�y�擾�����z\n�N���[�[�b�g�ɉB��Ă�����S���m�F�����O�ɏo�悤�Ƃ���";
        endingDetailText.text = explainText;
    }
    public void CloseEndingDetail()
    {
        endingDetail.gameObject.SetActive(false);
        explainText = "";
    }
}

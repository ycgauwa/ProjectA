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
        else if(i == 4)
            explainText = "End5 ���ꂽ�����̋~��\n�y�擾�����z\n��̒j�����E���ɂ��Ď����̂��߂ɗF�l�̐������тɂ���";
        else if(i == 5)
            explainText = "End6 ���ɂ܂݂ꂽ�ӎ`��\n�y�擾�����z\n�e�؂Ȃʂ�����݂����ɍ����������n���Ȃ�";
        else if(i == 6)
            explainText = "End7 �߂��ꂽ�l���̜ԚL\n�y�擾�����z\n�يE�̂Q���ڂ̖��ƂɂČ��ւ���o�悤�Ƃ���";//���O�ύX�A��
        else if(i == 7)
            explainText = "End8 ���Ă����E�̒���\n�y�擾�����z\n���g�̏�ԂŌ��ւ̃h�A���J����B";
        else if(i == 8)
            explainText = "End9 �S��Ƒ����̖ڊo��\n�y�擾�����z\n����Y���K�l���тɂ�����A�������E���ɂ���B";
        else if(i == 9)
            explainText = "End6 ���ɂ܂݂ꂽ�ӎ`��\n�y�擾�����z\n�e�؂Ȃʂ�����݂����ɍ����������n���Ȃ�";
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

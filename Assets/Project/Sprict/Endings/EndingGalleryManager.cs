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
        //���[�h�������ɃM�������[��ʂ�UI���X�V�܂��A�G���f�B���O����ɂ���Ă����v�f�������ŉ����Ă���
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
    public  async UniTask GetedEndings(int getedEndingCount, CancellationToken ct = default)
    {
        SecondHouseManager.secondHouse_instance.light2D.intensity = 0f;
        endCountNumber.text = getedEndTotalNumber.ToString();
        getedEndTotalNumber++;
        endCountNextNumber.text = getedEndTotalNumber.ToString();
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: ct);
        //���̊֐����Ăяo������擾��̐������ς�鉉�o���o��
        //��̓I�ɃG���f�B���O�̎擾�����ς��̂�������͏����������̓����x��������؂����i�K
        //�ŐV�����ق��̐��������X�ɕ\�L�����B�ōŏ��ƍŌ�ł�����ϐ��̓Z�[�u�ƃX�g�b�v�̃X�C�b�`
        //���Ƃ͂Q�̕����̓����x���Ō��0�ɕς���B�~�������Ε|��SE���~�����B
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

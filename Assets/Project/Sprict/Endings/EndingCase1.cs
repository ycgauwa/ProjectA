using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using UnityEngine;
using UnityEngine.EventSystems;
using Cysharp.Threading.Tasks;
using System.Threading;

public class EndingCase1 : MonoBehaviour
{
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<Sprite> image;
    [SerializeField]
    private List<string> messages2;
    [SerializeField]
    private List<string> names2;
    [SerializeField]
    private List<Sprite> image2;
    [SerializeField]
    private List<string> messages3;
    [SerializeField]
    private List<string> names3;
    [SerializeField]
    private List<Sprite> image3;
    [SerializeField]
    private List<string> messages4;
    [SerializeField]
    private List<string> names4;
    [SerializeField]
    private List<Sprite> image4;

    public GameObject player;
    public Light2D light2D;
    public Canvas window;
    public Canvas Selectwindow;
    public Text target;
    public Text nameText;
    public Image characterImage;
    public Image selection;
    public SoundManager soundManager;
    public AudioClip runSound;
    public AudioClip decision;
    public AudioClip tensionBGM;
    public GameObject firstSelect;

    //�A���T�[�Ƃ��ĉ��𓚂�����
    public  int answerNum;
    public bool answered = false;
    private bool isContacted = false;
    // Start is called before the first frame update
    void Start()
    {
        answerNum = 0;
        //0�������l1���ے肵����2�m�肵����
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player")) isContacted = true;
    }
    // collider�����I�u�W�F�N�g�̗̈�O�ɂł��Ƃ�(���L�Ő���1)
    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player")) isContacted = false;
    }
    private void Update()
    {
        //�b��������(�����͓��I�Ȃ��̂ƍ����bool�̂悤�ɍP��I�Ȃ��̂ŕ�������������)
        if(Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return))
        {
            if(isContacted == true && answered == false)
            {
                if (answerNum == 1) MessageManager.message_instance.MessageWindowActive(messages4, names4, image4, ct:destroyCancellationToken).Forget();
                else if (answerNum == 0)
                {
                    MessageManager.message_instance.MessageSelectWindowActive(messages, names, image,Selectwindow,selection,firstSelect, tensionBGM,ct: destroyCancellationToken).Forget();
                    answered = true;
                }
                else return;
            }
        }
    }
    public void End1SelectYes()
    {
        //��ʂ��ǂ�ǂ�Â��Ȃ��Ă����A�^���ÂȊԂ�TP���Ă�BTP����^�C�~���O�ňړ����Ȃ񂩂�������Ƃ悢
        soundManager.PlaySe(decision);
        answerNum = 2;
        StartCoroutine("Blackout");
        selection.gameObject.SetActive(false);
        Selectwindow.gameObject.SetActive(false);
        MessageManager.message_instance.isOpenSelect = false;
        soundManager.StopBgm(tensionBGM);
    }
    public void End1SelectNo()
    {
        //���b�Z�[�W���o���āA�E�B���h�E����邻�̂��ƃx�b�g�𒲂ׂ�ƐQ����悤�ɂ���B
        soundManager.PlaySe(decision);
        selection.gameObject.SetActive(false);
        Selectwindow.gameObject.SetActive(false);
        answerNum = 1;
        MessageManager.message_instance.isOpenSelect = false;
        soundManager.StopBgm(tensionBGM);
        answered = false;
    }
    private IEnumerator Blackout()
    {
        light2D.intensity = 1.0f;
        GameManager.m_instance.stopSwitch = true;
        soundManager.PlaySe(runSound);
        while(light2D.intensity > 0.01f)
        {
            light2D.intensity -= 0.007f;
            yield return null; //�����łP�t���[���҂��Ă���Ă�
        }
        player.transform.position = new Vector3(-11, -72,0);
        light2D.intensity = 1.0f;
        GameManager.m_instance.stopSwitch = false;
        soundManager.StopSe(runSound);
    }
    //��ԏ��߂̃G���f�B���O�ŁA�s���Ȃ��B�Q���I�������Ƃ���u�������������A���Ă��Ȃ������B�v�Ƃ����e�L�X�g�ƂƂ��Ɍ������V�[����`��
    //����Ƃ��ẮA���b�Z�[�W���I�������u�͂��vor�u�������v�Ń��b�Z�[�W�Ȃǂ��ς��B
}

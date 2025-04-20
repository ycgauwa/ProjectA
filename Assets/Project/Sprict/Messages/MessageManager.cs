using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MessageManager : MonoBehaviour
{
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<Sprite> image;
    public Canvas window;
    public Canvas SelectCanvas;
    public Text target;
    public Text nameText;
    public Image characterImage;
    public Image selection;
    public SoundManager soundManager;
    public AudioClip selectionBGM;
    public GameObject firstSelect;
    private Item selectedItem;
    public static MessageManager message_instance;
    public bool talking = false;
    public bool isTalking = false;
    public bool isOpenSelect = false;
    public bool isTextAdvanceEnabled = true;

    //�����Ń��b�Z�[�W�X�N���v�g���Ăяo���X�N���v�g���쐬����
    private void Awake()
    {
        if(message_instance == null)
        {
            message_instance = this;
        }
        else
        {
            Destroy(message_instance);
        }
    }
    public async UniTask MessageCoroutine(CancellationToken ct)
    {
        await UniTask.Delay(1, cancellationToken: ct);
        isTalking = true;
        //if���ŃR���[�`�������邩�ǂ����̏����������
        //���b�Z�[�W���������bool�����iIEnumerator���Łj
        window.gameObject.SetActive(true);
        await OnAction(ct);

        target.text = "";
        GameManager.m_instance.ImageErase(characterImage);
        window.gameObject.SetActive(false);
        //�Ăјb����悤�ɂȂ邽�߂̃X�C�b�`
        talking = false;
        Refrigerator.messageSwitch = false;
        PlayerManager.m_instance.m_speed = 0.075f;
        Homing.m_instance.speed = 2 + GameManager.m_instance.difficultyLevelManager.addEnemySpeed;
        SecondHouseManager.secondHouse_instance.ajure.speed = SecondHouseManager.secondHouse_instance.ajure.savedSpeed;
        SecondHouseManager.secondHouse_instance.ajure.acceleration = SecondHouseManager.secondHouse_instance.ajure.savedAcceleration;
        Debug.Log(SecondHouseManager.secondHouse_instance.ajure.speed);
        Time.timeScale = 1f;
        await UniTask.DelayFrame(1,cancellationToken : ct);
        isTalking = false;
    }
    public async UniTask OnceMessageCoroutine(CancellationToken ct)
    {
        window.gameObject.SetActive(true);
        await OnAction(ct);

        target.text = "";
        GameManager.m_instance.ImageErase(characterImage);
        window.gameObject.SetActive(false);
        PlayerManager.m_instance.m_speed = 0.075f;
        Homing.m_instance.speed = 2 + GameManager.m_instance.difficultyLevelManager.addEnemySpeed;
        SecondHouseManager.secondHouse_instance.ajure.speed = SecondHouseManager.secondHouse_instance.ajure.savedSpeed;
        SecondHouseManager.secondHouse_instance.ajure.acceleration = SecondHouseManager.secondHouse_instance.ajure.savedAcceleration;
        Debug.Log(SecondHouseManager.secondHouse_instance.ajure.speed);
        Time.timeScale = 1f;
        await UniTask.DelayFrame(1, cancellationToken: ct);
        isTalking = false;
    }
    private async UniTask SelectionMessages(CancellationToken ct)
    {
        isTalking = true;
        window.gameObject.SetActive(true);
        for(int i = 0; i < messages.Count; ++i)
        {
            await UniTask.DelayFrame(1, cancellationToken: ct);
            showMessage(messages[i], names[i], image[i]);
            if(i == messages.Count - 1)
            {
                soundManager.PlayBgm(selectionBGM);
                SelectCanvas.gameObject.SetActive(true);
                selection.gameObject.SetActive(true);
                EventSystem.current.SetSelectedGameObject(firstSelect);
                isOpenSelect = true;
                break;
            }
            await UniTask.WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return), cancellationToken:ct) ;
        }
        await UniTask.WaitUntil(() => !isOpenSelect, cancellationToken: ct);
        target.text = "";
        GameManager.m_instance.ImageErase(characterImage);
        window.gameObject.SetActive(false);
        isTalking = false;
    }
    protected void showMessage(string message, string name ,Sprite image)
    {
        target.text = message;
        nameText.text = name;
        characterImage.sprite = image;
    }
    async UniTask OnAction(CancellationToken ct)
    {
        for(int i = 0; i < messages.Count; ++i)
        {
            await UniTask.DelayFrame(5,cancellationToken :ct);
            showMessage(messages[i], names[i], image[i]);
            await UniTask.WaitUntil(() => (isTextAdvanceEnabled && (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return))), cancellationToken: ct);
        }
    }
    /*�i�j�̒��Ɉ���������邻�̈����̒��g��Test1.cs���n���Ă��Ă���
    �󂯎�鑤�ł�List<string>�܂Ō^��������*/
    public async UniTask MessageWindowActive(List<string>messages,List<string>names, List<Sprite> image,bool msgSwitch = true /*�������ŏ���������Ă邩��M���Ăق����Ȃ��Ȃ������false��^���Ă���*/,CancellationToken ct = default)
    {
        //�R���[�`���͎n�܂�O�Ɉ��͂������
        if(isTalking) return;
        Debug.Log("MessageWindowActive ���\�b�h�J�n");
        this.messages = messages;
        this.names = names;
        this.image = image;
        talking = msgSwitch;
        Debug.Log("MessageCoroutine ���\�b�h�Ăяo���O");
        await MessageCoroutine(ct);
        Debug.Log("MessageCoroutine ���\�b�h�Ăяo����");
    }
    /*�^��_�܂Ƃ�
    �P�󂯎�鑤�̈����̖��O��msg��nam�����ǂ���ǂ�����Ƃ��Ă��Ă�́H
    �������܂ł����x���B�A�h���X�����锠�̂悤�Ȃ��̂Ŗ��O�͊֌W�Ȃ������o�[�ϐ����������ꂽ���_�ŃA�h���X�����܂��B
    �����Test.cs�œn���͕̂�����ł͂Ȃ��A�h���X�̂݁BTest2��Test3���o�Ă��Ă��󂯎�鑤�̈����͔��Ȃ̂ł��̂܂܂ŗǂ�*/
    public async UniTask MessageWindowOnceActive(List<string> messages, List<string> names, List<Sprite> image , CancellationToken ct = default)
    {
        if(isTalking) return;
        this.messages = messages;
        this.names = names;
        this.image = image;
        await OnceMessageCoroutine(ct);
    }
    // �I���������鎞�p�̃��\�b�h�B�K�v�ɉ����ĕK�v�Ȉ������ς�邾�낤����I�[�o�[���[�h(�A�C�e���p�Ƃ�)��p�ӂ��Ă����B
    public async UniTask MessageSelectWindowActive(List<string> messages, List<string> names, List<Sprite> image,Canvas canvas,Image sel,GameObject firstsel,AudioClip bgm = null,CancellationToken ct = default)
    {
        if(isTalking) return;
        this.messages = messages;
        this.names = names;
        this.image = image;
        SelectCanvas = canvas;
        selection = sel;
        firstSelect = firstsel;
        selectionBGM = bgm;
        await SelectionMessages(ct);
    }
}

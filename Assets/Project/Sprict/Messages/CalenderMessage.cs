using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CalenderMessage : MonoBehaviour
{
    // �b�����������Ƀ��b�Z�[�W�E�B���h�E��\���B�E�B���h�E����\���ɂȂ�����ɃJ�����_�[���A�N�e�B�u�ɂ���
    // �b�������ĉ摜���o�Ă��āA�摜���o�Ă�Ƃ��̓^�C���X�P�[�����O�ɂ���B�����ăG���^�[�������ƃe�L�X�g
    // ���b�Z�[�W���o�Ă��āA�\�����I�������摜�������ă^�C���X�P�[�������ɖ߂��B

    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<Sprite> image;
    [SerializeField]
    private List<string> seiitirouMessages;
    [SerializeField]
    private List<string> seiitirouNames;
    [SerializeField]
    private List<Sprite> seiitirouImages;
    public Canvas window;
    public Text target;
    public Text nameText;
    public Canvas calCanvas;
    public Image characterImage;
    public Image calender;
    public Image seiCalender;
    public Image TVScreen;
    public GameObject firstSelect;
    public GameObject seiFirstSelect;
    private IEnumerator coroutine;
    public bool messageSwitch = false;
    private bool isContacted = false;
    private bool seiIsContacted = false;
    public bool talked = false;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag.Equals("Player"))
        {
            isContacted = true;
        }
        else if(collider.gameObject.tag.Equals("Seiitirou"))
        {
            seiIsContacted = true;
        }

    }
    // ���̏�Ԃ��ƕ�����摜�͏o��B�ł��摜������Ȃ��Ȃ��Ă��܂��B�㓮���Ȃ�
    //�@�摜��������Ă��������x�摜���o�����߂�
    private void OnTriggerExit2D(Collider2D collider)
    {
        messageSwitch = false;
        if (collider.gameObject.tag.Equals("Player"))
        {
            isContacted = false;
            if (calCanvas.gameObject.activeSelf)
            {
                calender.gameObject.SetActive(false);
                calCanvas.gameObject.SetActive(false);
            }
            if (TVScreen == null)
            {
                return;
            }
            if (TVScreen.gameObject.activeSelf)
            {
                TVScreen.gameObject.SetActive(false);
                calCanvas.gameObject.SetActive(false);
            }
        }
        else if(collider.gameObject.tag.Equals("Seiitirou"))
        {
            seiIsContacted = false;
            if(seiCalender == null) return;
            else if(calCanvas.gameObject.activeSelf)
            {
                calender.gameObject.SetActive(false);
                calCanvas.gameObject.SetActive(false);
            }
            if(TVScreen == null)
            {
                return;
            }
        }
    }

    public IEnumerator CreateCoroutine()
    {
        // window�N��
        window.gameObject.SetActive(true);

        // ���ۃ��\�b�h�Ăяo�� �ڍׂ͎q�N���X�Ŏ���
        yield return OnAction();
        // window�I��
        target.text = "";
        GameManager.m_instance.ImageErase(characterImage);
        window.gameObject.SetActive(false);
        Homing.m_instance.speed = 2 + GameManager.m_instance.difficultyLevelManager.addEnemySpeed;
        SecondHouseManager.secondHouse_instance.ajure.speed = SecondHouseManager.secondHouse_instance.ajure.savedSpeed;
        SecondHouseManager.secondHouse_instance.ajure.acceleration = SecondHouseManager.secondHouse_instance.ajure.savedAcceleration;

        StopCoroutine(coroutine);
        coroutine = null;
        if(!talked) talked = true;
        GameManager.m_instance.stopSwitch = false;
    }

    protected void showMessage(string message, string name, Sprite image)
    {
        target.text = message;
        nameText.text = name;
        characterImage.sprite = image;
    }

    IEnumerator OnAction()
    {

        for(int i = 0; i < messages.Count; ++i)
        {
            yield return null;
            showMessage(messages[i], names[i], image[i]);
            yield return new WaitUntil(() => (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)));
        }
        calCanvas.gameObject.SetActive(true);
        calender.gameObject.SetActive(true);
        if(firstSelect == null)yield break;
        EventSystem.current.SetSelectedGameObject(firstSelect);
        yield break;

    }
    private async UniTask ActiveSeiitirouCalender()
    {
        GameManager.m_instance.stopSwitch = true;
        await MessageManager.message_instance.MessageWindowActive(seiitirouMessages,seiitirouNames,seiitirouImages ,ct: destroyCancellationToken);
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
        calCanvas.gameObject.SetActive(true);
        seiCalender.gameObject.SetActive(true);
        if(seiFirstSelect == null)
        {
            await UniTask.WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
            seiCalender.gameObject.SetActive(false);
            calCanvas.gameObject.SetActive(false);
            GameManager.m_instance.stopSwitch = false;
            return;
        }
        EventSystem.current.SetSelectedGameObject(seiFirstSelect);
        await UniTask.WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
        seiCalender.gameObject.SetActive(false);
        calCanvas.gameObject.SetActive(false);
        if(!talked) talked = true;
        GameManager.m_instance.stopSwitch = false;
    }
    private void Update()
    {
        if (isContacted && messageSwitch == false && (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)))
        {
            messageSwitch = true;
            GameManager.m_instance.stopSwitch = true;
            coroutine = CreateCoroutine();
            StartCoroutine(coroutine);
        }
        else if(seiIsContacted && messageSwitch == false && seiCalender != null && (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)))
        {
            messageSwitch = true;
            GameManager.m_instance.stopSwitch = true;
            ActiveSeiitirouCalender().Forget();
        }
        else if(seiIsContacted && messageSwitch == false && seiCalender == null && (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)))
        {
            messageSwitch = true;
            return;
        }
        if (calender.gameObject.activeSelf)
        {
            GameManager.m_instance.stopSwitch = true;
            Time.timeScale = 0.0f;
            if(Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return))
            {
                calender.gameObject.SetActive(false);
                calCanvas.gameObject.SetActive(false);
                Time.timeScale = 1.0f;
                Homing.m_instance.speed = 2 + GameManager.m_instance.difficultyLevelManager.addEnemySpeed;
                SecondHouseManager.secondHouse_instance.ajure.speed = SecondHouseManager.secondHouse_instance.ajure.savedSpeed;
                SecondHouseManager.secondHouse_instance.ajure.acceleration = SecondHouseManager.secondHouse_instance.ajure.savedAcceleration;
                GameManager.m_instance.stopSwitch = false;
            }
        }
    }
}

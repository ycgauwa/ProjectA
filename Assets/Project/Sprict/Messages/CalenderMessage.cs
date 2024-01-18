using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    private List<string> messages2;
    [SerializeField]
    private List<string> names2;
    [SerializeField]
    private List<Sprite> image2;
    public Canvas window;
    public Text target;
    public Text nameText;
    public Canvas calCanvas;
    public Image characterImage;
    public Image calender;
    public Image TVScreen;
    private IEnumerator coroutine;
    public bool messageSwitch = false;
    private bool isContacted = false;

    /*private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                PlayerManager.m_instance.m_speed = 0;
                coroutine = CreateCoroutine();
                StartCoroutine(coroutine);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
        {
            if(calCanvas.gameObject.activeSelf)
            {
                calender.gameObject.SetActive(false);
                calCanvas.gameObject.SetActive(false);
            }
            if(TVScreen == null)
            {
                return;
            }
            if(TVScreen.gameObject.activeSelf)
            {
                TVScreen.gameObject.SetActive(false);
            }
        }
    }*/
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag.Equals("Player"))
        {
            isContacted = true;
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
        window.gameObject.SetActive(false);

        StopCoroutine(coroutine);
        coroutine = null;
        PlayerManager.m_instance.m_speed = 0.075f;
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
            // 1�t���[���� ������ҋ@(���L����1)
            yield return null;

            // ��b��window��text�t�B�[���h�ɕ\��
            showMessage(messages[i], names[i], image[i]);

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        }
        calCanvas.gameObject.SetActive(true);
        calender.gameObject.SetActive(true);
        yield break;

    }
    private void Update()
    {
        if (isContacted && messageSwitch == false && Input.GetKeyDown(KeyCode.Return))
        {
            messageSwitch = true;
            PlayerManager.m_instance.m_speed = 0;
            coroutine = CreateCoroutine();
            StartCoroutine(coroutine);
        }
        if (calender.gameObject.activeSelf)
        {
            Time.timeScale = 0.0f;
            if(Input.GetKeyDown(KeyCode.Return))
            {
                calender.gameObject.SetActive(false);
                Time.timeScale = 1.0f;
            }
        }
    }
}

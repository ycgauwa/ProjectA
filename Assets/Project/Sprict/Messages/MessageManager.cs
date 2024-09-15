using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public Text target;
    public Text nameText;
    public Image characterImage;
    public static MessageManager message_instance;
    private Coroutine coroutine;
    public static bool talking = false;

    //�����Ń��b�Z�[�W�X�N���v�g���Ăяo���X�N���v�g���쐬����
    void Start()
    {
        message_instance = this;
    }
    public IEnumerator MessageCoroutine()
    {
        //if���ŃR���[�`�������邩�ǂ����̏����������
        //���b�Z�[�W���������bool�����iIEnumerator���Łj
        // window�N��
        window.gameObject.SetActive(true);

        // ���ۃ��\�b�h�Ăяo�� �ڍׂ͎q�N���X�Ŏ���
        yield return OnAction();

        // window�I��
        target.text = "";
        window.gameObject.SetActive(false);
        Test1.messageSwitch = false;
        ItemGet2.messageSwitch = false;
        NotEnterLadder.messageSwitch = false;
        Case1Object.messageSwitch = false;
        Refrigerator.messageSwitch = false;

        PlayerManager.m_instance.m_speed = 0.075f;
        Homing.m_instance.speed = 2;
        Time.timeScale = 1f;
        talking = false;
        //StopCoroutine(coroutine);
        coroutine = null;
    }
    protected void showMessage(string message, string name ,Sprite image)
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

            // �L�[���͂�ҋ@ (���L����1)
            yield return new WaitUntil(() => (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)));
        }

        yield break;

    }
    /*�i�j�̒��Ɉ���������邻�̈����̒��g��Test1.cs���n���Ă��Ă���
    �󂯎�鑤�ł�List<string>�܂Ō^��������*/
    public void MessageWindowActive(List<string>messages,List<string>names, List<Sprite> image = null, AudioClip sound = null )
    {
        //�����ŃR���[�`���������Ȃ����̏���
        if(coroutine != null)
        {
            StopCoroutine(coroutine); 
        }
        this.messages = messages;
        this.names = names;
        this.image = image;
        coroutine = StartCoroutine(MessageCoroutine());
        if(sound != null)
        {
            GetComponent<AudioSource>().PlayOneShot(sound);
        }
    }
    /*�^��_�܂Ƃ�
    �P�󂯎�鑤�̈����̖��O��msg��nam�����ǂ���ǂ�����Ƃ��Ă��Ă�́H
    �������܂ł����x���B�A�h���X�����锠�̂悤�Ȃ��̂Ŗ��O�͊֌W�Ȃ������o�[�ϐ����������ꂽ���_�ŃA�h���X�����܂��B
    �����Test.cs�œn���͕̂�����ł͂Ȃ��A�h���X�̂݁BTest2��Test3���o�Ă��Ă��󂯎�鑤�̈����͔��Ȃ̂ł��̂܂܂ŗǂ�
    �QTest1�̃X�N���v�g��Debug.Log���n�܂����u�ԂQ��Ăяo�����B
    �R��������Start�֐�����Ȃ��̂Ɏn�܂����u�ԌĂ΂��̂Ȃ��H
    �����������^�C���}�b�v�̓����蔻�肪�Q�d�Ȃ��Ă������ߎn�܂����u�ԂɂQ��Ă΂�Ă��܂�
    ToEvent1�̊֐��ŌĂ΂�Ȃ������̂͒P���ɓ����蔻�肪�Ȃ���������B
    �STest.cs������Ă���PlayerMessage�̋@�\�����Ȃ��Ȃ���
    ���Ȃ���������Ȃ����ǃZ���t�����ׂď����Ă��B�ϐ�����ς���ƃv���O���������ǐՂł��Ȃ��Ȃ�I
    �TPlayeraMessage��start�֐����Q�񔽉����Ă���
    �U�T�ԂɊւ���start�֐��łȂ����ׂĂQ��ƂȂ��Ă���B�����炭PlayeraMessage�Ɖ����������N�����ĂQ�񂸂����`�ɂȂ��Ă��܂��Ă���B
    ���T�ƂU�͒P����PlayerMessage��2���邩��2��Ă΂�Ă��邾��*/
}

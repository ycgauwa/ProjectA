using System.Collections;
using System.Collections.Generic;
//using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class DiaryMessage : MonoBehaviour
{
    //��肽������
    //���ׂă{�^���������烁�b�Z�[�W���\�������B���̂��Ƃɓ��L���\������Ă��烁�b�Z�[�W���o��B
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<string> sentences;
    [SerializeField]
    private List<string> dates;
    [SerializeField]
    private List<Sprite> image;
    public Canvas diaryWindow;
    public Canvas window;
    public Text target;
    public Text nameText;
    public Text sentence;
    public Text date;
    public Image characterImage;
    public Image diary;
    public Image panel;
    private IEnumerator coroutine;
    private bool isContacted = false;
    public Homing homing;
    public SoundManager soundManager;
    public PlayerManager playerManager;
    public AudioClip pageSound;
    public AudioClip pageTojiSound;
    private void Start()
    {

    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
        {
            isContacted = true;
        }
    }

    // collider�����I�u�W�F�N�g�̗̈�O�ɂł��Ƃ�(���L�Ő���1)
    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
        {
            isContacted = false;
        }
    }
    private void Update()
    {
        if(isContacted == true && coroutine == null && diaryWindow.gameObject.activeInHierarchy == false)
        {
            //�b��������(�����͓��I�Ȃ��̂ƍ����bool�̂悤�ɍP��I�Ȃ��̂ŕ�������������)
            if(Input.GetKeyDown(KeyCode.Return))
            {
                coroutine = WindowAction();
                PlayerManager.m_instance.m_speed = 0;
                // �R���[�`���̋N��(���L����2)
                StartCoroutine(coroutine);
            }
        }
    }
    protected void showMessage(string message, string name)
    {
        target.text = message;
        nameText.text = name;
    }
    protected void showDiaryMessage(string message, string name)
    {
        sentence.text = message;
        date.text = name;
    }
    IEnumerator WindowAction()
    {
        //�b��������Ƃ܂��e�L�X�g�ŕ\���B
        window.gameObject.SetActive(true);
        for(int i = 0; i < messages.Count; ++i)
        {
            // 1�t���[���� ������ҋ@(���L����1)
            yield return null;
            // ��b��window��text�t�B�[���h�ɕ\��
            showMessage(messages[i], names[i]);
            yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
        }
        window.gameObject.SetActive(false);
        
        //���񂩉����ƃe�L�X�g�������āA���L�̕\���������B
        diaryWindow.gameObject.SetActive(true);
        
        for(int i = 0; i < sentences.Count; ++i)
        {
            playerManager.playerstate = PlayerManager.PlayerState.Talk;
            // 1�t���[���� ������ҋ@(���L����1)
            yield return null;
            // ��b��window��text�t�B�[���h�ɕ\��
            showDiaryMessage(sentences[i], dates[i]);
            soundManager.PlaySe(pageSound);
            yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
            if (i == sentences.Count - 1)
            {
                soundManager.PlaySe(pageTojiSound);
            }

        }
        diaryWindow.gameObject.SetActive(false);
        playerManager.playerstate = PlayerManager.PlayerState.Idol;
        coroutine = null;
        target.text = "";
        homing.speed = 2;
        yield break;
    }
}

using System.Collections;
using System.Collections.Generic;
//using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class DiaryMessage : MonoBehaviour
{
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<string> sentences;
    [SerializeField]
    private List<string> dates;
    public Canvas diaryWindow;
    public Canvas window;
    public Text target;
    public Text nameText;
    public Text sentence;
    public Text date;
    private IEnumerator coroutine;
    private bool isContacted = false;
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
        //�b��������(�����͓��I�Ȃ��̂ƍ����bool�̂悤�ɍP��I�Ȃ��̂ŕ�������������)
        if(Input.GetKeyDown(KeyCode.Return))
        {
            if(isContacted == true && coroutine == null && diaryWindow.gameObject.activeInHierarchy == false)
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
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        }
        window.gameObject.SetActive(false);
        
        //���񂩉����ƃe�L�X�g�������āA���L�̕\���������B
        diaryWindow.gameObject.SetActive(true);
        for(int i = 0; i < sentences.Count; ++i)
        {
            // 1�t���[���� ������ҋ@(���L����1)
            yield return null;
            // ��b��window��text�t�B�[���h�ɕ\��
            showDiaryMessage(sentences[i], dates[i]);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        }
        diaryWindow.gameObject.SetActive(false);
        PlayerManager.m_instance.m_speed = 0.075f;
        coroutine = null;
        yield break;
    }
}

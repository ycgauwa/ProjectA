using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ToMiniGame1 : MonoBehaviour
{
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<string> messages2;
    [SerializeField]
    private List<string> names2;
    [SerializeField]
    private List<string> messages3;
    [SerializeField]
    private List<string> names3;
    public Canvas window;
    public Canvas Selectwindow;
    public Text target;
    public Text nameText;
    public Button yes;
    public Button no;
    private IEnumerator coroutine;

    //�A���T�[�Ƃ��ĉ��𓚂�����
    private bool answer;

    private bool isOpenSelect = false;
    private bool isContacted = false;

    // Start is called before the first frame update
    void Start()
    {
        answer = false;
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
        //�b��������(�����͓��I�Ȃ��̂ƍ����bool�̂悤�ɍP��I�Ȃ��̂ŕ�������������)
        if(Input.GetKeyDown(KeyCode.Return))
        {
            if(isContacted == true && coroutine == null)
            {
                    coroutine = OnAction();
                    PlayerManager.m_instance.m_speed = 0;
                    // �R���[�`���̋N��(���L����2)
                    StartCoroutine(coroutine);

            }
        }

    }
    protected void showMessage(string message, string name)
    {
        this.target.text = message;
        this.nameText.text = name;
    }
    IEnumerator OnAction()
    {
        window.gameObject.SetActive(true);
        Debug.Log("OnAction");
        for(int i = 0; i < messages.Count; ++i)
        {
            // 1�t���[���� ������ҋ@(���L����1)
            yield return null;
            // ��b��window��text�t�B�[���h�ɕ\��
            showMessage(messages[i], names[i]);
            if(i == messages.Count - 1)
            {
                Selectwindow.gameObject.SetActive(true);
                isOpenSelect = true;
                break;
            }
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        }
        yield return new WaitUntil(() => !isOpenSelect);
        //�͂���I��
        if(answer == true)
        {
            for(int i = 0; i < messages2.Count; ++i)
            {
                yield return null;
                //���肷���\��
                showMessage(messages2[i], names2[i]);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
            }
            SceneManager.LoadScene("MiniGame1");
        }
        else
        {
            for(int i = 0; i < messages3.Count; ++i)
            {
                yield return null;
                //���肵�ĂȂ�
                showMessage(messages3[i], names3[i]);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
            }
        }

        window.gameObject.SetActive(false);
        PlayerManager.m_instance.m_speed = 0.075f;
        coroutine = null;
        yield break;

    }
    public void SelectAnswerYes()
    {
        answer = true;
        Selectwindow.gameObject.SetActive(false);
        isOpenSelect = false;
    }
    public void SelectAnswerNo()
    {
        answer = false;
        Selectwindow.gameObject.SetActive(false);
        isOpenSelect = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class Choice1 : MonoBehaviour
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
    public Canvas window;
    public Canvas Selectwindow;
    public Text target;
    public Text nameText;
    public Image characterImage;
    public Image selection;
    public Button yes;
    public Button no;
    private IEnumerator coroutine;
    public Inventry inventry;
    public Item item;
    private IEnumerator coroutine2;
    
    //�A���T�[�Ƃ��ĉ��𓚂�����
    private bool answer;
    //�A�C�e�������ɓ��肵�Ă��邩�ǂ���
    private bool isGet;
    private bool isOpenSelect = false;
    private bool isContacted = false;

    // Start is called before the first frame update
    void Start()
    {
        answer = false;
        isGet = false;
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
        if(Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return))
        {
            if(isContacted == true && coroutine == null)
            {
                if(isGet)
                {
                    coroutine = OnAction2();
                    PlayerManager.m_instance.m_speed = 0;
                    StartCoroutine(coroutine);
                }
                else
                {
                    coroutine = OnAction();
                    PlayerManager.m_instance.m_speed = 0;
                    // �R���[�`���̋N��(���L����2)
                    StartCoroutine(coroutine);
                }
            }
        }
    }
    protected void showMessage(string message, string name, Sprite image)
    {
        this.target.text = message;
        this.nameText.text = name;
        characterImage.sprite = image;
    }
    IEnumerator OnAction()
    {
        window.gameObject.SetActive(true);
        for(int i = 0; i < messages.Count; ++i)
        {
            // 1�t���[���� ������ҋ@(���L����1)
            yield return null;
            // ��b��window��text�t�B�[���h�ɕ\��
            showMessage(messages[i], names[i], image[i]);
            if(i == messages.Count - 1)
            {
                Selectwindow.gameObject.SetActive(true);
                selection.gameObject.SetActive(true);
                isOpenSelect = true;
                break;
            }
            yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
        }
        yield return new WaitUntil(() => !isOpenSelect );
        //�͂���I��
        if(answer == true)
        {
            for(int i = 0; i < messages2.Count; ++i)
            {
                yield return null;
                //���肷���\��
                showMessage(messages2[i], names2[i], image2[i]);
                isGet = true;
                yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
            }
            inventry.Add(item);
        }
        else
        {
            for(int i = 0; i < messages3.Count; ++i)
            {
                yield return null;
                //���肵�ĂȂ�
                showMessage(messages3[i], names3[i], image3[i]);
                yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
            }
        }
        
        window.gameObject.SetActive(false);
        PlayerManager.m_instance.m_speed = 0.075f;
        coroutine = null;
        yield break;

    }
    IEnumerator OnAction2()
    {
        window.gameObject.SetActive(true);

        for(int i = 0; i < messages4.Count; ++i)
        {
            // 1�t���[���� ������ҋ@(���L����1)
            yield return null;

            //���łɂ����Ă�
            showMessage(messages4[i], names4[i], image4[i]);


            // �L�[���͂�ҋ@ (���L����1)
            yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
        }
        window.gameObject.SetActive(false);
        PlayerManager.m_instance.m_speed = 0.075f;
        coroutine = null;
        yield break;
    }
    public void SelectAnswerYes()
    {
        answer = true;
        selection.gameObject.SetActive(false);
        Selectwindow.gameObject.SetActive(false);
        isOpenSelect = false;
    }
    public void SelectAnswerNo()
    {
        answer = false;
        selection.gameObject.SetActive(false);
        Selectwindow.gameObject.SetActive(false);
        isOpenSelect = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class NotEnter1 : MonoBehaviour
{
    /*�����𖞂������ɐG���ƃ��b�Z�[�WA���o�ē����Ȃ��Ȃ�d�g�݂̍쐬
    ToEvent�N���X��one�ϐ��̏����ɂ���Ă������̃N���X�̃��\�b�h�������������Ȃ��������
    TP����O�ɃE�B���h�E�̕\�������W�̌Œ������B
    �ϐ��̈����n���͈������g���čs��ToEvent��NotEnter�œn��*/
    public bool one;
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    public Canvas window;
    public Text target;
    public Text nameText;
    private IEnumerator coroutine;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        one = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //�C�x���g�Ȃ��ɂ͒ʂ�Ȃ��d�g��
        //false�̎����b�Z�[�W�E�B���h�E�̕\��
        //����厖�B�������O�ł����Ă����L�̂悤�Ȃ����ő���\
        this.one = ToEvent1.one;
        if(one == false)
        {
            PlayerManager.m_instance.Event1();
            coroutine = CreateCoroutine();
            // �R���[�`���̋N��(���L����2)
            StartCoroutine(coroutine);
        }
        if(one == true)
        {
            player.transform.position = new Vector2(-10, -102);
        }

    }
    public IEnumerator CreateCoroutine()
    {
        // window�N��
        window.gameObject.SetActive(true);

        // ���ۃ��\�b�h�Ăяo�� �ڍׂ͎q�N���X�Ŏ���
        yield return OnAction();

        // window�I��
        this.target.text = "";
        this.window.gameObject.SetActive(false);

        StopCoroutine(coroutine);
        coroutine = null;
        PlayerManager.m_instance.m_speed = 0.05f;


    }
    protected void showMessage(string message, string name)
    {
        this.target.text = message;
        this.nameText.text = name;
    }

    IEnumerator OnAction()
    {

        for(int i = 0; i < messages.Count; ++i)
        {
            // 1�t���[���� ������ҋ@(���L����1)
            yield return null;

            // ��b��window��text�t�B�[���h�ɕ\��
            showMessage(messages[i], names[i]);


            // �L�[���͂�ҋ@ (���L����1)
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        }

        yield break;

    }
}

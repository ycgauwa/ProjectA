using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotEnter2 : MonoBehaviour
{
    // ����͒P���ɂǂ�ȏ����ł����Ă��G���A�ɓ������烁�b�Z�[�W�E�B���h�E���o�Ă���d�g�݂ɂ���B

    /*�����𖞂������ɐG���ƃ��b�Z�[�WA���o�ē����Ȃ��Ȃ�d�g�݂̍쐬
    ToEvent�N���X��one�ϐ��̏����ɂ���Ă������̃N���X�̃��\�b�h�������������Ȃ��������
    TP����O�ɃE�B���h�E�̕\�������W�̌Œ������B
    �ϐ��̈����n���͈������g���čs��ToEvent��NotEnter�œn��*/
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
        {
            PlayerManager.m_instance.m_speed = 0;
            coroutine = CreateCoroutine();
            StartCoroutine(coroutine);
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
        PlayerManager.m_instance.m_speed = 0.075f;


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

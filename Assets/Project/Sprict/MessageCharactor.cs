using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * �t�B�[���h�I�u�W�F�N�g�̊�{����
 */
public abstract class FieldObjectBase : MonoBehaviour
{

    // Unity�̃C���X�y�N�^(UI��)�ŁA�O���ł������I�u�W�F�N�g���o�C���h����B
    // �i���� : �C���X�y�N�^��script��ǉ����āA�ݒ������ �Ő����j
    public Canvas window;
    public Text target;
    public Text charaname;

    // �ڐG����
    private bool isContacted = false;
    private IEnumerator coroutine;


    // collider�����I�u�W�F�N�g�̗̈�ɓ������Ƃ�(���L�Ő���1)
    private void OnTriggerEnter2D(Collider2D collider)
    {
        isContacted = collider.gameObject.tag.Equals("Player");
    }

    // collider�����I�u�W�F�N�g�̗̈�O�ɂł��Ƃ�(���L�Ő���1)
    private void OnTriggerExit2D(Collider2D collider)
    {
        isContacted = !collider.gameObject.tag.Equals("Player");
    }

    private void Update()
    {
        if (isContacted && coroutine == null && Input.GetButton("Submit") && Input.GetKeyDown(KeyCode.Return))
        {
            coroutine = CreateCoroutine();
            PlayerManager.m_instance.m_speed = 0;
            // �R���[�`���̋N��(���L����2)
            StartCoroutine(coroutine);
        }
    }

    /**
     * ���A�N�V�����p�R���[�`��(���L�Ő���2)
     */
    private IEnumerator CreateCoroutine()
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

    protected abstract IEnumerator OnAction();

    /**
     * ���b�Z�[�W��\������
     */
    protected void showMessage(string message,string name)
    {
        this.target.text = message;
        this.charaname.text = name;
    }
}
public class MessageCharactor : FieldObjectBase
{

    // �Z���t : Unity�̃C���X�y�N�^(UI��)�ŉ�b�����`���� 
    // �i���� : �C���X�y�N�^��script��ǉ����āA�ݒ������ �Ő����j
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> chara;

    // �e�N���X����Ă΂��R�[���o�b�N���\�b�h (�ڐG & �{�^���������Ƃ��Ɏ��s)
    protected override IEnumerator OnAction()
    {

        for (int i = 0; i < messages.Count; ++i)
        {
            // 1�t���[���� ������ҋ@(���L����1)
            yield return null;

            // ��b��window��text�t�B�[���h�ɕ\��
            showMessage(messages[i], chara[i]);

            // �L�[���͂�ҋ@ (���L����1)
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        }

        yield break;
    }
}
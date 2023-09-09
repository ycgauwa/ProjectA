using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMessage : MonoBehaviour
{
    [SerializeField]
    private List<string> messages;
    public Canvas window;
    public Text target;
    private IEnumerator coroutine;
    public static PlayerMessage instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        coroutine = CreateCoroutine();
        PlayerManager.m_instance.Event1();
        // �R���[�`���̋N��(���L����2)
        StartCoroutine(coroutine);
    }

    // Update is called once per frame
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
    private void FixedUpdate()
    {
        
    }
    protected void showMessage(string message)
    {
        this.target.text = message;
    }
    IEnumerator OnAction()
    {

        for (int i = 0; i < messages.Count; ++i)
        {
            // 1�t���[���� ������ҋ@(���L����1)
            yield return null;

            // ��b��window��text�t�B�[���h�ɕ\��
            showMessage(messages[i]);

            // �L�[���͂�ҋ@ (���L����1)
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        }

        yield break;

    }
    // Update is called once per frame
    void Update()
    {

    }
}

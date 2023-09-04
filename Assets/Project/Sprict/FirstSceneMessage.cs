using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField]
    private List<string> messages;
    public Canvas window;
    public Text target;
    private IEnumerator coroutine;


    // Start is called before the first frame update
    void Start()
    {
        
    }
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
        SceneManager.LoadScene("Game");

    }
    private void FixedUpdate()
    {
        if (coroutine == null && Input.GetButton("Submit") && Input.anyKeyDown)
        {
            coroutine = CreateCoroutine();
            // �R���[�`���̋N��(���L����2)
            StartCoroutine(coroutine);
        }
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
            yield return new WaitUntil(() => Input.anyKeyDown);
        }

        yield break;

    }
        // Update is called once per frame
    void Update()
    {
        
    }
}

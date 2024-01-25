using System.Collections;
using System.Collections.Generic;
//using UnityEditor.PackageManager.UI;
//using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMessage : MonoBehaviour
{
    [SerializeField]
    private List<string> Messages;
    public Canvas window;
    public Text target;
    private IEnumerator playercoroutine;
    public static PlayerMessage instance;
    public bool StartActive;
    public bool firstActive;
    public Canvas Demo;
    public Image DemoImage;
    public Image DemoPanel;



    // Start is called before the first frame update
    void Start()
    {
        firstActive = true;
        instance = this;
        playercoroutine = CreateCoroutine();
        PlayerManager.m_instance.m_speed = 0; ;
        // �R���[�`���̋N��(���L����2)
        StartCoroutine(playercoroutine);
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

        StopCoroutine(playercoroutine);
        playercoroutine = null;
        StartActive = true;
        PlayerManager.m_instance.m_speed = 0.075f;
        


    }
    protected void showMessage(string message)
    {
        this.target.text = message;
    }
    IEnumerator OnAction()
    {
        Debug.Log("PlayerMessage.OnAction");
        for (int i = 0; i < Messages.Count; ++i)
        {
            // 1�t���[���� ������ҋ@(���L����1)
            yield return null;

            // ��b��window��text�t�B�[���h�ɕ\��
            showMessage(Messages[i]);

            // �L�[���͂�ҋ@ (���L����1)
            yield return new WaitUntil(() => (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)));
        }

        yield break;

    }
    private void Update()
    {
        if (StartActive && firstActive == true)
        {
            DemoImage.gameObject.SetActive(true);
            DemoPanel.gameObject.SetActive(true);
        }
        if (DemoImage.gameObject.activeSelf)
        {
            if (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return))
            {
                DemoImage.gameObject.SetActive(false);
                DemoPanel.gameObject.SetActive(false);
                firstActive = false;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SafeMessage : MonoBehaviour
{
    //�@�߂Â��ăG���^�[�����ƃ��b�Z�[�W�E�B���h�E���\��
    //�@���b�Z�[�W���\���d�؂��

    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<Sprite> image;
    public Canvas window;
    public Text target;
    public Text nameText;
    public Canvas calCanvas;
    public Image characterImage;
    public Image calender;
    private IEnumerator coroutine;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
        {
            PlayerManager.m_instance.m_speed = 0;
            coroutine = CreateCoroutine();
            StartCoroutine(coroutine);
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
        {
            if(calCanvas.gameObject.activeSelf)
            {
                calender.gameObject.SetActive(false);
                calCanvas.gameObject.SetActive(false);
            }
        }
    }
    public IEnumerator CreateCoroutine()
    {
        // window�N��
        window.gameObject.SetActive(true);

        // ���ۃ��\�b�h�Ăяo�� �ڍׂ͎q�N���X�Ŏ���
        yield return OnAction();

        // window�I��
        target.text = "";
        window.gameObject.SetActive(false);

        StopCoroutine(coroutine);
        coroutine = null;
        PlayerManager.m_instance.m_speed = 0.075f;

    }

    protected void showMessage(string message, string name, Sprite image)
    {

        target.text = message;
        nameText.text = name;
        characterImage.sprite = image;

    }

    IEnumerator OnAction()
    {

        for(int i = 0; i < messages.Count; ++i)
        {
            // 1�t���[���� ������ҋ@(���L����1)
            yield return null;

            // ��b��window��text�t�B�[���h�ɕ\��
            showMessage(messages[i], names[i], image[i]);

            yield return new WaitUntil(() => (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)));
        }
        calCanvas.gameObject.SetActive(true);
        calender.gameObject.SetActive(true);
        yield break;

    }
}

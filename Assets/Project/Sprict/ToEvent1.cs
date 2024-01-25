using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ToEvent1 : MonoBehaviour
{
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<Sprite> images;
    public Canvas window;
    public Text target;
    public Text nameText;
    public Image Chara;
    public static bool one;
    public GameObject player;
    private IEnumerator coroutine;
    // Start is called before the first frame update
    void Start()
    {
        one = false;
    }

    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"colloder: {other.gameObject.name} ");

        //��񂵂��쓮���Ȃ����߂̎d�g��
        if (!one)
        {
            StartCoroutine(Event1());
            one = true;
        }
       
    }
    //�C�x���g�P�̂��߂̃R���[�`���B��g�̖������ʂ����Ă����B
    IEnumerator Event1()
    {
        
        yield return new WaitForSeconds(1);
        player.transform.position = new Vector3(-33, -34, 0);
        //�v���C���[�̌Œ�i���͕s���̂��ߕʂ̃N���X�̃��\�b�h���Ăяo���Ă���j
        //PlayerManager.m_instance.Event1();
        PlayerManager.m_instance.m_speed = 0;
        coroutine = CreateCoroutine();
        // �R���[�`���̋N��(���L����2)
        StartCoroutine(coroutine);
        
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
        Debug.Log("hhh");
        PlayerManager.m_instance.m_speed = 0.075f;


    }
    protected void showMessage(string message,string name, Sprite image)
    {
        this.target.text = message;
        this.nameText.text = name;
        Chara.sprite = image;
    }

    IEnumerator OnAction()
    {

        for(int i = 0; i < messages.Count; ++i)
        {
            // 1�t���[���� ������ҋ@(���L����1)
            yield return null;

            // ��b��window��text�t�B�[���h�ɕ\��
            showMessage(messages[i], names[i], images[i]);


            // �L�[���͂�ҋ@ (���L����1)
            yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
        }

        yield break;

    }
    
}

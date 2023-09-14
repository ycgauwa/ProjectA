using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test1 : MonoBehaviour
{
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    public Canvas window;
    public Text target;
    public Text nameText;
    // Start is called before the first frame update
    private void Awake()
    {
        Debug.Log("Awake");
    }
    void Start()
    {
        Debug.Log("TestStart");
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Debug.Log($"colloder: {collider.gameObject.name} ");
            /*�i�j�̒��Ɉ��������Ă�����Ǝ��s���̃��\�b�h���n�����ϐ��ŏ������s���Ă����B
            �������A�f�[�^��n�����͕ϐ������ł悢*/
            MessageManager.message_instance.MessageWindowActive(messages, names);
        }
       
    }
}

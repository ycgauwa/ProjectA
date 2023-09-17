using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToEvent2 : MonoBehaviour
{
    //�I����ʂ��o�ĉ�b���n�܂葾�ۂ̉��𗬂��B���̌㔒�����̉��o���o����ɒN�����Ȃ��Ȃ�
    //���W�Œ肵���J�������ړ����āi�������ڂɁj��l��NPC�������ɓ��葾�ۂ̑O�܂ōs��
    //�Z���t��b������ɂ܂��J�������ړ��A�x�����Ə������f���p�Ɉړ������ŉ�b���I�������
    //���̎q���G�t�F�N�g���o���ď��ŁB�x�����̃Z���t�������Ă���I���B
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    public Canvas window;
    public Text target;
    public Text nameText;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        isContacted = collider.gameObject.tag.Equals("Player");
    }

    // collider�����I�u�W�F�N�g�̗̈�O�ɂł��Ƃ�(���L�Ő���1)
    private void OnTriggerExit2D(Collider2D collider)
    {
        isContacted = !collider.gameObject.tag.Equals("Player");
    }
    private bool isContacted = false;

    private void FixedUpdate()
    {
        if(isContacted && Input.GetButton("Submit") && Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log(MessageManager.message_instance);
            Debug.Log(messages);
            Debug.Log(names);
            MessageManager.message_instance.MessageWindowActive(messages, names);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

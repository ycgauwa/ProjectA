using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemGet : MonoBehaviour
{�@/*�A�C�e�����Q�b�g���鎞�Ɏg���֐�
  �@���ׂ�ƃ��b�Z�[�W�E�B���h�E�̕\��
    �����ăC���x���g���ɃA�C�e����˂�����*/
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
            MessageManager.message_instance.MessageWindowActive(messages, names);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using static UnityEditor.Progress;

public class ItemGet : MonoBehaviour
{�@/*�A�C�e�����Q�b�g���鎞�Ɏg���֐�
  �@���ׂ�ƃ��b�Z�[�W�E�B���h�E�̕\��
    �����ăC���x���g���ɃA�C�e����˂�����
    ��񂵂���b�ł��Ȃ��悤�ɂ���*/
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<Sprite> images;
    public Canvas window;
    public Text target;
    public Text nameText;
    public Inventry inventry;
    public Item item;
    private bool isContacted = false;
    public static bool messageSwitch = false;

    private void Start()
    {
        messageSwitch = false;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
        {
            isContacted = true;
        }

    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
        {
            isContacted = false;
        }
    }

    private void Update()
    {
        if(isContacted && messageSwitch == false && (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)))
        {
            messageSwitch = true;
            item.checkPossession = true;
            MessageManager.message_instance.MessageWindowActive(messages, names,images);
            inventry.Add(item);
        }
    }

}

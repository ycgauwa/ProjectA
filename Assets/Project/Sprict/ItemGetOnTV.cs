using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemGetOnTV : MonoBehaviour
{
    //�@�e���r�𒲂ׂ��ۂɓ������p��Window���o�Ă��ĉ�ʂɃA�N�V�������N�����Đ��������
    //�@���b�Z�[�W���\������ăA�C�e�������ł���B

    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<Sprite> images;
    [SerializeField]
    private List<string> messages2;
    [SerializeField]
    private List<string> names2;
    [SerializeField]
    private List<Sprite> images2;
    public Canvas window;
    public Text target;
    public Text nameText;
    public Inventry inventry;
    public Item item;
    private bool isContacted = false;
    public static bool messageSwitch = false;

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
        if(isContacted && messageSwitch == false && Input.GetKeyDown(KeyCode.Return))
        {
            messageSwitch = true;
            MessageManager.message_instance.MessageWindowActive(messages, names, images);
            inventry.Add(item);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ItemGet2 : MonoBehaviour
{
    // �A�C�e�����C���x���g���ɓ����Ă鎞�ɓ����悤�ȃ��\�b�h����肽��
    // �n���}�[����肵�Ă��炶��Ȃ��ƃA�C�e�������ł��Ȃ��悤�ɂ���������܂ł͓K���ȃ��b�Z�[�W��\�������Ă���
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
    [SerializeField]
    private List<string> messages3;
    [SerializeField]
    private List<string> names3;
    [SerializeField]
    private List<Sprite> images3;
    public Canvas window;
    public Text target;
    public Text nameText;
    public Inventry inventry;
    public Item hummer;
    public Item detergent;
    private bool isContacted = false;
    private bool hummerGeted = false;


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
        // �A�C�e������肷��O
        if(hummer.checkPossession == false)
        {
            if(isContacted && (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)))
                MessageManager.message_instance.MessageWindowActive(messages, names, images, ct: destroyCancellationToken).Forget();
        }
        //�@�A�C�e������肵������
        else if(hummer.checkPossession == true)
        {
            if(isContacted && hummerGeted == false && (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)))
            {
                hummerGeted = true;
                MessageManager.message_instance.MessageWindowActive(messages2, names2, images2, ct: destroyCancellationToken).Forget();
                inventry.Add(detergent);
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Device;
//using static UnityEditor.Progress;

public class ItemGet3 : MonoBehaviour
{
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
    private List<Sprite> image2;
    [SerializeField]
    private List<string> messages3;
    [SerializeField]
    private List<string> names3;
    [SerializeField]
    private List<Sprite> image3;
    public ToEvent4 toevent4;
    public Item item;
    public Inventry inventry;
    private bool isContacted = false;
    public bool messageSwitch = false;

    // 最初は話しかけてもメッセージが出るだけだが、イベント４のフラグを回収すると別のメッセージが流れてアイテムをゲットできる。

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
            isContacted = true;
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        messageSwitch = false;
        if(collider.gameObject.tag.Equals("Player"))
            isContacted = false;
    }
    private void Update()
    {
        if(isContacted && messageSwitch == false && (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)))
        {
            if(toevent4.event4flag == false)
            {
                messageSwitch = true;
                PlayerManager.m_instance.m_speed = 0;
                MessageManager.message_instance.MessageWindowActive(messages, names, images, ct: destroyCancellationToken).Forget();
            }
            else if(toevent4.event4flag == true && !ItemDateBase.itemDate_instance.GetItemId(253).geted)
            {
                messageSwitch = true;
                PlayerManager.m_instance.m_speed = 0;
                MessageManager.message_instance.MessageWindowActive(messages2, names2, image2, ct: destroyCancellationToken).Forget();
                inventry.Add(item);
            }
            //　もうここには用はない的なメッセージを書く
            else if(toevent4.event4flag == true && ItemDateBase.itemDate_instance.GetItemId(253).geted)
            {
                messageSwitch = true;
                MessageManager.message_instance.MessageWindowActive(messages3, names3, image3, ct: destroyCancellationToken).Forget();
            }
        }
    }
}

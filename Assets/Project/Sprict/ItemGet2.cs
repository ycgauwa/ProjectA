using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemGet2 : MonoBehaviour
{
    // アイテムがインベントリに入ってる時に動くようなメソッドを作りたい
    // ハンマーを入手してからじゃないとアイテムを入手できないようにしたいそれまでは適当なメッセージを表示させておく
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
    public Item hummer;
    public Item detergent;
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
        // アイテムを入手する前
        if(hummer.checkPossession == false)
        {
            if(isContacted && messageSwitch == false && Input.GetKeyDown(KeyCode.Return))
            {
                messageSwitch = true;
                MessageManager.message_instance.MessageWindowActive(messages, names, images);
            }
        }
        //　アイテムを入手したあと
        if(hummer.checkPossession == true)
        {
            if(isContacted && messageSwitch == false && Input.GetKeyDown(KeyCode.Return))
            {
                messageSwitch = true;
                MessageManager.message_instance.MessageWindowActive(messages2, names2, images2);
                inventry.Add(detergent);
            }
        }
        
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotEnter5 : MonoBehaviour
{
    //　民家１の２階の一個目の鍵付きのドア
    // 鍵がないと入れないドアのためのスクリプト
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<Sprite> images;
    public bool getKey2;
    public ItemDateBase itemDateBase;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(getKey2 == false)
        {
            if(collider.gameObject.tag.Equals("Player"))
            {
                MessageManager.message_instance.MessageWindowActive(messages, names, images);
            }
        }
        else if(getKey2 == true)
        {
            if(collider.gameObject.tag.Equals("Player"))
            {
                this.gameObject.tag = "Minnka1-20";
                itemDateBase.Items5Delete();
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotEnter4 : MonoBehaviour
{
    // 鍵がないと入れないドアのためのスクリプト
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<Sprite> images;
    private bool getKey1;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(getKey1 == false)
        {
            if(collider.gameObject.tag.Equals("Player"))
            {
                MessageManager.message_instance.MessageWindowActive(messages, names, images);
            }
        }
        else if(getKey1 == true)
        {
            this.gameObject.tag = "Minnka1-17";
        }
    }
}

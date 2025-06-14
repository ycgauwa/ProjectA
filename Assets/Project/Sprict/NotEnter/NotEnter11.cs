using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.InputSystem;

public class NotEnter11 : MonoBehaviour
{
    public Item key5;
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<Sprite> images;
    //診療所にて1回犬に追われてから戻ると光ってる場所があるため、そこで手術室の鍵を手にする。
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(key5.geted == false)
        {
            if(collider.gameObject.tag.Equals("Player"))
            {
                MessageManager.message_instance.MessageWindowActive(messages, names, images, ct: destroyCancellationToken).Forget();
            }
        }
        else if(key5.geted)
        {
            if(collider.gameObject.tag.Equals("Player"))
            {
                name = "Warp2-23";
            }
        }
    }
}

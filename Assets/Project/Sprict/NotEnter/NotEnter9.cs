using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class NotEnter9 : MonoBehaviour
{
    public Item key4;
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
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(key4.checkPossession == false)
        {
            if(collider.gameObject.tag.Equals("Player"))
            {
                MessageManager.message_instance.MessageWindowActive(messages, names, images, ct: destroyCancellationToken).Forget();
            }
            else if(collider.gameObject.tag.Equals("Seiitirou"))
            {
                // 鍵が必要だというメッセージ
                MessageManager.message_instance.MessageWindowActive(messages2, names2, images2, ct: destroyCancellationToken).Forget();
            }
        }
        else if(key4.checkPossession == true)
        {
            if(collider.gameObject.tag.Equals("Player"))
            {
                gameObject.tag = "Minnka2-17";
            }
            else if(collider.gameObject.tag.Equals("Seiitirou"))
            {
                gameObject.tag = "Minnka2-17";
            }
        }
    }
}

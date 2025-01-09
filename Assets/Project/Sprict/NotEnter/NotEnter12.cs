using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.InputSystem;

public class NotEnter12 : MonoBehaviour
{
    public Item key6;
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<Sprite> images;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(key6.checkPossession == false)
        {
            if(collider.gameObject.tag.Equals("Player"))
            {
                MessageManager.message_instance.MessageWindowActive(messages, names, images, ct: destroyCancellationToken).Forget();
            }
        }
        else if(key6.checkPossession == true)
        {
            if(collider.gameObject.tag.Equals("Player"))
            {
                gameObject.tag = "Minnka2-21";
            }
        }
    }
}

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
    //f—ÃŠ‚É‚Ä1‰ñŒ¢‚É’Ç‚í‚ê‚Ä‚©‚ç–ß‚é‚ÆŒõ‚Á‚Ä‚éêŠ‚ª‚ ‚é‚½‚ßA‚»‚±‚Åèpº‚ÌŒ®‚ğè‚É‚·‚éB
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

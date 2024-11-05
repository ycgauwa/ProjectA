using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class NotEnter7 : MonoBehaviour
{
    public RescueEvent rescueEvent;
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<Sprite> images;
    [SerializeField]
    private List<string> messages1;
    [SerializeField]
    private List<string> names1;
    [SerializeField]
    private List<Sprite> images1;
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
    [SerializeField]
    private List<string> messages4;
    [SerializeField]
    private List<string> names4;
    [SerializeField]
    private List<Sprite> images4;
    [SerializeField]
    private List<string> messages5;
    [SerializeField]
    private List<string> names5;
    [SerializeField]
    private List<Sprite> images5;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (rescueEvent.RescueSwitch)
        {
            if (collider.gameObject.tag.Equals("Player"))
            {
                this.gameObject.tag = "Untagged";
                if (GameManager.m_instance.deathCount == 0)
                    MessageManager.message_instance.MessageWindowActive(messages, names, images, ct: destroyCancellationToken).Forget();
                else if (GameManager.m_instance.deathCount == 1)
                    MessageManager.message_instance.MessageWindowActive(messages1, names1, images1, ct: destroyCancellationToken).Forget();
                else if (GameManager.m_instance.deathCount == 2)
                    MessageManager.message_instance.MessageWindowActive(messages2, names2, images2, ct: destroyCancellationToken).Forget();
                else if (GameManager.m_instance.deathCount == 3)
                    MessageManager.message_instance.MessageWindowActive(messages3, names3, images3, ct: destroyCancellationToken).Forget();
                else if (GameManager.m_instance.deathCount == 4)
                    MessageManager.message_instance.MessageWindowActive(messages4, names4, images4, ct: destroyCancellationToken).Forget();
                else if (GameManager.m_instance.deathCount == 5)
                    MessageManager.message_instance.MessageWindowActive(messages5, names5, images5, ct: destroyCancellationToken).Forget();
            }
        }
        if(collider.gameObject.tag.Equals("Seiitirou"))
        {
            this.gameObject.tag = "Minnka1-18";
        }
    }
}

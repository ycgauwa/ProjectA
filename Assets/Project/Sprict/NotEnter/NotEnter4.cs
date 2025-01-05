using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
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
    [SerializeField]
    private List<string> messages2;
    [SerializeField]
    private List<string> names2;
    [SerializeField]
    private List<Sprite> images2;
    public bool getKey1;
    public GameObject seiitirou;
    public ToEvent3 toevent3;
    public NotEnter6 notEnter6;
    public RescueEvent rescueEvent;
    public ItemDateBase itemDateBase;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(getKey1 == false)
        {
            if(collider.gameObject.tag.Equals("Player"))
            {
                MessageManager.message_instance.MessageWindowActive(messages, names, images, ct: destroyCancellationToken).Forget();
            }
        }
        else if(getKey1 == true)
        {
            if(collider.gameObject.tag.Equals("Player"))
            {
                gameObject.tag = "Minnka1-17";
                // 一旦敵が出てこないようにする。←出来てない。
                Inventry.instance.Delete(itemDateBase.GetItemId(251));
                itemDateBase.GetItemId(251).checkPossession = false;
            }
        }
        
        if(rescueEvent.RescueSwitch == true)
        {
            gameObject.tag = "Untagged";
            if(collider.gameObject.tag.Equals("Seiitirou"))
            {
                if(notEnter6.seiitirouFlag == false)
                {
                    MessageManager.message_instance.MessageWindowActive(messages2, names2, images2, ct: destroyCancellationToken).Forget();
                }
                else
                {
                    seiitirou.transform.position = new Vector2(31, 60);
                }
            }
        }
    }
}

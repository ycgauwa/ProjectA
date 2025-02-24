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
    public GameObject seiitirou;
    public NotEnter6 notEnter6;
    public RescueEvent rescueEvent;
    public ItemDateBase itemDateBase;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(ItemDateBase.itemDate_instance.GetItemId(251).geted == false)
        {
            if(collider.gameObject.tag.Equals("Player"))
            {
                MessageManager.message_instance.MessageWindowActive(GameManager.m_instance.GetMessages(name, "NotEnter"), GameManager.m_instance.GetSpeakerName(name, "NotEnter"), images, ct: destroyCancellationToken).Forget();
            }
        }
        else if(ItemDateBase.itemDate_instance.GetItemId(251).geted == true)
        {
            if(collider.gameObject.tag.Equals("Player"))
            {
                gameObject.tag = "Minnka1-17";
                if(ItemDateBase.itemDate_instance.GetItemId(251).checkPossession)
                    Inventry.instance.Delete(ItemDateBase.itemDate_instance.GetItemId(251));
            }
        }
        
        if(rescueEvent.RescueSwitch == true)
        {
            gameObject.tag = "Untagged";
            if(collider.gameObject.tag.Equals("Seiitirou"))
            {
                if(notEnter6.seiitirouFlag == false)
                {
                    MessageManager.message_instance.MessageWindowActive(GameManager.m_instance.GetMessages(name+"A", "NotEnter"), GameManager.m_instance.GetSpeakerName(name+"A", "NotEnter"), images2, ct: destroyCancellationToken).Forget();
                }
                else
                {
                    seiitirou.transform.position = new Vector2(31, 60);
                }
            }
        }
    }
}

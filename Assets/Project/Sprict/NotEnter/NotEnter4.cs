using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotEnter4 : MonoBehaviour
{
    // �����Ȃ��Ɠ���Ȃ��h�A�̂��߂̃X�N���v�g
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
                MessageManager.message_instance.MessageWindowActive(messages, names, images);
            }
        }
        else if(getKey1 == true)
        {
            if(collider.gameObject.tag.Equals("Player"))
            {
                this.gameObject.tag = "Minnka1-17";
                // ��U�G���o�Ă��Ȃ��悤�ɂ���B
                toevent3.event3flag = false;
                itemDateBase.Items4Delete();
            }
        }
        
        if(rescueEvent.RescueSwitch == true)
        {
            if(collider.gameObject.tag.Equals("Seiitirou"))
            {
                if(!notEnter6.seiitirouFlag)
                {
                    MessageManager.message_instance.MessageWindowActive(messages2, names2, images2);
                }
                else
                {
                    seiitirou.transform.position = new Vector2(31, 60);
                }
            }
        }
    }
}

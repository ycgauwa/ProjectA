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
    [SerializeField]
    private List<string> messages2;
    [SerializeField]
    private List<string> names2;
    [SerializeField]
    private List<Sprite> images2;
    public bool getKey2;
    public ItemDateBase itemDateBase;
    public RescueEvent rescueEvent;
    public GameObject enemy;
    public Homing homing;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(getKey2 == false)
        {
            if(collider.gameObject.tag.Equals("Player"))
            {
                MessageManager.message_instance.MessageWindowActive(messages, names, images);
            }
        }
        else if(rescueEvent.RescueSwitch == true)
        {
            if(collider.gameObject.tag.Equals("Player"))
            {
                gameObject.tag = "Untagged";
                MessageManager.message_instance.MessageWindowActive(messages2, names2, images2);
            }
        }
        else if(getKey2 == true)
        {
            if(collider.gameObject.tag.Equals("Player"))
            {
                this.gameObject.tag = "Minnka1-20";
                itemDateBase.Items5Delete();
                if (enemy.gameObject.activeSelf)
                {
                    homing.teleportManager.StopChased();
                    homing.enemyEmerge = false;
                }

            }
        }

    }

}

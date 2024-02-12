using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotEnter3 : MonoBehaviour
{
    //　今回はフラグが回収されなかったらメッセージ表示、回収されたらタグを持たせてあげる

    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<Sprite> images;
    public GameObject player;
    public GameObject seiitirou;
    public ToEvent3 toevent3;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(toevent3.event3flag == false)
        {
            if(collider.gameObject.tag.Equals("Player"))
            {
                MessageManager.message_instance.MessageWindowActive(messages, names, images);
            }
        }
        // イベントが終わった後にTPできるようにしたい
        else if(toevent3.event3flag == true)
        {
            this.gameObject.tag = "Minnka1-1";
            if(collider.gameObject.tag.Equals("Seiitirou"))
            {
                seiitirou.transform.position = new Vector2(24, -2);
            }
        }
    }
}

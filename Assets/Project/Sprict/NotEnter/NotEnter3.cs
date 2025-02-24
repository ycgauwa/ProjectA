using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
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
                MessageManager.message_instance.MessageWindowActive(GameManager.m_instance.GetMessages(name, "NotEnter"), GameManager.m_instance.GetSpeakerName(name, "NotEnter"), images,ct: destroyCancellationToken).Forget();
            }
        }
        else
        {
            gameObject.tag = "Minnka1-1";
            if(collider.gameObject.tag.Equals("Seiitirou"))
            {
                seiitirou.transform.position = new Vector2(24, -2);
            }
        }
    }
}

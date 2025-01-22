using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotEnter2 : MonoBehaviour
{
    // これは単純にどんな条件であってもエリアに入ったらメッセージウィンドウが出てくる仕組みにする。

    /*条件を満たさずに触れるとメッセージAが出て動けなくなる仕組みの作成
    ToEventクラスのone変数の条件によってこっちのクラスのメソッドが動くか動かないかを作る
    TPする前にウィンドウの表示→座標の固定をする。
    変数の引き渡しは引数を使って行うToEvent→NotEnterで渡す*/
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<Sprite> images;
    public GameObject player;
    private async void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
        {
            PlayerManager.m_instance.m_speed = 0;
            await MessageManager.message_instance.MessageWindowActive(GameManager.m_instance.GetMessages(name, "NotEnter"), GameManager.m_instance.GetSpeakerName(name, "NotEnter"), images, ct: destroyCancellationToken);        
        }
    }
}

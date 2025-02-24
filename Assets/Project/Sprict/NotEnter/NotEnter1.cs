using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class NotEnter1 : MonoBehaviour
{
    // このスクリプトはイベント１が開始されていないとメッセージが出る開始されたら通れるようにする

    /*条件を満たさずに触れるとメッセージAが出て動けなくなる仕組みの作成
    ToEventクラスのone変数の条件によってこっちのクラスのメソッドが動くか動かないかを作る
    TPする前にウィンドウの表示→座標の固定をする。
    変数の引き渡しは引数を使って行うToEvent→NotEnterで渡す*/
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<Sprite> image;
    public Canvas window;
    public Text target;
    public Text nameText;
    public GameObject player;
    public bool one = false;
    private bool isContacted = false;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag.Equals("Player"))
        {
            isContacted = true;
            //イベントなしには通れない仕組み
            //falseの時メッセージウィンドウの表示
            //代入大事。同じ名前であっても下記のようなやり方で代入可能
            one = ToEvent1.one;
            if (one == false)
                MessageManager.message_instance.MessageWindowActive(GameManager.m_instance.GetMessages(name, "NotEnter"), GameManager.m_instance.GetSpeakerName(name, "NotEnter"), image, ct: destroyCancellationToken).Forget();
            //MessageManager.message_instance.MessageWindowActive(messages, names, image, ct: destroyCancellationToken).Forget();
            else if (one == true) player.transform.position = new Vector2(-10, -105);
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player")) isContacted = false;
    }
}

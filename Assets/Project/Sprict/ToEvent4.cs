using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToEvent4 : MonoBehaviour
{
    // 部屋に入った時にイベントあるから敵が出てこないようにして、これでイベントが終わったら
    // 次から敵が出てきてもいいようにする。

    public GameObject player;
    public bool event4flag;
    public bool playerStop;
    public ToEvent3 toevent3;
    private bool isContacted = false;

    // メッセージウィンドウ用の変数
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

    // Start is called before the first frame update
    void Start()
    {
        event4flag = false;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(!event4flag) //フラグが立ってないとき
        {
            if(collider.gameObject.tag.Equals("Player"))
            {
                // プレイヤーの速度が停止
                playerStop = true;
                // 効果音とメッセージを流す
                MessageManager.message_instance.MessageWindowActive(messages, names, images);
                event4flag = true; //フラグが立つ
                toevent3.event3flag = true; //　敵が出てくるようにする。
            }
        }
    }
}

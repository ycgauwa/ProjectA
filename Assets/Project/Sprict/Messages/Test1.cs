using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test1 : MonoBehaviour
{
    //①メッセージウィンドウが表示され続けキーを押しても反応せず動かない不具合を解決しておく
    //①はウィンドウが開いてるのにメソッドを動かしちゃってウィンドウが閉じる→消えるを一瞬で繰り返すから起きた。条件付けをしよう
    //このスクリプトは廃棄。他のスクリプトに干渉したり、一回目が時間経過？でウィンドウ閉じるのに
    //２回目は一生閉じたり消えたりするのが意味わからん→これも①と同じ感じ
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<Sprite> image;
    public Canvas window;
    public Text target;
    public Text nameText;
    public static bool messageSwitch = false;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
        {
            isContacted = true;
        }

    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
        {
            isContacted = false;
        }
    }
    
    private bool isContacted = false;
    //物理的な挙動の時はFixedUpdateでかけ！

    private void Update()//入力チェックはUpdateに書く
    {
        //メッセージウィンドウ閉じるときはこのメソッドを
        if(isContacted && messageSwitch == false && Input.GetKeyDown(KeyCode.Return))
        {
            messageSwitch = true;
            MessageManager.message_instance.MessageWindowActive(messages, names,image);
        }
    }
    /*private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player") &&Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log($"colloder: {collider.gameObject.name} ");
            /*（）の中に引数を入れてあげると実行元のメソッドが渡した変数で処理を行ってくれる。
            ただし、データを渡す側は変数だけでよい*/
           /* MessageManager.message_instance.MessageWindowActive(messages, names);
        }
       
    }*/
}

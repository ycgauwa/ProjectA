using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemGet : MonoBehaviour
{　/*アイテムをゲットする時に使う関数
  　調べるとメッセージウィンドウの表示
    そしてインベントリにアイテムを突っ込む*/
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    public Canvas window;
    public Text target;
    public Text nameText;
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        isContacted = collider.gameObject.tag.Equals("Player");
    }

    // colliderをもつオブジェクトの領域外にでたとき(下記で説明1)
    private void OnTriggerExit2D(Collider2D collider)
    {
        isContacted = !collider.gameObject.tag.Equals("Player");
    }
    private bool isContacted = false;

    private void FixedUpdate()
    {
        if(isContacted && Input.GetButton("Submit") && Input.GetKeyDown(KeyCode.Return))
        {
            MessageManager.message_instance.MessageWindowActive(messages, names);
        }
    }
}

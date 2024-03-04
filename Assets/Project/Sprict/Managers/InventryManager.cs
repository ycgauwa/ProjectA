using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventryManager : MonoBehaviour
{
    // インベントリのアイテムで所持情報を管理してイベントや会話にうまくつなげる
    // 要所でアイテムを入手した時にリストを増やす、アイテムを入れる、初期化？する
    // アイテムが増えた時、そのアイテムのCheckPossessionをTrueにしてあげる。
    public List<Item> inventryItems = new List<Item>();

    private void Start()
    {
        //items[0].checkPossession = true;
    }
    public void CheckPossess(Item item)
    {
        inventryItems.Add(item);
        item.checkPossession = true;
    }

}

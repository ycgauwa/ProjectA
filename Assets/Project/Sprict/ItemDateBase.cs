using System.Collections;
using System.Collections.Generic;
//using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;
//using static UnityEditor.Progress;

public class ItemDateBase : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public Inventry inventry;
    public NotEnter4 notEnter4;
    public Text itemTextMessage;
    public Canvas inventryCanvas;

    private void Start()
    {
        for(int i = 0; i < items.Count; ++i)
        {
            items[i].checkPossession = false;
        }
    }
    public Item GetItemId(int itemid)
    {
        Item item = new Item();
        for(int i = 0; i < items.Count; i++)
        {
            if(items[i].itemID == itemid)
                item = items[i];
        }
        return item;
    }
    //　アイテムのセレクト状態解除
    public void SelectDiff()
    {
        for (int i = 0; i < items.Count; ++i)
        {
            items[i].selectedItem = false;
        }
    }
    //　アイテム合成のメソッド
    public void synthesis()
    {
        //　ハンマーと洗剤で普通のハンマーになる
        if(GetItemId(3).selectedItem == true && GetItemId(4).selectedItem == true && notEnter4.getKey1 == false)
        {
            inventry.Add(GetItemId(5));
            inventry.Delete(GetItemId(3));
            inventry.Delete(GetItemId(4));
            // ここで合成のメッセージを出すようにする。
            itemTextMessage.gameObject.SetActive(true);
            itemTextMessage.text = "合成完了";
        }
        //　普通のハンマーと人形で鍵が出てくる。
        if(GetItemId(5).selectedItem == true && GetItemId(2).selectedItem == true && notEnter4.getKey1 == false)
        {
            inventry.Add(GetItemId(252));
            inventry.Delete(GetItemId(5));
            inventry.Delete(GetItemId(2));
            itemTextMessage.gameObject.SetActive(true);
            itemTextMessage.text = "合成完了";
            notEnter4.getKey1 = true;
        }
        
    }
    private void Update()
    {
        if(inventryCanvas.gameObject.activeSelf)
        {
            if(Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return))
            {
                itemTextMessage.gameObject.SetActive(false);
            }
        }
    }
}

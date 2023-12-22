using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Image icon;
    public Text itemTextMessage;
    public ItemDateBase itemDate;
    public bool checkItem;
    
    Item item;

    public void AddItem(Item newItem)
    {
        item = newItem;
        icon .sprite = newItem.icon;
    }
    public void ClearSlot()
    {
        item = null;
        icon .sprite = null;
    }

    // ここでアイテムをクリックした時に説明文が出てきてほしい
    // セレクト状態を保持しているとき別のアイテムをクリックすると複数セレクトされてることになる
    public void UseItem()
    {if (item == null)
        {
            return;
        }
        if(item.selectedItem == false)
        {
            item.selectedItem = true;
            itemTextMessage.gameObject.SetActive(true);
            itemTextMessage.text = item.itemText;
            itemDate.synthesis();
        }
        else
        {
            item.selectedItem = false;
            itemTextMessage.gameObject.SetActive(false);
        }
        //checkItem = item.checkPossession;

    }
    void Update()
    {
        if(itemTextMessage.gameObject.activeSelf)
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                itemTextMessage.gameObject.SetActive(false);
            }
        }
    }

}

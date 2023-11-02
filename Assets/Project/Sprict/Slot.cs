using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Image icon;
    public Text itemTextMessage;
    public bool checkItem;
    
    Item item;

    public void AddItem(Item newItem)
    {
        item = newItem;
        icon .sprite = newItem.icon;
    }

    //�����ŃA�C�e�����N���b�N�������ɐ��������o�Ă��Ăق���
    public void UseItem()
    {if (item == null)
        {
            return;
        }
        itemTextMessage.gameObject.SetActive(true);
        itemTextMessage.text = item.itemText;
        checkItem = item.checkPossession;

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

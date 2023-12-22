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

    // �����ŃA�C�e�����N���b�N�������ɐ��������o�Ă��Ăق���
    // �Z���N�g��Ԃ�ێ����Ă���Ƃ��ʂ̃A�C�e�����N���b�N����ƕ����Z���N�g����Ă邱�ƂɂȂ�
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

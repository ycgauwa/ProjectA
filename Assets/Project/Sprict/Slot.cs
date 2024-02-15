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
    public GameManager gameManager;
    public GameObject iconSprite;
    public Image iconImage;
    Item item;

    public void AddItem(Item newItem)
    {
        item = newItem;
        iconImage.color = Color.white;
        iconImage.sprite = newItem.icon;
        //if (iconImage.sprite == null)
        //{
        //    iconImage.color = new Color(56, 56, 56, 181);
        //}
    }
    public void ClearSlot()
    {
        item = null;
        iconImage.color = new Color32(56,56,56,181); 
        iconImage.sprite = null;
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
            gameManager.audioSource.PlayOneShot(gameManager.decision);
            itemDate.synthesis();
        }
        else
        {
            item.selectedItem = false;
            itemTextMessage.gameObject.SetActive(false);
            gameManager.audioSource.PlayOneShot(gameManager.cancel);
        }
        //checkItem = item.checkPossession;

    }
    void Update()
    {
        if(itemTextMessage.gameObject.activeSelf)
        {
            if(Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return))
            {
                itemTextMessage.gameObject.SetActive(false);
            }
        }
    }

}

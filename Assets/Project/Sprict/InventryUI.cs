using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventryUI : MonoBehaviour
{
    public Transform slotsParent;
    public Slot[] slots;
    public Inventry inventry;

    private void Awake()
    {
        //  これをやることで親要素にくっついているSlotを含んだ子要素を全部取得することができる
        //　つまりInventryParentの子要素（１２個のSlot）をぜーんぶ取得することができる。
        slots = slotsParent.GetComponentsInChildren<Slot>(true);
    }
    public void UpdateUI()
    {
        //　インベントリにアイテムが入ってるなら表示する入ってないならnullにする
        //　はじめにパスワードを入力するとエラーを吐き何回もエンター押すと今までの回数分以上出てくる
        for(int i = 0; i< slots.Length; i++)
        {
            if(i < inventry.items.Count)//UIではないインベントリのデータの中で
            {
                slots[i].AddItem(inventry.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
            
            if (slots[i].iconImage.sprite == null)
            {
                slots[i].iconImage.color = new Color32(56, 56, 56, 0);
            }
        }
    }
}

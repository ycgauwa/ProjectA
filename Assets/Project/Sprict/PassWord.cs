using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassWord : MonoBehaviour
{
    [SerializeField] GameObject fleldObj;
    public InputField passcode;
    public Item item;
    public Inventry inventry;
    private bool getItemDoll = false;
    void Start()
    {
        //これはお手本が画像を触って起こることだからImageを取得してるけど俺のゲームの場合は、、？
        GetComponent<Image>().sprite = item.icon;
        fleldObj = GameObject.Find("Passcode1");
        //passcode = fleldObj.GetComponent<InputField>();
    }

    public void InputText()
    {
        if(item.checkPossession == false)
        {
            if(passcode.text == "0622")
            {
                // 押せば押すほどアイテムが入手できてしまうから一度取得したら入手できない仕様にしてあげる。
                // Inventryの追加（アイテムの取得）
                inventry.Add(item);
                item.checkPossession = true;
            }
        }
    }
}

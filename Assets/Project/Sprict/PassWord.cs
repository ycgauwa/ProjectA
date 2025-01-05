using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PassWord : MonoBehaviour
{
    [SerializeField] GameObject fleldObj;
    public InputField passcode1;
    public InputField passcode2;
    public Item doll;
    public Item secondHouseKey;
    public Inventry inventry;
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<Sprite> images;
    void Start()
    {
        //これはお手本が画像を触って起こることだからImageを取得してるけど俺のゲームの場合は、、？
        GetComponent<Image>().sprite = doll.icon;
        fleldObj = GameObject.Find("Passcode1");
        //passcode = fleldObj.GetComponent<InputField>();
    }

    public void InputText()
    {
        if(doll.checkPossession == false)
        {
            if(passcode1.text == "0622")
            {
                // 押せば押すほどアイテムが入手できてしまうから一度取得したら入手できない仕様にしてあげる。
                // Inventryの追加（アイテムの取得）
                inventry.Add(doll);
                doll.checkPossession = true;
                MessageManager.message_instance.MessageWindowActive(messages, names, images, ct: destroyCancellationToken).Forget();
            }
        }
    }
    public void ToGetKey() 
    {
        fleldObj = GameObject.Find("Passcode2");
        if(secondHouseKey.checkPossession == false)
        {
            if(passcode2.text == "2007")
            {
                inventry.Add(secondHouseKey);
                secondHouseKey.checkPossession = true;
                MessageManager.message_instance.MessageWindowActive(messages, names, images, ct: destroyCancellationToken).Forget();
            }
        }
    }
}

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
    public InputField seiPasscode1;
    public Item doll;
    public Item secondHouseKey;
    public Inventry inventry;
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<Sprite> images;
    [SerializeField]
    private List<string> seiitirouMessages;
    [SerializeField]
    private List<string> seiitirouNames;
    [SerializeField]
    private List<Sprite> seiitirouImages;
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
                MessageManager.message_instance.MessageWindowActive(messages, names, images, ct: destroyCancellationToken).Forget();
            }
        }
    }
    public void SeiitirouToGetKey()
    {
        fleldObj = GameObject.Find("SeiitirouPasscode");
        if(secondHouseKey.checkPossession == false)
        {
            if(seiPasscode1.text == "511")
            {
                inventry.Add(secondHouseKey);
                MessageManager.message_instance.MessageWindowActive(seiitirouMessages, seiitirouNames, seiitirouImages, ct: destroyCancellationToken).Forget();
            }
        }
    }
}

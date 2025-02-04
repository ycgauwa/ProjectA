using System.Collections;
using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Test1 : MonoBehaviour
{
    //①メッセージウィンドウが表示され続けキーを押しても反応せず動かない不具合を解決しておく
    //①はウィンドウが開いてるのにメソッドを動かしちゃってウィンドウが閉じる→消えるを一瞬で繰り返すから起きた。条件付けをしよう
    //このスクリプトは廃棄。他のスクリプトに干渉したり、一回目が時間経過？でウィンドウ閉じるのに
    //２回目は一生閉じたり消えたりするのが意味わからん→これも①と同じ感じ
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<Sprite> image;
    [SerializeField]
    private List<Sprite> seiitirouImage;
    public Canvas window;
    public Text target;
    public Text nameText;
    public bool talked;
    private bool isContacted = false;
    private bool SeiContacted = false;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //幸人の時に表示されるメッセージ
        if(collider.gameObject.tag.Equals("Player"))
            isContacted = true;
        else if(collider.gameObject.tag.Equals("Seiitirou"))
            SeiContacted = true;
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
            isContacted = false;
        else if(collider.gameObject.tag.Equals("Seiitirou"))
            SeiContacted = false;
    }
    private void Update()//入力チェックはUpdateに書く
    {
        
        //メッセージウィンドウ閉じるときはこのメソッドを
        if(isContacted && (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)))
        {
            Debug.Log("Yukito");
            MessageManager.message_instance.MessageWindowActive(GameManager.m_instance.GetMessages(name,"Interior"),GameManager.m_instance.GetSpeakerName(name, "Interior"), image, ct: destroyCancellationToken).Forget();
            if(!talked) talked = true;
        }
        else if(SeiContacted && seiitirouImage.Count > 0 && (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)))
        {
            Debug.Log(seiitirouImage);
            MessageManager.message_instance.MessageWindowActive(GameManager.m_instance.GetMessages(name + "S","Interior"), GameManager.m_instance.GetSpeakerName(name + "S","Interior"), seiitirouImage, ct: destroyCancellationToken).Forget();
            if(!talked) talked = true;
        }
    }
}

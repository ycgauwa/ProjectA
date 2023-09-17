using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToEvent2 : MonoBehaviour
{
    //選択画面が出て会話が始まり太鼓の音を流す。その後白い光の演出が出た後に誰もいなくなる
    //座標固定したカメラが移動して（ゆっくり目に）一人のNPCが教室に入り太鼓の前まで行く
    //セリフを話した後にまたカメラを移動、警備員と少女が映る画角に移動そこで会話が終わった後
    //女の子がエフェクトを出して消滅。警備員のセリフを書いてから終幕。
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    public Canvas window;
    public Text target;
    public Text nameText;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        isContacted = collider.gameObject.tag.Equals("Player");
    }

    // colliderをもつオブジェクトの領域外にでたとき(下記で説明1)
    private void OnTriggerExit2D(Collider2D collider)
    {
        isContacted = !collider.gameObject.tag.Equals("Player");
    }
    private bool isContacted = false;

    private void FixedUpdate()
    {
        if(isContacted && Input.GetButton("Submit") && Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log(MessageManager.message_instance);
            Debug.Log(messages);
            Debug.Log(names);
            MessageManager.message_instance.MessageWindowActive(messages, names);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using UnityEngine;

public class Case1Object : MonoBehaviour
{
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<Sprite> image;
    [SerializeField]
    private List<string> messages2;
    [SerializeField]
    private List<string> names2;
    [SerializeField]
    private List<Sprite> image2;
    public Canvas endWindow;
    public Image end1Image;
    public Image end1retry;
    public Canvas window;
    public Text target;
    public Text nameText;
    public Light2D light2D;
    public EndingCase1 endingCase1;
    public SoundManager soundManager;
    public AudioClip ending1Sound;
    public AudioClip decision;
    public static bool messageSwitch = false;
    private bool isContacted = false;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
        {
            isContacted = true;
        }
    }

    // colliderをもつオブジェクトの領域外にでたとき(下記で説明1)
    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
        {
            isContacted = false;
        }
    }
    private void Update()//入力チェックはUpdateに書く
    {
        //メッセージウィンドウ閉じるときはこのメソッドを
        if(isContacted && messageSwitch == false && (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)))
        {
            if(isContacted == true)
            {
                if(endingCase1.answer == true)
                {
                    messageSwitch = true;
                    StartCoroutine("Sleep");
                }
                else if(endingCase1.answer == false)
                {
                    messageSwitch = true;
                    MessageManager.message_instance.MessageWindowActive(messages, names, image);
                }
            }
        }
        //ここでメッセージをすべて出し終わったら画面を切り替えたい。
        if(endWindow.gameObject.activeSelf)
        {
            if(messageSwitch == false && isContacted == false)
            {
                end1retry.gameObject.SetActive(true);
                end1Image.gameObject.SetActive(false);
            }
        }
    }
    private IEnumerator Sleep()
    {
        light2D.intensity = 1.0f;
        GameManager.m_instance.stopSwitch = true;
        while(light2D.intensity > 0.01f)
        {
            light2D.intensity -= 0.007f;
            yield return null; //ここで１フレーム待ってくれてる
        }
        soundManager.PlayBgm(ending1Sound);
        light2D.intensity = 1.0f;
        endWindow.gameObject.SetActive(true);
        messageSwitch = true;
        MessageManager.message_instance.MessageWindowActive(messages2, names2, image2);
        isContacted = false;
    }
    public void OnclickEnd1Retry()
    {
        end1Image.gameObject.SetActive(true);
        endWindow.gameObject.SetActive(false);
        endingCase1.player.transform.position = new Vector3(30, -35, 0);
        soundManager.StopBgm(ending1Sound);
        GameManager.m_instance.stopSwitch = false;
        endingCase1.answer = false;
    }
}

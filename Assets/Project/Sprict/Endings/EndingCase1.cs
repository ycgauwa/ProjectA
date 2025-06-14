using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using UnityEngine;
using UnityEngine.EventSystems;
using Cysharp.Threading.Tasks;
using System.Threading;

public class EndingCase1 : MonoBehaviour
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
    [SerializeField]
    private List<string> messages3;
    [SerializeField]
    private List<string> names3;
    [SerializeField]
    private List<Sprite> image3;
    [SerializeField]
    private List<string> messages4;
    [SerializeField]
    private List<string> names4;
    [SerializeField]
    private List<Sprite> image4;

    public GameObject player;
    public Light2D light2D;
    public Canvas window;
    public Canvas Selectwindow;
    public Text target;
    public Text nameText;
    public Image characterImage;
    public Image selection;
    public SoundManager soundManager;
    public AudioClip runSound;
    public AudioClip decision;
    public AudioClip tensionBGM;
    public GameObject firstSelect;
    public GameObject faliedSelect;

    //アンサーとして何を答えたか
    public  int answerNum;
    public bool answered = false;
    private bool isContacted = false;
    // Start is called before the first frame update
    void Start()
    {
        answerNum = 0;
        //0が初期値1が否定した後2肯定した後3がリトライを押した後。
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player")) isContacted = true;
    }
    // colliderをもつオブジェクトの領域外にでたとき(下記で説明1)
    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player")) isContacted = false;
    }
    private void Update()
    {
        //話しかける(条件は動的なものと今回のboolのように恒常的なもので分けた方がいい)
        if(Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return))
        {
            if(isContacted == true && answered == false)
            {
                if (answerNum == 1) MessageManager.message_instance.MessageWindowActive(messages4, names4, image4, ct:destroyCancellationToken).Forget();
                else if (answerNum == 0 || answerNum == 3)
                {
                    MessageManager.message_instance.MessageSelectWindowActive(messages, names, image,Selectwindow,selection,firstSelect, tensionBGM,ct: destroyCancellationToken).Forget();
                    answered = true;
                }
                else return;
            }
        }
    }
    public void End1SelectYes()
    {
        //画面がどんどん暗くなっていき、真っ暗な間にTPしてる。TPするタイミングで移動音なんかを加えるとよい
        soundManager.PlaySe(decision);
        answerNum = 2;
        StartCoroutine("Blackout");
        selection.gameObject.SetActive(false);
        Selectwindow.gameObject.SetActive(false);
        MessageManager.message_instance.isOpenSelect = false;
        soundManager.StopBgm(tensionBGM);
    }
    public void End1SelectNo()
    {
        //メッセージを出して、ウィンドウを閉じるそのあとベットを調べると寝られるようにする。
        soundManager.PlaySe(decision);
        selection.gameObject.SetActive(false);
        Selectwindow.gameObject.SetActive(false);
        MessageManager.message_instance.isOpenSelect = false;
        soundManager.StopBgm(tensionBGM);
        if(answerNum == 3)
        {
        }
        else answerNum = 1;
        answered = false;
    }
    private IEnumerator Blackout()
    {
        FlagsManager.flag_Instance.navigationPanel.gameObject.SetActive(false);
        light2D.intensity = 1.0f;
        GameManager.m_instance.stopSwitch = true;
        soundManager.PlaySe(runSound);
        while(light2D.intensity > 0.01f)
        {
            light2D.intensity -= 0.007f;
            yield return null; //ここで１フレーム待ってくれてる
        }
        player.transform.position = new Vector3(-11, -72,0);
        FlagsManager.flag_Instance.navigationPanel.gameObject.SetActive(true);
        FlagsManager.flag_Instance.ChangeUIDestnation(1,"Yukito");
        FlagsManager.flag_Instance.ChangeUILocation("School3");
        light2D.intensity = 1.0f;
        GameManager.m_instance.stopSwitch = false;
        soundManager.StopSe(runSound);
    }
    //一番初めのエンディングで、行かない。寝るを選択したところ「何日も何日も帰ってこなかった。」というテキストとともに後悔するシーンを描く
    //流れとしては、メッセージ→選択肢→「はい」or「いいえ」でメッセージなどが変わる。
}

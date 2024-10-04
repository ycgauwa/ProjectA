using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using UnityEngine;
using UnityEngine.EventSystems;

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
    private IEnumerator coroutine;

    //アンサーとして何を答えたか
    public  bool answer;
    private bool isOpenSelect = false;
    private bool isContacted = false;
    // Start is called before the first frame update
    void Start()
    {
        answer = false;
    }
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
    private void Update()
    {
        //話しかける(条件は動的なものと今回のboolのように恒常的なもので分けた方がいい)
        if(Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return))
        {
            if(isContacted == true && coroutine == null)
            {
                if(answer == true)
                {
                    coroutine = OnAction2();
                    PlayerManager.m_instance.m_speed = 0;
                    StartCoroutine(coroutine);
                }
                else
                {
                    coroutine = OnAction();
                    StartCoroutine(coroutine);
                }
            }
        }
    }
    protected void showMessage(string message, string name, Sprite image)
    {
        this.target.text = message;
        this.nameText.text = name;
        characterImage.sprite = image;
    }
    IEnumerator OnAction()
    {
        window.gameObject.SetActive(true);
        for(int i = 0; i < messages.Count; ++i)
        {
            yield return null;

            showMessage(messages[i], names[i], image[i]);
            if(i == messages.Count - 1)
            {
                soundManager.PlayBgm(tensionBGM);
                Selectwindow.gameObject.SetActive(true);
                selection.gameObject.SetActive(true);
                EventSystem.current.SetSelectedGameObject(firstSelect);
                isOpenSelect = true;
                break;
            }
            yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
        }
        yield return new WaitUntil(() => !isOpenSelect);
        target.text = "";
        window.gameObject.SetActive(false);
        coroutine = null;
        yield break;

    }
    IEnumerator OnAction2()
    {
        window.gameObject.SetActive(true);

        for(int i = 0; i < messages4.Count; ++i)
        {
            yield return null;

            showMessage(messages4[i], names4[i], image4[i]);

            yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
        }
        window.gameObject.SetActive(false);
        PlayerManager.m_instance.m_speed = 0.075f;
        coroutine = null;
        yield break;
    }
    public void End1SelectYes()
    {
        //画面がどんどん暗くなっていき、真っ暗な間にTPしてる。TPするタイミングで移動音なんかを加えるとよい
        soundManager.PlaySe(decision);
        StartCoroutine("Blackout");
        selection.gameObject.SetActive(false);
        Selectwindow.gameObject.SetActive(false);
        isOpenSelect = false;
        soundManager.StopBgm(tensionBGM);
    }
    public void End1SelectNo()
    {
        //メッセージを出して、ウィンドウを閉じるそのあとベットを調べると寝られるようにする。
        soundManager.PlaySe(decision);
        selection.gameObject.SetActive(false);
        Selectwindow.gameObject.SetActive(false);
        answer = true;
        isOpenSelect = false;
        soundManager.StopBgm(tensionBGM);
    }
    private IEnumerator Blackout()
    {
        light2D.intensity = 1.0f;
        GameManager.m_instance.stopSwitch = true;
        soundManager.PlaySe(runSound);
        while(light2D.intensity > 0.01f)
        {
            light2D.intensity -= 0.007f;
            yield return null; //ここで１フレーム待ってくれてる
        }
        player.transform.position = new Vector3(-11, -72,0);
        light2D.intensity = 1.0f;
        GameManager.m_instance.stopSwitch = false;
        soundManager.StopSe(runSound);
    }
    //一番初めのエンディングで、行かない。寝るを選択したところ「何日も何日も帰ってこなかった。」というテキストとともに後悔するシーンを描く
    //流れとしては、メッセージ→選択肢→「はい」or「いいえ」でメッセージなどが変わる。
}

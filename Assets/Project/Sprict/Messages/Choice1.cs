using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class Choice1 : MonoBehaviour
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
    public Canvas window;
    public Canvas Selectwindow;
    public Text target;
    public Text nameText;
    public Image characterImage;
    public Image selection;
    public Button yes;
    public Button no;
    private IEnumerator coroutine;
    public Inventry inventry;
    public Item item;
    private IEnumerator coroutine2;
    
    //アンサーとして何を答えたか
    private bool answer;
    //アイテムを既に入手しているかどうか
    private bool isGet;
    private bool isOpenSelect = false;
    private bool isContacted = false;

    // Start is called before the first frame update
    void Start()
    {
        answer = false;
        isGet = false;
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
                if(isGet)
                {
                    coroutine = OnAction2();
                    PlayerManager.m_instance.m_speed = 0;
                    StartCoroutine(coroutine);
                }
                else
                {
                    coroutine = OnAction();
                    PlayerManager.m_instance.m_speed = 0;
                    // コルーチンの起動(下記説明2)
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
            // 1フレーム分 処理を待機(下記説明1)
            yield return null;
            // 会話をwindowのtextフィールドに表示
            showMessage(messages[i], names[i], image[i]);
            if(i == messages.Count - 1)
            {
                Selectwindow.gameObject.SetActive(true);
                selection.gameObject.SetActive(true);
                isOpenSelect = true;
                break;
            }
            yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
        }
        yield return new WaitUntil(() => !isOpenSelect );
        //はいを選んだ
        if(answer == true)
        {
            for(int i = 0; i < messages2.Count; ++i)
            {
                yield return null;
                //入手するを表示
                showMessage(messages2[i], names2[i], image2[i]);
                isGet = true;
                yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
            }
            inventry.Add(item);
        }
        else
        {
            for(int i = 0; i < messages3.Count; ++i)
            {
                yield return null;
                //入手してない
                showMessage(messages3[i], names3[i], image3[i]);
                yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
            }
        }
        
        window.gameObject.SetActive(false);
        PlayerManager.m_instance.m_speed = 0.075f;
        coroutine = null;
        yield break;

    }
    IEnumerator OnAction2()
    {
        window.gameObject.SetActive(true);

        for(int i = 0; i < messages4.Count; ++i)
        {
            // 1フレーム分 処理を待機(下記説明1)
            yield return null;

            //すでにもってる
            showMessage(messages4[i], names4[i], image4[i]);


            // キー入力を待機 (下記説明1)
            yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
        }
        window.gameObject.SetActive(false);
        PlayerManager.m_instance.m_speed = 0.075f;
        coroutine = null;
        yield break;
    }
    public void SelectAnswerYes()
    {
        answer = true;
        selection.gameObject.SetActive(false);
        Selectwindow.gameObject.SetActive(false);
        isOpenSelect = false;
    }
    public void SelectAnswerNo()
    {
        answer = false;
        selection.gameObject.SetActive(false);
        Selectwindow.gameObject.SetActive(false);
        isOpenSelect = false;
    }
}

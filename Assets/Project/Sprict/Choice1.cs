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
    private List<string> messages2;
    [SerializeField]
    private List<string> names2;
    [SerializeField]
    private List<string> messages3;
    [SerializeField]
    private List<string> names3;
    [SerializeField]
    private List<string> messages4;
    [SerializeField]
    private List<string> names4;
    public Canvas window;
    public Canvas Selectwindow;
    public Text target;
    public Text nameText;
    public Button yes;
    public Button no;
    private IEnumerator coroutine;
    private bool yesSelection;
    private bool noSelection;
    //private bool choice1 = false;
    private bool isContacted = false;


    // Start is called before the first frame update
    void Start()
    {
        yesSelection = false;
        noSelection = false;

    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        isContacted = collider.gameObject.tag.Equals("Player");
    }

    // colliderをもつオブジェクトの領域外にでたとき(下記で説明1)
    private void OnTriggerExit2D(Collider2D collider)
    {
        isContacted = !collider.gameObject.tag.Equals("Player");
    }
    private void FixedUpdate()
    {
        if(isContacted && coroutine == null && Input.GetButton("Submit") && /*choice1 == false && */Input.GetKeyDown(KeyCode.Return))
        {
            coroutine = CreateCoroutine();
            PlayerManager.m_instance.m_speed = 0;
            // コルーチンの起動(下記説明2)
            StartCoroutine(coroutine);
        }
    }
    public void Update()
    {
        //Debug.Log(choice1);
    }
    IEnumerator CreateCoroutine() 
    {
        window.gameObject.SetActive(true);
        yield return OnAction();
        /*if(choice1 == false)
        {
            yield return OnAction();
        }
        else
        {
            yield return OnAction2();
        }*/
    }
    protected void showMessage(string message, string name)
    {
        this.target.text = message;
        this.nameText.text = name;
    }
    protected void showMessage2(string message2, string name2)
    {
        this.target.text = message2;
        this.nameText.text = name2;
    }
    protected void showMessage3(string message3, string name3)
    {
        this.target.text = message3;
        this.nameText.text = name3;
    }
    protected void showMessage4(string message4, string name4)
    {
        this.target.text = message4;
        this.nameText.text = name4;
    }
    IEnumerator OnAction()
    {
        for(int i = 0; i < messages.Count; ++i)
        {
            // 1フレーム分 処理を待機(下記説明1)
            yield return null;
            // 会話をwindowのtextフィールドに表示
            showMessage(messages[i], names[i]);
            if(i == messages.Count - 1)
            {
                Selectwindow.gameObject.SetActive(true);
            }
            // キー入力を待機 (下記説明1)
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        }
        for(int i = 0; i < messages2.Count && yesSelection == true; ++i)
        {
            Debug.Log("OnAction.yesSelection");
            yield return null;
            showMessage2(messages2[i], names2[i]);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        }
        for(int i = 0; i < messages3.Count && noSelection == true; ++i)
        {
            Debug.Log("OnAction.noSelection");
            yield return null;
            showMessage3(messages3[i], names3[i]);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        }

        window.gameObject.SetActive(false);
        noSelection = false;
        yesSelection = false;
        PlayerManager.m_instance.m_speed = 0.05f;
        //choice1 = true;
        yield break;

    }
    IEnumerator OnAction2()
    {
        Debug.Log("OnAction2");
        for(int i = 0; i < messages4.Count; ++i)
        {
            // 1フレーム分 処理を待機(下記説明1)
            yield return null;

            // 会話をwindowのtextフィールドに表示
            showMessage4(messages4[i], names4[i]);


            // キー入力を待機 (下記説明1)
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        }

        yield break;

    }

    //選択したあとにもっかい話しかける時のテキストの用意
    public void YesChangeText()
    {
        yesSelection = !yesSelection;
        Selectwindow.gameObject.SetActive(false);
    }
    public void NoChangeText()
    {
        noSelection = !noSelection;
        Selectwindow.gameObject.SetActive(false);
    }
}

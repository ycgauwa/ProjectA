using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalenderMessage : MonoBehaviour
{
    // 話しかけた時にメッセージウィンドウを表示。ウィンドウが非表示になった後にカレンダーをアクティブにする
    // 話しかけて画像が出てきて、画像が出てるときはタイムスケールを０にする。そしてエンターを押すとテキスト
    // メッセージが出てきて、表示し終わったら画像も消してタイムスケールを元に戻す。

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
    public Canvas window;
    public Text target;
    public Text nameText;
    public Canvas calCanvas;
    public Image characterImage;
    public Image calender;
    public Image TVScreen;
    private IEnumerator coroutine;
    public bool messageSwitch = false;
    private bool isContacted = false;

    /*private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                PlayerManager.m_instance.m_speed = 0;
                coroutine = CreateCoroutine();
                StartCoroutine(coroutine);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
        {
            if(calCanvas.gameObject.activeSelf)
            {
                calender.gameObject.SetActive(false);
                calCanvas.gameObject.SetActive(false);
            }
            if(TVScreen == null)
            {
                return;
            }
            if(TVScreen.gameObject.activeSelf)
            {
                TVScreen.gameObject.SetActive(false);
            }
        }
    }*/
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag.Equals("Player"))
        {
            isContacted = true;
        }

    }
    // この状態だと文字や画像は出る。でも画像が閉じれなくなってしまう。後動けない
    //　画像が一周してからもう一度画像を出すために
    private void OnTriggerExit2D(Collider2D collider)
    {
        messageSwitch = false;
        if (collider.gameObject.tag.Equals("Player"))
        {
            isContacted = false;
            if (calCanvas.gameObject.activeSelf)
            {
                calender.gameObject.SetActive(false);
                calCanvas.gameObject.SetActive(false);
            }
            if (TVScreen == null)
            {
                return;
            }
            if (TVScreen.gameObject.activeSelf)
            {
                TVScreen.gameObject.SetActive(false);
            }
        }
    }

    public IEnumerator CreateCoroutine()
    {
        // window起動
        window.gameObject.SetActive(true);

        // 抽象メソッド呼び出し 詳細は子クラスで実装
        yield return OnAction();

        // window終了
        target.text = "";
        window.gameObject.SetActive(false);

        StopCoroutine(coroutine);
        coroutine = null;
        PlayerManager.m_instance.m_speed = 0.075f;
    }

    protected void showMessage(string message, string name, Sprite image)
    {
        target.text = message;
        nameText.text = name;
        characterImage.sprite = image;
    }

    IEnumerator OnAction()
    {

        for(int i = 0; i < messages.Count; ++i)
        {
            // 1フレーム分 処理を待機(下記説明1)
            yield return null;

            // 会話をwindowのtextフィールドに表示
            showMessage(messages[i], names[i], image[i]);

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        }
        calCanvas.gameObject.SetActive(true);
        calender.gameObject.SetActive(true);
        yield break;

    }
    private void Update()
    {
        if (isContacted && messageSwitch == false && Input.GetKeyDown(KeyCode.Return))
        {
            messageSwitch = true;
            PlayerManager.m_instance.m_speed = 0;
            coroutine = CreateCoroutine();
            StartCoroutine(coroutine);
        }
        if (calender.gameObject.activeSelf)
        {
            Time.timeScale = 0.0f;
            if(Input.GetKeyDown(KeyCode.Return))
            {
                calender.gameObject.SetActive(false);
                Time.timeScale = 1.0f;
            }
        }
    }
}

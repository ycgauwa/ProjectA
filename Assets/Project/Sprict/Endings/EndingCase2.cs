using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using UnityEngine;
using UnityEngine.EventSystems;

public class EndingCase2 : MonoBehaviour
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
    [SerializeField]
    private List<string> messages5;
    [SerializeField]
    private List<string> names5;
    [SerializeField]
    private List<Sprite> image5;
    public Light2D light2D;
    public Canvas window;
    public Canvas Selectwindow;
    public Text target;
    public Text nameText;
    public Image characterImage;
    public Image selection;
    public Image end2Image;
    public Image end2Image2;
    public Image end2Image3;
    public Image end2retry;
    public SoundManager soundManager;
    public Homing homing;
    public AudioClip decision;
    private IEnumerator coroutine;
    public Canvas endWindow;
    public AudioClip ending2Sound;
    public static bool messageSwitch = false;
    private bool isContacted = false;
    public bool answer;
    private bool isOpenSelect = false;
    public GameObject firstSelect;
    public GameObject firstSelect2;

    //流れとしては外に出ようとして、つかむ画像の出現そのあとに凍るそのあとに白骨化からの餓死エンド。
    //変化が起こる前に、フェードアウトをはさむ予定ではある。選択肢→つかむ画像→一瞬光って画像差し替え→メッセージ送り
    //のあとフェードアウト→画像差し替えで白骨化した画像を出して勧告を表示する。
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag.Equals("Player"))
        {
            isContacted = true;
        }
    }

    // colliderをもつオブジェクトの領域外にでたとき(下記で説明1)
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag.Equals("Player"))
        {
            isContacted = false;
        }
    }
    private void Update()//入力チェックはUpdateに書く
    {
        if (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return))
        {
            if (isContacted == true && coroutine == null)
            {
                if (homing.enemyEmerge == true && messageSwitch == false)
                {
                    messageSwitch = true;
                    MessageManager.message_instance.MessageWindowActive(messages5, names5, image5);
                }
                else if (answer == true)
                {
                    coroutine = OnAction2();
                    StartCoroutine(coroutine);
                }
                else
                {
                    coroutine = OnAction();
                    StartCoroutine(coroutine);
                }
            }
        }
        
        //ここでメッセージをすべて出し終わったら画面を切り替えたい。
        /*if (endWindow.gameObject.activeSelf)
        {
            if (messageSwitch == false && isContacted == false)
            {
                end2retry.gameObject.SetActive(true);
                end2Image.gameObject.SetActive(false);
            }
        }*/
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
        for (int i = 0; i < messages2.Count; ++i)
        {
            yield return null;
            showMessage(messages2[i], names2[i], image2[i]);
            if (i == messages2.Count - 1)
            {
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
        yield break;
    }
    IEnumerator OnAction2()
    {
        window.gameObject.SetActive(true);

        for (int i = 0; i < messages.Count; ++i)
        {
            // 1フレーム分 処理を待機(下記説明1)
            yield return null;
            showMessage(messages[i], names[i], image[i]);
            yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
        }
        window.gameObject.SetActive(false);
        coroutine = null;
        yield break;
    }
    private IEnumerator Blackout()
    {
        window.gameObject.SetActive(true);
        for (int i = 0; i < messages3.Count; ++i)
        {
            yield return null;
            showMessage(messages3[i], names3[i], image3[i]);
            if (i == messages3.Count - 1)
            {
                end2Image2.gameObject.SetActive(true);
                break;
            }
            yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
        }
        light2D.intensity = 1.0f;
        GameManager.m_instance.stopSwitch = true;
        //画像の明るさを下げて真っ暗にする。
        yield return new WaitForSeconds(3.0f);
        end2Image2.gameObject.SetActive(false);
        end2Image3.gameObject.SetActive(true);
        light2D.intensity = 1.0f;
        yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
        for (int i = 0; i < messages4.Count; ++i)
        {
            yield return null;
            showMessage(messages4[i], names4[i], image4[i]);
            yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
        }
        yield return new WaitForSeconds(2.0f);
        yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
        end2retry.gameObject.SetActive(true);
        end2Image3.gameObject.SetActive(false);
        window.gameObject.SetActive(false);
        coroutine = null;
        GameManager.m_instance.stopSwitch = false;
    }
    public void OnclickEnd2Retry()
    {
        end2Image.gameObject.SetActive(true);
        endWindow.gameObject.SetActive(false);
        soundManager.StopBgm(ending2Sound);
        GameManager.m_instance.stopSwitch = false;
    }
    public void End2SelectYes()
    {
        //選択してから、１，２回メッセージを送って画像を出してその後から苦しみ出す（欲を言えば焦るようなBGMがほしい）
        //そのあとに苦しんでる様子をだしつつ画像が差し替えで凍ってく様子を見せる。
        soundManager.PlaySe(decision);
        StartCoroutine("Blackout");
        selection.gameObject.SetActive(false);
        Selectwindow.gameObject.SetActive(false);
        answer = true;
        isOpenSelect = false;
    }
    public void End2SelectNo()
    {
        //メッセージを出して、ウィンドウを閉じる。そのあとはもう選択肢が出ないようにする。
        soundManager.PlaySe(decision);
        selection.gameObject.SetActive(false);
        Selectwindow.gameObject.SetActive(false);
        answer = true;
        coroutine = null;
        isOpenSelect = false;
    }
}

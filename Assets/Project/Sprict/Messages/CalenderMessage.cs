using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
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
    private List<string> seiitirouMessages;
    [SerializeField]
    private List<string> seiitirouNames;
    [SerializeField]
    private List<Sprite> seiitirouImages;
    public Canvas window;
    public Text target;
    public Text nameText;
    public Canvas calCanvas;
    public Image characterImage;
    public Image calender;
    public Image seiCalender;
    public Image TVScreen;
    public GameObject firstSelect;
    public GameObject seiFirstSelect;
    private IEnumerator coroutine;
    public bool messageSwitch = false;
    private bool isContacted = false;
    private bool seiIsContacted = false;
    public bool talked = false;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag.Equals("Player"))
        {
            isContacted = true;
        }
        else if(collider.gameObject.tag.Equals("Seiitirou"))
        {
            seiIsContacted = true;
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
                calCanvas.gameObject.SetActive(false);
            }
        }
        else if(collider.gameObject.tag.Equals("Seiitirou"))
        {
            seiIsContacted = false;
            if(seiCalender == null) return;
            else if(calCanvas.gameObject.activeSelf)
            {
                calender.gameObject.SetActive(false);
                calCanvas.gameObject.SetActive(false);
            }
            if(TVScreen == null)
            {
                return;
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
        GameManager.m_instance.ImageErase(characterImage);
        window.gameObject.SetActive(false);
        Homing.m_instance.speed = 2 + GameManager.m_instance.difficultyLevelManager.addEnemySpeed;
        SecondHouseManager.secondHouse_instance.ajure.speed = SecondHouseManager.secondHouse_instance.ajure.savedSpeed;
        SecondHouseManager.secondHouse_instance.ajure.acceleration = SecondHouseManager.secondHouse_instance.ajure.savedAcceleration;

        StopCoroutine(coroutine);
        coroutine = null;
        if(!talked) talked = true;
        GameManager.m_instance.stopSwitch = false;
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
            yield return null;
            showMessage(messages[i], names[i], image[i]);
            yield return new WaitUntil(() => (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)));
        }
        calCanvas.gameObject.SetActive(true);
        calender.gameObject.SetActive(true);
        if(firstSelect == null)yield break;
        EventSystem.current.SetSelectedGameObject(firstSelect);
        yield break;

    }
    private async UniTask ActiveSeiitirouCalender()
    {
        GameManager.m_instance.stopSwitch = true;
        await MessageManager.message_instance.MessageWindowActive(seiitirouMessages,seiitirouNames,seiitirouImages ,ct: destroyCancellationToken);
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
        calCanvas.gameObject.SetActive(true);
        seiCalender.gameObject.SetActive(true);
        if(seiFirstSelect == null)
        {
            await UniTask.WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
            seiCalender.gameObject.SetActive(false);
            calCanvas.gameObject.SetActive(false);
            GameManager.m_instance.stopSwitch = false;
            return;
        }
        EventSystem.current.SetSelectedGameObject(seiFirstSelect);
        await UniTask.WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
        seiCalender.gameObject.SetActive(false);
        calCanvas.gameObject.SetActive(false);
        if(!talked) talked = true;
        GameManager.m_instance.stopSwitch = false;
    }
    private void Update()
    {
        if (isContacted && messageSwitch == false && (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)))
        {
            messageSwitch = true;
            GameManager.m_instance.stopSwitch = true;
            coroutine = CreateCoroutine();
            StartCoroutine(coroutine);
        }
        else if(seiIsContacted && messageSwitch == false && seiCalender != null && (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)))
        {
            messageSwitch = true;
            GameManager.m_instance.stopSwitch = true;
            ActiveSeiitirouCalender().Forget();
        }
        else if(seiIsContacted && messageSwitch == false && seiCalender == null && (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)))
        {
            messageSwitch = true;
            return;
        }
        if (calender.gameObject.activeSelf)
        {
            GameManager.m_instance.stopSwitch = true;
            Time.timeScale = 0.0f;
            if(Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return))
            {
                calender.gameObject.SetActive(false);
                calCanvas.gameObject.SetActive(false);
                Time.timeScale = 1.0f;
                Homing.m_instance.speed = 2 + GameManager.m_instance.difficultyLevelManager.addEnemySpeed;
                SecondHouseManager.secondHouse_instance.ajure.speed = SecondHouseManager.secondHouse_instance.ajure.savedSpeed;
                SecondHouseManager.secondHouse_instance.ajure.acceleration = SecondHouseManager.secondHouse_instance.ajure.savedAcceleration;
                GameManager.m_instance.stopSwitch = false;
            }
        }
    }
}

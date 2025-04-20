using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MessageManager : MonoBehaviour
{
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<Sprite> image;
    public Canvas window;
    public Canvas SelectCanvas;
    public Text target;
    public Text nameText;
    public Image characterImage;
    public Image selection;
    public SoundManager soundManager;
    public AudioClip selectionBGM;
    public GameObject firstSelect;
    private Item selectedItem;
    public static MessageManager message_instance;
    public bool talking = false;
    public bool isTalking = false;
    public bool isOpenSelect = false;
    public bool isTextAdvanceEnabled = true;

    //ここでメッセージスクリプトを呼び出すスクリプトを作成する
    private void Awake()
    {
        if(message_instance == null)
        {
            message_instance = this;
        }
        else
        {
            Destroy(message_instance);
        }
    }
    public async UniTask MessageCoroutine(CancellationToken ct)
    {
        await UniTask.Delay(1, cancellationToken: ct);
        isTalking = true;
        //if文でコルーチンがあるかどうかの条件式を作る
        //メッセージ中だよってboolを作る（IEnumerator内で）
        window.gameObject.SetActive(true);
        await OnAction(ct);

        target.text = "";
        GameManager.m_instance.ImageErase(characterImage);
        window.gameObject.SetActive(false);
        //再び話せるようになるためのスイッチ
        talking = false;
        Refrigerator.messageSwitch = false;
        PlayerManager.m_instance.m_speed = 0.075f;
        Homing.m_instance.speed = 2 + GameManager.m_instance.difficultyLevelManager.addEnemySpeed;
        SecondHouseManager.secondHouse_instance.ajure.speed = SecondHouseManager.secondHouse_instance.ajure.savedSpeed;
        SecondHouseManager.secondHouse_instance.ajure.acceleration = SecondHouseManager.secondHouse_instance.ajure.savedAcceleration;
        Debug.Log(SecondHouseManager.secondHouse_instance.ajure.speed);
        Time.timeScale = 1f;
        await UniTask.DelayFrame(1,cancellationToken : ct);
        isTalking = false;
    }
    public async UniTask OnceMessageCoroutine(CancellationToken ct)
    {
        window.gameObject.SetActive(true);
        await OnAction(ct);

        target.text = "";
        GameManager.m_instance.ImageErase(characterImage);
        window.gameObject.SetActive(false);
        PlayerManager.m_instance.m_speed = 0.075f;
        Homing.m_instance.speed = 2 + GameManager.m_instance.difficultyLevelManager.addEnemySpeed;
        SecondHouseManager.secondHouse_instance.ajure.speed = SecondHouseManager.secondHouse_instance.ajure.savedSpeed;
        SecondHouseManager.secondHouse_instance.ajure.acceleration = SecondHouseManager.secondHouse_instance.ajure.savedAcceleration;
        Debug.Log(SecondHouseManager.secondHouse_instance.ajure.speed);
        Time.timeScale = 1f;
        await UniTask.DelayFrame(1, cancellationToken: ct);
        isTalking = false;
    }
    private async UniTask SelectionMessages(CancellationToken ct)
    {
        isTalking = true;
        window.gameObject.SetActive(true);
        for(int i = 0; i < messages.Count; ++i)
        {
            await UniTask.DelayFrame(1, cancellationToken: ct);
            showMessage(messages[i], names[i], image[i]);
            if(i == messages.Count - 1)
            {
                soundManager.PlayBgm(selectionBGM);
                SelectCanvas.gameObject.SetActive(true);
                selection.gameObject.SetActive(true);
                EventSystem.current.SetSelectedGameObject(firstSelect);
                isOpenSelect = true;
                break;
            }
            await UniTask.WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return), cancellationToken:ct) ;
        }
        await UniTask.WaitUntil(() => !isOpenSelect, cancellationToken: ct);
        target.text = "";
        GameManager.m_instance.ImageErase(characterImage);
        window.gameObject.SetActive(false);
        isTalking = false;
    }
    protected void showMessage(string message, string name ,Sprite image)
    {
        target.text = message;
        nameText.text = name;
        characterImage.sprite = image;
    }
    async UniTask OnAction(CancellationToken ct)
    {
        for(int i = 0; i < messages.Count; ++i)
        {
            await UniTask.DelayFrame(5,cancellationToken :ct);
            showMessage(messages[i], names[i], image[i]);
            await UniTask.WaitUntil(() => (isTextAdvanceEnabled && (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return))), cancellationToken: ct);
        }
    }
    /*（）の中に引数をいれるその引数の中身はTest1.csが渡してきている
    受け取る側ではList<string>まで型を書いて*/
    public async UniTask MessageWindowActive(List<string>messages,List<string>names, List<Sprite> image,bool msgSwitch = true /*←ここで初期化されてるから弄ってほしくないなら引数でfalseを与えておく*/,CancellationToken ct = default)
    {
        //コルーチンは始まる前に一回はじかれる
        if(isTalking) return;
        Debug.Log("MessageWindowActive メソッド開始");
        this.messages = messages;
        this.names = names;
        this.image = image;
        talking = msgSwitch;
        Debug.Log("MessageCoroutine メソッド呼び出し前");
        await MessageCoroutine(ct);
        Debug.Log("MessageCoroutine メソッド呼び出し後");
    }
    /*疑問点まとめ
    １受け取る側の引数の名前がmsgとnamだけどこれどっからとってきてるの？
    →あくまでもラベル。アドレスを入れる箱のようなもので名前は関係なしメンバー変数が生成された時点でアドレスが生まれる。
    よってTest.csで渡すのは文字列ではなくアドレスのみ。Test2やTest3が出てきても受け取る側の引数は箱なのでそのままで良し*/
    public async UniTask MessageWindowOnceActive(List<string> messages, List<string> names, List<Sprite> image , CancellationToken ct = default)
    {
        if(isTalking) return;
        this.messages = messages;
        this.names = names;
        this.image = image;
        await OnceMessageCoroutine(ct);
    }
    // 選択肢がある時用のメソッド。必要に応じて必要な引数が変わるだろうからオーバーロード(アイテム用とか)を用意しておく。
    public async UniTask MessageSelectWindowActive(List<string> messages, List<string> names, List<Sprite> image,Canvas canvas,Image sel,GameObject firstsel,AudioClip bgm = null,CancellationToken ct = default)
    {
        if(isTalking) return;
        this.messages = messages;
        this.names = names;
        this.image = image;
        SelectCanvas = canvas;
        selection = sel;
        firstSelect = firstsel;
        selectionBGM = bgm;
        await SelectionMessages(ct);
    }
}

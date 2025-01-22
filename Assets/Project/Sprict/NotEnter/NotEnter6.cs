using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using static UnityEngine.GraphicsBuffer;

public class NotEnter6 : MonoBehaviour
{
    // 鍵がないと入れないドアのためのスクリプト
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<Sprite> images;
    [SerializeField]
    private List<string> messages2;
    [SerializeField]
    private List<string> names2;
    [SerializeField]
    private List<Sprite> images2;
    [SerializeField]
    private List<string> messages3;
    [SerializeField]
    private List<string> names3;
    [SerializeField]
    private List<Sprite> images3;
    [SerializeField]
    private List<string> advancemessages;
    [SerializeField]
    private List<string> advancenames;
    [SerializeField]
    private List<Sprite> advanceimages;
    [SerializeField]
    private List<string> rescuemessages;
    [SerializeField]
    private List<string> rescuenames;
    [SerializeField]
    private List<Sprite> rescueimages;
    [SerializeField]
    private List<string> messages4;
    [SerializeField]
    private List<string> names4;
    [SerializeField]
    private List<Sprite> images4;

    public Canvas window;
    public Text target;
    public Text nameText;
    public Image characterImage;
    public Image redScreen;
    public Canvas choicePanel;
    public Image notEnter6;

    public bool getKey;
    public bool toevent5;
    public bool choiced;
    public bool rescued;
    public bool seiitirouFlag;
    public bool cameraSwitch = false;

    private IEnumerator coroutine;
    public ItemDateBase itemDateBase;
    public Inventry inventry;
    public GameTeleportManager gameTeleportManager;
    public ItemSprictW itemSprictW;
    public Item underKey;
    public AudioClip fearBGM;
    public AudioClip scream;
    public AudioClip heartSound;
    public SoundManager soundManager;
    public RescueEvent rescueEvent;

    private int heartCounts;
    private float redNum = 0.0f;

    public GameObject player;
    public GameObject seiitirou;
    public GameObject enemy;
    public GameObject firstSelection;
    private CancellationTokenSource heartSoundCTS;

    private void Start()
    {
        redScreen.color = Color.clear;
        heartSoundCTS = new CancellationTokenSource();
    }
    private async void OnTriggerEnter2D(Collider2D collider)
    {
        if(getKey == false && collider.gameObject.tag.Equals("Player")) MessageManager.message_instance.MessageWindowActive(messages, names, images, ct: destroyCancellationToken).Forget();
        else if(getKey  == true)
        {
            if (collider.gameObject.tag.Equals("Player"))
            {
                await ToEvent5();
            }
        }
        if(collider.gameObject.tag.Equals("Seiitirou"))
        {
            if(underKey.checkPossession == false)
            {
                MessageManager.message_instance.MessageWindowActive(messages4, names4, images4, ct: destroyCancellationToken).Forget();
                seiitirouFlag = true;
                enemy.transform.position = new Vector2(0, 0);
                enemy.gameObject.SetActive(false);
            }
            else seiitirou.transform.position = new Vector3(128, 25, 0);
        }
    }
    async UniTask ToEvent5()
    {
        if(!choiced)
        {
            GameManager.m_instance.stopSwitch = true;
            Homing.m_instance.speed = 0;

            soundManager.PlaySe(scream);
            await UniTask.Delay(TimeSpan.FromSeconds(1.3f));
            await MessageManager.message_instance.MessageWindowActive(messages2, names2, images2, ct: destroyCancellationToken);

            //選択肢を出したりBGMを付ける。画面を揺らす？とか近づけたりしていろいろいじくる

            //選択肢が選ばれる前に会話が終わらないため下に行かない。
            await OnPanel1();
            heartSoundCTS = new CancellationTokenSource();
            HeartSounds(heartSoundCTS.Token).Forget(e => { Debug.Log("キャンセルされた"); });
            soundManager.PlayBgm(fearBGM);
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
            cameraSwitch = true;
            Homing.m_instance.speed = 2;
        }
        else MessageManager.message_instance.MessageWindowActive(messages3, names3, images3, ct: destroyCancellationToken).Forget();
    }
    IEnumerator ToResqueEvent()
    {
        rescueEvent.gameObject.SetActive(true);
        soundManager.StopBgm(fearBGM);
        heartCounts = 1000;
        cameraSwitch = false;
        notEnter6.gameObject.SetActive(false);
        choicePanel.gameObject.SetActive(false);
        heartSoundCTS.Cancel();
        soundManager.StopSe(heartSound);
        redScreen.gameObject.SetActive(false);
        GameManager.m_instance.stopSwitch = false;
        choiced = true;
        rescued = true;
        window.gameObject.SetActive(true);
        yield return OnMessage3();
        target.text = "";
        window.gameObject.SetActive(false);
        if (itemDateBase.GetItemId(301).checkPossession == true)
        {
            itemSprictW.ItemDelete();
        }
        else
        {
            yield break;
        }
        StopCoroutine(coroutine);
    }
    protected void showMessage(string message, string name, Sprite image)
    {
        target.text = message;
        nameText.text = name;
        characterImage.sprite = image;
    }

    async UniTask OnPanel1()
    {
        choicePanel.gameObject.SetActive(true);
        notEnter6.gameObject.SetActive(true);
        await UniTask.WaitUntil(() => choiced == false);
    }
    IEnumerator OnMessage3()
    {
        for(int i = 0; i < rescuemessages.Count; ++i)
        {
            yield return null;
            showMessage(rescuemessages[i], rescuenames[i], rescueimages[i]);
            yield return new WaitUntil(() => (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)));
        }
        if (cameraSwitch == true) cameraSwitch = false;
        yield break;
    }
    private IEnumerator Red()
    {
        while(redNum < 0.7)
        {
            redScreen.color = new Color(0.7f, 0, 0, redNum);
            redNum += 0.001f;
            yield return null;
        }
    }
    private async UniTask HeartSounds(CancellationToken ct)
    {
        while(heartCounts < 1000)
        {
            soundManager.PlaySe(heartSound);
            heartCounts++;
            try
            {
                await UniTask.Delay(TimeSpan.FromSeconds(1f), cancellationToken: ct);
            }
            catch(OperationCanceledException)
            {
                break;
            }
        }
    }
    public void OnAdvanceBotton()
    {
        // 進むボタンを押した時のメソッド要素として音をすべて消す、メッセージ、TP、戻れない。
        MessageManager.message_instance.MessageWindowActive(advancemessages, advancenames, advanceimages, ct: destroyCancellationToken).Forget();
        //ハートの変数を１０００にしてカメラのズームを元に戻せばよいレスキューも同じ
        //あとは選択肢表示の時のウィンドウの表示が怪しいからちゃんと非表示にしておくそして自由に動けるようになった後選択肢も非表示にする。
        heartCounts = 1000;
        cameraSwitch = false;
        notEnter6.gameObject.SetActive(false);
        choicePanel.gameObject.SetActive(false);
        heartSoundCTS.Cancel();
        soundManager.StopSe(heartSound);
        redScreen.gameObject.SetActive(false);
        choiced = true;
        player.transform.position = new Vector3(128, 25, 0);
        enemy.transform.position = new Vector2(0, 0);
        enemy.gameObject.SetActive(false);
        gameTeleportManager.soundManager.StopBgm(gameTeleportManager.toevent3.chasedBGM);
        Homing.m_instance.enemyEmerge = false;
        GameManager.m_instance.stopSwitch = false;
        inventry.Delete(itemDateBase.GetItemId(253));
        soundManager.StopBgm(fearBGM);
    }
    public void OnRescueBotton()
    {
        // 助けに行くボタンを押したときのメソッド要素として音をすべて消す、メッセージ、行けない
        coroutine = ToResqueEvent();
        StartCoroutine(coroutine);
    }
}

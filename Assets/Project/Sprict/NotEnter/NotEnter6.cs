using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public Canvas window;
    public Text target;
    public Text nameText;
    public Image characterImage;
    public Canvas choicePanel;
    public bool getKey;
    public bool toevent5;
    public bool choiced;
    private IEnumerator coroutine;
    public ItemDateBase itemDateBase;
    public AudioClip fearBGM;
    public AudioClip scream;
    public AudioClip heartSound;
    AudioSource audioSound;
    public AudioSource screamSound;
    public Image redScreen;
    private int heartCounts;
    public bool cameraSwitch = false;
    private float redNum = 0.0f;
    public GameObject player;

    private void Start()
    {
        audioSound = GetComponent<AudioSource>();
        redScreen.color = Color.clear;
    }
    private void Update()
    {
        //Debug.Log(heartCounts);
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(getKey == false)
        {
            if(collider.gameObject.tag.Equals("Player"))
            {
                MessageManager.message_instance.MessageWindowActive(messages, names, images);
            }
        }
        if(getKey  == true)
        {
            coroutine = ToEvent5();
            StartCoroutine(coroutine);
            ToEvent5();
        }
    }
    IEnumerator ToEvent5()
    {
        if(!choiced)
        {
            PlayerManager.m_instance.m_speed = 0;
            Homing.m_instance.speed = 0;

            screamSound.PlayOneShot(scream);
            yield return new WaitForSeconds(1.3f);
            //Time.timeScale = 0.0f;
            window.gameObject.SetActive(true);
            yield return OnMessage1();

            //　選択肢を出したりBGMを付ける。画面を揺らす？とか近づけたりしていろいろいじくる

            yield return OnPanel1();
            StartCoroutine(HeartSounds());
            audioSound.PlayOneShot(fearBGM);
            yield return new WaitForSeconds(0.5f);
            cameraSwitch = true;
            yield return Red();


            //window.gameObject.SetActive(true);
            //yield return OnMessage2();

            target.text = "";
            window.gameObject.SetActive(false);

            PlayerManager.m_instance.m_speed = 0.075f;
            Homing.m_instance.speed = 2;
            StopCoroutine(coroutine);
            coroutine = null;
        }
        else
        {
            window.gameObject.SetActive(true);
            yield return OnMessage2();
            window.gameObject.SetActive(false);
            PlayerManager.m_instance.m_speed = 0.075f;
            Homing.m_instance.speed = 2;
        }
    }
    protected void showMessage(string message, string name, Sprite image)
    {
        target.text = message;
        nameText.text = name;
        characterImage.sprite = image;
    }

    IEnumerator OnMessage1()
    {
        for(int i = 0; i < messages2.Count; ++i)
        {
            // 1フレーム分 処理を待機(下記説明1)
            yield return null;
            showMessage(messages2[i], names2[i], images2[i]);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        }
        yield break;
    }
    IEnumerator OnPanel1()
    {
        choicePanel.gameObject.SetActive(true);
        yield return new WaitUntil(() => choiced == false);
    }
    IEnumerator OnMessage2()
    {
        for(int i = 0; i < messages3.Count; ++i)
        {
            PlayerManager.m_instance.m_speed = 0;
            Homing.m_instance.speed = 0;
            // 1フレーム分 処理を待機(下記説明1)
            yield return null;
            showMessage(messages3[i], names3[i], images3[i]);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        }
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
    private IEnumerator HeartSounds()
    {
        while(heartCounts < 1000)
        {
            audioSound.PlayOneShot(heartSound);
            heartCounts++;
            yield return new WaitForSeconds(1f);
        }
    }
    public void OnAdvanceBotton()
    {
        // 進むボタンを押した時のメソッド要素として音をすべて消す、メッセージ、TP、戻れない。
        MessageManager.message_instance.MessageWindowActive(advancemessages, advancenames, advanceimages);
        //ハートの変数を１０００にしてカメラのズームを元に戻せばよいレスキューも同じ
        //あとは選択肢表示の時のウィンドウの表示が怪しいからちゃんと非表示にしておくそして自由に動けるようになった後選択肢も非表示にする。
        heartCounts = 1000;
        cameraSwitch = false;
        choicePanel.gameObject.SetActive(false);
        StopCoroutine(HeartSounds());
        audioSound.Stop();
        redScreen.gameObject.SetActive(false);
        choiced = true;
        player.transform.position = new Vector3(128, 25, 0);
    }
    public void OnRescueBotton()
    {
        // 助けに行くボタンを押したときのメソッド要素として音をすべて消す、メッセージ、行けない
        MessageManager.message_instance.MessageWindowActive(rescuemessages, rescuenames, rescueimages);
        heartCounts = 1000;
        cameraSwitch = false;
        choicePanel.gameObject.SetActive(false);
        StopCoroutine(HeartSounds());
        audioSound.Stop();
        redScreen.gameObject.SetActive(false);
        choiced = true;
    }
}

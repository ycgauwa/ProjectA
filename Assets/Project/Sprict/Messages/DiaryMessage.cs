using System.Collections;
using System.Collections.Generic;
//using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class DiaryMessage : MonoBehaviour
{
    //やりたいこと
    //調べてボタン押したらメッセージが表示される。そのあとに日記が表示されてからメッセージが出る。
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<string> sentences;
    [SerializeField]
    private List<string> dates;
    [SerializeField]
    private List<Sprite> image;
    public Canvas diaryWindow;
    public Canvas window;
    public Text target;
    public Text nameText;
    public Text sentence;
    public Text date;
    public Image characterImage;
    public Image diary;
    public Image panel;
    private IEnumerator coroutine;
    private bool isContacted = false;
    public Homing homing;
    public SoundManager soundManager;
    public PlayerManager playerManager;
    public AudioClip pageSound;
    public AudioClip pageTojiSound;
    private void Start()
    {

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
        if(isContacted == true && coroutine == null && diaryWindow.gameObject.activeInHierarchy == false)
        {
            //話しかける(条件は動的なものと今回のboolのように恒常的なもので分けた方がいい)
            if(Input.GetKeyDown(KeyCode.Return))
            {
                coroutine = WindowAction();
                PlayerManager.m_instance.m_speed = 0;
                // コルーチンの起動(下記説明2)
                StartCoroutine(coroutine);
            }
        }
    }
    protected void showMessage(string message, string name)
    {
        target.text = message;
        nameText.text = name;
    }
    protected void showDiaryMessage(string message, string name)
    {
        sentence.text = message;
        date.text = name;
    }
    IEnumerator WindowAction()
    {
        //話しかけるとまずテキストで表示。
        window.gameObject.SetActive(true);
        for(int i = 0; i < messages.Count; ++i)
        {
            // 1フレーム分 処理を待機(下記説明1)
            yield return null;
            // 会話をwindowのtextフィールドに表示
            showMessage(messages[i], names[i]);
            yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
        }
        window.gameObject.SetActive(false);
        
        //何回か押すとテキストが消えて、日記の表示がされる。
        diaryWindow.gameObject.SetActive(true);
        
        for(int i = 0; i < sentences.Count; ++i)
        {
            playerManager.playerstate = PlayerManager.PlayerState.Talk;
            // 1フレーム分 処理を待機(下記説明1)
            yield return null;
            // 会話をwindowのtextフィールドに表示
            showDiaryMessage(sentences[i], dates[i]);
            soundManager.PlaySe(pageSound);
            yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
            if (i == sentences.Count - 1)
            {
                soundManager.PlaySe(pageTojiSound);
            }

        }
        diaryWindow.gameObject.SetActive(false);
        playerManager.playerstate = PlayerManager.PlayerState.Idol;
        coroutine = null;
        target.text = "";
        homing.speed = 2;
        yield break;
    }
}

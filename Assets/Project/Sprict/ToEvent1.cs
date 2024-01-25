using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ToEvent1 : MonoBehaviour
{
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<Sprite> images;
    public Canvas window;
    public Text target;
    public Text nameText;
    public Image Chara;
    public static bool one;
    public GameObject player;
    private IEnumerator coroutine;
    // Start is called before the first frame update
    void Start()
    {
        one = false;
    }

    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"colloder: {other.gameObject.name} ");

        //一回しか作動しないための仕組み
        if (!one)
        {
            StartCoroutine(Event1());
            one = true;
        }
       
    }
    //イベント１のためのコルーチン。大枠の役割を果たしてくれる。
    IEnumerator Event1()
    {
        
        yield return new WaitForSeconds(1);
        player.transform.position = new Vector3(-33, -34, 0);
        //プレイヤーの固定（実力不足のため別のクラスのメソッドを呼び出している）
        //PlayerManager.m_instance.Event1();
        PlayerManager.m_instance.m_speed = 0;
        coroutine = CreateCoroutine();
        // コルーチンの起動(下記説明2)
        StartCoroutine(coroutine);
        
    }

    public IEnumerator CreateCoroutine()
    {
        // window起動
        window.gameObject.SetActive(true);

        // 抽象メソッド呼び出し 詳細は子クラスで実装
        yield return OnAction();

        // window終了
        this.target.text = "";
        this.window.gameObject.SetActive(false);

        StopCoroutine(coroutine);
        coroutine = null;
        Debug.Log("hhh");
        PlayerManager.m_instance.m_speed = 0.075f;


    }
    protected void showMessage(string message,string name, Sprite image)
    {
        this.target.text = message;
        this.nameText.text = name;
        Chara.sprite = image;
    }

    IEnumerator OnAction()
    {

        for(int i = 0; i < messages.Count; ++i)
        {
            // 1フレーム分 処理を待機(下記説明1)
            yield return null;

            // 会話をwindowのtextフィールドに表示
            showMessage(messages[i], names[i], images[i]);


            // キー入力を待機 (下記説明1)
            yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
        }

        yield break;

    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotEnter2 : MonoBehaviour
{
    // これは単純にどんな条件であってもエリアに入ったらメッセージウィンドウが出てくる仕組みにする。

    /*条件を満たさずに触れるとメッセージAが出て動けなくなる仕組みの作成
    ToEventクラスのone変数の条件によってこっちのクラスのメソッドが動くか動かないかを作る
    TPする前にウィンドウの表示→座標の固定をする。
    変数の引き渡しは引数を使って行うToEvent→NotEnterで渡す*/
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    public Canvas window;
    public Text target;
    public Text nameText;
    private IEnumerator coroutine;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
        {
            PlayerManager.m_instance.m_speed = 0;
            coroutine = CreateCoroutine();
            StartCoroutine(coroutine);
        }
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
        PlayerManager.m_instance.m_speed = 0.075f;


    }
    protected void showMessage(string message, string name)
    {
        this.target.text = message;
        this.nameText.text = name;
    }

    IEnumerator OnAction()
    {

        for(int i = 0; i < messages.Count; ++i)
        {
            // 1フレーム分 処理を待機(下記説明1)
            yield return null;

            // 会話をwindowのtextフィールドに表示
            showMessage(messages[i], names[i]);


            // キー入力を待機 (下記説明1)
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        }

        yield break;

    }
}

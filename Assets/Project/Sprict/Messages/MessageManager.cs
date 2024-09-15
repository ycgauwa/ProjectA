using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public Text target;
    public Text nameText;
    public Image characterImage;
    public static MessageManager message_instance;
    private Coroutine coroutine;
    public static bool talking = false;

    //ここでメッセージスクリプトを呼び出すスクリプトを作成する
    void Start()
    {
        message_instance = this;
    }
    public IEnumerator MessageCoroutine()
    {
        //if文でコルーチンがあるかどうかの条件式を作る
        //メッセージ中だよってboolを作る（IEnumerator内で）
        // window起動
        window.gameObject.SetActive(true);

        // 抽象メソッド呼び出し 詳細は子クラスで実装
        yield return OnAction();

        // window終了
        target.text = "";
        window.gameObject.SetActive(false);
        Test1.messageSwitch = false;
        ItemGet2.messageSwitch = false;
        NotEnterLadder.messageSwitch = false;
        Case1Object.messageSwitch = false;
        Refrigerator.messageSwitch = false;

        PlayerManager.m_instance.m_speed = 0.075f;
        Homing.m_instance.speed = 2;
        Time.timeScale = 1f;
        talking = false;
        //StopCoroutine(coroutine);
        coroutine = null;
    }
    protected void showMessage(string message, string name ,Sprite image)
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

            // キー入力を待機 (下記説明1)
            yield return new WaitUntil(() => (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)));
        }

        yield break;

    }
    /*（）の中に引数をいれるその引数の中身はTest1.csが渡してきている
    受け取る側ではList<string>まで型を書いて*/
    public void MessageWindowActive(List<string>messages,List<string>names, List<Sprite> image = null, AudioClip sound = null )
    {
        //ここでコルーチンが歩かないかの条件
        if(coroutine != null)
        {
            StopCoroutine(coroutine); 
        }
        this.messages = messages;
        this.names = names;
        this.image = image;
        coroutine = StartCoroutine(MessageCoroutine());
        if(sound != null)
        {
            GetComponent<AudioSource>().PlayOneShot(sound);
        }
    }
    /*疑問点まとめ
    １受け取る側の引数の名前がmsgとnamだけどこれどっからとってきてるの？
    →あくまでもラベル。アドレスを入れる箱のようなもので名前は関係なしメンバー変数が生成された時点でアドレスが生まれる。
    よってTest.csで渡すのは文字列ではなくアドレスのみ。Test2やTest3が出てきても受け取る側の引数は箱なのでそのままで良し
    ２Test1のスクリプトのDebug.Logが始まった瞬間２回呼び出される。
    ３そもそもStart関数じゃないのに始まった瞬間呼ばれるのなぜ？
    →そもそもタイルマップの当たり判定が２つ重なっていたため始まった瞬間に２回呼ばれてしまう
    ToEvent1の関数で呼ばれなかったのは単純に当たり判定がなかったから。
    ４Test.csを作ってからPlayerMessageの機能がしなくなった
    →なぜか分からないけどセリフがすべて消えてた。変数名を変えるとプログラム側が追跡できなくなる！
    ５PlayeraMessageのstart関数も２回反応している
    ６５番に関してstart関数でなくすべて２回となっている。おそらくPlayeraMessageと何かが干渉を起こして２回ずつ動く形になってしまっている。
    →５と６は単純にPlayerMessageが2つあるから2回呼ばれているだけ*/
}

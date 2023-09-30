using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * フィールドオブジェクトの基本処理
 */
public abstract class FieldObjectBase : MonoBehaviour
{

    // Unityのインスペクタ(UI上)で、前項でつくったオブジェクトをバインドする。
    // （次項 : インスペクタでscriptを追加して、設定をする で説明）
    public Canvas window;
    public Text target;
    public Text charaname;

    // 接触判定
    private bool isContacted = false;
    private IEnumerator coroutine;


    // colliderをもつオブジェクトの領域に入ったとき(下記で説明1)
    private void OnTriggerEnter2D(Collider2D collider)
    {
        isContacted = collider.gameObject.tag.Equals("Player");
    }

    // colliderをもつオブジェクトの領域外にでたとき(下記で説明1)
    private void OnTriggerExit2D(Collider2D collider)
    {
        isContacted = !collider.gameObject.tag.Equals("Player");
    }

    private void Update()
    {
        if (isContacted && coroutine == null && Input.GetButton("Submit") && Input.GetKeyDown(KeyCode.Return))
        {
            coroutine = CreateCoroutine();
            PlayerManager.m_instance.m_speed = 0;
            // コルーチンの起動(下記説明2)
            StartCoroutine(coroutine);
        }
    }

    /**
     * リアクション用コルーチン(下記で説明2)
     */
    private IEnumerator CreateCoroutine()
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
        PlayerManager.m_instance.m_speed = 0.05f;
    }

    protected abstract IEnumerator OnAction();

    /**
     * メッセージを表示する
     */
    protected void showMessage(string message,string name)
    {
        this.target.text = message;
        this.charaname.text = name;
    }
}
public class MessageCharactor : FieldObjectBase
{

    // セリフ : Unityのインスペクタ(UI上)で会話文を定義する 
    // （次項 : インスペクタでscriptを追加して、設定をする で説明）
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> chara;

    // 親クラスから呼ばれるコールバックメソッド (接触 & ボタン押したときに実行)
    protected override IEnumerator OnAction()
    {

        for (int i = 0; i < messages.Count; ++i)
        {
            // 1フレーム分 処理を待機(下記説明1)
            yield return null;

            // 会話をwindowのtextフィールドに表示
            showMessage(messages[i], chara[i]);

            // キー入力を待機 (下記説明1)
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        }

        yield break;
    }
}
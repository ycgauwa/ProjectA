using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
//using static UnityEditor.Progress;

/**
 * フィールドオブジェクトの基本処理
 */
public class MessageCharactor : MonoBehaviour
{

    // Unityのインスペクタ(UI上)で、前項でつくったオブジェクトをバインドする。
    // （次項 : インスペクタでscriptを追加して、設定をする で説明）
    //　特定のフラグを回収した状態で話しかけると会話内容が変わる。
    //　またインデックスをランダムでとることで会話内容に変化を持たせる。
    
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private string charactername;
    [SerializeField]
    private List<Sprite> images;
    public Canvas window;
    public Text target;
    public Image Chara;
    public Text charaname;
    private Sprite charaImage;
    public Character character;
    private IEnumerator coroutine;
    public NotEnter1 notEnter1;
    public NotEnter4 notEnter4;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
        {
            PlayerManager.m_instance.m_speed = 0;
            coroutine = CreateCoroutine();
            StartCoroutine(coroutine);
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
        {
            
        }
    }
    public IEnumerator CreateCoroutine()
    {
        // window起動
        window.gameObject.SetActive(true);

        // 抽象メソッド呼び出し 詳細は子クラスで実装
        yield return CharaShowMessage();

        // window終了
        target.text = "";
        window.gameObject.SetActive(false);

        StopCoroutine(coroutine);
        coroutine = null;
        PlayerManager.m_instance.m_speed = 0.075f;

    }

    protected void showMessage(string message, string name, Sprite image)
    {
        target.text = message;
        charaname.text = name;
        Chara.sprite = image;
    }

    /*イベントを回収するごとに話す内容を変更する。
    bool変数で条件を変更して話してほしい状況ならそのままShow Message
    違うなら最初の文はContinueで後回しにしてあげる
    会話内容ごとにリストを作ってあげたらいいのでは？
    特定の条件の時にはList1、別の条件ではList2を呼び出すことによってListを呼び出しながらも会話内容を変えることができる
    今悩んでいるのは、foreachを使った場合stringのListは回してくれるけどImageのListはどうなるの？ってはなし
    でもメッセージと画像は一対一対応してるから別にforeachじゃなくてもよくね？*/

    IEnumerator CharaShowMessage()
    {
        // 学校にいる間にしゃべらされる内容
        if(notEnter1.one == false)
        {
            charactername = character.charaName;
            images = character.characterImages1;
            messages = character.messageTexts1;
            int i = 0;
            // 要素の数だけループが行われる。
            foreach(string str in messages)
            {
                yield return null;
                showMessage(str, charactername, images[i]);
                i++;
                yield return new WaitUntil(() => (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)));
            }
            yield break;
        }
        else if(notEnter4.getKey1 == true)//居間に行けるようになってからの内容
        {
            charactername = character.charaName;
            images = character.characterImages2;
            messages = character.messageTexts2;
            int i = 0;
            // 要素の数だけループが行われる。
            foreach(string str in messages)
            {
                yield return null;
                showMessage(str, charactername, images[i]);
                i++;
                yield return new WaitUntil(() => (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)));
            }
            yield break;
        }
    }
    IEnumerator OnAction()
    {
        int i = 0;
        charactername = character.charaName;
        //　家ごとにセリフを割り当てる。
        if(notEnter1.one == false)
        {
            for(i = 0; i < messages.Count; ++i)
            {
                messages[i] = character.messageTexts1[i];
                images = character.characterImages1;
                // 1フレーム分 処理を待機(下記説明1)
                yield return null;

                // 会話をwindowのtextフィールドに表示
                showMessage(messages[i], charactername, images[i]);
                yield return new WaitUntil(() => (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)));
            }
            yield break;
        }
        else if(notEnter4.getKey1 == true)
        {
            for(i = 0; i < messages.Count; ++i)
            {
                messages[i] = character.messageTexts2[i];
                images = character.characterImages2;
                // 1フレーム分 処理を待機(下記説明1)
                yield return null;

                // 会話をwindowのtextフィールドに表示
                showMessage(messages[i], charactername, images[i]);

                yield return new WaitUntil(() => (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)));
            }
            yield break;
        }
        yield break;

    }
}
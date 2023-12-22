using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private Sprite images;
    public Canvas window;
    public Text target;
    public Image Chara;
    public Text charaname;
    private Sprite charaImage;
    public Character character;
    private IEnumerator coroutine;


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
        yield return OnAction();

        // window終了
        target.text = "";
        window.gameObject.SetActive(false);

        StopCoroutine(coroutine);
        coroutine = null;
        PlayerManager.m_instance.m_speed = 0.05f;

    }

    protected void showMessage(string message, string name, Sprite image)
    {
        target.text = message;
        charaname.text = name;
        Chara.sprite = image;
    }

    IEnumerator OnAction()
    {
        int i = 0;
        charactername = character.charaName;
        charaImage = character.characterImages[0];
        for(i = 0; i < messages.Count; ++i)
        {
            messages[i] = character.messageTexts[i];
            // 1フレーム分 処理を待機(下記説明1)
            yield return null;

            // 会話をwindowのtextフィールドに表示
            showMessage(messages[i], charactername, charaImage);

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        }
        yield break;

    }
}
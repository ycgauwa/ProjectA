using System.Collections;
using System.Collections.Generic;
//using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class DiaryMessage : MonoBehaviour
{
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<string> sentences;
    [SerializeField]
    private List<string> dates;
    public Canvas diaryWindow;
    public Canvas window;
    public Text target;
    public Text nameText;
    public Text sentence;
    public Text date;
    private IEnumerator coroutine;
    private bool isContacted = false;
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
        //話しかける(条件は動的なものと今回のboolのように恒常的なもので分けた方がいい)
        if(Input.GetKeyDown(KeyCode.Return))
        {
            if(isContacted == true && coroutine == null && diaryWindow.gameObject.activeInHierarchy == false)
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
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        }
        window.gameObject.SetActive(false);
        
        //何回か押すとテキストが消えて、日記の表示がされる。
        diaryWindow.gameObject.SetActive(true);
        for(int i = 0; i < sentences.Count; ++i)
        {
            // 1フレーム分 処理を待機(下記説明1)
            yield return null;
            // 会話をwindowのtextフィールドに表示
            showDiaryMessage(sentences[i], dates[i]);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        }
        diaryWindow.gameObject.SetActive(false);
        PlayerManager.m_instance.m_speed = 0.075f;
        coroutine = null;
        yield break;
    }
}

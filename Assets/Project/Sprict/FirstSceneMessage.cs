using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField]
    private List<string> messages;
    public Canvas window;
    public Text target;
    private IEnumerator coroutine;


    // Start is called before the first frame update
    void Start()
    {
        
    }
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
        SceneManager.LoadScene("Game");

    }
    private void FixedUpdate()
    {
        if (coroutine == null && Input.GetButton("Submit") && Input.anyKeyDown)
        {
            coroutine = CreateCoroutine();
            // コルーチンの起動(下記説明2)
            StartCoroutine(coroutine);
        }
    }
    protected void showMessage(string message)
    {
        this.target.text = message;
    }
    IEnumerator OnAction()
    {

        for (int i = 0; i < messages.Count; ++i)
        {
            // 1フレーム分 処理を待機(下記説明1)
            yield return null;

            // 会話をwindowのtextフィールドに表示
            showMessage(messages[i]);

            // キー入力を待機 (下記説明1)
            yield return new WaitUntil(() => Input.anyKeyDown);
        }

        yield break;

    }
        // Update is called once per frame
    void Update()
    {
        
    }
}

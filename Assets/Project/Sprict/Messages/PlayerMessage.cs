using System.Collections;
using System.Collections.Generic;
//using UnityEditor.PackageManager.UI;
//using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMessage : MonoBehaviour
{
    [SerializeField]
    private List<string> Messages;
    [SerializeField]
    private List<string> names;
    public Canvas window;
    public Text target;
    public Text nameText;
    private IEnumerator playercoroutine;
    public static PlayerMessage instance;
    public DifficultyLevelManager difficultylevelmanager;
    public bool StartActive;
    public bool firstActive;
    public Canvas Demo;
    public Canvas DifficultyCanvas;
    public Image DemoImage;
    public Image DemoPanel;
    public Image Instruction;



    // Start is called before the first frame update
    void Start()
    {
        firstActive = true;
        instance = this;
        playercoroutine = CreateCoroutine();
        PlayerManager.m_instance.m_speed = 0;
        // コルーチンの起動(下記説明2)
        StartCoroutine(playercoroutine);
    }
    private IEnumerator CreateCoroutine()
    {
        yield return CanvasActive();
        // window起動
        window.gameObject.SetActive(true);
        // 抽象メソッド呼び出し 詳細は子クラスで実装
        yield return OnAction();

        // window終了
        this.target.text = "";
        this.window.gameObject.SetActive(false);

        StopCoroutine(playercoroutine);
        playercoroutine = null;
        StartActive = true;
        PlayerManager.m_instance.m_speed = 0.075f;
    }
    protected void showMessage(string message ,string name)
    {
        this.target.text = message;
        nameText.text = name;
    }
    IEnumerator CanvasActive()
    {
        DifficultyCanvas.gameObject.SetActive(true);
        yield return (difficultylevelmanager.ActiveCanvas == true);
    }
    IEnumerator OnAction()
    {
        Debug.Log("PlayerMessage.OnAction");
        for (int i = 0; i < Messages.Count; ++i)
        {
            // 1フレーム分 処理を待機(下記説明1)
            yield return null;

            // 会話をwindowのtextフィールドに表示
            showMessage(Messages[i], names[i]);

            // キー入力を待機 (下記説明1)
            yield return new WaitUntil(() => (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)));
        }

        yield break;

    }
    private void Update()
    {
        if (StartActive && firstActive == true)
        {
            DemoImage.gameObject.SetActive(true);
            DemoPanel.gameObject.SetActive(true);
        }
        if (DemoImage.gameObject.activeSelf)
        {
            PlayerManager.m_instance.m_speed = 0;
            if (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return))
            {
                DemoImage.gameObject.SetActive(false);
                DemoPanel.gameObject.SetActive(false);
                Instruction.gameObject.SetActive(true);
                firstActive = false;
            }
        }
        else
        {
        }
    }
}

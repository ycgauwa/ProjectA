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
    private List<string> Messages2;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<Sprite> image;
    public Canvas window;
    public Text target;
    public Text nameText;
    public Image characterImage;
    private IEnumerator playercoroutine;
    public static PlayerMessage instance;
    public DifficultyLevelManager difficultylevelmanager;
    public bool StartActive = false;
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
        Instruction.gameObject.SetActive(true);
        PlayerManager.m_instance.m_speed = 0;
        //やりたいことはメニューを閉じ終わった後にメッセージを閉じれる
        // コルーチンの起動(下記説明2)
    }
    private IEnumerator CreateCoroutine()
    {
        yield return CanvasActive();
        yield return new WaitUntil(() => StartActive);
        yield return OnAction();

        this.target.text = "";
        this.window.gameObject.SetActive(false);

        StopCoroutine(playercoroutine);
        playercoroutine = null;
        //StartActive = true;
        GameManager.m_instance.stopSwitch = false;
        PlayerManager.m_instance.m_speed = 0.075f;
    }
    protected void showMessage(string message ,string name ,Sprite image)
    {
        target.text = message;
        nameText.text = name;
        characterImage.sprite = image;
    }
    IEnumerator CanvasActive()
    {
        DifficultyCanvas.gameObject.SetActive(true);
        yield return (difficultylevelmanager.ActiveCanvas == false);
    }
    IEnumerator OnAction()
    {
        window.gameObject.SetActive(true);
        for (int i = 0; i < Messages.Count; ++i)
        {
            // 1フレーム分 処理を待機(下記説明1)
            yield return null;
            if (StartActive == true)
            {
                // 会話をwindowのtextフィールドに表示
                showMessage(Messages[i], names[i], image[i]);
            }
            else
            {
                showMessage(Messages2[i], names[i], image[i]);
            }
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
            //GameManager.m_instance.stopSwitch = true;
            if (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return))
            {
                DemoImage.gameObject.SetActive(false);
                DemoPanel.gameObject.SetActive(false);
                firstActive = false;
            }
        }
    }
}

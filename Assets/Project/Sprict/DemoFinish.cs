using System.Collections;
using System.Collections.Generic;
//using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.LowLevel;

public class DemoFinish : MonoBehaviour
{
    //最初のUIなんかを管理するクラス
    //ゲームモード選択→難易度選択→説明みる→メニュー出現→メニューを閉じる
    //コルーチンでアクティブ状態で次に進めるように調節
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
    public static DemoFinish instance;
    public DifficultyLevelManager difficultylevelmanager;
    public bool StartActive = false;
    public bool firstActive;
    public Canvas Demo;
    public Canvas DifficultyCanvas;
    public Image DemoImage;
    public Image DemoPanel;
    public Image Instruction;
    public Canvas DemoFinishCanvas;
    public GameObject player;
    public Canvas gameModeCanvas;
    public string proMessage;
    public string norMessage;
    public GameObject firstSelect2;

    void Start()
    {
        firstActive = false;
        instance = this;
        StartCoroutine(CreateCoroutine());
    }
    private IEnumerator CreateCoroutine()
    {
        Debug.Log("1");
        yield return new WaitUntil(() => gameModeCanvas.gameObject.activeSelf == false);
        Debug.Log("2");
        yield return CanvasActive();
        Debug.Log("3");
        yield return new WaitUntil(() => StartActive);
        Debug.Log("4");
        Instruction.gameObject.SetActive(true);
        yield return new WaitUntil(() => firstActive == true);
        Debug.Log("5");
        yield return new WaitUntil(() => GameManager.m_instance.menuCanvas.gameObject.activeSelf == false);
        Debug.Log("6");
        yield return OnAction();
        Debug.Log("7");

        target.text = "";
        window.gameObject.SetActive(false);
        //StartActive = true;
        GameManager.m_instance.stopSwitch = false;
    }
    protected void showMessage(string message, string name, Sprite image)
    {
        target.text = message;
        nameText.text = name;
        characterImage.sprite = image;
    }
    IEnumerator CanvasActive()
    {
        DifficultyCanvas.gameObject.SetActive(true);
        yield return (difficultylevelmanager.ActiveCanvas == true);
    }
    IEnumerator OnAction()
    {
        if(GameManager.m_instance.stopSwitch == true)
            GameManager.m_instance.stopSwitch = false;
        window.gameObject.SetActive(true);
        for(int i = 0; i < Messages.Count; ++i)
        {
            yield return null;
            if(StartActive == true)
                showMessage(Messages[i], names[i], image[i]);
            else
                showMessage(Messages2[i], names[i], image[i]);
            yield return new WaitUntil(() => (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)));
        }
        target.text = "";
        GameManager.m_instance.ImageErase(characterImage);
        window.gameObject.SetActive(false);
        yield break;
    }
    private void Update()
    {
        if(StartActive == true && firstActive == false)
        {
            DemoImage.gameObject.SetActive(true);
            DemoPanel.gameObject.SetActive(true);
        }
        if(DemoImage.gameObject.activeSelf)
        {
            //GameManager.m_instance.stopSwitch = true;
            if(Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return))
            {
                DemoImage.gameObject.SetActive(false);
                DemoPanel.gameObject.SetActive(false);
                //firstActive = false;
            }
        }
        if(!GameManager.m_instance.menuCanvas.gameObject.activeSelf && firstActive == true)
        {
            OnAction();
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player") || collider.gameObject.name == "Matiba Haru")
        {
            PlayerManager.m_instance.m_speed = 0;
            Time.timeScale = 0.0f;
            DemoFinishCanvas.gameObject.SetActive(true);
        }
    }
    // Start is called before the first frame update
    void Awake()
    {
        gameModeCanvas.gameObject.SetActive(true);
    }
    public void prologue()
    {
        player.transform.position = new Vector2(30,-35);
        gameModeCanvas.gameObject.SetActive(false);
        EventSystem.current.SetSelectedGameObject(firstSelect2);
        StartActive = true;
        //playerMessage.
    }
    public void normal() 
    {
        player.transform.position = new Vector2(70, -45);
        gameModeCanvas.gameObject.SetActive(false);
        EventSystem.current.SetSelectedGameObject(firstSelect2);
        StartActive = true;
        Demo.gameObject.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
//using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.LowLevel;
using Cysharp.Threading.Tasks;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine.Rendering.Universal;

public class DemoFinish : MonoBehaviour
{
    //最初のUIなんかを管理するクラス
    //難易度選択→説明みる→メニュー出現→メニューを閉じる
    //コルーチンでアクティブ状態で次に進めるように調節
    [SerializeField]
    private List<string> Messages;
    [SerializeField]
    private List<string> Messages2;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<Sprite> image;
    public List<string> finishMessages;
    public List<string> finishNames;
    public List<Sprite> finishImage;
    [SerializeField]
    private List<string> finishMessages2;
    [SerializeField]
    private List<string> finishNames2;
    [SerializeField]
    private List<Sprite> finishImage2;
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
    public string proMessage;
    public string norMessage;
    public GameObject firstSelect2;
    public GameObject enterUI;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else Destroy(instance);
    }
    void Start()
    {
        if(SaveSlotsManager.save_Instance.saveState.loadIndex == 0)
        {
            player.transform.position = new Vector2(30, -35);
            EventSystem.current.SetSelectedGameObject(firstSelect2);
            MessageManager.message_instance.isTextAdvanceEnabled = false;
            Time.timeScale = 0;
            CreateCoroutine().Forget();
        }
        else enterUI.SetActive(false);
    }
    private async UniTask CreateCoroutine(CancellationToken ct = default)
    {//流れとして難易度決めてから説明行って、大体見たらセリフ出してゲーム開始。その間ESCは無効？
        Debug.Log("1");
        //yield return new WaitUntil(() => gameModeCanvas.gameObject.activeSelf == false);
        Debug.Log("2");
        await CanvasActive();
        //Debug.Log("3");
        Debug.Log("4");
        Instruction.gameObject.SetActive(true);
        await UniTask.WaitUntil(() => firstActive, cancellationToken: ct);//FAは説明が見終わったBool値
        Debug.Log("5");
        await UniTask.WaitUntil(() => GameManager.m_instance.menuCanvas.gameObject.activeSelf == false, cancellationToken: ct);
        Debug.Log("6");
        await MessageManager.message_instance.MessageWindowActive(Messages2, names, image, ct: destroyCancellationToken);
        Debug.Log("7");

        target.text = "";
        window.gameObject.SetActive(false);
        //StartActive = true;
        GameManager.m_instance.stopSwitch = false;
        enterUI.SetActive(false);
        Time.timeScale = 1;
    }
    protected void showMessage(string message, string name, Sprite image)
    {
        target.text = message;
        nameText.text = name;
        characterImage.sprite = image;
    }
    async UniTask CanvasActive()
    {
        DifficultyCanvas.gameObject.SetActive(true);
        await UniTask.WaitUntil(() => difficultylevelmanager.ActiveCanvas == true);
    }
    IEnumerator OnAction()//セリフだし
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
    private async void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Seiitirou"))
        {
            await Blackout();
            await MessageManager.message_instance.MessageWindowActive(finishMessages2, finishNames2, finishImage2, ct: destroyCancellationToken);
            DemoFinishCanvas.gameObject.SetActive(true);
        }
        
        /*if(collider.gameObject.tag.Equals("Player") || collider.gameObject.name == "Matiba Haru")
        {
            PlayerManager.m_instance.m_speed = 0;
            Time.timeScale = 0.0f;
            DemoFinishCanvas.gameObject.SetActive(true);
        }*/
    }
    public async UniTask Blackout()
    {
        GameManager.m_instance.stopSwitch = true;
        while(SecondHouseManager.secondHouse_instance.light2D.intensity > 0.01f)
        {
            SecondHouseManager.secondHouse_instance.light2D.intensity -= 0.008f;
            await UniTask.Delay(1);
        }
    }
}

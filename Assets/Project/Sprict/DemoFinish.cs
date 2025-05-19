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
    //�ŏ���UI�Ȃ񂩂��Ǘ�����N���X
    //��Փx�I���������݂遨���j���[�o�������j���[�����
    //�R���[�`���ŃA�N�e�B�u��ԂŎ��ɐi�߂�悤�ɒ���
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
    private bool exhibitionGameSwitch = true;
    public Canvas Demo;
    public Canvas DemoFinishCanvas;
    public Canvas confirmCanvas;
    public Canvas DifficultyCanvas;
    public Image DemoImage;
    public Image DemoPanel;
    public Image Instruction;
    public GameObject player;
    public GameObject firstSelect2;
    public GameObject enterUI;
    public string proMessage;
    public string norMessage;

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
            GameStartFlow().Forget();
        }
        else enterUI.SetActive(false);
    }
    private async UniTask GameStartFlow(CancellationToken ct = default)
    {//����Ƃ��ē�Փx���߂Ă�������s���āA��̌�����Z���t�o���ăQ�[���J�n�B���̊�ESC�͖����H
        Debug.Log("1");
        await confirmCanvasActive();
        Debug.Log("2");
        await difficultCanvasActive();
        Debug.Log("3");
        Debug.Log("4");
        Instruction.gameObject.SetActive(true);
        await UniTask.WaitUntil(() => firstActive, cancellationToken: ct);//FA�͐��������I�����Bool�l
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
    async UniTask confirmCanvasActive()
    {
        confirmCanvas.gameObject.SetActive(true);
        await UniTask.WaitUntil(() => !confirmCanvas.gameObject.activeSelf);
    }
    async UniTask difficultCanvasActive()
    {//��Փx�I�����\�b�h�B�{�^���������܂ł͐�ɐi�܂Ȃ��悤�ɂ������B
        DifficultyCanvas.gameObject.SetActive(true);
        await UniTask.WaitUntil(() => difficultylevelmanager.ActiveCanvas == true);
    }
    IEnumerator OnAction()//�Z���t����
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
    public void prologueButtonMethod()
    {
        // ���̂܂܃E�B���h�E�������ăQ�[���Ɉڂ�
        confirmCanvas.gameObject.SetActive(false);
        
    }
    public void GameButtonMethod()
    {
        // �l�X�ȏ����ݒ�����ă`���v�^�[1�ɂ���
        confirmCanvas.gameObject.SetActive(false);
        player.transform.position = new Vector3(70, -45, 0);
        FlagsManager.flag_Instance.flagBools[1] = true;
        FlagsManager.flag_Instance.navigationPanel.gameObject.SetActive(true);
        FlagsManager.flag_Instance.ChangeUIDestnation(4, "Yukito");
        FlagsManager.flag_Instance.locationText.text = "1F�L��";
        SaveSlotsManager.save_Instance.saveState.chapterNum++;
        FlagsManager.flag_Instance.LoadFlagsData();
        MessageManager.message_instance.MessageWindowActive(Messages, names, image, ct: destroyCancellationToken).Forget();
    }
}

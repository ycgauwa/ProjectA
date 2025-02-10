using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using Cysharp.Threading.Tasks;
using System.Linq;
using System.IO;
using DG.Tweening;
using Unity.VisualScripting.FullSerializer;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private List<string> cookedmessages;
    [SerializeField]
    private List<string> cookednames;
    [SerializeField]
    private List<Sprite> cookedimage;
    [SerializeField]
    private List<string>Firmessages3;
    [SerializeField]
    private List<string> Firnames3;
    [SerializeField]
    private List<Sprite> Firimages3;
    public static GameManager m_instance;
    public GameObject mainCamera;
    public GameObject player;
    public GameObject seiitirou;
    public PlayerManager playerManager;
    public GameTeleportManager teleportManager;
    public RescueEvent rescueEvent;

    public GameObject rescuePoint;
    public GameObject yukitoDead;
    public GameObject menuFirstSelect;
    public GameObject gamemodeFirstSelect;
    public GameObject saveMenuFirstSelect;
    public GameObject instructionFirstSelect;
    public GameObject instructionSecondSelect;
    public GameObject instructionThirdSelect;
    public GameObject instructionFourthSelect;
    public GameObject instructionFifthSelect;

    public float nowTime;
    public float gameTimerCountSecond;
    public int gameTimerCountMinute;
    public int gameTimerCountHour;
    public int deathCount;
    public GameObject enemy;
    public Homing homing;

    public Canvas menuCanvas;
    public Canvas inventryCanvas;
    public Canvas optionCanvas;
    public Canvas gameoverWindow;
    public Canvas InstructionsCanvas;
    public Canvas galleryCanvas;
    public Canvas messageCanvas;
    public Canvas gameUICanvas;
    public Canvas diaryCanvas;
    public Canvas diary2Canvas;
    public Canvas saveCanvas;

    public Image charaImage;
    public Image buttonPanel;
    public Image Instruction1;
    public Image Instruction2;
    public Image Instruction3;
    public Image Instruction4;
    public Image Instruction5;
    public Image gallery1;
    public Image gallery2;
    public Image lookPuzzle;

    public Sprite noneImage;
    public ItemDateBase itemDate;
    public Inventry inventry;
    public AudioClip cancel;
    public AudioClip decision;
    public AudioClip ikigire;
    public ToEvent3 ToEvent3;
    public FirEnemyMess firEnemyMess;
    public DishMessage chickenDish;
    public DishMessage fishDish;
    public DishMessage shrimpDish;
    public Cooktop cooktop;
    public SoundManager soundManager;
    public Volume postVolume;
    public Vignette vignette;
    public bool stopSwitch = false;
    public bool adjustVignette = false;
    private TextAsset csvInteriorsFile; // CSVファイル
    private List<string[]> csvInteriorsData = new List<string[]>(); // CSVファイルの中身を入れるリスト
    private TextAsset csvNotEntersFile; // CSVファイル
    private List<string[]> csvNotEntersData = new List<string[]>(); // CSVファイルの中身を入れるリスト
    private TextAsset csvEndingsFile; // CSVファイル
    private List<string[]> csvEndingsData = new List<string[]>(); // CSVファイルの中身を入れるリスト

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(gamemodeFirstSelect);
        stopSwitch = true;
        m_instance = this;
        postVolume.profile.TryGet(out vignette);
        csvInteriorsFile = Resources.Load("InteriorsText") as TextAsset; // ResourcesにあるCSVファイルを格納
        StringReader reader = new StringReader(csvInteriorsFile.text); // TextAssetをStringReaderに変換
        csvNotEntersFile = Resources.Load("NotEntersText") as TextAsset;
        StringReader notEnterReader = new StringReader(csvNotEntersFile.text);
        csvEndingsFile = Resources.Load("EndingsText") as TextAsset;
        StringReader endingReader = new StringReader(csvEndingsFile.text);

        while(reader.Peek() != -1)
        {
            string line = reader.ReadLine(); // 1行ずつ読み込む
            csvInteriorsData.Add(line.Split(',')); // csvDataリストに追加する
        }
        while(notEnterReader.Peek() != -1)
        {
            string line = notEnterReader.ReadLine(); // 1行ずつ読み込む
            csvNotEntersData.Add(line.Split(',')); // csvDataリストに追加する
        }
        while(endingReader.Peek() != -1)
        {
            string line = endingReader.ReadLine(); // 1行ずつ読み込む
            csvEndingsData.Add(line.Split(',')); // csvDataリストに追加する
        }
    }
    // Update is called once per frame
    void Update()
    {
        // ゲーム内時間をカウントしたい→60になったらｍを++して60ｍならｈを++
        nowTime += Time.deltaTime;
        gameTimerCountSecond += Time.deltaTime;
        if(gameTimerCountSecond >= 60)
        {
            gameTimerCountSecond = 0;
            gameTimerCountMinute ++;
        }
        if(gameTimerCountMinute >= 60)
        {
            gameTimerCountMinute = 0;
            gameTimerCountHour++;
        }
        FlagsManager.flag_Instance.playTime = gameTimerCountHour + ":" + gameTimerCountMinute + ":" + (int)gameTimerCountSecond;
        if(!adjustVignette)
            vignette.intensity.value = playerManager.staminaIntensity;
        if (messageCanvas.gameObject.activeSelf || diaryCanvas.gameObject.activeSelf || diary2Canvas.gameObject.activeSelf)
        {
            playerManager.playerstate = PlayerManager.PlayerState.Talk;
        }
        else if(stopSwitch == true)
        {
            playerManager.playerstate = PlayerManager.PlayerState.Stop;
        }
        else playerManager.playerstate = PlayerManager.PlayerState.Idol;
        
        if (!menuCanvas.gameObject.activeSelf)
        {
            if(cooktop.cooked.gameObject.activeSelf && !cooktop.ingredients.gameObject.activeSelf && !cooktop.interrupt.gameObject.activeSelf)
            {
                //中断するかを問う選択肢の出現
                if(Input.GetKeyDown("joystick button 1") || Input.GetKeyDown(KeyCode.Escape))
                {
                    MessageManager.message_instance.MessageWindowOnceActive(cookedmessages, cookednames, cookedimage, ct: destroyCancellationToken).Forget();
                    cooktop.interrupt.gameObject.SetActive(true);
                    soundManager.PlaySe(cancel);
                }
            }
            else if(cooktop.cooked.gameObject.activeSelf && cooktop.ingredients.gameObject.activeSelf)
            {
                if(Input.GetKeyDown("joystick button 1") || Input.GetKeyDown(KeyCode.Escape))
                {
                    //素材が出てる時に素材ウィンドウを消すメソッド
                    cooktop.ingredients.gameObject.SetActive(false);
                    if(cooktop.selectedDish1)cooktop.selectedDish1 = false;
                    else if(cooktop.selectedDish2)cooktop.selectedDish2 = false;
                    else if(cooktop.selectedDish3)cooktop.selectedDish3 = false;
                    EventSystem.current.SetSelectedGameObject(cooktop.firstSelect);
                    soundManager.PlaySe(cancel);
                }
            }
            else if(lookPuzzle.gameObject.activeSelf)
            {
                if(Input.GetKeyDown("joystick button 1") || Input.GetKeyDown(KeyCode.Escape))
                {
                    lookPuzzle.gameObject.SetActive(false);
                    soundManager.PlaySe(cancel);
                    stopSwitch = false;
                }
            }
            else
            {
                if(Input.GetKeyDown("joystick button 1") || Input.GetKeyDown(KeyCode.Escape))
                {
                    PlayerManager.m_instance.m_speed = 0;
                    Time.timeScale = 0;
                    menuCanvas.gameObject.SetActive(true);
                    EventSystem.current.SetSelectedGameObject(menuFirstSelect);
                    soundManager.PlaySe(cancel);
                }
            }
        }
        else if(menuCanvas.gameObject.activeSelf)
        {
            if(Input.GetKeyDown("joystick button 1") || Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 1;
                PlayerManager.m_instance.m_speed = 0.075f;
                menuCanvas.gameObject.SetActive(false);
                soundManager.PlaySe(cancel);
            }
        }
        if(inventryCanvas.gameObject.activeSelf)
        {
            if(Input.GetKeyDown("joystick button 1") || Input.GetKeyDown(KeyCode.Escape))
            {
                inventryCanvas.gameObject.SetActive(false);
                itemDate.SelectDiff();
                menuCanvas.gameObject.SetActive(true);
                EventSystem.current.SetSelectedGameObject(menuFirstSelect);
                soundManager.PlaySe(cancel);
            }
        }
        if (Instruction1.gameObject.activeSelf)
        {
            if (Input.GetKeyDown("joystick button 1") || Input.GetKeyDown(KeyCode.Escape))
            {
                Instruction1.gameObject.SetActive(false);
                menuCanvas.gameObject.SetActive(true);
                EventSystem.current.SetSelectedGameObject(menuFirstSelect);
                soundManager.PlaySe(cancel);
            }
        }
        if(optionCanvas.gameObject.activeSelf)
        {
            if(Input.GetKeyDown("joystick button 1") || Input.GetKeyDown(KeyCode.Escape))
            {
                optionCanvas.gameObject.SetActive(false);
                menuCanvas.gameObject.SetActive(true);
                EventSystem.current.SetSelectedGameObject(menuFirstSelect);
                soundManager.PlaySe(cancel);
            }
        }
        //０になるまでは通常の回復速度　０になってからスピードが0になって回復速度がMaxになるまで遅くなる。
        if (Input.GetKey(KeyCode.LeftShift) && playerManager.playerstate != PlayerManager.PlayerState.Talk && playerManager.playerstate != PlayerManager.PlayerState.Stop)
        {
            if (playerManager.playercondition == PlayerManager.PlayerCondition.Suffocation || playerManager.playercondition == PlayerManager.PlayerCondition.Suffocation2) return;
            if (playerManager.stamina > 0 && playerManager.staminastate == PlayerManager.StaminaState.normal)
            {
                int test = 2;
                playerManager.m_speed = 0.1f;
                playerManager.Dashing(test);
            }
            else 
            {
                playerManager.m_speed = 0.075f;
                playerManager.staminastate = PlayerManager.StaminaState.exhausted;
            }
        }
        else
        {
            switch(playerManager.staminastate)
            {
                case PlayerManager.StaminaState.normal:
                    playerManager.stamina += 2;
                    playerManager.staminaIntensity -= 0.005f;
                    break;
                case PlayerManager.StaminaState.exhausted:
                    playerManager.stamina += 1;
                    playerManager.staminaIntensity -= 0.002f;
                    if (playerManager.stamina == playerManager.staminaMax)
                    {
                        playerManager.staminastate = PlayerManager.StaminaState.normal;
                    }
                    break;
            }
                

        }

        switch (playerManager.playerstate)
        {
            case PlayerManager.PlayerState.Idol:
                playerManager.m_speed = 0.075f;
                if(playerManager.playercondition == PlayerManager.PlayerCondition.Suffocation2) playerManager.m_speed = 0.05f;
                if(playerManager.staminastate == PlayerManager.StaminaState.exhausted) playerManager.m_speed = 0.05f;
                break;
            case PlayerManager.PlayerState.Talk:
                PlayerManager.m_instance.m_speed = 0;
                Homing.m_instance.speed = 0;
                
                break;
            case PlayerManager.PlayerState.Stop:
                PlayerManager.m_instance.m_speed = 0;
                break;
        }
        //デバック用
        if(Input.GetKeyDown(KeyCode.F1))
        {
            player.transform.position = new Vector3(76, 170, 0);
        }
        else if(Input.GetKeyDown(KeyCode.F2))
        {
            player.transform.position = new Vector3(106, 140, 0);
        }
        else if(Input.GetKeyDown(KeyCode.F3))
        {
            player.transform.position = new Vector3(150, -12, 0);
        }
        else if(Input.GetKeyDown(KeyCode.F4))
        {
            player.transform.position = new Vector3(35, 66, 0);
            rescueEvent.gameObject.SetActive(true);
            rescueEvent.notEnter6.choiced = true;
            rescueEvent.notEnter6.rescued = true;
        }
        else if(Input.GetKeyDown(KeyCode.F5))
        {
            GameObject deadYukito = GameObject.Find("YukitoGhost");
            deadYukito.transform.position = new Vector3(64.33f, -46.27f, 0);
            deadYukito.transform.DOLocalMove(new Vector3(67.33f, -46.27f, 0), 3f);
        }
    }
    public List<string> GetSpeakerName(string interiorName, string type)
    {
        switch(type)
            {
            case "Interior":
                int stt = csvInteriorsData.IndexOf(csvInteriorsData.First(item => item[0] == interiorName));
                int end = csvInteriorsData.IndexOf(csvInteriorsData.Skip(stt + 1).First(item => item[0] != ""));
                return csvInteriorsData.Skip(stt).Take(end - 1 - stt).Select(item => item[1]).ToList();
            case "NotEnter":
                stt = csvNotEntersData.IndexOf(csvNotEntersData.First(item => item[0] == interiorName));
                end = csvNotEntersData.IndexOf(csvNotEntersData.Skip(stt + 1).First(item => item[0] != ""));
                return csvNotEntersData.Skip(stt).Take(end - 1 - stt).Select(item => item[1]).ToList();

            default:
                return new List<string>();
        }

    }
    public List<string>GetMessages(string interiorName, string type)
    {
        switch(type)
        {
            case "Interior":
                int stt = csvInteriorsData.IndexOf(csvInteriorsData.First(item => item[0] == interiorName));
                int end = csvInteriorsData.IndexOf(csvInteriorsData.Skip(stt + 1).First(item => item[0] != ""));
                return csvInteriorsData.Skip(stt).Take(end - 1 - stt).Select(item => item[2]).ToList();

            case "NotEnter":
                 stt = csvNotEntersData.IndexOf(csvNotEntersData.First(item => item[0] == interiorName));
                 end = csvNotEntersData.IndexOf(csvNotEntersData.Skip(stt + 1).First(item => item[0] != ""));
                return csvNotEntersData.Skip(stt).Take(end - 1 - stt).Select(item => item[2]).ToList();
            
            case "Ending":
                stt = csvEndingsData.IndexOf(csvEndingsData.First(item => item[0] == interiorName));
                end = csvEndingsData.IndexOf(csvEndingsData.Skip(stt + 1).First(item => item[0] != ""));
                return csvEndingsData.Skip(stt).Take(end - 1 - stt).Select(item => item[2]).ToList();

            default:
                return new List<string>();
        }
    }
    public void OnClickBackButton()
    {
        if(menuCanvas.gameObject.activeSelf)
        {
            Time.timeScale = 1;
            PlayerManager.m_instance.m_speed = 0.075f;
            menuCanvas.gameObject.SetActive(false);
            soundManager.PlaySe(cancel);
        }
    }
    public void OnClickMenuButton()
    {
        if(!menuCanvas.gameObject.activeSelf)
        {
            PlayerManager.m_instance.m_speed = 0;
            Time.timeScale = 0;
            menuCanvas.gameObject.SetActive(true);
            EventSystem.current.SetSelectedGameObject(menuFirstSelect);
            soundManager.PlaySe(cancel);
        }
    }
    public void OnClickInventryButton()
    {
        //　インベントリボタンをクリックしたときメニューキャンバスを消してインベントリを呼び出す
        //　インベントリだけ出てるのでESC押したらインベントリを消してメニューを呼べばよい
        soundManager.PlaySe(decision);
        menuCanvas.gameObject.SetActive(false);
        inventryCanvas.gameObject.SetActive(true);
    }
    public void OnclickOptionButton()
    {
        soundManager.PlaySe(decision);
        menuCanvas.gameObject.SetActive(false);
        optionCanvas.gameObject.SetActive(true);
    }
    public void OnclickGalleryButton()
    {
        soundManager.PlaySe(decision);
        menuCanvas.gameObject.SetActive(false);
        galleryCanvas.gameObject.SetActive(true);
        gallery1.gameObject.SetActive(true);
    }
    public void OnclickRetryButton(AudioClip meatSound = null)
    {
        player.transform.position = new Vector2(0,0);
        stopSwitch = false;
        if(homing.speed == 0)homing.speed = 2;
        soundManager.StopSe(meatSound);
        if(MessageManager.message_instance.window.gameObject.activeSelf)
        {
            MessageManager.message_instance.window.gameObject.SetActive(false);
        }
        if (deathCount > 4)
        {
            inventry.Delete(itemDate.GetItemId(253));
            yukitoDead.SetActive(true);
            seiitirou.gameObject.tag = "Seiitirou";

            //　カメラの位置を幸人から征一郎に変更して、征一郎をキー操作で動かせるようにする。
            cameraManager.playerCamera = false;
            player.gameObject.SetActive(false);
            cameraManager.seiitirouCamera = true;
            rescueEvent = rescuePoint.GetComponent<RescueEvent>();
            soundManager.StopSe(rescueEvent.ChasedBGM);
            playerManager = seiitirou.AddComponent<PlayerManager>();
            playerManager = seiitirou.GetComponent<PlayerManager>();
            playerManager.staminaMax = 300;
            playerManager.teleportManager = teleportManager;
            playerManager.homing = homing;
            Rigidbody2D rb = seiitirou.GetComponent<Rigidbody2D>();
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            homing.enemyEmerge = false;
            homing.enemyCount = 0;
        }
        if (rescueEvent.RescueSwitch)
        {
            player.transform.position = new Vector2(35, 68);
            enemy.transform.position = new Vector2(35, 71);
            buttonPanel.gameObject.SetActive(false);
            gameoverWindow.gameObject.SetActive(false);
            Homing.m_instance.speed = 2;
            deathCount++;
        }
        else
        {
            Homing.m_instance.speed = 2;
            soundManager.PlaySe(decision);
            buttonPanel.gameObject.SetActive(false);
            gameoverWindow.gameObject.SetActive(false);
            /*
            シーンをロードして最初からにする。
            SceneManager.LoadScene("Game");
            */
            //　最初は民家１の玄関前に復活できるようにする。
            //　2軒目に行く時点でチェックポイントを変える。
            //　その際は敵を消しておく。
            player.transform.position = new Vector2(69, -46);
            
            if(firEnemyMess.gameObject.activeSelf)
                firEnemyMess.firstDeath = true;
            if(enemy.activeSelf)
            {
                enemy.gameObject.SetActive(false);
                teleportManager.StopChased();
            }
        }
    }
    public void OnClickSaveButton()
    {
        menuCanvas.gameObject.SetActive(false);
        saveCanvas.gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(saveMenuFirstSelect);
        soundManager.PlaySe(decision);
    }
    public void OnClickTitleButton() 
    {
        soundManager.PlaySe(decision);
        buttonPanel.gameObject.SetActive(false);
        gameoverWindow.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Title");
    }
    public void OnClickHelpButton()
    {
        menuCanvas.gameObject.SetActive(false);
        Instruction1.gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(instructionFirstSelect);
        soundManager.PlaySe(decision);
    }
    public void OnClickToMenuButton()
    {
        if (Instruction5.gameObject.activeSelf)
        {
            Instruction5.gameObject.SetActive(false);
            menuCanvas.gameObject.SetActive(true);
            EventSystem.current.SetSelectedGameObject(menuFirstSelect);
            soundManager.PlaySe(cancel);
            if (DemoFinish.instance.firstActive == false) DemoFinish.instance.firstActive = true;
        }
        else if (inventryCanvas.gameObject.activeSelf)
        {
            inventryCanvas.gameObject.SetActive(false);
            menuCanvas.gameObject.SetActive(true);
            EventSystem.current.SetSelectedGameObject(menuFirstSelect);
            soundManager.PlaySe(cancel);
        }
        else if (optionCanvas.gameObject.activeSelf)
        {
            optionCanvas.gameObject.SetActive(false);
            menuCanvas.gameObject.SetActive(true);
            EventSystem.current.SetSelectedGameObject(menuFirstSelect);
            soundManager.PlaySe(cancel);
        }
        else if (galleryCanvas.gameObject.activeSelf)
        {
            galleryCanvas.gameObject.SetActive(false);
            menuCanvas.gameObject.SetActive(true);
            EventSystem.current.SetSelectedGameObject(menuFirstSelect);
            soundManager.PlaySe(cancel);
        }
        else if(saveCanvas.gameObject.activeSelf)
        {
            saveCanvas.gameObject.SetActive(false);
            menuCanvas.gameObject.SetActive(true);
            EventSystem.current.SetSelectedGameObject(menuFirstSelect);
            soundManager.PlaySe(cancel);
        }
    }
    public void OnClickNextHelpButton()
    {
        if(Instruction1.gameObject.activeSelf)
        {
            Instruction1.gameObject.SetActive(false);
            Instruction2.gameObject.SetActive(true);
            EventSystem.current.SetSelectedGameObject(instructionSecondSelect);
            soundManager.PlaySe(decision);
        }
        else if(Instruction2.gameObject.activeSelf)
        {
            Instruction2.gameObject.SetActive(false);
            Instruction3.gameObject.SetActive(true);
            EventSystem.current.SetSelectedGameObject(instructionThirdSelect);
            soundManager.PlaySe(decision);
        }
        else if (Instruction3.gameObject.activeSelf)
        {
            Instruction3.gameObject.SetActive(false);
            Instruction4.gameObject.SetActive(true);
            EventSystem.current.SetSelectedGameObject(instructionFourthSelect);
            soundManager.PlaySe(decision);
        }
        else if (Instruction4.gameObject.activeSelf)
        {
            Instruction4.gameObject.SetActive(false);
            Instruction5.gameObject.SetActive(true);
            EventSystem.current.SetSelectedGameObject(instructionFifthSelect);
            soundManager.PlaySe(decision);
        }
    }
    public void OnClickBackHelpButton() 
    {
        //特定の画像が表示されていた場合で、戻るか閉じるかを変える
        if (Instruction1.gameObject.activeSelf)
        {
            Instruction1.gameObject.SetActive(false);
            menuCanvas.gameObject.SetActive(true);
            EventSystem.current.SetSelectedGameObject(menuFirstSelect);
            soundManager.PlaySe(cancel);
            if(DemoFinish.instance.firstActive == false) DemoFinish.instance.firstActive = true;
        }
        else if (Instruction2.gameObject.activeSelf)
        {
            Instruction2.gameObject.SetActive(false);
            Instruction1.gameObject.SetActive(true);
            EventSystem.current.SetSelectedGameObject(instructionFirstSelect);
            soundManager.PlaySe(cancel);
        }
        else if(Instruction3.gameObject.activeSelf)
        {
            Instruction3.gameObject.SetActive(false);
            Instruction2.gameObject.SetActive(true);
            EventSystem.current.SetSelectedGameObject(instructionSecondSelect);
            soundManager.PlaySe(cancel);
        }
        else if (Instruction4.gameObject.activeSelf)
        {
            Instruction4.gameObject.SetActive(false);
            Instruction3.gameObject.SetActive(true);
            EventSystem.current.SetSelectedGameObject(instructionThirdSelect);
            soundManager.PlaySe(cancel);
        }
        else if (Instruction5.gameObject.activeSelf)
        {
            Instruction5.gameObject.SetActive(false);
            Instruction4.gameObject.SetActive(true);
            EventSystem.current.SetSelectedGameObject(instructionFourthSelect);
            soundManager.PlaySe(cancel);
        }
    }
    public void ImageErase(Image image)
    {
        charaImage.sprite = noneImage;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEditor.PackageManager.UI;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private List<string> cookedmessages;
    [SerializeField]
    private List<string> cookednames;
    [SerializeField]
    private List<Sprite> cookedimage;
    public static GameManager m_instance;
    public GameObject player;
    public GameObject seiitirou;
    public PlayerManager playerManager;
    public GameTeleportManager teleportManager;
    public RescueEvent rescueEvent;
    public GameObject rescuePoint;
    public GameObject yukitoDead;
    public int deathCount;
    public GameObject enemy;
    public Homing homing;
    public Canvas menuCanvas;
    public Canvas inventryCanvas;
    public Canvas optionCanvas;
    public Canvas gameoverWindow;
    public Canvas InstructionsCanvas;
    public Canvas messageCanvas;
    public Canvas diaryCanvas;
    public Canvas diary2Canvas;
    public Image buttonPanel;
    public Image Instruction1;
    public Image Instruction2;
    public Image Instruction3;
    public Image Instruction4;
    public ItemDateBase itemDate;
    public Inventry inventry;
    public AudioClip cancel;
    public AudioClip decision;
    public AudioClip ikigire;
    public ToEvent3 ToEvent3;
    public DishMessage chickenDish;
    public DishMessage fishDish;
    public DishMessage shrimpDish;
    public Cooktop cooktop;
    public SoundManager soundManager;
    public Volume postVolume;
    private Vignette vignette;
    public bool stopSwitch = false; 

    private void Start()
    {
        m_instance = this;
        postVolume.profile.TryGet(out vignette);
    }
    // Update is called once per frame
    void Update()
    {
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
                    MessageManager.message_instance.MessageWindowActive(cookedmessages, cookednames, cookedimage);
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
                    soundManager.PlaySe(cancel);
                }
            }
            else
            {
                if(Input.GetKeyDown("joystick button 1") || Input.GetKeyDown(KeyCode.Escape))
                {
                    PlayerManager.m_instance.m_speed = 0;
                    Time.timeScale = 0;
                    menuCanvas.gameObject.SetActive(true);
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
                soundManager.PlaySe(cancel);
            }
        }
        if (Instruction1.gameObject.activeSelf)
        {
            if (Input.GetKeyDown("joystick button 1") || Input.GetKeyDown(KeyCode.Escape))
            {
                Instruction1.gameObject.SetActive(false);
                menuCanvas.gameObject.SetActive(true);
                soundManager.PlaySe(cancel);
            }
        }
        if(optionCanvas.gameObject.activeSelf)
        {
            if(Input.GetKeyDown("joystick button 1") || Input.GetKeyDown(KeyCode.Escape))
            {
                optionCanvas.gameObject.SetActive(false);
                menuCanvas.gameObject.SetActive(true);
                soundManager.PlaySe(cancel);
            }
        }
        //０になるまでは通常の回復速度　０になってからスピードが0になって回復速度がMaxになるまで遅くなる。
        if (Input.GetKey(KeyCode.LeftShift) && playerManager.playerstate != PlayerManager.PlayerState.Talk && playerManager.playerstate != PlayerManager.PlayerState.Stop)
        {
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
                    playerManager.staminaIntensity -= 0.01f;
                    break;
                case PlayerManager.StaminaState.exhausted:
                    playerManager.stamina += 1;
                    playerManager.staminaIntensity -= 0.005f;
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
            player.transform.position = new Vector3(147,100,0);
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
   public void OnclickRetryButton()
    {
        if (deathCount > 4)
        {
            itemDate.items[7].checkPossession = false;
            inventry.Delete(itemDate.items[7]);
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
            playerManager.teleportManager = teleportManager;
            Rigidbody2D rb = seiitirou.GetComponent<Rigidbody2D>();
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            playerManager.SeiitirouRes();
        }
        if (rescueEvent.RescueSwitch)
        {
            Debug.Log(deathCount);
            player.transform.position = new Vector2(35, 68);
            enemy.transform.position = new Vector2(35, 71);
            buttonPanel.gameObject.SetActive(false);
            gameoverWindow.gameObject.SetActive(false);
            PlayerManager.m_instance.m_speed = 0.075f;
            Homing.m_instance.speed = 2;
            deathCount++;
        }
        else
        {
            PlayerManager.m_instance.m_speed = 0.075f;
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
            if(ToEvent3.firstchased == true)
            {
                player.transform.position = new Vector2(69, -46);
                if(enemy.activeSelf)
                {
                    enemy.gameObject.SetActive(false);
                    teleportManager.StopChased();
                }
            }
        }
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
        soundManager.PlaySe(decision);
    }
    public void OnClickToMenuButton()
    {
        if (Instruction4.gameObject.activeSelf)
        {
            Instruction4.gameObject.SetActive(false);
            menuCanvas.gameObject.SetActive(true);
            soundManager.PlaySe(cancel);
        }
        else if (inventryCanvas.gameObject.activeSelf)
        {
            inventryCanvas.gameObject.SetActive(false);
            menuCanvas.gameObject.SetActive(true);
            soundManager.PlaySe(cancel);
        }
        else if (optionCanvas.gameObject.activeSelf)
        {
            optionCanvas.gameObject.SetActive(false);
            menuCanvas.gameObject.SetActive(true);
            soundManager.PlaySe(cancel);
        }
    }
    public void OnClickNextHelpButton()
    {
        if(Instruction1.gameObject.activeSelf)
        {
            Instruction1.gameObject.SetActive(false);
            Instruction2.gameObject.SetActive(true);
            soundManager.PlaySe(decision);
        }
        else if(Instruction2.gameObject.activeSelf)
        {
            Instruction2.gameObject.SetActive(false);
            Instruction3.gameObject.SetActive(true);
            soundManager.PlaySe(decision);
        }
        else if (Instruction3.gameObject.activeSelf)
        {
            Instruction3.gameObject.SetActive(false);
            Instruction4.gameObject.SetActive(true);
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
            soundManager.PlaySe(cancel);
        }
        else if (Instruction2.gameObject.activeSelf)
        {
            Instruction2.gameObject.SetActive(false);
            Instruction1.gameObject.SetActive(true);
            soundManager.PlaySe(cancel);
        }
        else if(Instruction3.gameObject.activeSelf)
        {
            Instruction3.gameObject.SetActive(false);
            Instruction2.gameObject.SetActive(true);
            soundManager.PlaySe(cancel);
        }
        else if (Instruction4.gameObject.activeSelf)
        {
            Instruction4.gameObject.SetActive(false);
            Instruction3.gameObject.SetActive(true);
            soundManager.PlaySe(cancel);
        }
    }
    /*public void DishTaken()
    {
        if(shrimpDish.isContacted == true)
        {
            shrimpDish.selection.gameObject.SetActive(false);
            shrimpDish.Selectwindow.gameObject.SetActive(false);
            Debug.Log("test1");
            shrimpDish.shrimp.checkPossession = true;
            inventry.Add(shrimpDish.shrimp);
            shrimpDish.isOpenSelect = false;
            shrimpDish.window.gameObject.SetActive(false);
            shrimpDish.dish.SetActive(false);
        }
        //とったアイテムが鳥の丸焼きの時
        else if(chickenDish.isContacted == true)
        {
            chickenDish.selection.gameObject.SetActive(false);
            chickenDish.Selectwindow.gameObject.SetActive(false);
            Debug.Log("test2");
            chickenDish.chicken.checkPossession = true;
            inventry.Add(chickenDish.chicken);
            chickenDish.isOpenSelect = false;
            chickenDish.window.gameObject.SetActive(false);
            chickenDish.dish.SetActive(false);
        }
        else if(fishDish.isContacted == true)
        {
            fishDish.selection.gameObject.SetActive(false);
            fishDish.Selectwindow.gameObject.SetActive(false);
            Debug.Log("test3");
            fishDish.fish.checkPossession = true;
            inventry.Add(fishDish.fish);
            fishDish.isOpenSelect = false;
            fishDish.window.gameObject.SetActive(false);
            fishDish.dish.SetActive(false);
        }
    }
    public void DishNotTaken()
    {
        if(shrimpDish.isContacted == true)
        {
            shrimpDish.selection.gameObject.SetActive(false);
            shrimpDish.Selectwindow.gameObject.SetActive(false);
            shrimpDish.isOpenSelect = false;
        }
        else if(chickenDish.isContacted == true)
        {
            chickenDish.selection.gameObject.SetActive(false);
            chickenDish.Selectwindow.gameObject.SetActive(false);
            chickenDish.isOpenSelect = false;
        }
        else if(fishDish.isContacted == true)
        {
            fishDish.selection.gameObject.SetActive(false);
            fishDish.Selectwindow.gameObject.SetActive(false);
            fishDish.isOpenSelect = false;
        }
    }*/
}

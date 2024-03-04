using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
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
    public Canvas gameoverWindow;
    public Canvas InstructionsCanvas;
    public Canvas messageCanvas;
    public Image buttonPanel;
    public Image Instruction1;
    public Image Instruction2;
    public Image Instruction3;
    public ItemDateBase itemDate;
    public Inventry inventry;
    public AudioSource audioSource;
    public AudioClip cancel;
    public AudioClip decision;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        m_instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        if (messageCanvas.gameObject.activeSelf)
        {
            PlayerManager.m_instance.m_speed = 0;
            Homing.m_instance.speed = 0;
        }
        if(!menuCanvas.gameObject.activeSelf)
        {
            if(Input.GetKeyDown("joystick button 1") || Input.GetKeyDown(KeyCode.Escape))
            {
                PlayerManager.m_instance.m_speed = 0;
                Time.timeScale = 0;
                menuCanvas.gameObject.SetActive(true);
                audioSource.PlayOneShot(cancel);
            }
        }
        else if(menuCanvas.gameObject.activeSelf)
        {
            if(Input.GetKeyDown("joystick button 1") || Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 1;
                PlayerManager.m_instance.m_speed = 0.075f;
                menuCanvas.gameObject.SetActive(false);
                audioSource.PlayOneShot(cancel);
            }
        }
        if(inventryCanvas.gameObject.activeSelf)
        {
            if(Input.GetKeyDown("joystick button 1") || Input.GetKeyDown(KeyCode.Escape))
            {
                inventryCanvas.gameObject.SetActive(false);
                itemDate.SelectDiff();
                menuCanvas.gameObject.SetActive(true);
                audioSource.PlayOneShot(cancel);
            }
        }
        if (Instruction1.gameObject.activeSelf)
        {
            if (Input.GetKeyDown("joystick button 1") || Input.GetKeyDown(KeyCode.Escape))
            {
                Instruction1.gameObject.SetActive(false);
                menuCanvas.gameObject.SetActive(true);
                audioSource.PlayOneShot(cancel);
            }
        }
        //  メニュー画面をESCで呼べる。呼んだあとESCで閉じれる。だけどメニュー画面から
        //if(Input.GetKey(KeyCode.LeftShift))
        //{
        //    playerManager.m_speed = 0.5f;
        //}
    }
    public void OnClickInventryButton()
    {
        //　インベントリボタンをクリックしたときメニューキャンバスを消してインベントリを呼び出す
        //　インベントリだけ出てるのでESC押したらインベントリを消してメニューを呼べばよい
        audioSource.PlayOneShot(decision);
        menuCanvas.gameObject.SetActive(false);
        inventryCanvas.gameObject.SetActive(true);
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
            rescueEvent.ChasedBGM.Stop();
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
            audioSource.PlayOneShot(decision);
            buttonPanel.gameObject.SetActive(false);
            gameoverWindow.gameObject.SetActive(false);
            SceneManager.LoadScene("Game");
        }
    }
    public void OnClickTitleButton() 
    {
        audioSource.PlayOneShot(decision);
        buttonPanel.gameObject.SetActive(false);
        gameoverWindow.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Title");
    }
    public void OnClickHelpButton()
    {
        menuCanvas.gameObject.SetActive(false);
        Instruction1.gameObject.SetActive(true);
        audioSource.PlayOneShot(decision);
    }
    public void OnClickNextHelpButton()
    {
        if(Instruction1.gameObject.activeSelf)
        {
            Instruction1.gameObject.SetActive(false);
            Instruction2.gameObject.SetActive(true);
            audioSource.PlayOneShot(decision);
        }
        else if(Instruction2.gameObject.activeSelf)
        {
            Instruction2.gameObject.SetActive(false);
            Instruction3.gameObject.SetActive(true);
            audioSource.PlayOneShot(decision);
        }
    }
    public void OnClickBackHelpButton() 
    {
        //特定の画像が表示されていた場合で、戻るか閉じるかを変える
        if (Instruction1.gameObject.activeSelf)
        {
            Instruction1.gameObject.SetActive(false);
            menuCanvas.gameObject.SetActive(true);
            audioSource.PlayOneShot(cancel);
        }
        else if (Instruction2.gameObject.activeSelf)
        {
            Instruction2.gameObject.SetActive(false);
            Instruction1.gameObject.SetActive(true);
            audioSource.PlayOneShot(cancel);
        }
        else if(Instruction3.gameObject.activeSelf)
        {
            Instruction3.gameObject.SetActive(false);
            Instruction2.gameObject.SetActive(true);
            audioSource.PlayOneShot(cancel);
        }
    }
}

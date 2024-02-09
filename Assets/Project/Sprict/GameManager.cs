using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class GameManager : MonoBehaviour
{
    public static GameManager m_instance;
    public GameObject player;
    public GameObject seiitirou;
    public PlayerManager playerManager;
    public RescueEvent rescueEvent;
    public int deathCount;
    public GameObject enemy;
    public Homing homing;
    public Canvas menuCanvas;
    public Canvas inventryCanvas;
    public Canvas gameoverWindow;
    public Image buttonPanel;
    public ItemDateBase itemDate;
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
        //  メニュー画面をESCで呼べる。呼んだあとESCで閉じれる。だけどメニュー画面から
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
        if (deathCount > 5)
        {
            //　カメラの位置を幸人から征一郎に変更して、征一郎をキー操作で動かせるようにする。
            cameraManager.playerCamera = false;
            player.gameObject.SetActive(false);
            cameraManager.seiitirouCamera = true;
            playerManager = seiitirou.AddComponent<PlayerManager>();

        }
        if (rescueEvent.RescueSwitch)
        {
            player.transform.position = new Vector2(35, 68);
            enemy.transform.position = new Vector2(35, 71);
            buttonPanel.gameObject.SetActive(false);
            gameoverWindow.gameObject.SetActive(false);
            Time.timeScale = 1;
            PlayerManager.m_instance.m_speed = 0.075f;
            Homing.m_instance.speed = 2;
            deathCount++;
        }
        else
        {
            audioSource.PlayOneShot(decision);
            buttonPanel.gameObject.SetActive(false);
            gameoverWindow.gameObject.SetActive(false);
            Time.timeScale = 1.0f;
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
}

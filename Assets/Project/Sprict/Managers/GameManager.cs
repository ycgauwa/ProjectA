using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

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
    public Canvas optionCanvas;
    public Canvas gameoverWindow;
    public Canvas InstructionsCanvas;
    public Canvas messageCanvas;
    public Image buttonPanel;
    public Image Instruction1;
    public Image Instruction2;
    public Image Instruction3;
    public ItemDateBase itemDate;
    public Inventry inventry;
    public AudioClip cancel;
    public AudioClip decision;
    public ToEvent3 ToEvent3;
    public SoundManager soundManager;
    public Volume postVolume;
    private Vignette vignette;

    private void Start()
    {
        m_instance = this;
        postVolume.profile.TryGet(out vignette);
    }
    // Update is called once per frame
    void Update()
    {
        vignette.intensity.value = playerManager.staminaIntensity;
        if (messageCanvas.gameObject.activeSelf)
        {
            playerManager.playerstate = PlayerManager.PlayerState.Talk;
        }
        else playerManager.playerstate = PlayerManager.PlayerState.Idol;
        
        if (!menuCanvas.gameObject.activeSelf)
        {
            if(Input.GetKeyDown("joystick button 1") || Input.GetKeyDown(KeyCode.Escape))
            {
                PlayerManager.m_instance.m_speed = 0;
                Time.timeScale = 0;
                menuCanvas.gameObject.SetActive(true);
                soundManager.PlaySe(cancel);
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
        //�O�ɂȂ�܂ł͒ʏ�̉񕜑��x�@�O�ɂȂ��Ă���X�s�[�h��0�ɂȂ��ĉ񕜑��x��Max�ɂȂ�܂Œx���Ȃ�B
        if (Input.GetKey(KeyCode.LeftShift) && playerManager.playerstate != PlayerManager.PlayerState.Talk)
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
        //�@�C���x���g���{�^�����N���b�N�����Ƃ����j���[�L�����o�X�������ăC���x���g�����Ăяo��
        //�@�C���x���g�������o�Ă�̂�ESC��������C���x���g���������ă��j���[���Ăׂ΂悢
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

            //�@�J�����̈ʒu���K�l���琪��Y�ɕύX���āA����Y���L�[����œ�������悤�ɂ���B
            cameraManager.playerCamera = false;
            player.gameObject.SetActive(false);
            cameraManager.seiitirouCamera = true;
            rescueEvent = rescuePoint.GetComponent<RescueEvent>();
            soundManager.StopBgm(rescueEvent.ChasedBGM);
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
            �V�[�������[�h���čŏ�����ɂ���B
            SceneManager.LoadScene("Game");
            */
            //�@�ŏ��͖��ƂP�̌��֑O�ɕ����ł���悤�ɂ���B
            //�@2���ڂɍs�����_�Ń`�F�b�N�|�C���g��ς���B
            //�@���̍ۂ͓G�������Ă����B
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
    }
    public void OnClickBackHelpButton() 
    {
        //����̉摜���\������Ă����ꍇ�ŁA�߂邩���邩��ς���
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
    }
}

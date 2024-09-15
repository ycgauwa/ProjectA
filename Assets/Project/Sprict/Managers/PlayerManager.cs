using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    //メンバー変数として変数を定義してる。
    public float m_speed;//速さの定義
    public float Speed;
    public int staminaMax;
    public int stamina;
    public float staminaIntensity;
    public float staminaMaxIntensity;
    public float staminaIntensityMin;
    //Staticを使ってたり、インスタンス化している
    public static PlayerManager m_instance;
    public GameTeleportManager teleportManager;
    public GameObject mainCamera;
    public Rigidbody2D m_Rigidbody;
    public GameObject enemy;
    public Text StaminaString;
    public Canvas choiceCanvas;
    public Image stairChoice;
    public bool canUp = false;
    public string stringtext = "";
    public enum PlayerState
    {
        Idol,
        Run,
        Walk,
        Talk,
        Stop
    }
    public enum StaminaState
    {
        exhausted, //スタミナが０になってる状態から満タンまでの状態
        normal　　// 通常通りの状態
    }
    public PlayerState playerstate;
    public StaminaState staminastate;
    // Start is called before the first frame update
    void Start()
    {
        m_instance = this;
        m_speed = 0.075f;
        stamina = staminaMax;
        playerstate = PlayerState.Idol;
        staminastate = StaminaState.normal;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Application.targetFrameRate = 60;
        
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");
        var velocity = new Vector3(h, v)* Speed;
        transform.localPosition += velocity;
    }
    // 走っている時にスタミナが減るメソッド。
    public void Dashing(int staminaConsume)
    {
        playerstate = PlayerState.Run;
        stamina -= staminaConsume;
        staminaIntensity += 0.005f;
        
        if (0 < stamina) return;

        // スタミナが0になったら少しの間動けなくなりゆっくりスタミナが回復する。
        NoneStamina();
    }
    public void NoneStamina()
    {
        Speed = 0;
        stamina += 1;
        staminaIntensity -= 0.01f;
        Debug.Log("test");

    }
    private void Update()
    {
        //stringtext = stamina.ToString();
        //StaminaString.text = stringtext;
        Speed = m_speed;
        if (stamina > staminaMax)
        {
            stamina = staminaMax;
        }
        if(staminaIntensity < staminaIntensityMin)
        {
            staminaIntensity = staminaIntensityMin;
        }
        else if(staminaIntensity > staminaMaxIntensity)
        {
            staminaIntensity = staminaMaxIntensity;
        }

    }
    //ワープポイントに触れるとTPするコード
    /// <summary>
    /// 衝突した時のスクリプト、FindTeleportAddressの変数を使って関数を使っている
    /// ここではカメラやオブジェクトの位置を変数と＝で結んでおりこの関係があることによって
    /// 別スクリプトであるGameTeleportManageが活きる
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        bool isTeleport = other.gameObject.CompareTag("House") || other.gameObject.CompareTag("Warp1") ||
            other.gameObject.CompareTag("School1") || other.gameObject.CompareTag("School2") ||
            other.gameObject.CompareTag("School3") || other.gameObject.CompareTag("School4") ||
            other.gameObject.CompareTag("School5") || other.gameObject.CompareTag("School6") ||
            other.gameObject.CompareTag("School7") || other.gameObject.CompareTag("School8") ||
            other.gameObject.CompareTag("School9") || other.gameObject.CompareTag("School10") ||
            other.gameObject.CompareTag("Home1") || other.gameObject.CompareTag("Home2") ||
            other.gameObject.CompareTag("School11") || other.gameObject.CompareTag("School12") ||
            other.gameObject.CompareTag("School13") || other.gameObject.CompareTag("School14") ||
            other.gameObject.CompareTag("School15") || other.gameObject.CompareTag("School16") ||
            other.gameObject.CompareTag("School17") || other.gameObject.CompareTag("School18") ||
            other.gameObject.CompareTag("Minnka1-1") || other.gameObject.CompareTag("Minnka1-2") ||
            other.gameObject.CompareTag("Minnka1-3") || other.gameObject.CompareTag("Minnka1-4") ||
            other.gameObject.CompareTag("Minnka1-5") || other.gameObject.CompareTag("Minnka1-6") ||
            other.gameObject.CompareTag("Minnka1-7") || other.gameObject.CompareTag("Minnka1-8") ||
            other.gameObject.CompareTag("Minnka1-9") || other.gameObject.CompareTag("Minnka1-10") ||
            other.gameObject.CompareTag("Minnka1-11") || other.gameObject.CompareTag("Minnka1-12") ||
            other.gameObject.CompareTag("Minnka1-13") || other.gameObject.CompareTag("Minnka1-14") ||
            other.gameObject.CompareTag("Minnka1-15") || other.gameObject.CompareTag("Minnka1-16") ||
            other.gameObject.CompareTag("Minnka1-17") || other.gameObject.CompareTag("Minnka1-18") ||
            other.gameObject.CompareTag("Minnka1-20") || other.gameObject.CompareTag("Minnka1-21") ||
            other.gameObject.CompareTag("Minnka1-22") || other.gameObject.CompareTag("Minnka1-23") ||
            other.gameObject.CompareTag("Minnka1-24") || other.gameObject.CompareTag("Minnka1-25") ||
            other.gameObject.CompareTag("Minnka2-1") || other.gameObject.CompareTag("Minnka2-2") ||
            other.gameObject.CompareTag("Minnka2-3") || other.gameObject.CompareTag("Minnka2-4") ||
            other.gameObject.CompareTag("Minnka2-5") || other.gameObject.CompareTag("Minnka2-6") ||
            other.gameObject.CompareTag("Minnka2-7") || other.gameObject.CompareTag("Minnka2-8") ||
            other.gameObject.CompareTag("Minnka2-9") || other.gameObject.CompareTag("Minnka2-10") ||
            other.gameObject.CompareTag("Minnka2-11") || other.gameObject.CompareTag("Minnka2-12") ||
            other.gameObject.CompareTag("Minnka2-13") || other.gameObject.CompareTag("Minnka2-14") ||
            other.gameObject.CompareTag("Minnka2-15") || other.gameObject.CompareTag("Minnka2-16") ||
            other.gameObject.CompareTag("Minnka2-17") || other.gameObject.CompareTag("Minnka2-18") ||
            other.gameObject.CompareTag("Minnka2-19") || other.gameObject.CompareTag("Minnka2-20") ||
            other.gameObject.CompareTag("Minnka2-21") || other.gameObject.CompareTag("Minnka2-22") ||
            other.gameObject.CompareTag("Minnka2-23") || other.gameObject.CompareTag("Minnka2-24") ||
            other.gameObject.name == ("Ladder1-1") || other.gameObject.name == ("Ladder1-2");

        if (isTeleport == false) 
        {
            return;
        }
        //TeleportAddress型の変数、であり初期値はnull
        TeleportAddress teleportAddress = null;


        if (other.gameObject.CompareTag("House"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("House");
            transform.position = teleportAddress.playerPosition;
        }
        if (other.gameObject.CompareTag("Warp1"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Warp1");
            transform.position = teleportAddress.playerPosition;
        }
        if (other.gameObject.CompareTag("School1"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("School1");
            transform.position = teleportAddress.playerPosition;
            teleportManager.soundManager.PlaySe(teleportManager.schoolDoor);

        }
        if (other.gameObject.CompareTag("School2"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("School2");
            transform.position = teleportAddress.playerPosition;
            teleportManager.soundManager.PlaySe(teleportManager.schoolDoor);

        }
        if (other.gameObject.CompareTag("School3"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("School3");
            transform.position = teleportAddress.playerPosition;
            teleportManager.soundManager.PlaySe(teleportManager.schoolDoor);

        }
        if (other.gameObject.CompareTag("School4"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("School4");
            transform.position = teleportAddress.playerPosition;
            teleportManager.soundManager.PlaySe(teleportManager.schoolDoor);
        }
        if (other.gameObject.CompareTag("School5"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("School5");
            transform.position = teleportAddress.playerPosition;
            teleportManager.soundManager.PlaySe(teleportManager.schoolDoor);
        }
        if (other.gameObject.CompareTag("School6"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("School6");
            transform.position = teleportAddress.playerPosition;
            teleportManager.soundManager.PlaySe(teleportManager.schoolDoor);
        }
        if (other.gameObject.CompareTag("School7"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("School7");
            transform.position = teleportAddress.playerPosition;
            teleportManager.soundManager.PlaySe(teleportManager.schoolDoor);
        }
        if (other.gameObject.CompareTag("School8"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("School8");
            transform.position = teleportAddress.playerPosition;
            teleportManager.soundManager.PlaySe(teleportManager.schoolDoor);
        }
        if (other.gameObject.CompareTag("Home1"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Home1");
            transform.position = teleportAddress.playerPosition;
        }
        if (other.gameObject.CompareTag("Home2"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Home2");
            transform.position = teleportAddress.playerPosition;
        }
        if (other.gameObject.CompareTag("School9"))
        {
            GameManager.m_instance.stopSwitch = true;
            choiceCanvas.gameObject.SetActive(true);
            stairChoice.gameObject.SetActive(true);
        }
        if (other.gameObject.CompareTag("School10"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("School10");
            transform.position = teleportAddress.playerPosition;
            teleportManager.soundManager.PlaySe(teleportManager.schoolDoor);
        }
        if(other.gameObject.CompareTag("School11"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("School11");
            transform.position = teleportAddress.playerPosition;
            teleportManager.soundManager.PlaySe(teleportManager.schoolDoor);


        }
        if(other.gameObject.CompareTag("School12"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("School12");
            transform.position = teleportAddress.playerPosition;
            teleportManager.soundManager.PlaySe(teleportManager.schoolDoor);

        }
        if(other.gameObject.CompareTag("School13"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("School13");
            transform.position = teleportAddress.playerPosition;
            teleportManager.soundManager.PlaySe(teleportManager.schoolDoor);

        }
        if(other.gameObject.CompareTag("School14"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("School14");
            transform.position = teleportAddress.playerPosition;
            teleportManager.soundManager.PlaySe(teleportManager.schoolDoor);
        }
        if(other.gameObject.CompareTag("School15"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("School15");
            transform.position = teleportAddress.playerPosition;
            teleportManager.soundManager.PlaySe(teleportManager.schoolDoor);
        }
        if(other.gameObject.CompareTag("School16"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("School16");
            transform.position = teleportAddress.playerPosition;
            teleportManager.soundManager.PlaySe(teleportManager.schoolDoor);
        }
        if(other.gameObject.CompareTag("School17"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("School17");
            transform.position = teleportAddress.playerPosition;
            teleportManager.soundManager.PlaySe(teleportManager.schoolDoor);
        }
        if(other.gameObject.CompareTag("School18"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("School18");
            transform.position = teleportAddress.playerPosition;
            teleportManager.soundManager.PlaySe(teleportManager.schoolDoor);
        }
        if(other.gameObject.CompareTag("Minnka1-1"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka1-1");
            transform.position = teleportAddress.playerPosition;
            teleportManager.soundManager.PlaySe(teleportManager.climbStairs);
        }
        if(other.gameObject.CompareTag("Minnka1-2"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka1-2");
            transform.position = teleportAddress.playerPosition;
            teleportManager.soundManager.PlaySe(teleportManager.climbStairs);
        }
        if(other.gameObject.CompareTag("Minnka1-3"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka1-3");
            transform.position = teleportAddress.playerPosition;
            teleportManager.soundManager.PlaySe(teleportManager.minnkaDoor);
        }
        if(other.gameObject.CompareTag("Minnka1-4"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka1-4");
            transform.position = teleportAddress.playerPosition;
            teleportManager.soundManager.PlaySe(teleportManager.minnkaDoor);
        }
        if(other.gameObject.CompareTag("Minnka1-5"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka1-5");
            transform.position = teleportAddress.playerPosition;
            teleportManager.soundManager.PlaySe(teleportManager.bathDoor);
        }
        if(other.gameObject.CompareTag("Minnka1-6"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka1-6");
            transform.position = teleportAddress.playerPosition;
            teleportManager.soundManager.PlaySe(teleportManager.bathDoor);
        }
        if(other.gameObject.CompareTag("Minnka1-7"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka1-7");
            transform.position = teleportAddress.playerPosition;
            teleportManager.soundManager.PlaySe(teleportManager.minnkaDoor);
            if (!enemy.activeSelf)
            {
                enemy.transform.position = new Vector2(0, 0);
                Homing.m_instance.enemyEmerge = false;
            }
            else
            {
                return;
            }
        }
        if(other.gameObject.CompareTag("Minnka1-8"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka1-8");
            transform.position = teleportAddress.playerPosition;
            teleportManager.soundManager.PlaySe(teleportManager.minnkaDoor);
            if (Homing.m_instance.enemyEmerge == false)
            {
                Homing.m_instance.enemyEmerge = true;
            }
        }
        if(other.gameObject.CompareTag("Minnka1-9"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka1-9");
            transform.position = teleportAddress.playerPosition;
            teleportManager.soundManager.PlaySe(teleportManager.minnkaDoor);
        }
        if(other.gameObject.CompareTag("Minnka1-10"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka1-10");
            transform.position = teleportAddress.playerPosition;
            teleportManager.soundManager.PlaySe(teleportManager.minnkaDoor);
        }
        if(other.gameObject.CompareTag("Minnka1-11"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka1-11");
            transform.position = teleportAddress.playerPosition;
            teleportManager.soundManager.PlaySe(teleportManager.minnkaDoor);
        }
        if(other.gameObject.CompareTag("Minnka1-12"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka1-12");
            transform.position = teleportAddress.playerPosition;
            teleportManager.soundManager.PlaySe(teleportManager.minnkaDoor);
        }
        if(other.gameObject.CompareTag("Minnka1-13"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka1-13");
            transform.position = teleportAddress.playerPosition;
            teleportManager.soundManager.PlaySe(teleportManager.minnkaDoor);
        }
        if(other.gameObject.CompareTag("Minnka1-14"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka1-14");
            transform.position = teleportAddress.playerPosition;
            teleportManager.soundManager.PlaySe(teleportManager.minnkaDoor);
        }
        if(other.gameObject.CompareTag("Minnka1-15"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka1-15");
            transform.position = teleportAddress.playerPosition;
            teleportManager.soundManager.PlaySe(teleportManager.minnkaDoor);
        }
        if(other.gameObject.CompareTag("Minnka1-16"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka1-16");
            transform.position = teleportAddress.playerPosition;
            teleportManager.soundManager.PlaySe(teleportManager.minnkaDoor);
        }
        if(other.gameObject.CompareTag("Minnka1-17"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka1-17");
            transform.position = teleportAddress.playerPosition;
            teleportManager.soundManager.PlaySe(teleportManager.minnkaDoor);
        }
        if(other.gameObject.CompareTag("Minnka1-18"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka1-18");
            transform.position = teleportAddress.playerPosition;
            teleportManager.soundManager.PlaySe(teleportManager.minnkaDoor);
        }
        if(other.gameObject.CompareTag("Minnka1-19"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka1-19");
            transform.position = teleportAddress.playerPosition;
            teleportManager.soundManager.PlaySe(teleportManager.minnkaDoor);
        }
        if(other.gameObject.CompareTag("Minnka1-20"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka1-20");
            transform.position = teleportAddress.playerPosition;
            teleportManager.soundManager.PlaySe(teleportManager.minnkaDoor);
            if (!enemy.activeSelf)
            {
                enemy.transform.position = new Vector2(0, 0);
                Homing.m_instance.enemyEmerge = false;
            }
            else
            {
                return;
            }
        }
        if(other.gameObject.CompareTag("Minnka1-21"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka1-21");
            transform.position = teleportAddress.playerPosition;
            teleportManager.soundManager.PlaySe(teleportManager.minnkaDoor);
        }
        if(other.gameObject.CompareTag("Minnka1-22"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka1-22");
            transform.position = teleportAddress.playerPosition;
            teleportManager.soundManager.PlaySe(teleportManager.climbStairs);
        }
        if(other.gameObject.CompareTag("Minnka1-23"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka1-23");
            transform.position = teleportAddress.playerPosition;
            teleportManager.soundManager.PlaySe(teleportManager.climbStairs);
        }
        if(other.gameObject.CompareTag("Minnka1-24"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka1-24");
            transform.position = teleportAddress.playerPosition;
        }
        if(other.gameObject.CompareTag("Minnka1-25"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka1-25");
            transform.position = teleportAddress.playerPosition;
        }
        if(other.gameObject.CompareTag("Minnka2-1"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka2-1");
            transform.position = teleportAddress.playerPosition;
        }
        if(other.gameObject.CompareTag("Minnka2-2"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka2-2");
            transform.position = teleportAddress.playerPosition;
        }
        if(other.gameObject.CompareTag("Minnka2-3"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka2-3");
            transform.position = teleportAddress.playerPosition;
        }
        if(other.gameObject.CompareTag("Minnka2-4"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka2-4");
            transform.position = teleportAddress.playerPosition;
        }
        if(other.gameObject.CompareTag("Minnka2-5"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka2-5");
            transform.position = teleportAddress.playerPosition;
        }
        if(other.gameObject.CompareTag("Minnka2-6"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka2-6");
            transform.position = teleportAddress.playerPosition;
        }
        if(other.gameObject.CompareTag("Minnka2-7"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka2-7");
            transform.position = teleportAddress.playerPosition;
        }
        if(other.gameObject.CompareTag("Minnka2-8"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka2-8");
            transform.position = teleportAddress.playerPosition;
        }
        if(other.gameObject.CompareTag("Minnka2-9"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka2-9");
            transform.position = teleportAddress.playerPosition;
        }
        if(other.gameObject.CompareTag("Minnka2-10"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka2-10");
            transform.position = teleportAddress.playerPosition;
        }
        if(other.gameObject.CompareTag("Minnka2-11"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka2-11");
            transform.position = teleportAddress.playerPosition;
        }
        if(other.gameObject.CompareTag("Minnka2-12"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka2-12");
            transform.position = teleportAddress.playerPosition;
        }
        if(other.gameObject.CompareTag("Minnka2-13"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka2-13");
            transform.position = teleportAddress.playerPosition;
        }
        if(other.gameObject.CompareTag("Minnka2-14"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka2-14");
            transform.position = teleportAddress.playerPosition;
        }
        if(other.gameObject.CompareTag("Minnka2-15"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka2-15");
            transform.position = teleportAddress.playerPosition;
        }
        if(other.gameObject.CompareTag("Minnka2-16"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka2-16");
            transform.position = teleportAddress.playerPosition;
        }
        if(other.gameObject.CompareTag("Minnka2-17"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka2-17");
            transform.position = teleportAddress.playerPosition;
        }
        if(other.gameObject.CompareTag("Minnka2-18"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka2-18");
            transform.position = teleportAddress.playerPosition;
        }
        if(other.gameObject.CompareTag("Minnka2-19"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka2-19");
            transform.position = teleportAddress.playerPosition;
        }
        if(other.gameObject.CompareTag("Minnka2-20"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka2-20");
            transform.position = teleportAddress.playerPosition;
        }
        if(other.gameObject.CompareTag("Minnka2-21"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka2-21");
            transform.position = teleportAddress.playerPosition;
        }
        if(other.gameObject.CompareTag("Minnka2-22"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka2-22");
            transform.position = teleportAddress.playerPosition;
        }
        if(other.gameObject.CompareTag("Minnka2-23"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka2-23");
            transform.position = teleportAddress.playerPosition;
        }
        if (other.gameObject.name == "Ladder1-1")
        {
            teleportAddress = teleportManager.FindTeleportAddress("Ladder1-1");
            transform.position = teleportAddress.playerPosition;
        }
        if (other.gameObject.name == ("Ladder1-2"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Ladder1-2");
            transform.position = teleportAddress.playerPosition;
        }
        //別クラスのメソッドの行使引数はteleportAddress
        teleportManager.OnPlayerTeleport(teleportAddress);
    }
    public void SeiitirouRes()
    {
        //if(m_Rigidbody == null)
        //{
        //    Debug.Log("null");
        //}
        //Rigidbody2D rb = GetComponent<Rigidbody2D>();
        //GameTeleportManager gametp = GetComponent<GameTeleportManager>();
    }
    public void YesUp()
    {
        canUp = true;
        gameObject.transform.position = new Vector2(-7, -71);
        stairChoice.gameObject.SetActive(false);
        choiceCanvas.gameObject.SetActive(false);
        GameManager.m_instance.stopSwitch = false;
    }
    public void NoUp()
    {
        canUp = false;
        stairChoice.gameObject.SetActive(false);
        choiceCanvas.gameObject.SetActive(false);
        GameManager.m_instance.stopSwitch = false;
    }

}

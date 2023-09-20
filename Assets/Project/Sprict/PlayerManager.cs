using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //メンバー変数として変数を定義してる。
    public float m_speed;//速さの定義
    //Staticを使ってたり、インスタンス化している
    public static PlayerManager m_instance;
    public GameTeleportManager teleportManager;
    public GameObject mainCamera;
    public Rigidbody2D m_Rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        m_instance = this;
        m_speed = 0.05f;

    }

    // Update is called once per frame
    private void Update()
    {
        Application.targetFrameRate = 60;

        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        var velocity = new Vector3(h, v)* m_speed;
        transform.localPosition += velocity;
        
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
            other.gameObject.CompareTag("Minnka1-17") || other.gameObject.CompareTag("Minnka1-18");

        if(isTeleport == false) 
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


        }
        if (other.gameObject.CompareTag("School2"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("School2");
            transform.position = teleportAddress.playerPosition;


        }
        if (other.gameObject.CompareTag("School3"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("School3");
            transform.position = teleportAddress.playerPosition;


        }
        if (other.gameObject.CompareTag("School4"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("School4");
            transform.position = teleportAddress.playerPosition;
        }
        if (other.gameObject.CompareTag("School5"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("School5");
            transform.position = teleportAddress.playerPosition;
        }
        if (other.gameObject.CompareTag("School6"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("School6");
            transform.position = teleportAddress.playerPosition;
        }
        if (other.gameObject.CompareTag("School7"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("School7");
            transform.position = teleportAddress.playerPosition;
        }
        if (other.gameObject.CompareTag("School8"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("School8");
            transform.position = teleportAddress.playerPosition;
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
            teleportAddress = teleportManager.FindTeleportAddress("School9");
            transform.position = teleportAddress.playerPosition;
        }
        if (other.gameObject.CompareTag("School10"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("School10");
            transform.position = teleportAddress.playerPosition;
        }
        if(other.gameObject.CompareTag("School11"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("School11");
            transform.position = teleportAddress.playerPosition;


        }
        if(other.gameObject.CompareTag("School12"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("School12");
            transform.position = teleportAddress.playerPosition;


        }
        if(other.gameObject.CompareTag("School13"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("School13");
            transform.position = teleportAddress.playerPosition;


        }
        if(other.gameObject.CompareTag("School14"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("School14");
            transform.position = teleportAddress.playerPosition;
        }
        if(other.gameObject.CompareTag("School15"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("School15");
            transform.position = teleportAddress.playerPosition;
        }
        if(other.gameObject.CompareTag("School16"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("School16");
            transform.position = teleportAddress.playerPosition;
        }
        if(other.gameObject.CompareTag("School17"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("School17");
            transform.position = teleportAddress.playerPosition;
        }
        if(other.gameObject.CompareTag("School18"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("School18");
            transform.position = teleportAddress.playerPosition;
        }
        if(other.gameObject.CompareTag("Minnka1-1"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka1-1");
            transform.position = teleportAddress.playerPosition;
        }
        if(other.gameObject.CompareTag("Minnka1-2"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka1-2");
            transform.position = teleportAddress.playerPosition;
        }
        if(other.gameObject.CompareTag("Minnka1-3"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka1-3");
            transform.position = teleportAddress.playerPosition;
        }
        if(other.gameObject.CompareTag("Minnka1-4"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka1-4");
            transform.position = teleportAddress.playerPosition;
        }
        if(other.gameObject.CompareTag("Minnka1-5"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka1-5");
            transform.position = teleportAddress.playerPosition;
        }
        if(other.gameObject.CompareTag("Minnka1-6"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka1-6");
            transform.position = teleportAddress.playerPosition;
        }
        if(other.gameObject.CompareTag("Minnka1-7"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka1-7");
            transform.position = teleportAddress.playerPosition;
        }
        if(other.gameObject.CompareTag("Minnka1-8"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka1-8");
            transform.position = teleportAddress.playerPosition;
        }
        if(other.gameObject.CompareTag("Minnka1-9"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka1-9");
            transform.position = teleportAddress.playerPosition;
        }
        if(other.gameObject.CompareTag("Minnka1-10"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka1-10");
            transform.position = teleportAddress.playerPosition;
        }
        if(other.gameObject.CompareTag("Minnka1-11"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka1-11");
            transform.position = teleportAddress.playerPosition;
        }
        if(other.gameObject.CompareTag("Minnka1-12"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka1-12");
            transform.position = teleportAddress.playerPosition;
        }
        if(other.gameObject.CompareTag("Minnka1-13"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka1-13");
            transform.position = teleportAddress.playerPosition;
        }
        if(other.gameObject.CompareTag("Minnka1-14"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka1-14");
            transform.position = teleportAddress.playerPosition;
        }
        if(other.gameObject.CompareTag("Minnka1-15"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka1-15");
            transform.position = teleportAddress.playerPosition;
        }
        if(other.gameObject.CompareTag("Minnka1-16"))
        {
            teleportAddress = teleportManager.FindTeleportAddress("Minnka1-16");
            transform.position = teleportAddress.playerPosition;
        }
        //別クラスのメソッドの行使引数はteleportAddress
        teleportManager.OnPlayerTeleport(teleportAddress);
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.LowLevel;

public class Homing : MonoBehaviour
{
    //プレイヤーのTransform
    Transform playerTr;
    //Staticを使ってたり、インスタンス化している
    public static Homing m_instance;
    public GameTeleportManager teleportManager;
    [SerializeField] 
    public float speed = 2; //敵の動くスピード
    public ToEvent3 toevent3;
    public RescueEvent rescueEvent;
    public ItemDateBase itemDateBase;
    public ItemSprictW itemSprictW;
    public GameObject player;
    public float enemyCount = 0.0f; //　敵が追いかけている時間
    public Canvas gameoverWindow;
    public Image buttonPanel;
    public bool enemyEmerge;
    public bool isMove;
    Rigidbody2D rbody;
    NPCAnimationController cr;
    public Vector2 enemyPosition;
    private Vector2 enemyMovement;

    private void Start()
    {
        m_instance = this;
        // プレイヤーのTransformを取得（プレイヤーのタグPlayerに設定必要）
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
        cr = GetComponentInChildren<NPCAnimationController>();
        isMove = true;
        rbody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if(toevent3.event3flag && enemyEmerge)
        {
            if(Vector2.Distance(transform.position, playerTr.position) < 0.1f)
                return;
            // プレイヤーに向けて進む
            enemyMovement = Vector2.MoveTowards(
                transform.position,
                new Vector2(playerTr.position.x, playerTr.position.y),
                speed);

        transform.position = Vector2.MoveTowards(
                transform.position,
                new Vector2(playerTr.position.x, playerTr.position.y),
                speed * Time.deltaTime);
        }
        if(speed > 0)
        {
            enemyCount += Time.deltaTime;
        }
    }
    void FixedUpdate()
    {
        if (isMove)
        {
            Vector2 currentPos = rbody.position;

            enemyPosition.x = transform.position.x - enemyPosition.x;
            enemyPosition.y = transform.position.y - enemyPosition.y;
            enemyMovement = new Vector2(enemyPosition.x,enemyPosition.y);
            cr.SetDirection(enemyMovement);
        }
        enemyPosition.x = transform.position.x;
        enemyPosition.y = transform.position.y;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag.Equals("Player"))
        {
            // 藁人形を人形を持っているときの処理
            if(itemDateBase.items[8].checkPossession == true)
            {
                itemSprictW.ItemEffect();
            }
            else
            {
                // 食べられた時のサウンドを流す
                // ゲームオーバー画面を出すためのキャンバスとその数秒後にボタンを出す
                gameoverWindow.gameObject.SetActive(true);
                GameManager.m_instance.stopSwitch = true;
                Invoke("AppearChoice", 2.5f);
            }
        }
    }

    public void AppearChoice()
    {
        buttonPanel.gameObject.SetActive(true);
    }

    // 敵が一定の距離を動いた状態でプレイヤーがワープしたら追っかけてこなくなる
    // 動いた距離を記録（あるいは時間を記録する変数を作成）その後動いた距離が一定以上になると
    // その変数の値が０となる。０の時は（一定の数以下なら）ワープしないを追加する


    //敵が時間差テレポートするメソッド
    public void TimerTeleport()
    {
        if(toevent3.event3flag && enemyEmerge)
        {
            var teleportAddress = teleportManager.FindTeleportAddress("House");
        }
    }
    //プレイヤーがTPことを認知させる
    

}

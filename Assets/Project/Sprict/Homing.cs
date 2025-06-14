using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.LowLevel;

public class Homing : MonoBehaviour
{
    Transform playerTr;
    public static Homing m_instance;
    public GameTeleportManager teleportManager;
    [SerializeField]
    public float speed; //敵の動くスピード
    public ToEvent3 toevent3;
    public RescueEvent rescueEvent;
    public ItemDateBase itemDateBase;
    public ItemSprictW itemSprictW;
    public GameObject player;
    public float enemyCount = 0.0f; //　敵が追いかけている時間
    public Canvas gameoverWindow;
    public Image buttonPanel;
    public bool eventEnemySpawnToggle; //イベント用に敵の出現を調節するBool変数
    public bool enemyEmerge; //この変数がtrueの時に敵が出てこれるようになりfalseの時は出てこない。
    public bool isMove;
    Rigidbody2D rbody;
    NPCAnimationController cr;
    public Vector2 enemyPosition;
    private Vector2 enemyMovement;
    public AudioClip meatEat;
    public AudioClip chasedBGM;


    private void Awake()
    {
        if(m_instance == null)
        {
            m_instance = this;
        }
        else Destroy(m_instance);
    }
    private void Start()
    {
        // プレイヤーのTransformを取得（プレイヤーのタグPlayerに設定必要）
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
        cr = GetComponentInChildren<NPCAnimationController>();
        isMove = true;
        eventEnemySpawnToggle = true;
        rbody = GetComponent<Rigidbody2D>();
        enemyCount = 0;
    }
    private void Update()
    {
        if(enemyEmerge)
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
        if(enemyEmerge == true && speed > 0 && PlayerManager.m_instance.playerstate != PlayerManager.PlayerState.Talk && PlayerManager.m_instance.playerstate != PlayerManager.PlayerState.Stop)
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
            if(itemDateBase.GetItemId(301).checkPossession == true)
            {
                itemSprictW.ItemEffect();
            }
            else
            {
                // 食べられた時のサウンドを流す
                SoundManager.sound_Instance.PlaySe(meatEat);
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
        if (gameObject.activeSelf && rescueEvent.RescueSwitch == false) teleportManager.StopChased();
    }

    // 敵が一定の距離を動いた状態でプレイヤーがワープしたら追っかけてこなくなる
    // 動いた距離を記録（あるいは時間を記録する変数を作成）その後動いた距離が一定以上になると
    // その変数の値が０となる。０の時は（一定の数以下なら）ワープしないを追加する


    /*
    public void TimerTeleport()
    {
        if(toevent3.event3flag && enemyEmerge)
        {
            var teleportAddress = teleportManager.FindTeleportAddress("House");
        }
    }
    */
    

}

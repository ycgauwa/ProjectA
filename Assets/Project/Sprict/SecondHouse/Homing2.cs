using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Homing2 : MonoBehaviour
{
    Transform playerTr;
    public static Homing2 m_instance;
    public GameTeleportManager teleportManager;
    [SerializeField]
    public float speed ; //速度のため、加速度と関係を持つ(max5)
    public int count = 0;
    public GameObject player;
    public float enemyCount = 0.0f; 
    public float acceleration = 0.0f;//加速度(max1.5)
    public Canvas gameoverWindow;
    public Image buttonPanel;
    public bool enemyEmerge;
    public bool isMove;
    public float savedSpeed = 0;
    public float savedAcceleration = 0;
    Rigidbody2D rbody;
    //NPCAnimationController cr;
    public Vector2 enemyPosition;
    private Vector2 enemyMovement;

    private Rigidbody2D rb;
    private  Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool facingRight = true;
    private Vector3 previousPosition;
    private Vector3 currentPosition;
    //2軒目の犬の動作スクリプト。流れとしては溜めて一気に動く感じ。
    private void Start()
    {
        m_instance = this;
        // プレイヤーのTransformを取得（プレイヤーのタグPlayerに設定必要）
        if(GameObject.FindGameObjectWithTag("Player") == null)
        {
            playerTr = GameObject.FindGameObjectWithTag("Seiitirou").transform;
        }
        else
        {
            playerTr = GameObject.FindGameObjectWithTag("Player").transform;
        }
        isMove = true;
        rbody = GetComponent<Rigidbody2D>();
        enemyCount = 0;
        GameTeleportManager.chasedTime = true;
        animator = GetComponent<Animator>();
        currentPosition = transform.position;
        previousPosition = currentPosition;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if(enemyEmerge)
        {
            if(count % 60 == 0)
            {
                if(acceleration < 1f)
                    acceleration += 0.3f;
                if(speed < 5.0f)
                    speed += acceleration;
            }
            count++; // カウントアップ
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

        previousPosition = currentPosition;
        // 現在の位置を更新
        currentPosition = transform.position;

        // 移動距離を計算
        Vector3 movement = currentPosition - previousPosition;
        // アニメーターに速度を設定
        animator.SetFloat("Speed", speed);

        if(movement.x > 0 && facingRight)
        {
            Debug.Log("Right");
            Flip(-1.5f);
        }
        else if(movement.x < 0 && !facingRight)
        {
            Debug.Log("Left");
            Flip(1.5f);
        }
    }
    void FixedUpdate()
    {
        if(isMove)
        {
            Vector2 currentPos = rbody.position;

            enemyPosition.x = transform.position.x - enemyPosition.x;
            enemyPosition.y = transform.position.y - enemyPosition.y;
            enemyMovement = new Vector2(enemyPosition.x, enemyPosition.y);
            //cr.SetDirection(enemyMovement);
        }
        enemyPosition.x = transform.position.x;
        enemyPosition.y = transform.position.y;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
        {
            /* 藁人形を人形を持っているときの処理
            if(itemDateBase.items[8].checkPossession == true)
            {
                itemSprictW.ItemEffect();
            }
            else*/
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
        if(gameObject.activeSelf) teleportManager.StopChased();
    }

    // 敵が一定の距離を動いた状態でプレイヤーがワープしたら追っかけてこなくなる
    // 動いた距離を記録（あるいは時間を記録する変数を作成）その後動いた距離が一定以上になると
    // その変数の値が０となる。０の時は（一定の数以下なら）ワープしないを追加する


    //敵が時間差テレポートするメソッド
    public void TimerTeleport()
    {
        if(enemyEmerge)
        {
            var teleportAddress = teleportManager.FindTeleportAddress("House");
        }
    }
    //プレイヤーがTPことを認知させる
    public void StopEnemy()
    {
        if(!gameObject.activeSelf)
            gameObject.SetActive(true);
        SoundManager.sound_Instance.PauseBgm(SecondHouseManager.secondHouse_instance.fearMusic);

        if(acceleration != 0)
            savedAcceleration = acceleration;
        acceleration = 0;
        if(speed != 0)
            savedSpeed = speed;
        speed = 0;
        enemyEmerge = false;
    }
    public void MoveEnemy()
    {
        acceleration = savedAcceleration;
        speed = savedSpeed;
        SoundManager.sound_Instance.UnPauseBgm(SecondHouseManager.secondHouse_instance.fearMusic);
        if(gameObject.activeSelf)
            gameObject.SetActive(false);
        enemyEmerge = true;
    }
    void Flip(float scaleX)
    {
        // 向きを反転
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x = scaleX;
        transform.localScale = scaler;
    }
}

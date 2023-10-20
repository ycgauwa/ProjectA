using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public static float ACCELERATION = 10.0f;               //加速度
    public static float SPEED_MIN = 2.0f;                   //速度の最小値
    public static float SPEED_MAX = 4.0f;                   //速度の最大値
    public static float JUMP_HEIGHT_MAX = 3.0f;             //ジャンプの高さ
    public static float JUMP_KEY_RELEASE_REDUSE = 0.5f;     //ジャンプからの減速値

    private Rigidbody2D rb;

    public enum STEP        //　Playerの各種状態を表すデータ型
    {
        NONE = -1,          //　状態情報なし。
        RUN = 0,            //　走る。
        JUMP,               //　ジャンプ。
        MISS,               //　ミス。
        NUM,                //　状態が何種類あるかを示す　（＝3）
    };

    public STEP step = STEP.NONE;       //Playerの現在の状態
    public STEP next_step = STEP.NONE;  //Playerの次の状態。


    public float step_timer = 0.0f;         //経過時間 
    private bool is_landed = false;         //着地しているかどうか
    private bool is_collided = false;       //何かとぶつかっているか
    private bool is_key_released = false;   //ボタンが離されているかどうか

    public static float NARAKU_HEIGHT = -5.0f;

    public float current_speed = 0.0f;
    public LevelControl level_control = null;

    private float click_timer = -1.0f;      //　ボタンが押されてからの時間
    private float CLICK_GRACE_TIME = 0.5f;  //　「ジャンプしたい意思」を受け付ける時間。

    public Canvas resultCanvas;             //　ゲーム終了時のキャンバス
    public Text resultBoard;                //　リザルト時のスコア表示テキスト

    void Start()
    {
        //ゲーム開始早々から走り出すようにしたい。
        next_step = STEP.RUN;

        rb = gameObject.AddComponent<Rigidbody2D>() as Rigidbody2D;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.freezeRotation = true;
    }

    void Update()
    {
        Debug.Log(is_landed);
        this.transform.Translate(new Vector3(0.1f, 0.0f, 0.0f )* Time.deltaTime);
        
        //Vector2 velocity = rigidbady.velocity;  速度を設定。
        Vector2 velocity = rb.velocity;
        current_speed = level_control.getPlayerSpeed();

                                // 着地状態がどうかチェック。

        switch(this.step)
        {
            case STEP.RUN:
            case STEP.JUMP:
                // 現在の位置がしきい値よりも下ならば
                if(transform.position.y < NARAKU_HEIGHT)
                {
                    next_step = STEP.MISS;    //　「ミス」状態にする
                }
                break;
        }

        step_timer += Time.deltaTime;            // 経過時間を進める。

        if(Input.GetMouseButtonDown(0))          // タイマーをリセット
        {
            click_timer = 0.0f;                 
        }
        else
        {
            if(click_timer >= 0.0f)              // そうでなければ
            {
                click_timer += Time.deltaTime;   // 経過時間を加算
            }
        }

        // 「次の状態が決まっていなければ、状態の変化を調べる。」
        if(next_step == STEP.NONE)
        {
            switch(this.step)// Playerの現在の状態で分岐。
            {
                case STEP.RUN:
                    // click_timerが０以上、CLICK_GRACE＿TIME以下ならば。
                    if(0.0f <= click_timer && click_timer <= CLICK_GRACE_TIME)
                    {
                        if(is_landed)               // 着地しているならば「ボタンが押されていない」ことを示す。
                        {
                            click_timer = -1.0f;    // -1.0fに
                            next_step = STEP.JUMP;  // ジャンプ状態に
                            is_landed = false;
                        }
                    }
                    //走行中の場合
                    //if(!is_landed)
                    //{
                    //    //　走行中、着地していない場合、何もしない
                    //}
                    //else if(transform.position.y < 1.1)
                    //{
                    //    if(Input.GetMouseButtonDown(0))// 走行中で、着地していて、左ボタンが押されたら。
                    //    {
                    //        // 次の状態をジャンプに変更。（押した瞬間しかJUMPにならない?）
                    //        next_step = STEP.JUMP;
                    //    }
                    //}
                    break;
                case STEP.JUMP:
                    if(is_landed)
                    {
                        Debug.Log("STEP.JUMP is_landed");
                        // ジャンプ中、着地していたら、次の状態を走行中に変更。
                        next_step = STEP.RUN;
                    }
                    break;
            }
        }

        while(next_step != STEP.NONE)
        {
            step = next_step;
            next_step = STEP.NONE;
            switch(step)
            {
                case STEP.JUMP:
                    //　ジャンプの高さからジャンプの初速を計算
                    velocity.y = Mathf.Sqrt(2.0f * 9.8f * PlayerControl.JUMP_HEIGHT_MAX);
                    // 「ボタンが離されたフラグ」をクリアする。
                    is_key_released = false;
                    Debug.Log("next_step != STEP.NONE　STEP.JUMP");
                    break;
            }
            step_timer = 0.0f; //　状態が変化したので、経過時間をゼロにリセット。
        }

        //　状態ごとの、毎フレームの更新処理。
        switch(step)
        {
            case STEP.RUN:

                velocity.x += PlayerControl.ACCELERATION * Time.deltaTime;
                //　速度が最高速度の制限超えたら。
                //if(Mathf.Abs(velocity.x) > PlayerControl.SPEED_MAX)
                //{
                //    //　最高速度の制限以下に保つ。
                //    velocity.x *= PlayerControl.SPEED_MAX / Mathf.Abs(velocity.x);
                //}

                //　計算で求めたスピードが、設定すべきスピードを超えていたら。
                if(Mathf.Abs(velocity.x) > this.current_speed)
                {
                    //　超えないように調整する。
                    velocity.x *= current_speed / Mathf.Abs(velocity.x);
                }
                break;
            case STEP.JUMP:
                do
                {
                    // 「ボタンが離された瞬間」じゃなかった。
                    if(!Input.GetMouseButtonUp(0))
                    {
                        Debug.Log("velocity.y <= 0.0f");
                        break;      // 何もせずにループを抜ける
                    }
                    //　減速済みなら、（２回以上減速しないように）
                    if(is_key_released)
                    {
                        Debug.Log("velocity.y <= 0.0f");
                        break;      // 何もせずにループを抜ける
                    }
                    //　上下方向の速度が０以下なら（下降中なら）
                    if(velocity.y <= 0.0f)
                    {
                        Debug.Log("velocity.y <= 0.0f");
                        break; // 何もせずにループ
                    }
                    //　ボタンが離されていて、上昇中なら、減速開始
                    //　ジャンプの上昇はここでおしまい
                    velocity.y *= JUMP_KEY_RELEASE_REDUSE;

                    is_key_released = true;
                } while(false);
                break;

            case STEP.MISS:
                //　加速度（ACCELERATION）を引き算して、Playerの速度を遅くしていく。
                velocity.x -= PlayerControl.ACCELERATION * Time.deltaTime;
                if(velocity.x < 0.0f)       //プレイヤーの速度が負なら
                {
                    velocity.x = 0.0f;      //ゼロにする
                }
                if(transform.position.y  <  -60 )
                {
                    Time.timeScale = 0;     //時間を停止する
                    resultBoard.text = string.Format("{0}", GameRoot.score );
                    resultCanvas.gameObject.SetActive(true);
                }
                break;
        }
        //  Rigidbodyの速度を、上記で求めた速度で更新。
        //　(この行は、状態にかかわらず毎回実行される)
        rb.velocity = velocity;
    }
    /*private void check_landed()
    {
        is_landed = false;      //とりあえずfalseにしておく。
        do
        {
            Vector3 s = this.transform.position;    //Playerの現在の位置。
            Vector3 e = s +  Vector3.down * 1.0f;    //sから下に1.0fに移動した位置。
            
            RaycastHit2D hit = Physics2D.Linecast(s, e);
            if(! hit)    //sからeの間に何もない場面。
            {
                Debug.Log("nanimonai");
                break;　 //何もせずにdo～whileループを抜ける（脱出口へ）
            }
            Debug.Log(hit);
            //　sからeの間に何かがあった場合、以下の処理が行われる
            if(this.step == STEP.JUMP)      // 現在、ジャンプ状態ならば、
            {
                Debug.Log("nankaaru1");
                //　経過時間が3.0f未満ならば
                if(step_timer < Time.deltaTime * 3.0f)
                {
                    break;　//何もせずdo～whileループを抜ける（脱出口へ）
                }
            }
            Debug.Log("nankaaru2");
            //　sからeの間に何かがあり、JUMP直後でない場合のみ、以下が実行される。
            is_landed = true;

        } while(false);
        //ループの脱出口。
    }*/
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);
        is_landed = true;
    }

}

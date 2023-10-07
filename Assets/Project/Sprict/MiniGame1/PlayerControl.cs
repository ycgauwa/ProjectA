using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public static float ACCELERATION = 10.0f;               //加速度
    public static float SPEED_MIN = 2.0f;                   // 速度の最小値
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

    void Start()
    {
        //ゲーム開始早々から走り出すようにしたい。
        next_step = STEP.RUN;

        rb = gameObject.AddComponent<Rigidbody2D>() as Rigidbody2D;
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    void Update()
    {
        Debug.Log(step);
        this.transform.Translate(new Vector3(0.001f, 0.0f, 0.0f )* Time.deltaTime);
        
        //Vector2 velocity = rigidbady.velocity;  速度を設定。
        Vector2 velocity = rb.velocity;
        check_landed();                          // 着地状態がどうかチェック。

        switch(this.step)
        {
            case STEP.RUN:
            case STEP.JUMP:
                // 現在の位置がしきい値よりも下ならば
                if(transform.position.y < NARAKU_HEIGHT)
                {
                    this.next_step = STEP.MISS;    //　「ミス」状態にする
                }
                break;
        }

        step_timer += Time.deltaTime;            // 経過時間を進める。

        // 「次の状態が決まっていなければ、状態の変化を調べる。」
        if(next_step == STEP.NONE)
        {
            Debug.Log("next_step == STEP.NONE");
            switch(this.step)// Playerの現在の状態で分岐。
            {
                case STEP.RUN:　//走行中の場合
                    if(!is_landed)
                    {
                        Debug.Log("!is_landed_only");
                        //　走行中、着地していない場合、何もしない
                    }
                    else
                    {
                        if(Input.GetMouseButtonDown(0))
                        {
                            // 走行中で、着地していて、左ボタンが押されたら。
                            // 次の状態をジャンプに変更。
                            next_step = STEP.JUMP;
                            Debug.Log("!is_landed0");
                        }
                    }
                    break;
                case STEP.JUMP:
                    if(is_landed)
                    {
                        Debug.Log("!is_landed1");
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
                if(Mathf.Abs(velocity.x) > PlayerControl.SPEED_MAX)
                {
                    //　最高速度の制限以下に保つ。
                    velocity.x *= PlayerControl.SPEED_MAX / Mathf.Abs(velocity.x);
                }
                break;
            case STEP.JUMP:
                do
                {
                    // 「ボタンが離された瞬間」じゃなかった。
                    if(!Input.GetMouseButton(0))
                    {
                        break;      // 何もせずにループを抜ける
                    }
                    //　減速済みなら、（２回以上減速しないように）
                    if(is_key_released)
                    {
                        break;      // 何もせずにループを抜ける
                    }
                    //　上下方向の速度が０以下なら（下降中なら）
                    if(velocity.y <= 0.0f)
                    {
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
                break;
        }
        //  Rigidbodyの速度を、上記で求めた速度で更新。
        //　(この行は、状態にかかわらず毎回実行される)
        rb.velocity = velocity;
    }
    private void check_landed()
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
    }
}

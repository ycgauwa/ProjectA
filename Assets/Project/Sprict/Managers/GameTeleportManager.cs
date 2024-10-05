using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameTeleportManager : MonoBehaviour
{
    [SerializeField] private Homing Enemy = null;
    
    //TeleportAddress型の変数、であり初期値はnull
    //これが本当にTeleportAddress型なのか確認するそしてなぜそっちじゃなくてこっちのスプリクトでかくのか？
    private TeleportAddress enemyTeleportAddress = null;

    //テレポートまでの時間を設定する変数
    [SerializeField] private float enemyTeleportTimer;

    // この変数は確率を起こすために乱数を格納するもの
    public int enemyRndNum;
    private float enemyTeleportTime;
    public GameObject enemy;
    public GameObject Yukito;
    public static bool chasedTime;
    public AudioClip minnkaDoor;
    public AudioClip schoolDoor;
    public AudioClip bathDoor;
    public AudioClip climbStairs;
    public DifficultyLevelManager difficultyLevelManager;
    public SoundManager soundManager;

    private void Start()
    {

    }
    /// <summary>
    /// この配列はテレポートのタグと位置をまとめたものであるこの配列がまとめられている
    /// １つ１つの箱はTeleportAddress.csから作られていてその変数を引っ張ってきている。
    /// 配列の中身は実際にUnity内で使われるタグや座標を設定している
    /// </summary>
    TeleportAddress[] teleports = new TeleportAddress[]
    {
        new TeleportAddress(){tag="House",playerPosition = new Vector2(67, 64)},
        new TeleportAddress(){tag="Warp1",playerPosition = new Vector2(0, 0)},
        new TeleportAddress(){tag="School1",playerPosition = new Vector2(1, -33)},
        new TeleportAddress(){tag="School2",playerPosition = new Vector2(1, -42)},
        new TeleportAddress(){tag="School3",playerPosition = new Vector2(29, -72)},
        new TeleportAddress(){tag="School4",playerPosition = new Vector2(22, -72)},
        new TeleportAddress(){tag="School5",playerPosition = new Vector2(12, -72)},
        new TeleportAddress(){tag="School6",playerPosition = new Vector2(4, -72)},
        new TeleportAddress(){tag="School7",playerPosition = new Vector2(-29, -33)},
        new TeleportAddress(){tag="School8",playerPosition = new Vector2(-29, -42)},
        new TeleportAddress(){tag="Home1",playerPosition = new Vector2(30, -36)},
        new TeleportAddress(){tag="Home2",playerPosition = new Vector2(-11, -72)},
        new TeleportAddress(){tag="School9",playerPosition = new Vector2(-7, -71)},
        new TeleportAddress(){tag="School10",playerPosition = new Vector2(-10, -102)},
        new TeleportAddress(){tag="School11",playerPosition = new Vector2(-85, -42)},
        new TeleportAddress(){tag="School12",playerPosition = new Vector2(-4, -102)},
        new TeleportAddress(){tag="School13",playerPosition = new Vector2(-66, -42)},
        new TeleportAddress(){tag="School14",playerPosition = new Vector2(15, -102)},
        new TeleportAddress(){tag="School15",playerPosition = new Vector2(-86, 5)},
        new TeleportAddress(){tag="School16",playerPosition = new Vector2(23, -102)},
        new TeleportAddress(){tag="School17",playerPosition = new Vector2(-76, 5)},
        new TeleportAddress(){tag="School18",playerPosition = new Vector2(30, -102)},
        new TeleportAddress(){tag="Minnka1-1",playerPosition = new Vector2(24, -2)},
        new TeleportAddress(){tag="Minnka1-2",playerPosition = new Vector2(60, -42)},
        new TeleportAddress(){tag="Minnka1-3",playerPosition = new Vector2(79, -64)},
        new TeleportAddress(){tag="Minnka1-4",playerPosition = new Vector2(81, -47)},
        new TeleportAddress(){tag="Minnka1-5",playerPosition = new Vector2(99, -66)},
        new TeleportAddress(){tag="Minnka1-6",playerPosition = new Vector2(88, -67)},
        new TeleportAddress(){tag="Minnka1-7",playerPosition = new Vector2(107, -45)},
        new TeleportAddress(){tag="Minnka1-8",playerPosition = new Vector2(87, -45)},
        new TeleportAddress(){tag="Minnka1-9",playerPosition = new Vector2(90, -23)},
        new TeleportAddress(){tag="Minnka1-10",playerPosition = new Vector2(70, 3)},
        new TeleportAddress(){tag="Minnka1-11",playerPosition = new Vector2(69, -21)},
        new TeleportAddress(){tag="Minnka1-12",playerPosition = new Vector2(71, -35)},
        new TeleportAddress(){tag="Minnka1-13",playerPosition = new Vector2(93, 4)},
        new TeleportAddress(){tag="Minnka1-14",playerPosition = new Vector2(-14, 4)},
        new TeleportAddress(){tag="Minnka1-15",playerPosition = new Vector2(10, 1)},
        new TeleportAddress(){tag="Minnka1-16",playerPosition = new Vector2(45, -1)},
        new TeleportAddress(){tag="Minnka1-17",playerPosition = new Vector2(31, 60)},
        new TeleportAddress(){tag="Minnka1-18",playerPosition = new Vector2(24, 0)},
        new TeleportAddress(){tag="Minnka1-19",playerPosition = new Vector2(24, -2)},
        new TeleportAddress(){tag="Minnka1-20",playerPosition = new Vector2(26, 27)},
        new TeleportAddress(){tag="Minnka1-21",playerPosition = new Vector2(35, 70)},
        new TeleportAddress(){tag="Minnka1-22",playerPosition = new Vector2(150, -2)},
        new TeleportAddress(){tag="Minnka1-23",playerPosition = new Vector2(137, 21)},
        new TeleportAddress(){tag="Minnka1-24",playerPosition = new Vector2(24, -2)},
        new TeleportAddress(){tag="Minnka1-25",playerPosition = new Vector2(24, -2)},
        new TeleportAddress(){tag="Minnka2-1",playerPosition = new Vector2(80, 66)},
        new TeleportAddress(){tag="Minnka2-2",playerPosition = new Vector2(131, -14)},
        new TeleportAddress(){tag="Minnka2-3",playerPosition = new Vector2(103, 73)},
        new TeleportAddress(){tag="Minnka2-4",playerPosition = new Vector2(84, 73)},
        new TeleportAddress(){tag="Minnka2-5",playerPosition = new Vector2(105, 135)},
        new TeleportAddress(){tag="Minnka2-6",playerPosition = new Vector2(114, 72)},
        new TeleportAddress(){tag="Minnka2-7",playerPosition = new Vector2(81, 88)},
        new TeleportAddress(){tag="Minnka2-8",playerPosition = new Vector2(98, 86)},
        new TeleportAddress(){tag="Minnka2-9",playerPosition = new Vector2(108, 102)},
        new TeleportAddress(){tag="Minnka2-10",playerPosition = new Vector2(106, 89)},
        new TeleportAddress(){tag="Minnka2-11",playerPosition = new Vector2(132, 98)},
        new TeleportAddress(){tag="Minnka2-12",playerPosition = new Vector2(100, 107)},
        new TeleportAddress(){tag="Minnka2-13",playerPosition = new Vector2(85, 108)},
        new TeleportAddress(){tag="Minnka2-14",playerPosition = new Vector2(110, 85)},
        new TeleportAddress(){tag="Minnka2-15",playerPosition = new Vector2(129, 142)},
        new TeleportAddress(){tag="Minnka2-16",playerPosition = new Vector2(109, 140)},
        new TeleportAddress(){tag="Minnka2-17",playerPosition = new Vector2(88, 140)},
        new TeleportAddress(){tag="Minnka2-18",playerPosition = new Vector2(104, 140)},
        new TeleportAddress(){tag="Minnka2-19",playerPosition = new Vector2(130, 161)},
        new TeleportAddress(){tag="Minnka2-20",playerPosition = new Vector2(109, 147)},
        new TeleportAddress(){tag="Minnka2-21",playerPosition = new Vector2(65, 164)},
        new TeleportAddress(){tag="Minnka2-22",playerPosition = new Vector2(64, 150)},
        new TeleportAddress(){name="Ladder1-1",playerPosition = new Vector2(103, 8)},
        new TeleportAddress(){name="Ladder1-2",playerPosition = new Vector2(95, -19)},
    };
    public ToEvent3 toevent3;

    /*ここでテレポートアドレスから引っ張ってきたPTAの変数が引数となって
     プレイヤーがテレポートしたという情報を持っている。invokeもプレイヤーが
    テレポートするメソッドの中に入れるのではなくテレポートしたという
    情報があればよいというのが考え方の違いがあった。こっちの方がおんなじクラスで導入しやすい*/
    public void OnPlayerTeleport(TeleportAddress playerTeleportAddress)
    {
        //ETAにPTAを代入している
        enemyTeleportAddress = playerTeleportAddress;
        enemyRndNum = Random.Range(1, 101);
        if(Yukito.activeSelf)
        {
            if(!enemy.activeSelf)
            {
                EnemyStartChase();
            }
        }
        // 敵がいようがいまいがTPしてしまう。→敵から追いかけられているときだけTPする。
        if(chasedTime == true)
        {
            Invoke("OnEnemyTeleport", 0f);
            Debug.Log("zzz");
        }
    }
    /*nullチェックもしておりエネミーがいなくなるとはじかれる。また、この関数は
    エネミーの位置がプレイヤーが飛んだTPの位置まで移動してくれるものである*/
    private void OnEnemyTeleport()
    {
        if(Enemy == null)
        {
            Debug.LogError("エネミーの参照がありません");
            return;
        }
        // EnemyがTPするメソッド(ここに確率を加えたい)
        if(toevent3.event3flag && Homing.m_instance.enemyEmerge)
        {
            switch(difficultyLevelManager.difficultyLevel)
            {
                case DifficultyLevelManager.DifficultyLevel.Easy:
                    if(Homing.m_instance.enemyCount < 10.0f)
                    {
                        enemy.gameObject.SetActive(false);
                        //左は向かう先、右辺はプレイヤーが踏んだTPの位置
                        Enemy.transform.position = enemyTeleportAddress.playerPosition;
                        enemyTeleportTime += Time.deltaTime;
                        Debug.Log(enemyTeleportTime);
                        //EnemyがTPする前に自分がTPしたらTP先が更新されるのを防ぐ→TPして0.5秒は
                        //流れとして、敵が同時に同じ位置にTPする。SetActiveが偽になってる。0.5fしたら真にする。
                        if(enemyTeleportTime > 0.5)
                        {
                            Invoke("EnemyEmerge", 1f);
                        }
                        else
                        {
                            Invoke("EnemyEmerge", 2f);
                        }
                    }
                    else
                    {
                        StopChased();
                    }
                    break;
                case DifficultyLevelManager.DifficultyLevel.Normal:
                    if(Homing.m_instance.enemyCount < 12.0f)
                    {
                        enemy.gameObject.SetActive(false);
                        Enemy.transform.position = enemyTeleportAddress.playerPosition;
                        enemyTeleportTime += Time.deltaTime;
                        if(enemyTeleportTime > 0.5)
                        {
                            Invoke("EnemyEmerge", 1f);
                        }
                        else
                        {
                            Invoke("EnemyEmerge", 2f);
                        }
                    }
                    else
                    {
                        StopChased();
                    }
                    break;
                case DifficultyLevelManager.DifficultyLevel.Hard:
                    if(Homing.m_instance.enemyCount < 16.0f)
                    {
                        enemy.gameObject.SetActive(false);
                        Enemy.transform.position = enemyTeleportAddress.playerPosition;
                        enemyTeleportTime += Time.deltaTime;
                        if(enemyTeleportTime > 0.5)
                        {
                            Invoke("EnemyEmerge", 1f);
                        }
                        else
                        {
                            Invoke("EnemyEmerge", 2f);
                        }
                    }
                    else
                    {
                        StopChased();
                    }
                    break;
                case DifficultyLevelManager.DifficultyLevel.Extreme:
                    if(Homing.m_instance.enemyCount < 15.0f)
                    {
                        enemy.gameObject.SetActive(false);
                        Enemy.transform.position = enemyTeleportAddress.playerPosition;
                        enemyTeleportTime += Time.deltaTime;
                        if(enemyTeleportTime > 0.5)
                        {
                            Invoke("EnemyEmerge", 1f);
                        }
                        else
                        {
                            Invoke("EnemyEmerge", 2f);
                        }
                    }
                    else
                    {
                        StopChased();
                    }
                    break;
            }
        }
    }
    public void StopChased()
    {
        chasedTime = false;
        enemy.gameObject.SetActive(false);
        soundManager.StopBgm(toevent3.chasedBGM);
        Enemy.transform.position = new Vector2(0, 0);
        Homing.m_instance.enemyCount = 0;
    }
    private void EnemyEmerge()
    {
        if(!enemy.activeSelf)
        {
            enemy.gameObject.SetActive(true);
            enemyTeleportTime = 0;
        }
    }
    public void EnemyStartChase()
    {
        switch(difficultyLevelManager.difficultyLevel)
        {
            case DifficultyLevelManager.DifficultyLevel.Easy:
                if(enemyRndNum > 92 && Homing.m_instance.enemyEmerge)
                {
                    Enemy.gameObject.SetActive(true);
                    soundManager.PlayBgm(toevent3.chasedBGM);
                    chasedTime = true;
                }
                break;
            case DifficultyLevelManager.DifficultyLevel.Normal:
                if(enemyRndNum > 85 && Homing.m_instance.enemyEmerge)
                {
                    Enemy.gameObject.SetActive(true);
                    soundManager.PlayBgm(toevent3.chasedBGM);
                    chasedTime = true;
                }
                break;
            case DifficultyLevelManager.DifficultyLevel.Hard:
                if(enemyRndNum > 78 && Homing.m_instance.enemyEmerge)
                {
                    Enemy.gameObject.SetActive(true);
                    soundManager.PlayBgm(toevent3.chasedBGM);
                    chasedTime = true;
                }
                break;
            case DifficultyLevelManager.DifficultyLevel.Extreme:
                if(enemyRndNum > 62 && Homing.m_instance.enemyEmerge)
                {
                    Enemy.gameObject.SetActive(true);
                    soundManager.PlayBgm(toevent3.chasedBGM);
                    chasedTime = true;
                }
                break;
        }
    }
    /// <summary>
    /// 下のスクリプトは配列＋forが仲良しという理論で作られている。
    /// forは繰り返し処理をしているためforのあとの（）は左から初期化、条件式、変化式となっている
    /// 最初はi(変数)は0となっていてhouseがorderTagの中身にあっているか確認するそのあと変化式である
    /// i++によって1ずつ増えていくためiは1,2,3,4となっていく。条件式に応じて範囲が変わるためこの場合iは
    /// teleports.Lengthまでの値をとる。このスクリプトは配列を１つ１つorderTagと同じであるかどうかを
    /// 処理していくものである。
    /// </summary>
    /// <param name="orderTag">ほかのスクリプトの関数によって[orderTag]の中に何らかのTagがこの中に入る</param>
    /// <returns></returns>
    public TeleportAddress FindTeleportAddress(string orderTag)
    {
        for(int i = 0; i < teleports.Length; i++)
        {
            if(teleports[i].tag==orderTag)
            {
                return teleports[i]; 
            }
            else if (teleports[i].name == orderTag)
            {
                return teleports[i];
            }
        }
        return null;
    }
    public TeleportAddress FindTeleportName(string orderName)
    {
        for (int i = 0; i < teleports.Length; i++)
        {
            if (teleports[i].name == orderName)
            {
                return teleports[i];
            }
        }
        return null;
    }
}

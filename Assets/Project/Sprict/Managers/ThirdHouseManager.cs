using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdHouseManager : MonoBehaviour
{
    public static ThirdHouseManager thirdHouse_instance;
    public ThirdUnlockBasement thirdUnlockBasement;
    public GameObject futon;
    public GameObject[] thirdBloodObject;
    public NotEnter18 secondFloorTrigger;
    public AudioClip enemyScream;
    public AudioClip glassBreak;
    private void Awake()
    {
        if(thirdHouse_instance == null)
            thirdHouse_instance = this;
        else
        {
            Destroy(thirdHouse_instance);
        }
    }
    /*
     * 敵の仕様
     * ランダムの時間で目が開き光が照らし出される。その光に当たると難易度によっての回数制限があるが基準値を超えると動きが完全に止まり
     * 木の化け物が動くタイプの化け物を呼び出してとどめをさす。
     * 光をよけながらアイテムを回収してしかるべき場所で敵を枯らして先に進む。だから、光に当たるたび大きな足音がなる。
     * Easyなら最小一回Hardなら最初から3回鳴って、足音が三回鳴ったら画面に敵が現れてゲームオーバー
     * レベル1～3まで存在していて3は3方向ランダムに光り続けて常に誰かは光ってる。 触ったらアウトな分範囲は広めにとって良いその代わりインターバル長めに。
     * 一つの目でどこの方向に光るか分からない。 パラメーターいじれるようにして難易度調節できるようにしよう
     * インターバル・照射時間・照射範囲・照射角度で目が増えるごとにランダムな値が増えていく
     */
}

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using Cysharp.Threading.Tasks;
using System;
using UnityEditor.Rendering;
using UnityEngine.AI;

public class FlagsManager : MonoBehaviour
{
    //このスクリプトはセーブデータは全く関係なくたくさん参照を取って
    //どのシナリオまで進んだりしたのかを記録したりするスクリプトにしたい
    //セーブ関係はSaveslotsManagerにまかせよう
    public static FlagsManager flag_Instance;
    public ToEvent1 toEvent1;
    public ToEvent2 toEvent2;
    public ToEvent3 toEvent3;
    public ToEvent4 toEvent4;
    public ToEvent5 toEvent5;
    public NotEnter6 notEnter6;
    public NotEnter9 notEnter9;
    public NotEnter10 notEnter10;
    public NotEnter15 notEnter15;
    public Meat meat;
    public RescueEvent rescueEvent;
    public TunnelSeiitirouEvent tunnelSeiitirou;
    private ColorAdjustments colorAdjustments;
    public bool[] flagBools = new bool[7];
    public bool[] seiitirouFlagBools = new bool[15];
    public bool[] haruFlagBools = new bool[7];

    void Awake()
    {
        if(flag_Instance == null)
        {
            flag_Instance = this;
        }
        else
            Destroy(gameObject);
    }
    public void LoadFlagsData()
    {
        //Jsonからスクリプタブルオブジェクトへいってそこからここに格納される
        ToEvent1.one = flagBools[0];
        toEvent2.eventFinished = flagBools[1];
        if(toEvent2.eventFinished)
        {
            toEvent2.volume.profile.TryGet<ColorAdjustments>(out colorAdjustments);
            colorAdjustments.active = true;
        }
        
        toEvent3.event3flag = flagBools[2];
        if(toEvent3.event3flag)
        {
            GameManager.m_instance.enemy.gameObject.SetActive(false);
            Homing.m_instance.enemyEmerge = true;
        }
        // 征一郎との会話イベントはフラグで取れる。
        //NotEnter4は通過したらenemyEmergeがfalseになる。でそこからは一生falseのままでいい。用はGetしたけど持ってなかったら消していい
        if(!ItemDateBase.itemDate_instance.GetItemId(252).checkPossession && ItemDateBase.itemDate_instance.GetItemId(252).geted)
        {
            Homing.m_instance.teleportManager.StopChased();
            Homing.m_instance.enemyEmerge = false;
        }
        toEvent4.event4flag = flagBools[3];
        notEnter6.choiced = flagBools[4];
        
        notEnter6.rescued = seiitirouFlagBools[0];
        if(notEnter6.rescued)
        {
            notEnter6.rescueEvent.gameObject.SetActive(true);
        }
        
        rescueEvent.RescueSwitch = seiitirouFlagBools[1];
        if(seiitirouFlagBools[2])
        {
            rescueEvent.yukitoProfile.gameObject.SetActive(false);
            rescueEvent.seiitirouProfile.gameObject.SetActive(true);
            if(rescueEvent.SeiitirouAnimation.GetComponent<AnimationStateController>().enabled == false)
                rescueEvent.SeiitirouAnimation.GetComponent<AnimationStateController>().enabled = true;
            GameManager.m_instance.inventry.Delete(ItemDateBase.itemDate_instance.GetItemId(253));
            GameManager.m_instance.yukitoDead.SetActive(true);
            GameManager.m_instance.seiitirou.gameObject.tag = "Seiitirou";
            SaveSlotsManager.save_Instance.saveState.characterName = "征一郎";

            //　カメラの位置を幸人から征一郎に変更して、征一郎をキー操作で動かせるようにする。
            cameraManager.cameraInstance.playerCamera = false;
            GameManager.m_instance.player.gameObject.SetActive(false);
            cameraManager.cameraInstance.seiitirouCamera = true;
            rescueEvent = GameManager.m_instance.rescuePoint.GetComponent<RescueEvent>();
            GameManager.m_instance.soundManager.StopSe(rescueEvent.ChasedBGM);
            GameManager.m_instance.playerManager = GameManager.m_instance.seiitirou.AddComponent<PlayerManager>();
            GameManager.m_instance.playerManager = GameManager.m_instance.seiitirou.GetComponent<PlayerManager>();
            GameManager.m_instance.playerManager.staminaMax = 300;
            GameManager.m_instance.playerManager.teleportManager = GameManager.m_instance.teleportManager;
            GameManager.m_instance.playerManager.homing = GameManager.m_instance.homing;
            Rigidbody2D rb = GameManager.m_instance.seiitirou.GetComponent<Rigidbody2D>();
            rb.constraints = RigidbodyConstraints2D.None;
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            GameManager.m_instance.homing.enemyEmerge = false;
            GameManager.m_instance.homing.enemyCount = 0;
        }

        tunnelSeiitirou.tunnelEvent = seiitirouFlagBools[3];
        if(tunnelSeiitirou.tunnelEvent)
        {
            cameraManager.cameraInstance.seiitirouCamera = true;
            //オブジェクトの総入れ替え
            SecondHouseManager.secondHouse_instance.chickenDish.gameObject.SetActive(false);
            SecondHouseManager.secondHouse_instance.shrimpDish.gameObject.SetActive(false);
            SecondHouseManager.secondHouse_instance.fishDish.gameObject.SetActive(false);
            SecondHouseManager.secondHouse_instance.bear.gameObject.SetActive(false);
            SecondHouseManager.secondHouse_instance.chicken.gameObject.SetActive(false);
            SecondHouseManager.secondHouse_instance.mushroom.gameObject.SetActive(false);
            SecondHouseManager.secondHouse_instance.toEvent5.gameObject.SetActive(false);
            SecondHouseManager.secondHouse_instance.weightObject.SetActive(true);
            SecondHouseManager.secondHouse_instance.weightSwitch.SetActive(true);
            SecondHouseManager.secondHouse_instance.talkingWithHaru.gameObject.transform.position = new Vector3(141, 144, 0);
            tunnelSeiitirou.skullObject.SetActive(true);
            tunnelSeiitirou.gameObject.SetActive(false);
        }

        notEnter15.keyOpened = seiitirouFlagBools[4];

        toEvent5.event5Start = flagBools[5];
        if(toEvent5.event5Start)
        {
            toEvent5.ColliderTrigger();
            toEvent5.gameObject.SetActive(false);
        }

        SecondHouseManager.secondHouse_instance.animalKeys[3] = flagBools[6];
        //基本的にミスしたらやり直し（死んでもロードとかはない。）だから詰まない様になってる。
        //怖いのは中途半端な状態でセーブされてデータの引継ぎが出来ない事。だから、謎解き中はセーブできないようする。
        //保存しておくのは基本的にCheckPossessionなんだけど謎解きが終わったらGetedで判定取る
        //こっちで判定取るのはfirstkeyと選択を誤った回数かな？

        if(SecondHouseManager.secondHouse_instance.animalKeys[3])
        {
            SecondHouseManager.secondHouse_instance.chickenDish.gameObject.SetActive(false);
            SecondHouseManager.secondHouse_instance.fishDish.gameObject.SetActive(false);
            SecondHouseManager.secondHouse_instance.shrimpDish.gameObject.SetActive(false);
            SecondHouseManager.secondHouse_instance.bear.gameObject.SetActive(false);
            SecondHouseManager.secondHouse_instance.chicken.gameObject.SetActive(false);
            SecondHouseManager.secondHouse_instance.mushroom.gameObject.SetActive(false);
            SecondHouseManager.secondHouse_instance.cooktop.refrigerator.isTaken = true;
        }
        if(ItemDateBase.itemDate_instance.GetItemId(254).geted)
            notEnter9.enemy2.SetActive(true);

        notEnter10.advanceEnter = flagBools[6];
        if(notEnter10.advanceEnter)
        {
            if(!GameTeleportManager.chasedTime)
                GameTeleportManager.chasedTime = true;
            notEnter10.AttachComponent();
            for(int i = 0; i < 4; i ++)
            {
                SecondHouseManager.secondHouse_instance.interiors[i].talked = true;
            }
        }

        meat.meatEatAfter = flagBools[7];
        if(meat.meatEatAfter)
        {
            notEnter10.gameObject.SetActive(false);
            notEnter10.sleptAjure.gameObject.SetActive(false);
            meat.ajure.gameObject.SetActive(true);
            meat.FalseComponent();
            meat.gameObject.SetActive(true);
            if(!ItemDateBase.itemDate_instance.GetItemId(255).geted)
                meat.lightAnimation.gameObject.SetActive(true);
        }
        if(ItemDateBase.itemDate_instance.GetItemId(257).geted)
            SecondHouseManager.secondHouse_instance.enemyEncounter.gameObject.SetActive(true);
    }
}

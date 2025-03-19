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
using UnityEngine.UI;

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
    public Image navigationPanel;
    public Text locationText;
    public Text destinationText;
    public CharactersFlag[] charactersFlags;
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
    //この下の関数がif文が深くなりそうなときに関数を使ったりして深くなるのを防ぐやり方である。
    public void ChangeUIDestnation(int progressionLevel,string characterName)
    {
        CharactersFlag selectedCharacterFlags = GetCharacterFlagsById(characterName);

        //スクリプタブルオブジェクトを使って代入したい。
        //目的地に関してはイベント又はアイテムの入手時に変更されるものだからイベントの都度関数を入れて変更したって良い。
        string[] destinationList = GetDestinationListByLanguage(selectedCharacterFlags, GameManager.m_instance.languageIndex);

        if (destinationList != null && progressionLevel >= 1 && progressionLevel <= destinationList.Length)
        {
            destinationText.text = destinationList[progressionLevel]; // progressionLevel は1から始まる想定
        }
    }
    // キャラクターIDに基づいてCharacterFlagsを取得
    private CharactersFlag GetCharacterFlagsById(string characterId)
    {
        // 例えば、characterFlagsが配列だとして、キャラクターIDに基づいて該当するCharacterFlagsを取得
        foreach (var flags in charactersFlags)
        {
            if (flags.name == characterId)  // キャラクターIDで一致するものを探す
            {
                return flags;
            }
        }
        return null;  // 見つからなかった場合
    }
    private string[] GetDestinationListByLanguage(CharactersFlag flags, int languageNum)
    {
        switch (languageNum)
        {
            case 0:
                return flags.JPN_characterDestination;
            case 1:
                return flags.EN_characterDestination;
            default:
                return null;  // サポートされていない言語の場合はnull
        }
    }
    public void ChangeUILocation(string LocationName)
    {
        if (true) //日本語の時
        {
            switch (LocationName)
            {
                case "School1":
                    locationText.text = "第2教室";
                    break;
                case "School2":
                    locationText.text = "第2教室";
                    break ;
                case "School3":
                    locationText.text = "2F廊下";
                    break;
                case "School4":
                    locationText.text = "2F廊下";
                    break;
                case "School5":
                    locationText.text = "2F廊下";
                    break;
                case "School6":
                    locationText.text = "2F廊下";
                    break;
                case "School7":
                    locationText.text = "第1教室";
                    break;
                case "School8":
                    locationText.text = "第1教室";
                    break;
                case "School9":
                    locationText.text = "2F廊下";
                    break;
                case "School10":
                    locationText.text = "3F廊下";
                    break;
                case "School11":
                    locationText.text = "理科室";
                    break;
                case "School12":
                    locationText.text = "3F廊下";
                    break;
                case "School13":
                    locationText.text = "理科室";
                    break;
                case "School14":
                    locationText.text = "3F廊下";
                    break;
                case "School15":
                    locationText.text = "音楽室";
                    break;
                case "School16":
                    locationText.text = "3F廊下";
                    break;
                case "School17":
                    locationText.text = "音楽室";
                    break;
                case "School18":
                    locationText.text = "3F廊下";
                    break;

                case "Minnka1-1":
                    locationText.text = "2F廊下";
                    break;
                case "Minnka1-2":
                    locationText.text = "1F廊下";
                    break;
                case "Minnka1-3":
                    locationText.text = "脱衣所";
                    break;
                case "Minnka1-4":
                    locationText.text = "1F廊下";
                    break;
                case "Minnka1-5":
                    locationText.text = "浴室";
                    break;
                case "Minnka1-6":
                    locationText.text = "脱衣所";
                    break;
                case "Minnka1-7":
                    locationText.text = "1Fトイレ";
                    break;
                case "Minnka1-8":
                    locationText.text = "1F廊下";
                    break;
                case "Minnka1-9":
                    locationText.text = "1F寝室";
                    break;
                case "Minnka1-10":
                    locationText.text = "ダイニングキッチン";
                    break;
                case "Minnka1-11":
                    locationText.text = "1F廊下";
                    break;
                case "Minnka1-12":
                    locationText.text = "1F廊下";
                    break;
                case "Minnka1-13":
                    locationText.text = "2F客間";
                    break;
                case "Minnka1-14":
                    locationText.text = "2F寝室";
                    break;
                case "Minnka1-15":
                    locationText.text = "2F廊下";
                    break;
                case "Minnka1-16":
                    locationText.text = "2F廊下";
                    break;
                case "Minnka1-17":
                    locationText.text = "2F居間";
                    break;
                case "Minnka1-18":
                    locationText.text = "2F廊下";
                    break;
                case "Minnka1-19":
                    locationText.text = "2F子供部屋";
                    break;
                case "Minnka1-20":
                    locationText.text = "2F奥の寝室";
                    break;
                case "Minnka1-21":
                    locationText.text = "2F居間";
                    break;
                case "Minnka1-22":
                    locationText.text = "坑道";
                    break;
                case "Minnka1-23":
                    locationText.text = "地下へ続く書斎";
                    break;
                case "Minnka1-24":
                    locationText.text = "";
                    break;
            }
        }
        else if(false)//英語の時
        {

        }
    }
}

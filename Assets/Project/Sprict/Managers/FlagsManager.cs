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
    //���̃X�N���v�g�̓Z�[�u�f�[�^�͑S���֌W�Ȃ���������Q�Ƃ������
    //�ǂ̃V�i���I�܂Ői�񂾂肵���̂����L�^�����肷��X�N���v�g�ɂ�����
    //�Z�[�u�֌W��SaveslotsManager�ɂ܂����悤
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
        //Json����X�N���v�^�u���I�u�W�F�N�g�ւ����Ă������炱���Ɋi�[�����
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
        // ����Y�Ƃ̉�b�C�x���g�̓t���O�Ŏ���B
        //NotEnter4�͒ʉ߂�����enemyEmerge��false�ɂȂ�B�ł�������͈ꐶfalse�̂܂܂ł����B�p��Get�������ǎ����ĂȂ�����������Ă���
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
            SaveSlotsManager.save_Instance.saveState.characterName = "����Y";

            //�@�J�����̈ʒu���K�l���琪��Y�ɕύX���āA����Y���L�[����œ�������悤�ɂ���B
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
            //�I�u�W�F�N�g�̑�����ւ�
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
        //��{�I�Ƀ~�X�������蒼���i����ł����[�h�Ƃ��͂Ȃ��B�j������l�܂Ȃ��l�ɂȂ��Ă�B
        //�|���̂͒��r���[�ȏ�ԂŃZ�[�u����ăf�[�^�̈��p�����o���Ȃ����B������A��������̓Z�[�u�ł��Ȃ��悤����B
        //�ۑ����Ă����̂͊�{�I��CheckPossession�Ȃ񂾂��Ǔ�������I�������Geted�Ŕ�����
        //�������Ŕ�����̂�firstkey�ƑI����������񐔂��ȁH

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
    //���̉��̊֐���if�����[���Ȃ肻���ȂƂ��Ɋ֐����g�����肵�Đ[���Ȃ�̂�h�������ł���B
    public void ChangeUIDestnation(int progressionLevel,string characterName)
    {
        CharactersFlag selectedCharacterFlags = GetCharacterFlagsById(characterName);

        //�X�N���v�^�u���I�u�W�F�N�g���g���đ���������B
        //�ړI�n�Ɋւ��Ă̓C�x���g���̓A�C�e���̓��莞�ɕύX�������̂�����C�x���g�̓s�x�֐������ĕύX�������ėǂ��B
        string[] destinationList = GetDestinationListByLanguage(selectedCharacterFlags, GameManager.m_instance.languageIndex);

        if (destinationList != null && progressionLevel >= 1 && progressionLevel <= destinationList.Length)
        {
            destinationText.text = destinationList[progressionLevel]; // progressionLevel ��1����n�܂�z��
        }
    }
    // �L�����N�^�[ID�Ɋ�Â���CharacterFlags���擾
    private CharactersFlag GetCharacterFlagsById(string characterId)
    {
        // �Ⴆ�΁AcharacterFlags���z�񂾂Ƃ��āA�L�����N�^�[ID�Ɋ�Â��ĊY������CharacterFlags���擾
        foreach (var flags in charactersFlags)
        {
            if (flags.name == characterId)  // �L�����N�^�[ID�ň�v������̂�T��
            {
                return flags;
            }
        }
        return null;  // ������Ȃ������ꍇ
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
                return null;  // �T�|�[�g����Ă��Ȃ�����̏ꍇ��null
        }
    }
    public void ChangeUILocation(string LocationName)
    {
        if (true) //���{��̎�
        {
            switch (LocationName)
            {
                case "School1":
                    locationText.text = "��2����";
                    break;
                case "School2":
                    locationText.text = "��2����";
                    break ;
                case "School3":
                    locationText.text = "2F�L��";
                    break;
                case "School4":
                    locationText.text = "2F�L��";
                    break;
                case "School5":
                    locationText.text = "2F�L��";
                    break;
                case "School6":
                    locationText.text = "2F�L��";
                    break;
                case "School7":
                    locationText.text = "��1����";
                    break;
                case "School8":
                    locationText.text = "��1����";
                    break;
                case "School9":
                    locationText.text = "2F�L��";
                    break;
                case "School10":
                    locationText.text = "3F�L��";
                    break;
                case "School11":
                    locationText.text = "���Ȏ�";
                    break;
                case "School12":
                    locationText.text = "3F�L��";
                    break;
                case "School13":
                    locationText.text = "���Ȏ�";
                    break;
                case "School14":
                    locationText.text = "3F�L��";
                    break;
                case "School15":
                    locationText.text = "���y��";
                    break;
                case "School16":
                    locationText.text = "3F�L��";
                    break;
                case "School17":
                    locationText.text = "���y��";
                    break;
                case "School18":
                    locationText.text = "3F�L��";
                    break;

                case "Minnka1-1":
                    locationText.text = "2F�L��";
                    break;
                case "Minnka1-2":
                    locationText.text = "1F�L��";
                    break;
                case "Minnka1-3":
                    locationText.text = "�E�ߏ�";
                    break;
                case "Minnka1-4":
                    locationText.text = "1F�L��";
                    break;
                case "Minnka1-5":
                    locationText.text = "����";
                    break;
                case "Minnka1-6":
                    locationText.text = "�E�ߏ�";
                    break;
                case "Minnka1-7":
                    locationText.text = "1F�g�C��";
                    break;
                case "Minnka1-8":
                    locationText.text = "1F�L��";
                    break;
                case "Minnka1-9":
                    locationText.text = "1F�Q��";
                    break;
                case "Minnka1-10":
                    locationText.text = "�_�C�j���O�L�b�`��";
                    break;
                case "Minnka1-11":
                    locationText.text = "1F�L��";
                    break;
                case "Minnka1-12":
                    locationText.text = "1F�L��";
                    break;
                case "Minnka1-13":
                    locationText.text = "2F�q��";
                    break;
                case "Minnka1-14":
                    locationText.text = "2F�Q��";
                    break;
                case "Minnka1-15":
                    locationText.text = "2F�L��";
                    break;
                case "Minnka1-16":
                    locationText.text = "2F�L��";
                    break;
                case "Minnka1-17":
                    locationText.text = "2F����";
                    break;
                case "Minnka1-18":
                    locationText.text = "2F�L��";
                    break;
                case "Minnka1-19":
                    locationText.text = "2F�q������";
                    break;
                case "Minnka1-20":
                    locationText.text = "2F���̐Q��";
                    break;
                case "Minnka1-21":
                    locationText.text = "2F����";
                    break;
                case "Minnka1-22":
                    locationText.text = "�B��";
                    break;
                case "Minnka1-23":
                    locationText.text = "�n���֑�������";
                    break;
                case "Minnka1-24":
                    locationText.text = "";
                    break;
            }
        }
        else if(false)//�p��̎�
        {

        }
    }
}

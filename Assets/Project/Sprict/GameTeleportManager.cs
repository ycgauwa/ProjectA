using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTeleportManager : MonoBehaviour
{
    [SerializeField] private Homing Enemy = null;
    
    //TeleportAddress�^�̕ϐ��A�ł��菉���l��null
    //���ꂪ�{����TeleportAddress�^�Ȃ̂��m�F���邻���ĂȂ�����������Ȃ��Ă������̃X�v���N�g�ł����̂��H
    private TeleportAddress enemyTeleportAddress = null;
    
    //�e���|�[�g�܂ł̎��Ԃ�ݒ肷��ϐ�
    [SerializeField] private float enemyTeleportTimer = 0f;

    // ���̕ϐ��͊m�����N�������߂ɗ������i�[�������
    public int enemyRndNum;
    public GameObject enemy;
    public AudioSource chasedBGM;
    public AudioClip openDoor;

    private void Start()
    {
        chasedBGM = GetComponent<AudioSource>();
    }
    /// <summary>
    /// ���̔z��̓e���|�[�g�̃^�O�ƈʒu���܂Ƃ߂����̂ł��邱�̔z�񂪂܂Ƃ߂��Ă���
    /// �P�P�̔���TeleportAddress.cs�������Ă��Ă��̕ϐ������������Ă��Ă���B
    /// �z��̒��g�͎��ۂ�Unity���Ŏg����^�O����W��ݒ肵�Ă���
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

    };
    public ToEvent3 toevent3;

    /*�����Ńe���|�[�g�A�h���X������������Ă���PTA�̕ϐ��������ƂȂ���
     �v���C���[���e���|�[�g�����Ƃ������������Ă���Binvoke���v���C���[��
    �e���|�[�g���郁�\�b�h�̒��ɓ����̂ł͂Ȃ��e���|�[�g�����Ƃ���
    ��񂪂���΂悢�Ƃ����̂��l�����̈Ⴂ���������B�������̕�������Ȃ��N���X�œ������₷��*/
    public void OnPlayerTeleport(TeleportAddress playerTeleportAddress)
    {
        chasedBGM.PlayOneShot(openDoor);
        //ETA��PTA�������Ă���
        enemyTeleportAddress = playerTeleportAddress;
        enemyRndNum = Random.Range(1, 101);
        if(!enemy.activeSelf)
        {
            if(enemyRndNum > 75 && Homing.m_instance.enemyEmerge)
            {
                Debug.Log(enemyRndNum);
                Enemy.gameObject.SetActive(true);
                chasedBGM.Play();
            }
        }
        Invoke("OnEnemyTeleport", enemyTeleportTimer);
    }
    /*null�`�F�b�N�����Ă���G�l�~�[�����Ȃ��Ȃ�Ƃ͂������B�܂��A���̊֐���
    �G�l�~�[�̈ʒu���v���C���[�����TP�̈ʒu�܂ňړ����Ă������̂ł���*/
    private void OnEnemyTeleport()
    {
        if(Enemy == null)
        {
            Debug.LogError("�G�l�~�[�̎Q�Ƃ�����܂���");
            return;
        }
        // Enemy��TP���郁�\�b�h(�����Ɋm������������)
        if(toevent3.event3flag && Homing.m_instance.enemyEmerge)
        {
            if(Homing.m_instance.enemyCount < 15.0f)
            {
                //���͌�������A�E�ӂ̓v���C���[������TP�̈ʒu
                Enemy.transform.position = enemyTeleportAddress.playerPosition;
            }
            else 
            {
                Enemy.gameObject.SetActive(false);
                chasedBGM.Stop();
                toevent3.chasedBGM.Stop();
                Enemy.transform.position = new Vector2(0,0);
                Homing.m_instance.enemyCount = 0;
            }
        }
    }
    /// <summary>
    /// ���̃X�N���v�g�͔z��{for�����ǂ��Ƃ������_�ō���Ă���B
    /// for�͌J��Ԃ����������Ă��邽��for�̂��Ƃ́i�j�͍����珉�����A�������A�ω����ƂȂ��Ă���
    /// �ŏ���i(�ϐ�)��0�ƂȂ��Ă���house��orderTag�̒��g�ɂ����Ă��邩�m�F���邻�̂��ƕω����ł���
    /// i++�ɂ����1�������Ă�������i��1,2,3,4�ƂȂ��Ă����B�������ɉ����Ĕ͈͂��ς�邽�߂��̏ꍇi��
    /// teleports.Length�܂ł̒l���Ƃ�B���̃X�N���v�g�͔z����P�P��orderTag�Ɠ����ł��邩�ǂ�����
    /// �������Ă������̂ł���B
    /// </summary>
    /// <param name="orderTag">�ق��̃X�N���v�g�̊֐��ɂ����[orderTag]�̒��ɉ��炩��Tag�����̒��ɓ���</param>
    /// <returns></returns>
    public TeleportAddress FindTeleportAddress(string orderTag)
    {
        for(int i = 0; i < teleports.Length; i++)
        {
            if(teleports[i].tag==orderTag)
            {
                return teleports[i]; 
            }
        }
        return null;
    }
}

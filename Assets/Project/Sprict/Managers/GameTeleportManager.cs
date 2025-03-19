using UnityEngine;

public class GameTeleportManager : MonoBehaviour
{
    [SerializeField] private Homing Enemy = null;
    [SerializeField] private Homing2 Enemy2 = null;

    //TeleportAddress�^�̕ϐ��A�ł��菉���l��null
    //���ꂪ�{����TeleportAddress�^�Ȃ̂��m�F���邻���ĂȂ�����������Ȃ��Ă������̃X�v���N�g�ł����̂��H
    private TeleportAddress enemyTeleportAddress = null;

    // ���̕ϐ��͊m�����N�������߂ɗ������i�[�������
    public int enemyRndNum;
    public float enemyTeleportTime;
    private bool enemyTeleportSwitch = false;
    public GameObject enemy;
    public GameObject Yukito;
    public static bool chasedTime;
    private bool enemyStartTPTime;
    public AudioClip minnkaDoor;
    public AudioClip schoolDoor;
    public AudioClip bathDoor;
    public AudioClip climbStairs;
    public DifficultyLevelManager difficultyLevelManager;
    public SoundManager soundManager;
    public ToEvent3 toevent3;


    //1�̖ڂ̓G��2�̖ڂ̓G�͕��������B�ǂ����番���邩���厖�ł��邪����͂��Ȃ�[���Ƃ���ŕ����Ă��܂��Ă邽�ߔ�Q���o�Ă���
    //�󂢊K�w�ŕ����邱�Ƃł�����Ƃ������ʂ��o����C������B
    private void Update()
    {
        if(PlayerManager.m_instance.playerstate != PlayerManager.PlayerState.Talk && PlayerManager.m_instance.playerstate != PlayerManager.PlayerState.Stop && chasedTime == true)
        {       
            enemyTeleportTime += Time.deltaTime;
            if(enemyStartTPTime == true && enemyTeleportTime > 2.0f)
            {
                Debug.Log("1");
                Invoke("OnEnemyTeleport", 0f);
                enemyStartTPTime = false;
            }
        }
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
        new TeleportAddress(){name="Ladder1-1",playerPosition = new Vector2(103, 8)},
        new TeleportAddress(){name="Ladder1-2",playerPosition = new Vector2(95, -19)},
        new TeleportAddress(){name="Warp2-23",playerPosition = new Vector2(40, 120)},
        new TeleportAddress(){name="Warp2-24",playerPosition = new Vector2(64, 144)},
        new TeleportAddress(){name="Warp2-25",playerPosition = new Vector2(116, 169)},
        new TeleportAddress(){name="Warp2-26",playerPosition = new Vector2(106, 151)},
        new TeleportAddress(){name="Warp3-1",playerPosition = new Vector2(192.6f, 33.5f)},
        new TeleportAddress(){name="Warp3-2",playerPosition = new Vector2(190, 10.7f)},
        new TeleportAddress(){name="Warp3-3",playerPosition = new Vector2(213.1f, 37.9f)},
        new TeleportAddress(){name="Warp3-4",playerPosition = new Vector2(195.9f, 37.8f)},
        new TeleportAddress(){name="Warp3-5",playerPosition = new Vector2(214, 58.8f)},
        new TeleportAddress(){name="Warp3-6",playerPosition = new Vector2(215, 42.2f)},
        new TeleportAddress(){name="Warp3-7",playerPosition = new Vector2(239.1f, 34.8f)},
        new TeleportAddress(){name="Warp3-8",playerPosition = new Vector2(217.9f, 33.8f)},
        new TeleportAddress(){name="Warp3-9",playerPosition = new Vector2(241.5f, 56.7f)},
        new TeleportAddress(){name="Warp3-10",playerPosition = new Vector2(251.5f, 41.3f)},
        new TeleportAddress(){name="Warp3-11",playerPosition = new Vector2(247.5f, 75.5f)},
        new TeleportAddress(){name="Warp3-12",playerPosition = new Vector2(253, 59.4f)},
        new TeleportAddress(){name="Warp3-13",playerPosition = new Vector2(227.9f, 81.7f)},
        new TeleportAddress(){name="Warp3-14",playerPosition = new Vector2(239.2f, 78.8f)},
        new TeleportAddress(){name="Warp3-15",playerPosition = new Vector2(275.1f, 58.8f)},
        new TeleportAddress(){name="Warp3-16",playerPosition = new Vector2(259.9f, 57.7f)},
        new TeleportAddress(){name="Warp3-17",playerPosition = new Vector2(286.5f, 83.5f)},
        new TeleportAddress(){name="Warp3-18",playerPosition = new Vector2(272.6f, 50)},
        new TeleportAddress(){name="Warp3-19",playerPosition = new Vector2(271.6f, 104.7f)},
        new TeleportAddress(){name="Warp3-20",playerPosition = new Vector2(272, 88.3f)},
        new TeleportAddress(){name="Warp3-21",playerPosition = new Vector2(252.9f, 107.8f)},
        new TeleportAddress(){name="Warp3-22",playerPosition = new Vector2(266.3f, 106.6f)},
        new TeleportAddress(){name="Warp3-23",playerPosition = new Vector2(249.4f, 126.5f)},
        new TeleportAddress(){name="Warp3-24",playerPosition = new Vector2(251, 110.3f)},
        new TeleportAddress(){name="Warp3-25",playerPosition = new Vector2(228, 111.4f)},
        new TeleportAddress(){name="Warp3-26",playerPosition = new Vector2(250.4f, 104.6f)},
        new TeleportAddress(){name="Warp3-27",playerPosition = new Vector2(106, 151)},
        new TeleportAddress(){name="Warp3-28",playerPosition = new Vector2(106, 151)},
        new TeleportAddress(){name="Warp3-29",playerPosition = new Vector2(289.5f, 129.6f)},
        new TeleportAddress(){name="Warp3-30",playerPosition = new Vector2(291, 107.4f)},
    };

    /*�����Ńe���|�[�g�A�h���X������������Ă���PTA�̕ϐ��������ƂȂ���
     �v���C���[���e���|�[�g�����Ƃ������������Ă���Binvoke���v���C���[��
    �e���|�[�g���郁�\�b�h�̒��ɓ����̂ł͂Ȃ��e���|�[�g�����Ƃ���
    ��񂪂���΂悢�Ƃ����̂��l�����̈Ⴂ���������B�������̕�������Ȃ��N���X�œ������₷��*/
    public void OnPlayerTeleport(TeleportAddress playerTeleportAddress)
    {
        //ETA��PTA�������Ă���
        enemyTeleportAddress = playerTeleportAddress;
        enemyRndNum = Random.Range(1, 101);
        if(Yukito.activeSelf)
        {
            if(!enemy.activeSelf)
            {
                EnemyStartChase();
            }
        }
        // �G�����悤�����܂���TP���Ă��܂��B���G����ǂ��������Ă���Ƃ�����TP����B
        if(chasedTime == true)
        {
            enemy.gameObject.SetActive(false);
            if(enemyTeleportTime > 0f)
                enemyTeleportTime = 0;
            enemyStartTPTime = true;
        }
    }
    /*null�`�F�b�N�����Ă���G�l�~�[�����Ȃ��Ȃ�Ƃ͂������B�܂��A���̊֐���
    �G�l�~�[�̈ʒu���v���C���[�����TP�̈ʒu�܂ňړ����Ă������̂ł���*/
    private void OnEnemyTeleport()
    {
        Debug.Log("2");

        if(Enemy == null)
        {
            Debug.LogError("�G�l�~�[�̎Q�Ƃ�����܂���");
            return;
        }
        // Enemy��TP���郁�\�b�h(�����Ɋm������������)�ł��Q�C�ڂ͊�{�C�x���g�݂̂Ȃ̂�stopchased�͕K�v�Ȃ��B
        if((Homing.m_instance.eventEnemySpawnToggle && Homing.m_instance.enemyEmerge))
        {
            switch(difficultyLevelManager.difficultyLevel)
            {
                case DifficultyLevelManager.DifficultyLevel.Easy:
                    if(Homing.m_instance.enemyCount < 10.0f)
                    {
                        enemy.gameObject.SetActive(false);
                        enemyTeleportTime += Time.deltaTime;
                        if(enemyTeleportSwitch) CancelInvoke("EnemyEmerge");
                        Invoke("EnemyEmerge", 0f);
                        enemyTeleportSwitch = true;
                    }
                    else StopChased();
                    break;
                case DifficultyLevelManager.DifficultyLevel.Normal:
                    if(Homing.m_instance.enemyCount < 12.0f)
                    {
                        enemy.gameObject.SetActive(false);
                        enemyTeleportTime += Time.deltaTime;
                        if(enemyTeleportSwitch) CancelInvoke("EnemyEmerge");
                        Invoke("EnemyEmerge", 0f);
                        enemyTeleportSwitch = true;
                    }
                    else StopChased();
                    break;
                case DifficultyLevelManager.DifficultyLevel.Hard:
                    if(Homing.m_instance.enemyCount < 16.0f)
                    {
                        enemy.gameObject.SetActive(false);
                        enemyTeleportTime += Time.deltaTime;
                        if(enemyTeleportSwitch) CancelInvoke("EnemyEmerge");
                        Invoke("EnemyEmerge", 0f);
                        enemyTeleportSwitch = true;
                    }
                    else StopChased();
                    break;
                case DifficultyLevelManager.DifficultyLevel.Extreme:
                    if(Homing.m_instance.enemyCount < 15.0f)
                    {
                        enemy.gameObject.SetActive(false);
                        enemyTeleportTime += Time.deltaTime;
                        if(enemyTeleportTime > 0)
                        {
                            if(enemyTeleportSwitch) CancelInvoke("EnemyEmerge");
                            Invoke("EnemyEmerge", 2f);
                            enemyTeleportSwitch = true;
                        }
                    }
                    else StopChased();
                    break;
            }
        }
        else if(SecondHouseManager.secondHouse_instance.ajure.gameObject.activeSelf == true)
        {
            Debug.Log("3");
            if(Homing2.m_instance.enemyEmerge)
            {
                Debug.Log("4");
                SecondHouseManager.secondHouse_instance.ajure.gameObject.SetActive(false);
                if(enemyTeleportSwitch) CancelInvoke("EnemyEmerge");
                Invoke("EnemyEmerge2", 0f);
                enemyTeleportSwitch = true;
            }
        }
    }
    public void StopChased()
    {
        chasedTime = false;
        enemy.gameObject.SetActive(false);
        soundManager.StopBgm(Homing.m_instance.chasedBGM);
        Enemy.transform.position = new Vector2(0, 0);
        Homing.m_instance.enemyCount = 0;
        GameManager.m_instance.notSaveSwitch = false;
    }
    private void EnemyEmerge()
    {
        if(!enemy.activeSelf)
        {
            enemy.gameObject.SetActive(true);
            enemyTeleportSwitch = false;
            Enemy.transform.position = enemyTeleportAddress.playerPosition;
            enemyTeleportTime = 0;
        }
    }
    private void EnemyEmerge2()
    {
        Debug.Log("5");
        if(!SecondHouseManager.secondHouse_instance.ajure.gameObject.activeSelf)
        {
            Debug.Log("6");
            enemyTeleportSwitch = false;
            Enemy2.gameObject.SetActive(true);
            SecondHouseManager.secondHouse_instance.ajure.gameObject.transform.position = enemyTeleportAddress.playerPosition;
            Vector3 newPosition = SecondHouseManager.secondHouse_instance.ajure.gameObject.transform.position;
            newPosition.y -= 1.3f;
            SecondHouseManager.secondHouse_instance.ajure.gameObject.transform.position = newPosition;
            Enemy2.speed -= 1.5f;
            enemyTeleportTime = 0;
        }
    }
    public void EnemyStartChase()
    {
        //2�C�ڂ̓C�x���g�݂̂Ȃ̂Ŋm��o�������炱��������Ȃ��B
        switch(difficultyLevelManager.difficultyLevel)
        {
            case DifficultyLevelManager.DifficultyLevel.Easy:
                if(enemyRndNum > 92 && Homing.m_instance.enemyEmerge)
                {
                    GameManager.m_instance.notSaveSwitch = true;
                    Enemy.gameObject.SetActive(true);
                    soundManager.PlayBgm(Homing.m_instance.chasedBGM);
                    Homing.m_instance.enemyCount += 0.1f;
                    chasedTime = true;
                }
                break;
            case DifficultyLevelManager.DifficultyLevel.Normal:
                if(enemyRndNum > 87 && Homing.m_instance.enemyEmerge)
                {
                    GameManager.m_instance.notSaveSwitch = true;
                    Enemy.gameObject.SetActive(true);
                    soundManager.PlayBgm(Homing.m_instance.chasedBGM);
                    Homing.m_instance.enemyCount += 0.1f;
                    chasedTime = true;
                }
                break;
            case DifficultyLevelManager.DifficultyLevel.Hard:
                if(enemyRndNum > 82 && Homing.m_instance.enemyEmerge)
                {
                    GameManager.m_instance.notSaveSwitch = true;
                    Enemy.gameObject.SetActive(true);
                    soundManager.PlayBgm(Homing.m_instance.chasedBGM);
                    Homing.m_instance.enemyCount += 0.1f;
                    chasedTime = true;
                }
                break;
            case DifficultyLevelManager.DifficultyLevel.Extreme:
                if(enemyRndNum > 74 && Homing.m_instance.enemyEmerge)
                {
                    GameManager.m_instance.notSaveSwitch = true;
                    Enemy.gameObject.SetActive(true);
                    soundManager.PlayBgm(Homing.m_instance.chasedBGM);
                    Homing.m_instance.enemyCount += 0.1f;
                    chasedTime = true;
                }
                break;
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
            if(teleports[i].tag == orderTag)
            {
                return teleports[i];
            }
            else if(teleports[i].name == orderTag)
            {
                return teleports[i];
            }
        }
        return null;
    }
    public TeleportAddress FindTeleportName(string orderName)
    {
        for(int i = 0; i < teleports.Length; i++)
        {
            if(teleports[i].name == orderName)
            {
                return teleports[i];
            }
        }
        return null;
    }
}

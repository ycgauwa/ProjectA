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

    };
    
    // Start is called before the first frame update
    void Start()
    {
         

    }

    /*�����Ńe���|�[�g�A�h���X������������Ă���PTA�̕ϐ��������ƂȂ���
     �v���C���[���e���|�[�g�����Ƃ������������Ă���Binvoke���v���C���[��
    �e���|�[�g���郁�\�b�h�̒��ɓ����̂ł͂Ȃ��e���|�[�g�����Ƃ���
    ��񂪂���΂悢�Ƃ����̂��l�����̈Ⴂ���������B�������̕�������Ȃ��N���X�œ������₷��*/
    public void OnPlayerTeleport(TeleportAddress playerTeleportAddress)
    {
        //ETA��PTA�������Ă���
        enemyTeleportAddress = playerTeleportAddress;
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
        //���͌�������A�E�ӂ̓v���C���[������TP�̈ʒu
        Enemy.transform.position = enemyTeleportAddress.playerPosition;
    }
    // Update is called once per frame
    void Update()
    {
        
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

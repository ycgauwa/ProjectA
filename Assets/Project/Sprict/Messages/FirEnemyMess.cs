using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class FirEnemyMess : MonoBehaviour
{
    [SerializeField]
    private List<string> messages2;
    [SerializeField]
    private List<string> names2;
    [SerializeField]
    private List<Sprite> images2;
    public GameObject enemy;
    public bool firstDeath;
    void Update()
    {
        if(!GameManager.m_instance.homing.enemyEmerge)
        {
            if(GameManager.m_instance.homing.enemyEmerge)
            {
                MessageManager.message_instance.MessageWindowActive(messages2, names2, images2, ct: destroyCancellationToken).Forget();
                enabled = false;
                gameObject.SetActive(false);
            }
        }
    }
    public void Message()
    {
        //��ԍŏ��̃`�F�C�X���Ɏ���ł��܂����Ƃ��̃��b�Z�[�W
        //���̃I�u�W�F�N�g��true�̎��Ɏ��񂾂�����B�����ł����񂾂炱�̃I�u�W�F�N�g�͑��݂�������
        //�܂�ꍇ�����Ƃ��āA���񂾂Ƃ��Ǝ���łȂ����ŕ�����K�v����
        //����łȂ��Ƃ��͏�L�ŏ\���������񂾂Ƃ��̓G�l�~�[�������Ă�Ƃ��Ƀ��g���C�{�^���ŏo�Ăق�������
        
    }
}

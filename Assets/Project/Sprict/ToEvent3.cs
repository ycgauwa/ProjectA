using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

//using UnityEditor.VersionControl;
using UnityEngine;

public class ToEvent3 : MonoBehaviour
{
    //�w��̍��W�ɍs������J�������ړ����G��⑫
    //���̂��ƂɃ��b�Z�[�W�E�B���h�E��\�����ă{�^�����������т�
    //���ʉ��𔭐� ���̉�b�܂Ői�ނ�BGM���苿���ǂ������Ă���
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<Sprite> images;
    public bool event3flag = false;
    public AudioClip eatSound;
    private async void OnTriggerEnter2D(Collider2D collider)
    {
        if(!event3flag) //�t���O�������ĂȂ��Ƃ�
        {
            if(collider.gameObject.tag.Equals("Player"))
            {
                SoundManager.sound_Instance.PlaySe(eatSound);
                await MessageManager.message_instance.MessageWindowActive(messages, names, images, ct: destroyCancellationToken);
                SoundManager.sound_Instance.StopSe(eatSound);
                event3flag = true; //�t���O������
                FlagsManager.flag_Instance.flagBools[2] = true;
                GameManager.m_instance.notSaveSwitch = true;
                GameTeleportManager.chasedTime = true;
                Homing.m_instance.speed = 2 + GameManager.m_instance.difficultyLevelManager.addEnemySpeed;
                Homing.m_instance.enemyEmerge = true; // �G���o������
                SoundManager.sound_Instance.PlayBgm(Homing.m_instance.chasedBGM);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {

    }
}

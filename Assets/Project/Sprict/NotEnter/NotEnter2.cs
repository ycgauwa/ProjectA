using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotEnter2 : MonoBehaviour
{
    // ����͒P���ɂǂ�ȏ����ł����Ă��G���A�ɓ������烁�b�Z�[�W�E�B���h�E���o�Ă���d�g�݂ɂ���B

    /*�����𖞂������ɐG���ƃ��b�Z�[�WA���o�ē����Ȃ��Ȃ�d�g�݂̍쐬
    ToEvent�N���X��one�ϐ��̏����ɂ���Ă������̃N���X�̃��\�b�h�������������Ȃ��������
    TP����O�ɃE�B���h�E�̕\�������W�̌Œ������B
    �ϐ��̈����n���͈������g���čs��ToEvent��NotEnter�œn��*/
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<Sprite> images;
    public GameObject player;
    private async void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
        {
            PlayerManager.m_instance.m_speed = 0;
            await MessageManager.message_instance.MessageWindowActive(GameManager.m_instance.GetMessages(name, "NotEnter"), GameManager.m_instance.GetSpeakerName(name, "NotEnter"), images, ct: destroyCancellationToken);        
        }
    }
}

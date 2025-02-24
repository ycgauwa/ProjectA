using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class NotEnter1 : MonoBehaviour
{
    // ���̃X�N���v�g�̓C�x���g�P���J�n����Ă��Ȃ��ƃ��b�Z�[�W���o��J�n���ꂽ��ʂ��悤�ɂ���

    /*�����𖞂������ɐG���ƃ��b�Z�[�WA���o�ē����Ȃ��Ȃ�d�g�݂̍쐬
    ToEvent�N���X��one�ϐ��̏����ɂ���Ă������̃N���X�̃��\�b�h�������������Ȃ��������
    TP����O�ɃE�B���h�E�̕\�������W�̌Œ������B
    �ϐ��̈����n���͈������g���čs��ToEvent��NotEnter�œn��*/
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<Sprite> image;
    public Canvas window;
    public Text target;
    public Text nameText;
    public GameObject player;
    public bool one = false;
    private bool isContacted = false;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag.Equals("Player"))
        {
            isContacted = true;
            //�C�x���g�Ȃ��ɂ͒ʂ�Ȃ��d�g��
            //false�̎����b�Z�[�W�E�B���h�E�̕\��
            //����厖�B�������O�ł����Ă����L�̂悤�Ȃ����ő���\
            one = ToEvent1.one;
            if (one == false)
                MessageManager.message_instance.MessageWindowActive(GameManager.m_instance.GetMessages(name, "NotEnter"), GameManager.m_instance.GetSpeakerName(name, "NotEnter"), image, ct: destroyCancellationToken).Forget();
            //MessageManager.message_instance.MessageWindowActive(messages, names, image, ct: destroyCancellationToken).Forget();
            else if (one == true) player.transform.position = new Vector2(-10, -105);
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player")) isContacted = false;
    }
}

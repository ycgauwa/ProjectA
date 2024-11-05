using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class NotEnterLadder : MonoBehaviour
{
    //��q�ňړ����銴���B�ŏ��̓G�Ɖ�܂ňړ��ł��Ȃ�
    //��ԏ��߂Ɖ���Ă���Ń��b�Z�[�W���Ⴄ�B���Ƃ��̓M�V�M�V�ƒ�q�̉�������B
    //���O�Ƀ��b�Z�[�W����ԏ��߂ɂ��銴���B�����烁�b�Z�[�W���K�v�Ȃ������ɂ���B
    //�t���O�����������ɒ��ׂ�Ƃ܂����ƂƂ��Ɉړ�����B���̂��ƂɃ��b�Z�[�W���o�Ă���B
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<Sprite> images;
    [SerializeField]
    private List<string> messages2;
    [SerializeField]
    private List<string> names2;
    [SerializeField]
    private List<Sprite> images2;
    public GameObject player;
    public ToEvent3 toevent3;
    private bool isContacted = false;
    public SoundManager soundManager;
    public AudioClip ladderSound;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag.Equals("Player"))
        {
            isContacted = true;
            if(gameObject.name == "Ladder1-1") soundManager.PlaySe(ladderSound);
        }
        // �C�x���g���I��������TP�ł���悤�ɂ�����
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag.Equals("Player"))
        {
            isContacted = false;
        }
    }
    private void Update()
    {
        if (isContacted && (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)))
        {
            if (toevent3.event3flag == false) MessageManager.message_instance.MessageWindowActive(messages, names, images, ct: destroyCancellationToken).Forget();
            else if (toevent3.event3flag == true)
            {
                MessageManager.message_instance.MessageWindowActive(messages2, names2, images2, ct: destroyCancellationToken).Forget();
                gameObject.name = "Ladder1-1";
            }
        }
    }
}

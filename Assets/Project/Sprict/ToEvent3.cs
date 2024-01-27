using System.Collections;
using System.Collections.Generic;
//using UnityEditor.VersionControl;
using UnityEngine;

public class ToEvent3 : MonoBehaviour
{
    //�w��̍��W�ɍs������J�������ړ����G��⑫
    //���̂��ƂɃ��b�Z�[�W�E�B���h�E��\�����ă{�^�����������т�
    //���ʉ��𔭐� ���̉�b�܂Ői�ނ�BGM���苿���ǂ������Ă���

    public GameObject eventcamera;
    public GameObject enemy;
    public GameObject player;
    //����̃G���A�ɏ��߂ē���C�x���g���J�n�����
    public bool event3flag;

    // ���b�Z�[�W�E�B���h�E�p�̕ϐ�
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
    public AudioSource chasedBGM;
    public Homing homing;
    bool firstchased = false;

    // ���ʉ�
    AudioSource audioSound;
    public AudioClip eatSound;

    // Start is called before the first frame update
    void Start()
    {
        event3flag = false;
        chasedBGM = GetComponent<AudioSource>();
        audioSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //�@�G���ǂ������I������牽��������������
        if(!enemy.activeSelf)
        {
            MessageManager.message_instance.MessageWindowActive(messages2, names2, images2);
            Destroy(this);
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(!event3flag) //�t���O�������ĂȂ��Ƃ�
        {
            if(collider.gameObject.tag.Equals("Player"))
            {
                MessageManager.message_instance.MessageWindowActive(messages,names,images);
                audioSound.PlayOneShot(eatSound);
                event3flag = true; //�t���O������
                homing.enemyEmerge = true; // �G���o������
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
        {
            if(!firstchased)
            {
                audioSound.Stop();
                chasedBGM.Play();
                firstchased = true;
            }
        }
    }
}

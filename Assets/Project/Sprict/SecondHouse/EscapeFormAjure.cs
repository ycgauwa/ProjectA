using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static UnityEngine.EventSystems.EventTrigger;

public class EscapeFormAjure : MonoBehaviour
{
    /*2��ڂ̑������A�G���瓦���邽�߂̃X�N���v�g�B
     �ŏ���Setobject��False�����C�x���g�t���O����������
    Active��True�ɂȂ�K�i���o���b����������悤�ɂȂ�B
    �b��������ƃZ���t�u�Ȃ�ł����Ȃ�K�i���c�c�H�v�u�Ƃ肠�����i�����I�v
    �Ó]�Ɠ����ɊK�i�̉��B���Z���t������u���񂾂񋷂��Ȃ��Ă����ȁv�u�������ˁc�c�������A�}�Ɏ~�܂�Ȃ��ł�I�I�v
    �i��ŐG���ƑO�͍s���~�܂�A���ɂ͓_����������݂������ȁj
    �����o���Ă���h�X���Ƃ����l�������鉹���Z���t�łтт鐰�Ƃ������K�l��`���Ȃ��猻�ݒn�̐����ւƂ�����ɖ��邳���߂�
    ���R�ɍs���\*/

    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<Sprite> image;
    [SerializeField]
    private List<string> messages2;
    [SerializeField]
    private List<string> names2;
    [SerializeField]
    private List<Sprite> image2;
    [SerializeField]
    private List<string> messages3;
    [SerializeField]
    private List<string> names3;
    [SerializeField]
    private List<Sprite> image3;
    [SerializeField]
    private List<string> messages4;
    [SerializeField]
    private List<string> names4;
    [SerializeField]
    private List<Sprite> image4;
    [SerializeField]
    private List<string> messages5;
    [SerializeField]
    private List<string> names5;
    [SerializeField]
    private List<Sprite> image5;

    public EnemyEncounter enemyEncounter;
    public GameObject player;
    public GameObject haru;
    public Homing2 ajure;
    public SoundManager soundManager;
    public AudioClip stairSound;
    public AudioClip fallSound;
    public AudioClip panelOpenSound;
    private bool isContacted = false;
    public Light2D light2D;


    private void OnTriggerEnter2D(Collider2D collider)
    {
        //�K�l�̎��ɕ\������郁�b�Z�[�W
        if(collider.gameObject.tag.Equals("Player"))
            isContacted = true;
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
            isContacted = false;
    }

    private void Update()//���̓`�F�b�N��Update�ɏ���
    {

        //���b�Z�[�W�E�B���h�E����Ƃ��͂��̃��\�b�h��
        if(isContacted && (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)))
        {
            isContacted = false;
            ajure.acceleration = 0;
            ajure.speed = 0;
            ajure.enemyEmerge = false;
            soundManager.PauseBgm(SecondHouseManager.secondHouse_instance.fearMusic);
            EscapeAjure().Forget();
            GameManager.m_instance.stopSwitch = true;
        }
    }

    private async UniTask EscapeAjure()
    {
        await MessageManager.message_instance.MessageWindowActive(messages, names, image, ct: destroyCancellationToken);
        await Blackout();
        soundManager.PlaySe(stairSound);
        await UniTask.Delay(TimeSpan.FromSeconds(2.6f));
        soundManager.PlaySe(stairSound);
        await UniTask.Delay(TimeSpan.FromSeconds(2.6f));
        soundManager.PlaySe(stairSound);
        //�u�~�܂��ăh�A�𔭌�����܂Łv
        await MessageManager.message_instance.MessageWindowActive(messages2, names2, image2, ct: destroyCancellationToken);  
        soundManager.PlaySe(panelOpenSound);
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
        //�u���̗l�q���m�F�A���~���ӎv���m�F���K�l����ɍ~���B�����f�v
        await MessageManager.message_instance.MessageWindowActive(messages3, names3, image3, ct: destroyCancellationToken);
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        soundManager.PlaySe(fallSound);
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
        //�u���~���I�v�|���恨�󂯎~�߂Ă�邩�瑁�������I�P���Ă��m��񂼁I��
        await MessageManager.message_instance.MessageWindowActive(messages4, names4, image4, ct: destroyCancellationToken);
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        soundManager.PlaySe(fallSound);
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
        player.transform.position = new Vector3(108, 107, 0);
        haru.transform.position = new Vector3(109, 107, 0);
        light2D.intensity = 1.0f;
        //�u���C��̓_�����ɂȂ����Ă��݂������ȁB�v�ɂ��Ă��Ȃ�ł���ȂƂ���ɉB���ʂƂ��������񂾂낤�H�u�������炢�����I�v
        await MessageManager.message_instance.MessageWindowActive(messages5, names5, image5, ct: destroyCancellationToken);
        await Blackout();
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
        light2D.intensity = 1.0f;
        haru.transform.position = new Vector3(0, 0, 0);
        ajure.gameObject.transform.position = new Vector3(9999,9999,0);
        GameManager.m_instance.stopSwitch = false;
        ajure.enemyEmerge = true;
        soundManager.UnPauseBgm(SecondHouseManager.secondHouse_instance.fearMusic);
        SecondHouseManager.secondHouse_instance.haruImportant.gameObject.SetActive(true);
    }
    private async UniTask Blackout()
    {
        light2D.intensity = 1.0f;
        GameManager.m_instance.stopSwitch = true;
        while(light2D.intensity > 0.01f)
        {
            light2D.intensity -= 0.012f;
            await UniTask.Delay(1);
        }
    }
}

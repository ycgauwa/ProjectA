using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class HaruImportantEvent : MonoBehaviour
{
    /*���e�������Ă����Ԃŗ�������ƃC�x���g�����I
     * �ŏ��̓Z���t�i�������͑������B�����������̂悤�ɑ��͂̂��邠�̌��ɒǂ����ꂻ���ɂȂ��Ă����j
     * ���̐S���܂�āu����������B���̂܂ܒǂ�����Ď��ʂ�������v�ƌ����ċ]���ɂȂ낤�Ƃ���B
     * ���̎��ɍK�l�͑I������������B�u���̂Ă�v�u������v
     * ���̎��Ɍ��̂Ă�ΐV�����I�u�W�F�N�g���o�����āu���̐�͐����P���Ă���B���̊Ԃɏ����ł������Ƃ̋����𗣂��Ȃ���ΐ��̋]�������ʂɂȂ�B�v
     * ���̃��[�g��I�ԂƍK�l�̂܂ܐi�ނ��Ƃ��ł���B
     * �������I�������ꍇ�͎��_�����ւƈڂ�B���̑O�ɃC�x���g�Ƃ��Ĉ�u������b�����萰����]�����\��ł��̏����ɂ���B���Ƃ��đ���\�ɂȂ�
     * �_�C�i�}�C�g�ő���j��ł��邻�̂���END�J�[�h�\���󂷍ۂɂ͕a�ݗ����������̊炪�\��������B
     */
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<Sprite> image;

    public GameObject panel;
    public GameObject firstSelection;
    public GameObject haru;
    public GameObject yukitoProfile;
    public GameObject haruProfile;
    public Canvas choiceCanvas;
    public Item bomb;
    public GameObject player;
    public GameObject enemy;

    private bool choiced = false;
    public bool cameraSwitch = false;

    public PlayerManager playerManager;
    public EnemyEncounter enemyEncounter;
    public Light2D light2D;
    public Homing2 ajure;
    public SoundManager soundManager;
    public AudioClip fearBGM;
    public AudioClip heartSound;
    public AudioClip meatSound;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player") && bomb.checkPossession)
        {
            GameManager.m_instance.stopSwitch = true;
            HaruEvent().Forget();
        }

    }
    private async UniTask HaruEvent()
    {
        Blackout().Forget();
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
        light2D.intensity = 1.0f;
        player.gameObject.transform.position = new Vector3(160, -11, 0);
        haru.gameObject.transform.position = new Vector3(159, -11, 0);
        //�Z���t���e�́i�������͑������B�����������̂悤�ɑ��͂̂��邠�̌��ɒǂ����ꂻ���ɂȂ��Ă����j�I�ȓ��e
        await MessageManager.message_instance.MessageWindowActive(messages, names, image, ct: destroyCancellationToken);
        GameManager.m_instance.stopSwitch = true;
        soundManager.PlayBgm(heartSound);
        choiceCanvas.gameObject.SetActive(true);
        panel.gameObject.SetActive(true);
        await UniTask.WaitUntil(() => choiced == true);
        SecondHouseManager.secondHouse_instance.notEnter13.gameObject.SetActive(true);
        soundManager.StopBgm(heartSound);
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
    public async void ResqueBotton()
    {
        choiced = true;
        panel.gameObject.SetActive(false);
        choiceCanvas.gameObject.SetActive(false);
        soundManager.StopBgm(enemyEncounter.fearMusic);
        //���S�O�̍Ō�̉�b
        await MessageManager.message_instance.MessageWindowActive(messages, names, image, ct: destroyCancellationToken);
        Blackout().Forget();
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
        light2D.intensity = 1.0f;

        //Playable�𐰂ɕς���s��
        if(yukitoProfile.gameObject.activeSelf)
        {
            yukitoProfile.gameObject.SetActive(false);
            haruProfile.gameObject.SetActive(true);
        }
        player.gameObject.tag = "Untagged";
        player.gameObject.SetActive(false);
        cameraManager.playerCamera = false;
        player.gameObject.SetActive(false);
        cameraManager.haruCamera = true;
        soundManager.StopBgm(enemyEncounter.fearMusic);
        playerManager = haru.AddComponent<PlayerManager>();
        playerManager = haru.GetComponent<PlayerManager>();
        playerManager.staminaMax = 300;
        playerManager.teleportManager = GameManager.m_instance.teleportManager;
        GameManager.m_instance.playerManager = playerManager;
        playerManager.homing = GameManager.m_instance.homing;
        Rigidbody2D rb = haru.GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        haru.gameObject.transform.position = new Vector3(169, -11, 0);
        //���̓Ƃ茾
        await MessageManager.message_instance.MessageWindowActive(messages, names, image, ct: destroyCancellationToken);
        GameManager.m_instance.stopSwitch = false;
        ajure.acceleration = 0;
        ajure.speed = 0;
        ajure.enemyEmerge = false;
    }
    public async void AbandonBotton()
    {
        choiced = true;
        panel.gameObject.SetActive(false);
        choiceCanvas.gameObject.SetActive(false);
        //���̂Ă����ƍŌ�̕ʂ�����ɏo���Ă��̏����ɂ���B
        await MessageManager.message_instance.MessageWindowActive(messages, names, image, ct: destroyCancellationToken);
        soundManager.StopBgm(enemyEncounter.fearMusic);
        Blackout().Forget();
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
        light2D.intensity = 1.0f;
        haru.gameObject.transform.position = new Vector3(0, 0, 0);
        GameManager.m_instance.stopSwitch = false;
        player.gameObject.transform.position = new Vector3(169,-11,0);
        ajure.acceleration = 0;
        ajure.speed = 0;
        ajure.enemyEmerge = false;
    }
}

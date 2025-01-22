using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemyEncounter : MonoBehaviour
{
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<Sprite> image;
    public Item bomb;

    public GameObject cameraObject;
    public GameObject player;
    public GameObject haru;
    public GameObject woodStair;
    public BombDefuse bombDefuse;
    public bool afterDifuse;
    public Homing2 ajure;
    public Light2D light2D;

    public SoundManager soundManager;
    public AudioClip heartSound;
    public AudioClip stepSound;
    public AudioClip fearMusic;
    public AudioClip clip;
    //���e���������ăA�C�e���Ƃ��ē��肵�������̃G���A�𓥂ނƐ��ƍ�������
    //���̂܂܃A�j���[�V�����ňꏏ�ɐi��(���ڐ��A�Q��ڐ��E�A�R��ڍK�l�A�S��r�����琰)TP�����Ƃ��ɃJ����������ēG���P���ė���
    //���̍ۂɐ��͈ꏏ�ɍs�������I�ƌ����ď�����B

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player") && bomb.checkPossession)
        {
            GameManager.m_instance.stopSwitch = true;
            EnemyEncount().Forget();
            haru.transform.position = new Vector2(117, 142);
        }

    }
    private async UniTask EnemyEncount()
    {
        // ��H��������I�R�����g
        await MessageManager.message_instance.MessageWindowActive(messages, names, image, ct: destroyCancellationToken);
        cameraManager.playerCamera = false;
        soundManager.PlayBgm(heartSound);
        cameraObject.transform.DOLocalMove(new Vector3(106, 136, -10), 3f);
        await UniTask.Delay(TimeSpan.FromSeconds(3f));
        haru.transform.position = new Vector3(106, 133, 0);

        soundManager.StopBgm(heartSound);
        await MessageManager.message_instance.MessageWindowActive(messages, names, image, ct: destroyCancellationToken);
        haru.transform.DOLocalMove(new Vector3(106, 137, 0), 3f);

        await UniTask.Delay(TimeSpan.FromSeconds(3f));
        await MessageManager.message_instance.MessageWindowActive(messages, names, image, ct: destroyCancellationToken);
        haru.transform.DOLocalMove(new Vector3(107, 137, 0), 0.5f);

        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
        player.transform.DOLocalMove(new Vector3(106, 139, 0),1f);
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        player.transform.DOLocalMove(new Vector3(106, 134, 0), 3f);

        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
        haru.transform.DOLocalMove(new Vector3(107, 134, 0),1.8f);
        await Blackout();
        await UniTask.Delay(TimeSpan.FromSeconds(1.6f));
        cameraManager.playerCamera = true;

        light2D.intensity = 1.0f;
        player.transform.position = new Vector3(113, 72, 0);
        //�����Ő��̋��������������B
        haru.transform.position = new Vector3(114, 72, 0);
        player.transform.DOLocalMove(new Vector3(113, 69, 0), 1f);
        haru.transform.DOLocalMove(new Vector3(114, 69, 0), 1f);

        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        player.transform.DOLocalMove(new Vector3(106, 69, 0), 2.5f);
        haru.transform.DOLocalMove(new Vector3(107, 69, 0), 2.5f);

        await UniTask.Delay(TimeSpan.FromSeconds(2.5f));
        cameraManager.playerCamera = false;
        ajure.gameObject.transform.position = new Vector3(106, 82, 0);
        cameraObject.transform.DOLocalMove(new Vector3(106, 81, -10), 3f);
        await UniTask.Delay(TimeSpan.FromSeconds(3f));

        //�����ǉ��ŉ��b���҂�+�Z���t�Ɛ��̎p�����B
        soundManager.PlaySe(clip);
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        await MessageManager.message_instance.MessageWindowActive(messages, names, image, ct: destroyCancellationToken);
        Blackout().Forget();
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
        haru.gameObject.transform.position = new Vector2(0,0);
        soundManager.PlayBgm(fearMusic);
        GameManager.m_instance.stopSwitch = false;
        player.transform.position = new Vector3(107, 70, 0);
        ajure.speed = 3f;
        ajure.gameObject.transform.position = new Vector3(103, 80, 0);
        light2D.intensity = 1.0f;
        ajure.enemyEmerge = true;
        cameraManager.playerCamera = true;
        woodStair.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
    //����1�K�Ɉړ����鎞�ɏu�Ԉړ�����
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

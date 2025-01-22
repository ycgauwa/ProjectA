using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;

public class RescueEvent : MonoBehaviour
{
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
    [SerializeField]
    private List<string> messages3;
    [SerializeField]
    private List<string> names3;
    [SerializeField]
    private List<Sprite> images3;
    [SerializeField]
    private List<string> firstDeathMessages;
    [SerializeField]
    private List<string> firstDeathNames;
    [SerializeField]
    private List<Sprite> firstDeathImages;
    public Image characterImage;
    public Canvas window;
    public Text target;
    public Text nameText;

    public SoundManager soundManager;
    public AudioClip doorSound;
    public AudioClip ChasedBGM;
    public NotEnter6 notEnter6;
    public ToEvent3 toEvent3;
    public GameTeleportManager gameTeleportManager;
    public GameObject Seiitirou;
    public GameObject SeiitirouAnimation;
    public AnimationStateController animationStateController;
    public GameObject Enemy;
    public GameObject Player;
    public GameObject yukitoProfile;
    public GameObject seiitirouProfile;
    public static bool messageSwitch = false;
    private bool SeiitirouMove;
    public bool RescueSwitch;
    public Light2D light2D;

    void Update()
    {
        // ���̃C�x���g���͓G�͏����Ȃ����G���o�Ă���Ԃ�BGM�𗬂�������B
        if (RescueSwitch == true)
        {
            gameTeleportManager.enemyRndNum = 99;
            if(Player.activeSelf)soundManager.PlayBgm(ChasedBGM);
        }

        if (SeiitirouMove == true && RescueSwitch == false)
            Player.gameObject.tag = "Untagged";
        else Player.gameObject.tag = "Player";
        // �Q�[���I�[�o�[��5�񂵂����ʂ��^���ÂɂȂ�C�x���g���쐬����i�h�A�̕����ω������艽������̕ω��͂��Ă��������j
        // ��񎀂ʂ��Ƃɉ�ʂ��Â��Ȃ�BBGM�͂����܂܂œG��Player�̈ʒu�̓C�x���g���n�܂��������̈ʒu�Ɉړ����Ă���B
        // ���g���C�{�^���������Ǝ����I�ɗ�̏ꏊ�ɖ߂����B
        if (GameManager.m_instance.deathCount == 1)
        {
            if (messageSwitch == false)
            {
                messageSwitch = true;
                MessageManager.message_instance.MessageWindowActive(firstDeathMessages, firstDeathNames, firstDeathImages, ct: destroyCancellationToken).Forget();
            }
            light2D.intensity = 0.8f;
        }
        else if (GameManager.m_instance.deathCount == 2)
        {
            light2D.intensity = 0.6f;
        }
        else if (GameManager.m_instance.deathCount == 3)
        {
            light2D.intensity = 0.4f;
        }
        else if (GameManager.m_instance.deathCount == 4)
        {
            light2D.intensity = 0.2f;
        }
        else if (GameManager.m_instance.deathCount == 5)
        {
            light2D.intensity = 0.0f;
        }
        else if (GameManager.m_instance.deathCount == 6)
        {
            light2D.intensity = 1.0f;
            if (yukitoProfile.gameObject.activeSelf)
            {
                yukitoProfile.gameObject.SetActive(false);
                seiitirouProfile.gameObject.SetActive(true);
                notEnter6.inventry.Delete(notEnter6.itemDateBase.GetItemId(251));
                notEnter6.inventry.Delete(notEnter6.itemDateBase.GetItemId(252));
                soundManager.StopBgm(toEvent3.chasedBGM);
                if(SeiitirouAnimation.GetComponent<AnimationStateController>().enabled == false)
                    SeiitirouAnimation.GetComponent<AnimationStateController>().enabled = true;
                GameManager.m_instance.deathCount = 0;
                gameObject.SetActive(false);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag.Equals("Player"))
        {
            if (notEnter6.rescued == false)
                return;
            else if (notEnter6.rescued == true && RescueSwitch == false)
            {
                CapsuleCollider2D capsuleCollider = Seiitirou.GetComponent<CapsuleCollider2D>();
                GameManager.m_instance.stopSwitch = true;
                capsuleCollider.enabled = false;
                RescueSeiitirouEvent().Forget();
                Homing.m_instance.enemyEmerge = true;
                toEvent3.event3flag = true;
            }
        }
    }
    private async UniTask RescueSeiitirouEvent()
    {
        Debug.Log("EventStart");
        await MessageManager.message_instance.MessageWindowActive(messages, names, images, ct: destroyCancellationToken);
        //����Y���o�Ă��ĉ�b����
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        soundManager.PlaySe(doorSound);
        Seiitirou.transform.position = new Vector2(35, 71);
        Seiitirou.transform.DOLocalMove(new Vector3(35, 70, 0), 1f);
        await UniTask.Delay(TimeSpan.FromSeconds(1f));

        await MessageManager.message_instance.MessageWindowActive(messages2, names2, images2, ct: destroyCancellationToken);
        //����Y�������Ă����B�h�A�̉��ƂƂ��ɉ�������������o�Ă���
        Seiitirou.transform.DOLocalMove(new Vector3(35, 69, 0), 0.5f);
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
        Seiitirou.transform.DOLocalMove(new Vector3(31, 69, 0), 1.5f);
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
        Seiitirou.transform.DOLocalMove(new Vector3(31, 60, 0), 4.0f);
        await UniTask.Delay(TimeSpan.FromSeconds(3.5f));
        soundManager.PlaySe(doorSound);
        await UniTask.Delay(TimeSpan.FromSeconds(2.0f));
        Seiitirou.transform.position = new Vector2(24, 0);

        Enemy.gameObject.SetActive(true);
        RescueSwitch = true;
        Homing.m_instance.speed = 0;
        Enemy.transform.position = new Vector2(35, 71);
        await MessageManager.message_instance.MessageWindowActive(messages3, names3, images3, ct: destroyCancellationToken);
        GameManager.m_instance.stopSwitch = false;
        Homing.m_instance.speed = 2;
        soundManager.PlayBgm(ChasedBGM);
        BoxCollider2D boxCollider = gameObject.GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;
    }
}

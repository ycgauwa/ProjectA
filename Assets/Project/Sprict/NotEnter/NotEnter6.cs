using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using static UnityEngine.GraphicsBuffer;

public class NotEnter6 : MonoBehaviour
{
    // �����Ȃ��Ɠ���Ȃ��h�A�̂��߂̃X�N���v�g
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
    private List<string> advancemessages;
    [SerializeField]
    private List<string> advancenames;
    [SerializeField]
    private List<Sprite> advanceimages;
    [SerializeField]
    private List<string> rescuemessages;
    [SerializeField]
    private List<string> rescuenames;
    [SerializeField]
    private List<Sprite> rescueimages;
    [SerializeField]
    private List<string> messages4;
    [SerializeField]
    private List<string> names4;
    [SerializeField]
    private List<Sprite> images4;

    public Canvas window;
    public Text target;
    public Text nameText;
    public Image characterImage;
    public Image redScreen;
    public Image notEnter6;
    public Canvas choicePanel;
    public Canvas redCanvas;

    public bool choiced;
    public bool rescued;
    public bool seiitirouFlag;//����Y�Ɉ��G�点��B

    public ItemDateBase itemDateBase;
    public Inventry inventry;
    public GameTeleportManager gameTeleportManager;
    public ItemSprictW itemSprictW;
    public Item underKey;
    public AudioClip fearBGM;
    public AudioClip scream;
    public AudioClip heartSound;
    public SoundManager soundManager;
    public RescueEvent rescueEvent;

    private int heartCounts;
    private float redNum = 0.0f;

    public GameObject player;
    public GameObject seiitirou;
    public GameObject enemy;
    public GameObject firstSelection;
    private CancellationTokenSource heartSoundCTS;

    private void Start()
    {
        redScreen.color = Color.clear;
        heartSoundCTS = new CancellationTokenSource();
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(ItemDateBase.itemDate_instance.GetItemId(253).checkPossession == false && collider.gameObject.tag.Equals("Player")) 
            MessageManager.message_instance.MessageWindowActive(messages, names, images, ct: destroyCancellationToken).Forget();
        else if(ItemDateBase.itemDate_instance.GetItemId(253).checkPossession)
        {
            if (collider.gameObject.tag.Equals("Player"))
            {
                ToEvent5().Forget();
            }
        }
        if(collider.gameObject.tag.Equals("Seiitirou"))
        {
            if(ItemDateBase.itemDate_instance.GetItemId(253).checkPossession == false)
            {
                MessageManager.message_instance.MessageWindowActive(messages4, names4, images4, ct: destroyCancellationToken).Forget();
                seiitirouFlag = true;
                enemy.transform.position = new Vector2(0, 0);
                enemy.gameObject.SetActive(false);
            }
            else seiitirou.transform.position = new Vector3(128, 25, 0);
        }
    }
    async UniTask ToEvent5()
    {
        if(!choiced)
        {
            GameManager.m_instance.stopSwitch = true;
            GameManager.m_instance.notSaveSwitch = true;
            Homing.m_instance.speed = 0;

            soundManager.PlaySe(scream);
            await UniTask.Delay(TimeSpan.FromSeconds(1.3f));
            await MessageManager.message_instance.MessageWindowActive(messages2, names2, images2, ct: destroyCancellationToken);

            //�I�������o������BGM��t����B��ʂ�h�炷�H�Ƃ��߂Â����肵�Ă��낢�낢������

            //�I�������I�΂��O�ɉ�b���I���Ȃ����߉��ɍs���Ȃ��B
            await OnPanel1();
            heartSoundCTS = new CancellationTokenSource();
            HeartSounds(heartSoundCTS.Token).Forget(e => { Debug.Log("�L�����Z�����ꂽ"); });
            soundManager.PlayBgm(fearBGM);
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
            redCanvas.gameObject.SetActive(true);
            redScreen.gameObject.SetActive(true);
            RedScreenActive().Forget();
            while(cameraManager.cameraInstance.cameraSize > 0.5)
            {
                if(!choiced)
                {
                    cameraManager.cameraInstance.cameraSize -= 0.01f;
                    await UniTask.Delay(1);
                }
                else break;
            }
            Homing.m_instance.speed = 2 + GameManager.m_instance.difficultyLevelManager.addEnemySpeed;
        }
        else MessageManager.message_instance.MessageWindowActive(messages3, names3, images3, ct: destroyCancellationToken).Forget();
    }
    async UniTask ToResqueEvent()
    {
        rescueEvent.gameObject.SetActive(true);
        soundManager.StopBgm(fearBGM);
        heartCounts = 1000;
        cameraManager.cameraInstance.cameraSize = 5f;
        notEnter6.gameObject.SetActive(false);
        choicePanel.gameObject.SetActive(false);
        heartSoundCTS.Cancel();
        soundManager.StopSe(heartSound);
        redScreen.gameObject.SetActive(false);
        redCanvas.gameObject.SetActive(false);
        GameManager.m_instance.stopSwitch = false;
        choiced = true;
        FlagsManager.flag_Instance.flagBools[4] = true;
        rescued = true;
        FlagsManager.flag_Instance.seiitirouFlagBools[0] = true;
        window.gameObject.SetActive(true);
        await OnMessage3();
        target.text = "";
        window.gameObject.SetActive(false);
        GameManager.m_instance.notSaveSwitch = false;
        if(itemDateBase.GetItemId(301).checkPossession == true)
        {
            itemSprictW.ItemDelete();
        }
        else return;
    }
    protected void showMessage(string message, string name, Sprite image)
    {
        target.text = message;
        nameText.text = name;
        characterImage.sprite = image;
    }

    async UniTask OnPanel1()
    {
        choicePanel.gameObject.SetActive(true);
        notEnter6.gameObject.SetActive(true);
        await UniTask.WaitUntil(() => choiced == false);
    }
    IEnumerator OnMessage3()
    {
        for(int i = 0; i < rescuemessages.Count; ++i)
        {
            yield return null;
            showMessage(rescuemessages[i], rescuenames[i], rescueimages[i]);
            yield return new WaitUntil(() => (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)));
        }
        yield break;
    }
    private async UniTask RedScreenActive()
    {
        while(redNum < 0.7)
        {
            redScreen.color = new Color(0.7f, 0, 0, redNum);
            redNum += 0.003f;
            await UniTask.Delay(1);
        }
    }
    private async UniTask HeartSounds(CancellationToken ct)
    {
        while(heartCounts < 1000)
        {
            soundManager.PlaySe(heartSound);
            heartCounts++;
            try
            {
                await UniTask.Delay(TimeSpan.FromSeconds(1f), cancellationToken: ct);
            }
            catch(OperationCanceledException)
            {
                break;
            }
        }
    }
    public void OnAdvanceBotton()
    {
        // �i�ރ{�^�������������̃��\�b�h�v�f�Ƃ��ĉ������ׂď����A���b�Z�[�W�ATP�A�߂�Ȃ��B
        MessageManager.message_instance.MessageWindowActive(advancemessages, advancenames, advanceimages, ct: destroyCancellationToken).Forget();
        //�n�[�g�̕ϐ����P�O�O�O�ɂ��ăJ�����̃Y�[�������ɖ߂��΂悢���X�L���[������
        //���Ƃ͑I�����\���̎��̃E�B���h�E�̕\�������������炿���Ɣ�\���ɂ��Ă��������Ď��R�ɓ�����悤�ɂȂ�����I��������\���ɂ���B
        heartCounts = 1000;
        cameraManager.cameraInstance.cameraSize = 5f;
        notEnter6.gameObject.SetActive(false);
        choicePanel.gameObject.SetActive(false);
        heartSoundCTS.Cancel();
        soundManager.StopSe(heartSound);
        redScreen.gameObject.SetActive(false);
        redCanvas.gameObject.SetActive(false);
        choiced = true;
        FlagsManager.flag_Instance.flagBools[4] = true;
        player.transform.position = new Vector3(128, 25, 0);
        enemy.transform.position = new Vector2(0, 0);
        enemy.gameObject.SetActive(false);
        gameTeleportManager.soundManager.StopBgm(Homing.m_instance.chasedBGM);
        Homing.m_instance.enemyEmerge = false;
        GameManager.m_instance.stopSwitch = false;
        GameManager.m_instance.inventry.Delete(ItemDateBase.itemDate_instance.GetItemId(253));//1�x���肾����IF�͂���Ȃ�
        soundManager.StopBgm(fearBGM);
        SaveSlotsManager.save_Instance.saveState.chapterNum ++;
        GameManager.m_instance.notSaveSwitch = false;
        FlagsManager.flag_Instance.ChangeUILocation("Minnka1-23");
    }
    public void OnRescueBotton()
    {
        // �����ɍs���{�^�����������Ƃ��̃��\�b�h�v�f�Ƃ��ĉ������ׂď����A���b�Z�[�W�A�s���Ȃ�
        ToResqueEvent().Forget();
    }
}

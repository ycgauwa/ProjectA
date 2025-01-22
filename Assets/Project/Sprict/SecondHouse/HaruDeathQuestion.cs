using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using static UnityEngine.Rendering.DebugUI;
using System.Threading;
using DG.Tweening;
using System;

public class HaruDeathQuestion : MonoBehaviour
{
    public SecondHouseManager secondHouseManager;
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
    private List<string> messages4;
    [SerializeField]
    private List<string> names4;
    [SerializeField]
    private List<Sprite> images4;
    [SerializeField]
    private List<string> messages5;
    [SerializeField]
    private List<string> names5;
    [SerializeField]
    private List<Sprite> images5;
    public GameObject player;
    public GameObject enemy;
    public GameObject haruDead;
    public GameObject panel;
    public GameObject firstSelection;
    public Light2D light2D;
    public Canvas choiceCanvas;
    public Canvas end5Canvas;
    public Image end5Image;

    private int heartCounts;
    private bool choiced = false;
    private bool isContacted = false;
    public bool cameraSwitch = false;

    private CancellationTokenSource heartSoundCTS;
    public Homing2 ajure;
    public SoundManager soundManager;
    public AudioClip fearBGM;
    public AudioClip heartSound;
    public AudioClip doorSound;
    public AudioClip ending5Sound;
    public NotEnter10 notEnter10;
    // このスクリプトでは犬に追われている時に入ろうとすると晴を見捨てるかを選ぶことができる

    private async void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player") && ajure.enemyEmerge)
        {
            isContacted = true;
            await HaruDeathSelection();
            GameManager.m_instance.stopSwitch = true;
        }
        
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
            isContacted = false;
    }
    private void Update()
    {
        if(ajure.enemyEmerge)
            gameObject.tag = "Untagged";
    }

    async UniTask HaruDeathSelection()
    {
        notEnter10.StopEnemy();
        await MessageManager.message_instance.MessageWindowActive(messages, names, images, ct: destroyCancellationToken);
        await OnPanel1();
        heartSoundCTS = new CancellationTokenSource();
        HeartSounds(heartSoundCTS.Token).Forget(e => { Debug.Log("キャンセルされた"); });
        soundManager.PlayBgm(fearBGM);
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
        cameraSwitch = true;
    }
    async UniTask OnPanel1()
    {
        choiceCanvas.gameObject.SetActive(true);
        panel.gameObject.SetActive(true);
        await UniTask.WaitUntil(() => choiced == false);
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
    public void OnIgnoredBotton()
    {
        //無視するボタン。音楽を再開して敵を再び動かす。
        heartCounts = 1000;
        cameraSwitch = false;
        heartSoundCTS.Cancel();
        soundManager.StopSe(heartSound);
        soundManager.StopBgm(fearBGM);
        notEnter10.MoveEnemy();
        choiced = true;
        panel.gameObject.SetActive(false);
        choiceCanvas.gameObject.SetActive(false);
        GameManager.m_instance.stopSwitch = false;
    }
    public async void OnAbandonBotton()
    {
        // 進むボタン
        heartCounts = 1000;
        cameraSwitch = false;
        heartSoundCTS.Cancel();
        soundManager.StopSe(heartSound);
        soundManager.StopBgm(fearBGM);
        choiced = true;
        panel.gameObject.SetActive(false);
        choiceCanvas.gameObject.SetActive(false);
        await Blackout();
        soundManager.PlaySe(doorSound);
        player.transform.position = new Vector3(83, 73, 0);
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
        light2D.intensity = 1.0f;
        player.transform.DOLocalMove(new Vector3(80, 73, 0), 2f);
        await UniTask.Delay(TimeSpan.FromSeconds(2f));

        //「晴ごめん……許して」的なセリフとその反応を加える。
        await MessageManager.message_instance.MessageWindowActive(messages2, names2, images2, ct: destroyCancellationToken);
        player.transform.DOLocalMove(new Vector3(80, 65, 0), 4f);
        await UniTask.Delay(TimeSpan.FromSeconds(4f));
        await Blackout();
        player.transform.position = new Vector3(131, -12, 0);
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
        // 晴の叫び声と幸人の後悔の描写
        await MessageManager.message_instance.MessageWindowActive(messages3, names3, images3, ct: destroyCancellationToken);

        light2D.intensity = 1.0f;
        player.transform.DOLocalMove(new Vector3(131, -16, 0), 5f);
        await UniTask.Delay(TimeSpan.FromSeconds(3f));
        // 幸人が決心して戻る。
        await MessageManager.message_instance.MessageWindowActive(messages4, names4, images4, ct: destroyCancellationToken);
        
        player.transform.DOLocalMove(new Vector3(131, -12, 0), 2.5f);
        await UniTask.Delay(TimeSpan.FromSeconds(2.5f));
        await Blackout();
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        player.transform.position = new Vector3(80, 65, 0);
        haruDead.gameObject.SetActive(true);
        light2D.intensity = 1.0f;
        player.transform.DOLocalMove(new Vector3(80, 73, 0), 8f);
        await UniTask.Delay(TimeSpan.FromSeconds(2.5f));
        soundManager.PlayBgm(ending5Sound);
        await UniTask.Delay(TimeSpan.FromSeconds(5.5f));
        //　晴の死体を見てしまう→うわ～ってなってEnding
        await MessageManager.message_instance.MessageWindowActive(messages5, names5, images5, ct: destroyCancellationToken);
        
        await BlackoutSlow();
        await UniTask.Delay(TimeSpan.FromSeconds(4f));
        light2D.intensity = 1.0f;
        end5Canvas.gameObject.SetActive(true);
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
    private async UniTask BlackoutSlow()
    {
        light2D.intensity = 1.0f;
        GameManager.m_instance.stopSwitch = true;
        while(light2D.intensity > 0.01f)
        {
            light2D.intensity -= 0.003f;
            await UniTask.Delay(1);
        }
    }
    public void OnclickEnd5Retry()
    {
        end5Canvas.gameObject.SetActive(false);
        light2D.intensity = 1.0f;
        soundManager.StopBgm(ending5Sound);
        //GameManager.m_instance.OnclickRetryButton();
        EndingGalleryManager.m_gallery.endingGallerys[4].sprite = end5Image.sprite;
        EndingGalleryManager.m_gallery.endingFlag[4] = true;
    }
}

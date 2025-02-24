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
    [SerializeField]
    private List<string> messages6;
    [SerializeField]
    private List<string> names6;
    [SerializeField]
    private List<Sprite> images6;
    [SerializeField]
    private List<string> advancemessages;
    [SerializeField]
    private List<string> advancenames;
    [SerializeField]
    private List<Sprite> advanceimages;
    public GameObject player;
    public GameObject enemy;
    public GameObject haruDead;
    public GameObject panel;
    public GameObject firstSelection;
    public Character haru;
    public Light2D light2D;
    public Canvas choiceCanvas;
    public Canvas end5Canvas;
    public Image end5Image;

    private int heartCounts;
    private bool choiced = false;

    private CancellationTokenSource heartSoundCTS;
    public Homing2 ajure;
    public SoundManager soundManager;
    public AudioClip fearBGM;
    public AudioClip heartSound;
    public AudioClip doorSound;
    public NotEnter10 notEnter10;
    // このスクリプトでは犬に追われている時に入ろうとすると晴を見捨てるかを選ぶことができる

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(ItemDateBase.itemDate_instance.GetItemId(257).geted)
            return;
        if(choiced)
        {
            MessageManager.message_instance.MessageWindowActive(messages6, names6, images6, ct: destroyCancellationToken).Forget();
        }
        else if(collider.gameObject.tag.Equals("Player") && ajure.enemyEmerge)
        {
            gameObject.tag = "Untagged";
            HaruDeathSelection().Forget();
            GameManager.m_instance.stopSwitch = true;
        }        
    }
    async UniTask HaruDeathSelection()
    {
        ajure.StopEnemy();
        await MessageManager.message_instance.MessageWindowActive(messages, names, images, ct: destroyCancellationToken);
        await OnPanel1();
        heartSoundCTS = new CancellationTokenSource();
        HeartSounds(heartSoundCTS.Token).Forget(e => { Debug.Log("キャンセルされた"); });
        soundManager.PlayBgm(fearBGM);
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
        while(cameraManager.cameraInstance.cameraSize > 0.5)
        {
            if(!choiced)
            {
                cameraManager.cameraInstance.cameraSize -= 0.01f;
                await UniTask.Delay(1);
            }
            else break;
        }
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
    public async void OnIgnoredBotton()
    {
        //無視するボタン。音楽を再開して敵を再び動かす。
        heartCounts = 1000;
        choiced = true;
        cameraManager.cameraInstance.cameraSize = 5f;
        heartSoundCTS.Cancel();
        soundManager.StopSe(heartSound);
        soundManager.StopBgm(fearBGM);
        panel.gameObject.SetActive(false);
        choiceCanvas.gameObject.SetActive(false);
        await MessageManager.message_instance.MessageWindowActive(advancemessages, advancenames, advanceimages, ct: destroyCancellationToken);
        ajure.MoveEnemy();
        GameManager.m_instance.stopSwitch = false;
    }
    public async void OnAbandonBotton()
    {
        heartCounts = 1000;
        cameraManager.cameraInstance.cameraSize = 5f;
        heartSoundCTS.Cancel();
        soundManager.StopSe(heartSound);
        soundManager.StopBgm(fearBGM);
        soundManager.StopBgm(SecondHouseManager.secondHouse_instance.fearMusic);
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
        SecondHouseManager.secondHouse_instance.haru.transform.position = new Vector2(0, 0);
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
        soundManager.PlayBgm(EndingGalleryManager.m_gallery.ending5Bgm);
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
        GameManager.m_instance.buttonPanel.gameObject.SetActive(false);
        GameManager.m_instance.gameoverWindow.gameObject.SetActive(false);
        GameManager.m_instance.stopSwitch = false;
        notEnter10.gameObject.SetActive(true);
        light2D.intensity = 1.0f;
        SecondHouseManager.secondHouse_instance.haru.transform.position = new Vector2(80, 75);
        haruDead.gameObject.SetActive(false);
        soundManager.StopBgm(EndingGalleryManager.m_gallery.ending5Bgm);
        GameManager.m_instance.player.transform.position = new Vector2(66, 148.4f);
        SecondHouseManager.secondHouse_instance.ajure.gameObject.transform.position = new Vector3(83.8f,146.8f,0);
        SecondHouseManager.secondHouse_instance.ajure.savedSpeed = 0;
        SecondHouseManager.secondHouse_instance.ajure.savedAcceleration = 0;
        EndingGalleryManager.m_gallery.endingGallerys[4].sprite = end5Image.sprite;
        EndingGalleryManager.m_gallery.endingFlag[4] = true;
        haru.FavorabilityCount -= 40;
    }
}

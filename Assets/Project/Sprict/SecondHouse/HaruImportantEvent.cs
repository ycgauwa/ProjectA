using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class HaruImportantEvent : MonoBehaviour
{
    /*爆弾を持っている状態で立ち入るとイベント発生！
     * 最初はセリフ（俺たちは走った。だが化け物のように走力のあるあの犬に追いつかれそうになっていた）
     * 晴の心が折れて「もういいよ。このまま追いつかれて死ぬだけだよ」と言って犠牲になろうとする。
     * その時に幸人は選択を強いられる。「見捨てる」「助ける」
     * この時に見捨てれば新しいオブジェクトが出現して「この先は晴が襲われている。その間に少しでもあいつとの距離を離さなければ晴の犠牲が無駄になる。」
     * このルートを選ぶと幸人のまま進むことができる。
     * 助けるを選択した場合は視点が晴へと移る。その前にイベントとして一瞬だけ会話が入り晴が絶望した表情でその場を後にする。晴として操作可能になり
     * ダイナマイトで大岩を破壊できるそのあとENDカード表示壊す際には病み落ちした晴の顔が表示させる。
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
        //セリフ内容は（俺たちは走った。だが化け物のように走力のあるあの犬に追いつかれそうになっていた）的な内容
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
        //死亡前の最後の会話
        await MessageManager.message_instance.MessageWindowActive(messages, names, image, ct: destroyCancellationToken);
        Blackout().Forget();
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
        light2D.intensity = 1.0f;

        //Playableを晴に変える行程
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
        //晴の独り言
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
        //見捨てる後悔と最後の別れを口に出してその場を後にする。
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

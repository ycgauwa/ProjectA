using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static UnityEngine.EventSystems.EventTrigger;

public class EscapeFormAjure : MonoBehaviour
{
    /*2回目の遭遇時、敵から逃げるためのスクリプト。
     最初はSetobjectはFalseだがイベントフラグを回収すると
    ActiveがTrueになり階段が出現話しかけられるようになる。
    話しかけるとセリフ「なんでいきなり階段が……？」「とりあえず進もう！」
    暗転と同時に階段の音。一回セリフを入れる「だんだん狭くなってきたな」「そうだね……いたっ、急に止まらないでよ！！」
    （手で触れると前は行き止まり、下には点検口があるみたいだな）
    少し経ってからドスンという人が落ちる音→セリフでびびる晴とせかす幸人を描きながら現在地の説明へとした後に明るさが戻り
    自由に行動可能*/

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
        //幸人の時に表示されるメッセージ
        if(collider.gameObject.tag.Equals("Player"))
            isContacted = true;
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
            isContacted = false;
    }

    private void Update()//入力チェックはUpdateに書く
    {

        //メッセージウィンドウ閉じるときはこのメソッドを
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
        //「止まってドアを発見するまで」
        await MessageManager.message_instance.MessageWindowActive(messages2, names2, image2, ct: destroyCancellationToken);  
        soundManager.PlaySe(panelOpenSound);
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
        //「下の様子を確認、＆降りる意思を確認→幸人勝手に降りる。晴困惑」
        await MessageManager.message_instance.MessageWindowActive(messages3, names3, image3, ct: destroyCancellationToken);
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        soundManager.PlaySe(fallSound);
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
        //「晴降りろ！」怖いよ→受け止めてやるから早く来い！襲われても知らんぞ！→
        await MessageManager.message_instance.MessageWindowActive(messages4, names4, image4, ct: destroyCancellationToken);
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        soundManager.PlaySe(fallSound);
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
        player.transform.position = new Vector3(108, 107, 0);
        haru.transform.position = new Vector3(109, 107, 0);
        light2D.intensity = 1.0f;
        //「風呂場の点検口につながってたみたいだな。」にしてもなんであんなところに隠し通とがあったんだろう？「いいからいくそ！」
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

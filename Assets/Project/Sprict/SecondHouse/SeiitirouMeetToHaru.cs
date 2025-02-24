using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SeiitirouMeetToHaru : MonoBehaviour
{
    //このスクリプトは征一郎状態で晴に会おうとしてドアが何かに押さえつけられて開かない。そのために何か道具が必要だと
    //考える征一郎。目的のアイテムを入手したらイベントはスタートする
    //アイテムを入手したあとメッセージが走り、ドアを開ける音がする。そのあとに暗転して部屋に移動晴との会話イベントが始まる。
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> messages2;
    [SerializeField]
    private List<string> messages3;
    [SerializeField]
    private List<string> messages4;
    [SerializeField]
    private List<string> messages5;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<string> names2;
    [SerializeField]
    private List<string> names3;
    [SerializeField]
    private List<string> names4;
    [SerializeField]
    private List<string> names5;
    [SerializeField]
    private List<Sprite> images;
    [SerializeField]
    private List<Sprite> images2;
    [SerializeField]
    private List<Sprite> images3;
    [SerializeField]
    private List<Sprite> images4;
    [SerializeField]
    private List<Sprite> images5;
    public GameObject haru;
    public Item　metalBlade;
    public Item key;
    public AudioClip crying;
    public AudioClip doorSound;
    public Light2D light2D;
    private async void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Seiitirou"))
        {
            if(!metalBlade.checkPossession)
            {
                if(!enabled) return;
                gameObject.tag = "Untagged";
                GameManager.m_instance.stopSwitch = true;
                await HaruCryingEvent();
            }
            else if(metalBlade.checkPossession)
            {
                GameManager.m_instance.inventry.Delete(metalBlade);
                await OpendKey();
            }
        }
    }
    private async UniTask HaruCryingEvent()
    {
        await MessageManager.message_instance.MessageWindowActive(messages, names, images, ct: destroyCancellationToken);
        SoundManager.sound_Instance.PlaySe(crying);
        await UniTask.Delay(TimeSpan.FromSeconds(9f));
        //誰か泣いている？
        await MessageManager.message_instance.MessageWindowActive(messages2, names2, images2, ct: destroyCancellationToken);
        GameManager.m_instance.stopSwitch = false;
        SecondHouseManager.secondHouse_instance.metalBlade.gameObject.SetActive(true);
    }
    private async UniTask OpendKey()
    {
        // コメント「鍵が開けられる。」体で押したわずかな隙間に金属板を差し込むとドアが少しずつだが開いた。
        await MessageManager.message_instance.MessageWindowActive(messages3, names3, images3, ct: destroyCancellationToken);
        SoundManager.sound_Instance.PlaySe(doorSound);
        //GameManager.m_instance.inventry.Delete(metalBlade);
        GameManager.m_instance.stopSwitch = true;
        await UniTask.Delay(TimeSpan.FromSeconds(4f));
        await Blackout();
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
        // 大丈夫か？電気つけるぞ？のセリフ差し込み
        await MessageManager.message_instance.MessageWindowActive(messages4, names4, images4, ct: destroyCancellationToken);
        GameManager.m_instance.seiitirou.gameObject.transform.position = new Vector3(130, 142.2f, 0);
        haru.gameObject.transform.position = new Vector3(141, 144.5f, 0);
        haru.gameObject.transform.DOLocalMove(new Vector3(141, 144, 0), 0.3f);
        light2D.intensity = 1.0f;
        GameManager.m_instance.seiitirou.gameObject.transform.DOLocalMove(new Vector3(141, 142.2f, 0), 5f);
        await UniTask.Delay(TimeSpan.FromSeconds(5f));
        // お互いの事情を話し合い、ヒントを貰う。
        await MessageManager.message_instance.MessageWindowActive(messages5, names5, images5, ct: destroyCancellationToken);
        await Blackout();
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
        light2D.intensity = 1.0f; 
        GameManager.m_instance.stopSwitch =false;
        gameObject.tag = "Minnka2-15";
        enabled = false;
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

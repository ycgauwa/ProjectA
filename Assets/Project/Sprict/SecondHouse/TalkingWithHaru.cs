using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Threading;
using System;

public class TalkingWithHaru : MonoBehaviour
{
    // 幸人ルートはこのまま。征一郎ルートは普通に話しかけと、鍵持ってからイベントスタート
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<Sprite> image;
    [SerializeField]
    private List<string> topButtonMessages;
    [SerializeField]
    private List<string> topButtonNames;
    [SerializeField]
    private List<Sprite> topButtonImage;
    [SerializeField]
    private List<string> middleButtonMessages;
    [SerializeField]
    private List<string> middleButtonNames;
    [SerializeField]
    private List<Sprite> middleButtonImage;
    [SerializeField]
    private List<string> underButtonMessages;
    [SerializeField]
    private List<string> underButtonNames;
    [SerializeField]
    private List<Sprite> underButtonImage;
    [SerializeField]
    private List<string> seiitirouMessages;
    [SerializeField]
    private List<string> seiitirouNames;
    [SerializeField]
    private List<Sprite> seiitirouImages;
    [SerializeField]
    private List<string> seiitirouMessages2;
    [SerializeField]
    private List<string> seiitirouNames2;
    [SerializeField]
    private List<Sprite> seiitirouImages2;
    [SerializeField]
    private List<string> seiitirouMessages3;
    [SerializeField]
    private List<string> seiitirouNames3;
    [SerializeField]
    private List<Sprite> seiitirouImages3;
    public Canvas selectionCanvas;
    public Image selection;
    public GameObject firstSelect;
    public GameObject haru;
    private bool isContacted = false;
    private bool seiIsContacted = false;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //幸人の時に表示されるメッセージ
        if(collider.gameObject.tag.Equals("Player"))
            isContacted = true;
        else if(collider.gameObject.tag.Equals("Seiitirou"))
            seiIsContacted = true;
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
            isContacted = false;
        else if(collider.gameObject.tag.Equals("Seiitirou"))
            seiIsContacted = false;
    }
    private void Update()
    {
        if(isContacted && (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)))
        {
            HaruTalking(ct: destroyCancellationToken).Forget();
            isContacted = false;
        }
        else if(seiIsContacted && (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)))
        {
            SeiitirouTalkWithHaru().Forget();
            seiIsContacted = false;
        }
    }
    private async UniTask SeiitirouTalkWithHaru()
    {
        if(!SecondHouseManager.secondHouse_instance.clinicKey.checkPossession)
            await MessageManager.message_instance.MessageWindowActive(seiitirouMessages, seiitirouNames, seiitirouImages, ct: destroyCancellationToken);
        else
        {
            //鍵を持っている場合イベントがくる。イベントの詳細は会話をした後に鍵を渡して先に行ってもらう。んで、部屋から出た時にイベントが発生するようなCsを作る
            //「おじさん鍵とれた？」「おじさんじゃねぇお兄さんな？」「ほらよ。家じゅうあるかされて懲り懲りしたぜ。」的なコメント
            GameManager.m_instance.stopSwitch = true;
            await MessageManager.message_instance.MessageWindowActive(seiitirouMessages2, seiitirouNames2, seiitirouImages2, ct: destroyCancellationToken);
            GameManager.m_instance.inventry.Delete(SecondHouseManager.secondHouse_instance.clinicKey);
            SecondHouseManager.secondHouse_instance.haruTakedKey = true;
            SecondHouseManager.secondHouse_instance.endingCase9.gameObject.SetActive(true);
            GameManager.m_instance.seiitirou.transform.DOLocalMove(new Vector3(141, 143, 0), 1f);
            await UniTask.Delay(TimeSpan.FromSeconds(1f));
            GameManager.m_instance.seiitirou.transform.DOLocalMove(new Vector3(142, 143, 0), 0.7f);
            await UniTask.Delay(TimeSpan.FromSeconds(0.7f));
            haru.gameObject.transform.DOLocalMove(new Vector3(141, 143, 0), 1f);
            await UniTask.Delay(TimeSpan.FromSeconds(1f));
            haru.gameObject.transform.DOLocalMove(new Vector3(130, 143, 0), 5f);
            await UniTask.Delay(TimeSpan.FromSeconds(5.1f));
            SoundManager.sound_Instance.PlaySe(SecondHouseManager.secondHouse_instance.doorSound);
            haru.gameObject.transform.position = new Vector3(82.8f, 142.3f,0f);
            SecondHouseManager.secondHouse_instance.ajure.gameObject.transform.position = new Vector3(77.8f, 142.8f, 0f);
            await MessageManager.message_instance.MessageWindowActive(seiitirouMessages3, seiitirouNames3, seiitirouImages3, ct: destroyCancellationToken);
            gameObject.SetActive(false);
            GameManager.m_instance.stopSwitch = false;
        }
    }
    private async UniTask HaruTalking(CancellationToken ct)
    {
        await MessageManager.message_instance.MessageSelectWindowActive(messages, names, image,selectionCanvas,selection,firstSelect, ct: destroyCancellationToken);
    }
    public async void HaruTalkTopButton()
    {
        selectionCanvas.gameObject.SetActive(false);
        selection.gameObject.SetActive(false);
        MessageManager.message_instance.isOpenSelect = false;
        await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
        MessageManager.message_instance.MessageWindowActive(topButtonMessages, topButtonNames, topButtonImage, ct: destroyCancellationToken).Forget();
    }
    public async void HaruTalkMiddleButton()
    {
        selectionCanvas.gameObject.SetActive(false);
        selection.gameObject.SetActive(false);
        MessageManager.message_instance.isOpenSelect = false;
        await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
        MessageManager.message_instance.MessageWindowActive(middleButtonMessages, middleButtonNames, middleButtonImage, ct: destroyCancellationToken).Forget();
    }
    public async void HaruTalkUnderButton()
    {
        selectionCanvas.gameObject.SetActive(false);
        selection.gameObject.SetActive(false);
        MessageManager.message_instance.isOpenSelect = false;
        await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
        MessageManager.message_instance.MessageWindowActive(underButtonMessages, underButtonNames, underButtonImage, ct: destroyCancellationToken).Forget();
    }
}

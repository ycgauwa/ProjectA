using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine.Rendering.Universal;

public class YukitoDeadItem : MonoBehaviour
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
    public Canvas selectionCanvas;
    public Image selection;
    public GameObject firstSelect;
    //　幸人が死体になった時にアイテムを取るためだけのスクリプト。
    //設計としてはまず分岐として充希のお守りを持っているかどうかで変わる。
    //共通としてはカメラがズームされて縁が暗くなり晴が死んだときと同じ音楽が流れる。
    //お守りを持っていると追加でセリフと選択が入る。
    private bool seiIsContacted = false;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Seiitirou"))
            seiIsContacted = true;
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Seiitirou"))
            seiIsContacted = false;
    }
    private void Update()
    {
        if(!ItemDateBase.itemDate_instance.GetItemId(253).checkPossession && seiIsContacted && (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)))
        {
            //　触ります、カメラが変化します音楽も流れます。セリフも出します。終わったら選択も出します。選択肢で展開を変えます。
            GetItemFromYdead().Forget();
            seiIsContacted = false;
        }
    }
    private async UniTask GetItemFromYdead()
    {
        GameManager.m_instance.stopSwitch = true;
        GameManager.m_instance.adjustVignette = true;
        while(GameManager.m_instance.vignette.intensity.value < 0.6f)
        {
            GameManager.m_instance.vignette.intensity.value += 0.01f;
            await UniTask.Delay(1);
        }
        while(cameraManager.cameraInstance.cameraSize > 2.5)
        {
            cameraManager.cameraInstance.cameraSize -= 0.01f;
            await UniTask.Delay(2);
        }
        SoundManager.sound_Instance.PlayBgm(EndingGalleryManager.m_gallery.ending5Bgm);
        await MessageManager.message_instance.MessageWindowActive(messages, names, images, ct: destroyCancellationToken);
        GameManager.m_instance.inventry.Add(ItemDateBase.itemDate_instance.GetItemId(253));
        if(ItemDateBase.itemDate_instance.GetItemId(201).geted)
        {
            await MessageManager.message_instance.MessageSelectWindowActive(messages2, names2, images2, selectionCanvas, selection, firstSelect, ct: destroyCancellationToken);
            return;
        }
        await Blackout();
    }
    public void GetAmulet()
    {
        GameManager.m_instance.inventry.Add(ItemDateBase.itemDate_instance.GetItemId(201));
        MessageManager.message_instance.MessageWindowActive(messages2, names2, images2, ct: destroyCancellationToken).Forget();
        selectionCanvas.gameObject.SetActive(false);
        selection.gameObject.SetActive(false);
        MessageManager.message_instance.isOpenSelect = false;
        Blackout().Forget();
    }
    public void NotGetAmulet()
    {
        MessageManager.message_instance.MessageWindowActive(messages2, names2, images2, ct: destroyCancellationToken).Forget();
        selectionCanvas.gameObject.SetActive(false);
        selection.gameObject.SetActive(false);
        MessageManager.message_instance.isOpenSelect = false;
        Blackout().Forget();
    }
    private async UniTask Blackout()
    {
        while(SecondHouseManager.secondHouse_instance.light2D.intensity > 0.01f)
        {
            SecondHouseManager.secondHouse_instance.light2D.intensity -= 0.012f;
            await UniTask.Delay(1);
        }
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        cameraManager.cameraInstance.cameraSize = 5;
        SecondHouseManager.secondHouse_instance.light2D.intensity = 1.0f;
        GameManager.m_instance.stopSwitch = false;
        GameManager.m_instance.adjustVignette = false;
        SoundManager.sound_Instance.StopBgm(EndingGalleryManager.m_gallery.ending5Bgm);
    }
}

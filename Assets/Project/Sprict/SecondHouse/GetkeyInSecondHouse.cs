using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GetkeyInSecondHouse : MonoBehaviour
{
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
    public Canvas selectionCanvas;
    public Canvas diaryCanvas;
    public Image selection;
    public Image jpMemo;
    public GameObject firstSelect;
    public GameObject haru;
    public AudioClip paperSound;
    public bool isContacted = false;
    public bool isSeiContacted = false;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //幸人の時に表示されるメッセージ
        if(collider.gameObject.tag.Equals("Player"))
            isContacted = true;
        else if(collider.gameObject.tag.Equals("Seiitirou"))
            isSeiContacted = true;

    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
            isContacted = false;
        else if(collider.gameObject.tag.Equals("Seiitirou"))
            isSeiContacted = false;
    }
    private void Update()
    {
        //メッセージウィンドウ閉じるときはこのメソッドを
        if(isContacted && (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)))
            Talking(ct: destroyCancellationToken).Forget();
        else if(isSeiContacted && (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)))
            ActiveMemoWindow().Forget();
    }
    private async UniTask Talking(CancellationToken ct)
    {
        await MessageManager.message_instance.MessageSelectWindowActive(messages, names, image, selectionCanvas, selection, firstSelect, ct: destroyCancellationToken);
    }
    private async UniTask ActiveMemoWindow()
    {
        //　メモが見れる。
        GameManager.m_instance.stopSwitch = true;
        isSeiContacted = false;
        await MessageManager.message_instance.MessageWindowActive(seiitirouMessages, seiitirouNames, seiitirouImages, ct: destroyCancellationToken);
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
        SoundManager.sound_Instance.PlaySe(paperSound);
        diaryCanvas.gameObject.SetActive(true);
        jpMemo.gameObject.SetActive(true);
        await UniTask.WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
        jpMemo.gameObject.SetActive(false);
        diaryCanvas.gameObject.SetActive(false);
        SoundManager.sound_Instance.PlaySe(paperSound);
        await MessageManager.message_instance.MessageWindowActive(seiitirouMessages2, seiitirouNames2, seiitirouImages2, ct: destroyCancellationToken);
        GameManager.m_instance.stopSwitch = false;
    }
    public async void TopButton()
    {
        selectionCanvas.gameObject.SetActive(false);
        selection.gameObject.SetActive(false);
        MessageManager.message_instance.isOpenSelect = false;
        await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
        MessageManager.message_instance.MessageWindowActive(topButtonMessages, topButtonNames, topButtonImage, ct: destroyCancellationToken).Forget();
    }
    public async void MiddleButton()
    {
        selectionCanvas.gameObject.SetActive(false);
        selection.gameObject.SetActive(false);
        MessageManager.message_instance.isOpenSelect = false;
        await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
        MessageManager.message_instance.MessageWindowActive(middleButtonMessages, middleButtonNames, middleButtonImage, ct: destroyCancellationToken).Forget();
    }
    public async void UnderButton()
    {
        selectionCanvas.gameObject.SetActive(false);
        selection.gameObject.SetActive(false);
        MessageManager.message_instance.isOpenSelect = false;
        await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
        MessageManager.message_instance.MessageWindowActive(underButtonMessages, underButtonNames, underButtonImage, ct: destroyCancellationToken).Forget();
    }
}

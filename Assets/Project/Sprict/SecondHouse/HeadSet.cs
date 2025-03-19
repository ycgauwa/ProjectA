using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System.Diagnostics;

public class HeadSet : MonoBehaviour
{
    //受話器のスクリプト。征一郎が選択することで選択状況を保持。その保持された選択状況を読み取って。メッセージを幸人側で受け取る
    //幸人は話しかけて状態がダメなら「何もないみたいだ……」でおわり
    //征一郎は話しかけて選択肢の表記選択肢によってセリフを出す。
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
    private List<string> messages7;
    [SerializeField]
    private List<string> names7;
    [SerializeField]
    private List<Sprite> images7;
    public Canvas selectionCanvas;
    public Image selection;
    public GameObject firstSelect;
    public Button[] talkSelection = new Button[3];
    public AudioClip noiseSound;
    private bool isContacted = false;
    private bool SeiContacted = false;
    public AudioClip callStart;
    public AudioClip callStop;
    public SaveDate saveDate;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
            isContacted = true;
        else if(collider.gameObject.tag.Equals("Seiitirou"))
            SeiContacted = true;
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
            isContacted = false;
        else if(collider.gameObject.tag.Equals("Seiitirou"))
            SeiContacted = false;
    }
    private void Update()
    {
        if(isContacted && (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)))
        {
            YukitoEvent().Forget();
            isContacted = false;
        }
        else if(SeiContacted && (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)))
        {
            SeiitirouEvent().Forget();
            SeiContacted = false;
        }
    }
    private async UniTask YukitoEvent()
    {
        await MessageManager.message_instance.MessageWindowActive(messages, names, images, ct: destroyCancellationToken);
        switch (saveDate.callNumber)
        {
            
            case 0:
                SoundManager.sound_Instance.PlaySe(callStop);
                await MessageManager.message_instance.MessageWindowActive(messages2, names2, images2, ct: destroyCancellationToken);
                return;
            case 1:
                SoundManager.sound_Instance.PlaySe(callStart);
                await MessageManager.message_instance.MessageWindowActive(messages3, names3, images3, ct: destroyCancellationToken);
                break;
            case 2:
                SoundManager.sound_Instance.PlaySe(callStart);
                await MessageManager.message_instance.MessageWindowActive(messages4, names4, images4, ct: destroyCancellationToken);
                break;
            case 3:
                SoundManager.sound_Instance.PlaySe(callStart);
                await MessageManager.message_instance.MessageWindowActive(messages5, names5, images5, ct: destroyCancellationToken);
                break;
            default: 
                break;
        }
    }
    private async UniTask SeiitirouEvent()
    {
        talkSelection[0].gameObject.SetActive(false);
        await MessageManager.message_instance.MessageSelectWindowActive(messages6, names6, images6, selectionCanvas, selection, firstSelect, ct: destroyCancellationToken);
    }
    public async void PressTopButton()
    {
        selectionCanvas.gameObject.SetActive(false);
        selection.gameObject.SetActive(false);
        MessageManager.message_instance.isOpenSelect = false;
        saveDate.callNumber = 1;
        await MessageManager.message_instance.MessageSelectWindowActive(messages7, names7, images7, selectionCanvas, selection, firstSelect, ct: destroyCancellationToken);
    }
    public async void PressMiddleButton()
    {
        selectionCanvas.gameObject.SetActive(false);
        selection.gameObject.SetActive(false);
        MessageManager.message_instance.isOpenSelect = false;
        saveDate.callNumber = 2;
        await MessageManager.message_instance.MessageSelectWindowActive(messages7, names7, images7, selectionCanvas, selection, firstSelect, ct: destroyCancellationToken);
    }
    public async void PressBottomButton()
    {
        selectionCanvas.gameObject.SetActive(false);
        selection.gameObject.SetActive(false);
        MessageManager.message_instance.isOpenSelect = false;
        saveDate.callNumber = 3;
        await MessageManager.message_instance.MessageSelectWindowActive(messages7, names7, images7, selectionCanvas, selection, firstSelect, ct: destroyCancellationToken);
    }
}

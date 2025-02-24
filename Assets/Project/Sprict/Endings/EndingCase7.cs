using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Rendering.Universal;
using System;

public class EndingCase7 : MonoBehaviour
{
    //　幸人が迎える２軒目のドアエンディング。
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
    [SerializeField]
    private List<string> messages8;
    [SerializeField]
    private List<string> names8;
    [SerializeField]
    private List<Sprite> images8;
    [SerializeField]
    private List<string> messages9;
    [SerializeField]
    private List<string> names9;
    [SerializeField]
    private List<Sprite> images9;
    [SerializeField]
    private List<string> messages10;
    [SerializeField]
    private List<string> names10;
    [SerializeField]
    private List<Sprite> images10;
    [SerializeField]
    private List<string> messages11;
    [SerializeField]
    private List<string> names11;
    [SerializeField]
    private List<Sprite> images11;
    [SerializeField]
    private List<string> messages12;
    [SerializeField]
    private List<string> names12;
    [SerializeField]
    private List<Sprite> images12;
    [SerializeField]
    private List<string> messages13;
    [SerializeField]
    private List<string> names13;
    [SerializeField]
    private List<Sprite> images13;
    [SerializeField]
    private List<string> messages14;
    [SerializeField]
    private List<string> names14;
    [SerializeField]
    private List<Sprite> images14;
    public Canvas window;
    public Canvas Selectwindow;
    public Canvas end7Window;
    public Canvas end8Window;
    public Text target;
    public Text nameText;
    public Image characterImage;
    public Image selection;
    public Image ending7Screen;
    public Image end7Image1;
    public Image end7Image2;
    public Image end7Image3;
    public Image end8Image1;
    public Image end8Image2;
    public Image icedHandImage;
    public Image handImage;
    public Color color;
    public AudioClip ending7Sound;
    public AudioClip ending8Sound;
    public AudioClip heavyDoorSound;
    private bool isContacted = false;
    private bool seiContacted = false;
    public bool answer;
    public GameObject firstSelect;
    public GameObject firstSelect2;
    public GameObject entrance;
    public GameObject wall;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
            isContacted = true;
        else if(collider.gameObject.tag.Equals("Seiitirou"))
            seiContacted = true;
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
            isContacted = false;
        else if(collider.gameObject.tag.Equals("Seiitirou"))
            seiContacted = false;
    }
    private void Update()
    {
        if(Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return))
        {
            if((isContacted == true || seiContacted == true) && SecondHouseManager.secondHouse_instance.ajure.enemyEmerge == true)//どのキャラでも敵に追われているとき出すセリフ
            {
                MessageManager.message_instance.MessageWindowActive(messages14, names14, images14, ct: destroyCancellationToken).Forget();
                isContacted = false;
                seiContacted = false;
            }
            else if(isContacted == true && SecondHouseManager.secondHouse_instance.ajure.enemyEmerge == false)
            {
                 if(answer == true && EndingGalleryManager.m_gallery.endingFlag[7])
                 {
                    MessageManager.message_instance.MessageWindowActive(messages13, names13, images13, ct: destroyCancellationToken).Forget();
                    isContacted = false;
                 }
                 else if(answer)
                 {
                    MessageManager.message_instance.MessageWindowActive(messages, names, images, ct: destroyCancellationToken).Forget();
                    isContacted = false;
                 }
                 else
                 {
                    MessageManager.message_instance.MessageSelectWindowActive(messages2, names2, images2, Selectwindow, selection, firstSelect, ct: destroyCancellationToken).Forget();
                    isContacted = false;
                 }
                    
            }
            else if(seiContacted == true && SecondHouseManager.secondHouse_instance.ajure.enemyEmerge == false)
            {
                MessageManager.message_instance.MessageWindowActive(messages3, names3, images3, ct: destroyCancellationToken).Forget();
                seiContacted = false;
            }
                
        }
    }
    protected void showMessage(string message, string name, Sprite image)
    {
        target.text = message;
        nameText.text = name;
        characterImage.sprite = image;
    }
    private async UniTask EndingEvent()
    {
        GameManager.m_instance.notSaveSwitch = true;
        await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
        window.gameObject.SetActive(true);
        for(int i = 0; i < messages4.Count; ++i)
        {
            await UniTask.Delay(1);
            showMessage(messages4[i], names4[i], images4[i]);
            if(i == messages4.Count - 1)
            {
                color.a = 0f;
                break;
            }
            await UniTask.WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
        }
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        Debug.Log("color=");
        color = icedHandImage.GetComponent<Image>().color;
        icedHandImage.gameObject.SetActive(true);
        color.a = 0f;
        while(color.a <= 1f)
        {
            if(color.a >= 0.299f && color.a <=0.303f) SoundManager.sound_Instance.PlaySe(EndingGalleryManager.m_gallery.freezeSound);
            color.a += 0.004f;
            icedHandImage.color = color;
            Debug.Log(icedHandImage.color);
            await UniTask.Delay(1);
        }
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));

        handImage.gameObject.SetActive(false);
        await UniTask.WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
        await MessageManager.message_instance.MessageWindowActive(messages5, names5, images5, ct: destroyCancellationToken);
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));

        icedHandImage.gameObject.SetActive(false);
        SecondHouseManager.secondHouse_instance.haru.transform.position = new Vector3(106,68,0);
        SoundManager.sound_Instance.PlaySe(SecondHouseManager.secondHouse_instance.doorSound);
        SecondHouseManager.secondHouse_instance.haru.transform.DOLocalMove(new Vector3(106, 64.2f, 0), 4f);
        SoundManager.sound_Instance.PlaySe(SecondHouseManager.secondHouse_instance.runSound);
        await UniTask.Delay(TimeSpan.FromSeconds(2f));

        SoundManager.sound_Instance.StopSe(SecondHouseManager.secondHouse_instance.runSound);
        cameraManager.cameraInstance.playerCamera = false;
        GameManager.m_instance.mainCamera.transform.DOLocalMove(new Vector3(106, 62f, -10), 2f);
        await UniTask.Delay(TimeSpan.FromSeconds(2f));

        await MessageManager.message_instance.MessageWindowActive(messages6, names6, images6, ct: destroyCancellationToken);
        SecondHouseManager.secondHouse_instance.haru.transform.DOLocalMove(new Vector3(106, 68f, 0), 1.5f);
        SoundManager.sound_Instance.PlaySe(SecondHouseManager.secondHouse_instance.runSound);
        await UniTask.Delay(TimeSpan.FromSeconds(3f));
        SoundManager.sound_Instance.StopSe(SecondHouseManager.secondHouse_instance.runSound);
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        SoundManager.sound_Instance.PlaySe(SecondHouseManager.secondHouse_instance.doorSound);

        await Blackout();
        if(EndingGalleryManager.m_gallery.endingFlag[1] == false)
            End7Event().Forget();
        else
        {
            End8Event().Forget();
        }


    }
    public async UniTask End7Event()
    {
        end7Image1.gameObject.SetActive(true);
        await MessageManager.message_instance.MessageWindowActive(messages7, names7, images7, ct: destroyCancellationToken);
        SoundManager.sound_Instance.PlaySe(heavyDoorSound);
        await UniTask.Delay(TimeSpan.FromSeconds(2.5f));

        SoundManager.sound_Instance.PlayBgm(ending7Sound);
        color = end7Image2.GetComponent<Image>().color;
        end7Image2.gameObject.SetActive(true);
        color.a = 0f;
        while(color.a <= 1f)
        {
            color.a += 0.004f;
            end7Image2.color = color;
            await UniTask.Delay(1);
        }
        end7Image1.gameObject.SetActive(false);
        await MessageManager.message_instance.MessageWindowActive(messages8, names8, images8, ct: destroyCancellationToken);
        await UniTask.Delay(TimeSpan.FromSeconds(1f));

        color = end7Image2.GetComponent<Image>().color;
        while(color.a > 0)
        {
            color.a -= 0.004f;
            end7Image2.color = color;
            await UniTask.Delay(1);
        }
        end7Image2.gameObject.SetActive(false);
        end7Image3.gameObject.SetActive(true);
        color = end7Image3.GetComponent<Image>().color;
        color.a = 0;
        while(color.a < 1f)
        {
            color.a += 0.004f;
            end7Image3.color = color;
            await UniTask.Delay(1);
        }
        await MessageManager.message_instance.MessageWindowActive(messages9, names9, images9, ct: destroyCancellationToken); //「噓だろ」
        end7Image3.gameObject.SetActive(false);
        ending7Screen.gameObject.SetActive(true);
        entrance.gameObject.SetActive(false);
        wall.gameObject.SetActive(true);
    }
    public async UniTask End8Event()
    {
        SoundManager.sound_Instance.PlayBgm(ending8Sound);
        await MessageManager.message_instance.MessageWindowActive(messages10, names10, images10, ct: destroyCancellationToken);
        SoundManager.sound_Instance.PlaySe(SecondHouseManager.secondHouse_instance.noiseSound);
        end8Window.gameObject.SetActive(true);
        end8Image1.gameObject.SetActive(true);
        await UniTask.Delay(TimeSpan.FromSeconds(0.7f));

        SoundManager.sound_Instance.StopSe(SecondHouseManager.secondHouse_instance.noiseSound);
        end8Image1.gameObject.SetActive(false);
        await MessageManager.message_instance.MessageWindowActive(messages11, names11, images11, ct: destroyCancellationToken);
        SoundManager.sound_Instance.PlaySe(heavyDoorSound);
        await UniTask.Delay(TimeSpan.FromSeconds(2f));

        SoundManager.sound_Instance.PlaySe(EndingGalleryManager.m_gallery.blizzardSound);
        await MessageManager.message_instance.MessageWindowActive(messages12, names12, images12, ct: destroyCancellationToken);
        SoundManager.sound_Instance.StopSe(EndingGalleryManager.m_gallery.blizzardSound);
        SoundManager.sound_Instance.PlaySe(EndingGalleryManager.m_gallery.freezeSound);
        await UniTask.Delay(TimeSpan.FromSeconds(2f));

        end8Image2.gameObject.SetActive(true);
        color = end8Image2.GetComponent<Image>().color;
        color.a = 0;
        while(color.a < 1f)
        {
            color.a += 0.012f;
            end8Image2.color = color;
            await UniTask.Delay(1);
        }
    }
    private async UniTask Blackout()
    {
        while(SecondHouseManager.secondHouse_instance.light2D.intensity > 0.01f)
        {
            SecondHouseManager.secondHouse_instance.light2D.intensity -= 0.004f;
            await UniTask.Delay(1);
        }
    }
    public void OnclickEnd7Retry()
    {
        end7Image2.gameObject.SetActive(false);
        end7Window.gameObject.SetActive(false);
        cameraManager.cameraInstance.playerCamera = true;
        SecondHouseManager.secondHouse_instance.light2D.intensity = 1;
        SoundManager.sound_Instance.StopBgm(ending7Sound);
        target.text = "";
        window.gameObject.SetActive(false);
        GameManager.m_instance.ImageErase(characterImage);
        GameManager.m_instance.stopSwitch = false;
        GameManager.m_instance.notSaveSwitch = false;
        GameManager.m_instance.player.transform.position = new Vector2(79.5f, 66);
        SecondHouseManager.secondHouse_instance.haru.transform.position = new Vector2(80, 75);
        EndingGalleryManager.m_gallery.endingGallerys[6].sprite = end7Image2.sprite;
        EndingGalleryManager.m_gallery.endingFlag[6] = true;
    }
    public void OnclickEnd8Retry()
    {
        end8Image2.gameObject.SetActive(false);
        end8Window.gameObject.SetActive(false);
        cameraManager.cameraInstance.playerCamera = true;
        SecondHouseManager.secondHouse_instance.light2D.intensity = 1;
        SoundManager.sound_Instance.StopBgm(ending8Sound);
        target.text = "";
        window.gameObject.SetActive(false);
        GameManager.m_instance.ImageErase(characterImage);
        GameManager.m_instance.stopSwitch = false;
        GameManager.m_instance.notSaveSwitch = false;
        GameManager.m_instance.player.transform.position = new Vector2(79.5f, 66);
        SecondHouseManager.secondHouse_instance.haru.transform.position = new Vector2(80, 75);
        EndingGalleryManager.m_gallery.endingGallerys[7].sprite = end8Image2.sprite;
        EndingGalleryManager.m_gallery.endingFlag[7] = true;
    }
    public void End7SelectYes()
    {
        GameManager.m_instance.stopSwitch = true;
        SoundManager.sound_Instance.PlaySe(GameManager.m_instance.decision);
        selection.gameObject.SetActive(false);
        Selectwindow.gameObject.SetActive(false);
        MessageManager.message_instance.isOpenSelect = false;
        end7Window.gameObject.SetActive(true);
        handImage.gameObject.SetActive(true);
        EndingEvent().Forget();
        answer = true;
    }
    public void End7SelectNo()
    {
        //メッセージを出して、ウィンドウを閉じる。そのあとはもう選択肢が出ないようにする。
        SoundManager.sound_Instance.PlaySe(GameManager.m_instance.decision);
        selection.gameObject.SetActive(false);
        Selectwindow.gameObject.SetActive(false);
        MessageManager.message_instance.isOpenSelect = false;
        answer = true;
    }
}

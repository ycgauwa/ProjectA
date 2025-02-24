using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using DG.Tweening;
using static UnityEngine.Rendering.DebugUI;


public class EndingCase9 : MonoBehaviour
{
    /*
     * 晴に鍵を渡してから出るとイベント開始、鍵を取ってからは警告だす「この鍵はあいつに～」
     * 征一郎状態で診療所の鍵を取ってから部屋を出ようとすると。悲鳴が聞こえる（Se入れるか考え中）
     * セリフを出してから征一郎を診療所に入れるとアジュレが晴と相対してる。
     * 晴はもう腰をぬかして動けない様子でそのまま立ち去るか、晴を助けるかの選択を強いられる。
     * 晴を見捨てる選択をしたら、征一郎はひっそりと立ち去る。悲痛な晴の叫びがある中。隣の部屋にて絶望する。
     * そこにいきなり暗くなり、幸人の亡霊らしきものが現れて、近づいたら画面は暗転。血が噴き出す音とともに死亡する。
     */

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
    public Canvas selectionCanvas;
    public Canvas end9Canvas;
    public Image selection;
    public Image end9Image;
    public GameObject firstSelect;
    public GameObject secondSelect;
    public GameObject weightObject;
    public GameObject yukitoGhost;
    public GameObject haru;
    public AudioClip ending9Music;
    public AudioClip noiseSound;
    public AudioClip haruScream;
    public AudioClip BloodSound;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Seiitirou"))
        {
            GameManager.m_instance.stopSwitch = true;
            EnterClinicRoom().Forget();
        }
    }
    private async UniTask EnterClinicRoom()
    {
        //イベントの流れ、入る前にセリフからの悲鳴。で暗転からの入っていく。
        await MessageManager.message_instance.MessageWindowActive(messages, names, images, ct: destroyCancellationToken);
        SecondHouseManager.secondHouse_instance.ajure.gameObject.SetActive(true);
        SecondHouseManager.secondHouse_instance.sleepAjure.gameObject.SetActive(false);
        SoundManager.sound_Instance.PlaySe(haruScream);
        await UniTask.Delay(TimeSpan.FromSeconds(3f));
        await MessageManager.message_instance.MessageWindowActive(messages2, names2, images2, ct: destroyCancellationToken);
        await Blackout();
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
        GameManager.m_instance.seiitirou.gameObject.transform.position = new Vector3(87.1f, 139.7f, 0);
        GameManager.m_instance.mainCamera.gameObject.transform.position = new Vector3(87.1f, 139.7f, 0);
        SecondHouseManager.secondHouse_instance.light2D.intensity = 1.0f;
        cameraManager.cameraInstance.seiitirouCamera = false;
        GameManager.m_instance.mainCamera.transform.DOLocalMove(new Vector3(81,142.3f, -10), 3f);
        await UniTask.Delay(TimeSpan.FromSeconds(3f));
        await MessageManager.message_instance.MessageWindowActive(messages3, names3, images3, ct: destroyCancellationToken);
        SoundManager.sound_Instance.PlaySe(SecondHouseManager.secondHouse_instance.dogSound);
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
        //　こっから心臓の音を鳴らして選択肢。
        await MessageManager.message_instance.MessageSelectWindowActive(messages4, names4, images4, selectionCanvas, selection, firstSelect, SecondHouseManager.secondHouse_instance.heartSound, ct: destroyCancellationToken);
    }
    public void OnAbandonBotton()
    {
        //見捨てるボタンだからイベント発生後エンディング
        cameraManager.cameraInstance.seiitirouCamera = true;
        selectionCanvas.gameObject.SetActive(false);
        selection.gameObject.SetActive(false);
        MessageManager.message_instance.isOpenSelect = false;
        SoundManager.sound_Instance.StopBgm(SecondHouseManager.secondHouse_instance.heartSound);
        OnAbandonEvent().Forget();
    }
    private async UniTask OnAbandonEvent()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
        // 「ごめんな」
        await MessageManager.message_instance.MessageWindowActive(messages5, names5, images5, ct: destroyCancellationToken);
        await Blackout();
        GameManager.m_instance.seiitirou.transform.DOLocalMove(new Vector3(88, 139.7f, 0), 0.7f);
        await UniTask.Delay(TimeSpan.FromSeconds(0.7f));
        SoundManager.sound_Instance.PlaySe(SecondHouseManager.secondHouse_instance.doorSound);
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
        // 「悲鳴は聞こえなかった」
        await MessageManager.message_instance.MessageWindowActive(messages6, names6, images6, ct: destroyCancellationToken);
        SoundManager.sound_Instance.PlaySe(SecondHouseManager.secondHouse_instance.doorSound);
        GameManager.m_instance.seiitirou.gameObject.transform.position = new Vector3(130, 142.1f, 0);
        SecondHouseManager.secondHouse_instance.light2D.intensity = 0.6f;
        GameManager.m_instance.seiitirou.gameObject.transform.DOLocalMove(new Vector3(142, 142.1f, 0), 6f);
        await UniTask.Delay(TimeSpan.FromSeconds(6f));
        // 「なんか薄暗いな」「ふふっふははははまた殺した。」
        await MessageManager.message_instance.MessageWindowActive(messages7, names7, images7, ct: destroyCancellationToken);
        await UniTask.Delay(TimeSpan.FromSeconds(0.7f));
        SoundManager.sound_Instance.PlaySe(SecondHouseManager.secondHouse_instance.doorSound);
        yukitoGhost.gameObject.transform.position = new Vector3(130, 142.1f, 0);
        SoundManager.sound_Instance.PlayBgm(ending9Music);
        cameraManager.cameraInstance.seiitirouCamera = false;
        GameManager.m_instance.mainCamera.transform.DOLocalMove(new Vector3(130, 142.1f, -10), 4f);
        await UniTask.Delay(TimeSpan.FromSeconds(4f));
        SoundManager.sound_Instance.PlayBgm(noiseSound);
        await UniTask.Delay(TimeSpan.FromSeconds(0.9f));
        SoundManager.sound_Instance.PauseBgm(noiseSound);
        GameManager.m_instance.mainCamera.transform.position = new Vector3(135.4f, 142.2f, -10);
        await MessageManager.message_instance.MessageWindowActive(messages8, names8, images8, ct: destroyCancellationToken);
        SoundManager.sound_Instance.UnPauseBgm(noiseSound);
        yukitoGhost.transform.DOLocalMove(new Vector3(142, 142.2f, 0), 2.3f);
        await Blackout();
        SoundManager.sound_Instance.PlaySe(BloodSound);
        await UniTask.Delay(TimeSpan.FromSeconds(2.3f));
        end9Canvas.gameObject.SetActive(true);
        end9Image.gameObject.SetActive(true);
    }
    public void OnResqueBotton()
    {
        // 助けるボタンだから少しイベント入れた後に追いかけっこ発生。暗転入れて、晴をにがしーのセリフであの部屋なら逃げ切れる。と入れる。
        cameraManager.cameraInstance.seiitirouCamera = true;
        GameManager.m_instance.mainCamera.transform.position = GameManager.m_instance.seiitirou.transform.position;
        selectionCanvas.gameObject.SetActive(false);
        selection.gameObject.SetActive(false);
        MessageManager.message_instance.isOpenSelect = false;
        SoundManager.sound_Instance.StopBgm(SecondHouseManager.secondHouse_instance.heartSound);
        OnResqueEvent().Forget();
    }
    private async UniTask OnResqueEvent()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
        await MessageManager.message_instance.MessageWindowActive(messages9, names9, images9, ct: destroyCancellationToken);
        await Blackout();
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        SoundManager.sound_Instance.PlayBgm(SecondHouseManager.secondHouse_instance.runSound);
        await UniTask.Delay(TimeSpan.FromSeconds(2f));
        SoundManager.sound_Instance.StopBgm(SecondHouseManager.secondHouse_instance.runSound);
        SoundManager.sound_Instance.PlaySe(SecondHouseManager.secondHouse_instance.doorSound);
        await UniTask.Delay(TimeSpan.FromSeconds(0.7f));
        haru.gameObject.transform.position = new Vector3(141, 144.5f, 0);
        SecondHouseManager.secondHouse_instance.light2D.intensity = 1;
        await MessageManager.message_instance.MessageWindowActive(messages10, names10, images10, ct: destroyCancellationToken);
        SecondHouseManager.secondHouse_instance.ajure.enemyEmerge = true;
        SoundManager.sound_Instance.PlayBgm(SecondHouseManager.secondHouse_instance.fearMusic);
        GameManager.m_instance.stopSwitch = false;
    }
    public void OnclickEnd9Retry()
    {
        end9Image.gameObject.SetActive(false);
        end9Canvas.gameObject.SetActive(false);
        SecondHouseManager.secondHouse_instance.light2D.intensity = 1.0f;
        SoundManager.sound_Instance.StopBgm(ending9Music);
        //GameManager.m_instance.OnclickRetryButton();
        EndingGalleryManager.m_gallery.endingGallerys[8].sprite = end9Image.sprite;
        EndingGalleryManager.m_gallery.endingFlag[8] = true;
    }
    private async UniTask Blackout()
    {
        while(SecondHouseManager.secondHouse_instance.light2D.intensity > 0.01f)
        {
            SecondHouseManager.secondHouse_instance.light2D.intensity -= 0.012f;
            await UniTask.Delay(1);
        }
    }
}

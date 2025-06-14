using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.Playables;
using Cysharp.Threading.Tasks;
using System;

public class ToEvent2 : MonoBehaviour
{
    //選択画面が出て会話が始まりそのあとに太鼓の音を流す。その後白い光の演出が出た後に誰もいなくなる
    //座標固定したカメラが移動して（ゆっくり目に）一人のNPCが教室に入り太鼓の前まで行く
    //セリフを話した後にまたカメラを移動、警備員と少女が映る画角に移動そこで会話が終わった後
    //女の子がエフェクトを出して消滅。警備員のセリフを書いてから終幕。
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
    private List<string> beforeMessages;
    [SerializeField]
    private List<string> beforeNames;
    [SerializeField]
    private List<Sprite> beforeImages;
    public Canvas window;
    public Canvas timelineCanvas;
    public Text target;
    public Text nameText;
    public Image characterImage;
    public bool eventFinished;
    private bool isContacted = false;
    public SoundManager soundManager;
    private GameSceneController gameSceneController;
    public AudioClip sound;
    public AudioClip doorSound;
    public AudioClip flashSe;
    public AudioClip suspiciousBgm;
    public Light2D light2D;
    public GameObject player;
    public GameObject[] friends;
    public GameObject girl;
    public GameObject guards;
    public GameObject eventcamera;
    public GameObject gameMenuUI;
    public float speed;
    public Item item;
    private bool playerCamera;
    public PlayableDirector playableDirector;
    public Animator cameraAnimator;
    public Volume volume;
    private ColorAdjustments colorAdjustments;
    public float x, y,t;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
            isContacted = true;
    }

    // colliderをもつオブジェクトの領域外にでたとき(下記で説明1)
    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
            isContacted = false;
    }
    private void FixedUpdate()
    {
        if(isContacted == true  && Input.GetButton("Submit") && Input.GetKeyDown(KeyCode.Return))
        {
            if (item.checkPossession)
            {
                light2D = gameObject.GetComponent<Light2D>();
                item.checkPossession = false;
                CreateCoroutine().Forget();
            }
        }
    }
    async UniTask CreateCoroutine()
    {
        gameSceneController = GameManager.m_instance.gameSceneController;
        GameManager.m_instance.notSaveSwitch = true;
        volume.profile.TryGet<ColorAdjustments>(out colorAdjustments);
        GameManager.m_instance.inventry.Delete(item);
        await MessageManager.message_instance.MessageWindowOnceActive(beforeMessages, beforeNames, beforeImages, ct: destroyCancellationToken);

        await Blackout();
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
        soundManager.PlaySe(doorSound);
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        soundManager.PlaySe(SecondHouseManager.secondHouse_instance.runSound);
        await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
        soundManager.PlaySe(SecondHouseManager.secondHouse_instance.runSound);
        await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
        soundManager.PlaySe(SecondHouseManager.secondHouse_instance.runSound);
        await UniTask.Delay(TimeSpan.FromSeconds(3f));
        soundManager.StopSe(SecondHouseManager.secondHouse_instance.runSound);

        await LightOut();
        light2D.intensity = 1.0f;
        window.gameObject.SetActive(true);

        await MessageManager.message_instance.MessageWindowOnceActive(messages, names, images, ct: destroyCancellationToken);

        target.text = "";
        window.gameObject.SetActive(false);
        soundManager.PlaySe(sound);

        await UniTask.Delay(TimeSpan.FromSeconds(2.0f));
        await Flash();

        //startCoroutineではなくてyield　return を書いてあげると動く（yield returnの意味を調べておく）
        await UniTask.Delay(TimeSpan.FromSeconds(2.0f));
        light2D.intensity = 1.0f;
        soundManager.StopSe(flashSe);
        soundManager.PlayBgm(suspiciousBgm);
        //cameraの処理
        Event2Camera();
        girl.transform.DOMoveY(6.01f, 1f);
        eventcamera.transform.DOMove(new Vector3(-80,6,-10),6);

        await UniTask.Delay(TimeSpan.FromSeconds(6.0f));

        window.gameObject.SetActive(true);
        await MessageManager.message_instance.MessageWindowOnceActive(messages2, names2, images2, ct: destroyCancellationToken);
        
        target.text = "";
        window.gameObject.SetActive(false);
        
        cameraManager.cameraInstance.girlCamera = true;
        girl.transform.DOMoveX(-86,4f);
        await UniTask.Delay(TimeSpan.FromSeconds(2.0f));
        soundManager.PlaySe(doorSound);
        await UniTask.Delay(TimeSpan.FromSeconds(2.0f));
        
        guards.transform.position = new Vector3(-76, 5, 0);
        cameraManager.cameraInstance.girlCamera = false;
        eventcamera.transform.DOMove(new Vector3(x, y, -10), t);
        girl.transform.DOMoveX(-85.9f, 0.3f);

        await UniTask.Delay(TimeSpan.FromSeconds(3.0f));

        window.gameObject.SetActive(true);
        await MessageManager.message_instance.MessageWindowOnceActive(messages3, names3, images3, ct: destroyCancellationToken);

        target.text = "";
        window.gameObject.SetActive(false);

        await Flash();

        await UniTask.Delay(TimeSpan.FromSeconds(3.0f));

        girl.transform.position = new Vector3(0, 0, 0);
        light2D.intensity = 1.0f;
        soundManager.StopSe(flashSe);
        window.gameObject.SetActive(true);
        await MessageManager.message_instance.MessageWindowOnceActive(messages4, names4, images4, ct: destroyCancellationToken);

        target.text = "";
        window.gameObject.SetActive(false);
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        await Blackout2();
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
        soundManager.StopBgm(suspiciousBgm);
        cameraManager.cameraInstance.playerCamera = true;
        cameraAnimator.enabled = true;
        light2D.intensity = 1.0f;
        gameMenuUI.SetActive(false);
        timelineCanvas.gameObject.SetActive(true);
        playableDirector.Play();
        await UniTask.Delay(TimeSpan.FromSeconds(68f));
        await Blackout2();
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
        cameraAnimator.enabled = false;
        timelineCanvas.gameObject.SetActive(false);
        colorAdjustments.active = true;
        FlagsManager.flag_Instance.flagBools[1] = true;
        FlagsManager.flag_Instance.navigationPanel.gameObject.SetActive(true);
        FlagsManager.flag_Instance.ChangeUIDestnation(4, "Yukito");
        FlagsManager.flag_Instance.locationText.text = "1F廊下";
        SaveSlotsManager.save_Instance.saveState.chapterNum++;
        await MessageManager.message_instance.MessageSelectWindowActive(gameSceneController.saveMessages, gameSceneController.saveNames, gameSceneController.saveImages, gameSceneController.Selectwindow, gameSceneController.saveConfilmPanel, gameSceneController.firstSelect, ct: destroyCancellationToken);
        await UniTask.WaitUntil(() => !GameManager.m_instance.saveCanvas.gameObject.activeSelf);
        light2D.intensity = 1.0f;
        gameMenuUI.SetActive(true);

        window.gameObject.SetActive(true);
        await MessageManager.message_instance.MessageWindowOnceActive(messages5, names5, images5, ct: destroyCancellationToken);

        target.text = "";
        GameManager.m_instance.ImageErase(characterImage);
        window.gameObject.SetActive(false);
        GameManager.m_instance.stopSwitch = false;
        GameManager.m_instance.notSaveSwitch = false;
        eventFinished = true;
    }
    private async UniTask Flash()
    {
        light2D = gameObject.GetComponent<Light2D>();
        light2D.intensity = 1.0f;
        soundManager.PlaySe(flashSe);
        while(light2D.intensity < 7.0f)
        {
            light2D.intensity += 0.1f;
            await UniTask.Delay(1);
        }
    }
    private void Event2Camera()
    {
        playerCamera = cameraManager.cameraInstance.playerCamera;
        cameraManager.cameraInstance.playerCamera = false;
        player.transform.position = new Vector3(70, -45, 0);
        friends[0].transform.position = new Vector3(0, 0, 0);
        friends[1].transform.position = new Vector3(0, 0, 0);
        friends[2].transform.position = new Vector3(0, 0, 0);
        friends[3].transform.position = new Vector3(0, 0, 0);
        girl.transform.position = new Vector3(-80, 6, 0);
    }
    protected void showMessage(string message, string name , Sprite image)
    {
        target.text = message;
        nameText.text = name;
        characterImage.sprite = image;
    }

    IEnumerator OnAction6()
    {
        window.gameObject.SetActive(true);
        for (int i = 0; i < beforeMessages.Count; ++i)
        {
            yield return null;
            showMessage(beforeMessages[i], beforeNames[i], beforeImages[i]);
            yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
        }
        window.gameObject.SetActive(false);
        yield break;
    }
    private async UniTask Blackout()
    {
        light2D.intensity = 1.0f;
        GameManager.m_instance.stopSwitch = true;
        FlagsManager.flag_Instance.navigationPanel.gameObject.SetActive(false);
        while (light2D.intensity > 0.01f)
        {
            light2D.intensity -= 0.012f;
            await UniTask.Delay(1);
        }
        friends[0].transform.position = new Vector3(-82, 19, 0);
        friends[1].transform.position = new Vector3(-81, 21, 0);
        friends[2].transform.position = new Vector3(-79, 21, 0);
        friends[3].transform.position = new Vector3(-78, 20, 0);
    }
    private async UniTask Blackout2()
    {
        light2D.intensity = 1.0f;
        GameManager.m_instance.stopSwitch = true;
        while (light2D.intensity > 0.01f)
        {
            light2D.intensity -= 0.012f;
            await UniTask.Delay(1);
        }
    }
    private async UniTask LightOut()
    {
        while(light2D.intensity < 1f)
        {
            light2D.intensity += 0.008f;
            await UniTask.Delay(1);
        }
    }
}

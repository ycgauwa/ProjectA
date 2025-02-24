using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using System;
using UnityEngine.UIElements;

public class Meat : MonoBehaviour
{
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
    private List<Sprite> images2;
    public GameObject player;
    public GameObject lightAnimation;
    public GameObject haruSelectionObject;
    public bool meatEatAfter;
    public Light2D light2D;
    public Homing2 ajure;
    public NotEnter10 notEnter10;
    public SoundManager soundManager;
    public AudioClip meatSound;
    private bool isContacted = false;
    //犬に追われている最中に出てくる。これに話しかけるとイベントが開始。画面が暗転して
    //プレイヤーと犬の位置が移動してメッセージが出る

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
            GameManager.m_instance.stopSwitch = true;
            ajure.acceleration = 0;
            ajure.speed = 0;
            ajure.enemyEmerge = false;
            MeatEated().Forget();
        }
    }
    private async UniTask MeatEated()
    {
        SecondHouseManager.secondHouse_instance.messagedBear.SetActive(false);
        SecondHouseManager.secondHouse_instance.messagedMushroom.SetActive(false);
        await MessageManager.message_instance.MessageWindowActive(messages, names, image, ct: destroyCancellationToken);
        await Blackout();
        soundManager.PlaySe(meatSound);
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
        notEnter10.soundManager.StopBgm(SecondHouseManager.secondHouse_instance.fearMusic);
        light2D.intensity = 1.0f;
        await MessageManager.message_instance.MessageWindowActive(messages2, names2, images2, ct: destroyCancellationToken);
        GameManager.m_instance.stopSwitch = false;
        GameManager.m_instance.notSaveSwitch = false;
        soundManager.StopSe(meatSound);
        lightAnimation.gameObject.SetActive(true);
        haruSelectionObject.tag = "Minnka2-4";
        meatEatAfter = true;
        FlagsManager.flag_Instance.flagBools[7] = true;
        FalseComponent();
    }
    public void FalseComponent()
    {
        Meat meat = GetComponent<Meat>();
        meat.enabled = false;
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
        player.transform.position = new Vector3(142, 92, 0);
        SecondHouseManager.secondHouse_instance.ajure.transform.position = new Vector3(138, 88, 0);

    }
}

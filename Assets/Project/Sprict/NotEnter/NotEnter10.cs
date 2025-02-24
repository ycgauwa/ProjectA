using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class NotEnter10 : MonoBehaviour
{
    public SecondHouseManager secondHouseManager;
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
    private int i = 0;
    public bool advanceEnter;
    public GameObject cameraObject;
    public GameObject sleptAjure;
    public Homing2 ajure;
    public SoundManager soundManager;
    public AudioClip clip;
    public BoxCollider2D box;
    //特定のオブジェクトに話しかけ終わったら進めるようになる仕組み
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
        {
            if(secondHouseManager.CalenderInteriors.talked == true && !SecondHouseManager.secondHouse_instance.meat.meatEatAfter)
            {
                gameObject.SetActive(true);
            }
            for(i = 0; i < 4; i++)
            {
                if(i == 3)
                {
                    //鍵が開く処理
                    box = GetComponent<BoxCollider2D>();
                    box.enabled = false;
                    advanceEnter = true;
                    FlagsManager.flag_Instance.flagBools[6] = true;
                    break;
                }
                if(secondHouseManager.interiors[i].talked == true)
                    continue;
                break;
            }
            if(i < 3)
                MessageManager.message_instance.MessageWindowActive(messages, names, images, ct: destroyCancellationToken).Forget();


            if(secondHouseManager.CalenderInteriors.talked == true)
            {
                cameraManager.cameraInstance.playerCamera = false;
                GameManager.m_instance.stopSwitch = true;
                GameManager.m_instance.notSaveSwitch = true;
                sleptAjure.SetActive(false);
                ajure.gameObject.SetActive(true);
                cameraObject.transform.DOLocalMove(new Vector3(78, 149, -10), 2f);
                Invoke("MessageActive", 2f);
            }
        }
    }
    public void AttachComponent()
    {
        box = GetComponent<BoxCollider2D>();
        box.enabled = false;
    }
    public async void MessageActive()
    {
        if(!GameTeleportManager.chasedTime)
            GameTeleportManager.chasedTime = true;
        await MessageManager.message_instance.MessageWindowActive(messages2, names2, images2, ct: destroyCancellationToken);
        soundManager.PlaySe(clip);
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
        await MessageManager.message_instance.MessageWindowActive(messages3, names3, images3, ct: destroyCancellationToken);
        DemoFinished().Forget();
        
        /*soundManager.PlayBgm(SecondHouseManager.secondHouse_instance.fearMusic);        
        GameManager.m_instance.stopSwitch = false;
        secondHouseManager.messagedBear.SetActive(true);
        secondHouseManager.messagedMushroom.SetActive(true);
        ajure.enemyEmerge = true;
        cameraManager.cameraInstance.playerCamera = true;
        SecondHouseManager.secondHouse_instance.meat.gameObject.SetActive(true);
        gameObject.SetActive(false);*/
    }
    public async UniTask DemoFinished()
    {
        await DemoFinish.instance.Blackout();
        await MessageManager.message_instance.MessageWindowActive(DemoFinish.instance.finishMessages, DemoFinish.instance.finishNames, DemoFinish.instance.finishImage, ct: destroyCancellationToken);
        DemoFinish.instance.DemoFinishCanvas.gameObject.SetActive(true);
    }
}

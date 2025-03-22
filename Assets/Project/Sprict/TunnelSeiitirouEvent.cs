using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TunnelSeiitirouEvent : MonoBehaviour
{
    //征一郎が坑道にて一人語りをするイベント
    //セリフを出して、カメラを大岩に移してから
    //カメラを戻してセリフを出して暗転して進める。
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

    public bool tunnelEvent;
    public GameObject cameraObject;
    public GameObject skullObject;
    public Light2D light2D;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Seiitirou") && !tunnelEvent)
        {
            TunnelEvent().Forget();
            tunnelEvent = true;
            FlagsManager.flag_Instance.navigationPanel.gameObject.SetActive(false);
            FlagsManager.flag_Instance.seiitirouFlagBools[3] = true;
        }
        else if(collider.gameObject.tag.Equals("Player")) gameObject.SetActive(false);
    }
    private async UniTask TunnelEvent()
    {
        Debug.Log("EventStart");
        GameManager.m_instance.stopSwitch = true;
        GameManager.m_instance.notSaveSwitch = true;
        await MessageManager.message_instance.MessageWindowActive(messages, names, images, ct: destroyCancellationToken);
        cameraManager.cameraInstance.seiitirouCamera = false;
        cameraObject.transform.DOLocalMove(new Vector3(190, 11, -10), 4f);
        await UniTask.Delay(TimeSpan.FromSeconds(4f));
        await MessageManager.message_instance.MessageWindowActive(messages2, names2, images2, ct: destroyCancellationToken);
        await Blackout();
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
        FlagsManager.flag_Instance.navigationPanel.gameObject.SetActive(true);
        FlagsManager.flag_Instance.ChangeUIDestnation(9, "Seiitirou");
        cameraManager.cameraInstance.seiitirouCamera = true;
        //オブジェクトの総入れ替え
        SecondHouseManager.secondHouse_instance.chickenDish.gameObject.SetActive(false);
        SecondHouseManager.secondHouse_instance.shrimpDish.gameObject.SetActive(false);
        SecondHouseManager.secondHouse_instance.fishDish.gameObject.SetActive(false);
        SecondHouseManager.secondHouse_instance.bear.gameObject.SetActive(false);
        SecondHouseManager.secondHouse_instance.chicken.gameObject.SetActive(false);
        SecondHouseManager.secondHouse_instance.mushroom.gameObject.SetActive(false);
        SecondHouseManager.secondHouse_instance.toEvent5.gameObject.SetActive(false);
        SecondHouseManager.secondHouse_instance.weightObject.SetActive(true);
        SecondHouseManager.secondHouse_instance.weightSwitch.SetActive(true);
        SecondHouseManager.secondHouse_instance.talkingWithHaru.gameObject.transform.position = new Vector3(141,144,0);
        skullObject.SetActive(true);
        GameManager.m_instance.stopSwitch = false;
        GameManager.m_instance.notSaveSwitch = false;
        light2D.intensity = 1.0f;
        gameObject.SetActive(false);
    }
    private async UniTask Blackout()
    {
        light2D.intensity = 1.0f;
        while(light2D.intensity > 0.01f)
        {
            light2D.intensity -= 0.012f;
            await UniTask.Delay(1);
        }
    }
}

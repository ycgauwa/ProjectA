using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ToEvent4 : MonoBehaviour
{
    // 部屋に入った時にイベントあるから敵が出てこないようにして、これでイベントが終わったら
    // 次から敵が出てきてもいいようにする。調べた時には音楽を止めて選択肢を出して別の音楽を出す。

    public GameObject player;
    public bool event4flag;
    public ToEvent3 toevent3;

    // メッセージウィンドウ用の変数
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

    public AudioClip eventBGMClip;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(!event4flag) //フラグが立ってないとき
        {
            if(collider.gameObject.tag.Equals("Player"))
            {
                // ここもうちょい凝っても良い。カメラ近づけて怪しげなBGMを流してもよい
                strangerEncounterEvent().Forget();
            }
        }
        else return;
    }
    private async UniTask strangerEncounterEvent()
    {
        GameManager.m_instance.stopSwitch = true;
        cameraManager.cameraInstance.playerCamera = false;
        while(cameraManager.cameraInstance.cameraSize > 2.5)
        {
            cameraManager.cameraInstance.cameraSize -= 0.025f;
            await UniTask.Delay(1);
        }
        SoundManager.sound_Instance.PlayBgm(eventBGMClip);
        GameManager.m_instance.mainCamera.transform.DOLocalMove(new Vector3(28.6f, 32.3f, -10),1f);
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        GameManager.m_instance.mainCamera.transform.DOLocalMove(new Vector3(28.6f, 34.5f, -10), 1.5f);
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
        await MessageManager.message_instance.MessageWindowActive(messages, names, images, ct: destroyCancellationToken);
        FlagsManager.flag_Instance.flagBools[3] = true;
        event4flag = true;
        await Blackout();
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
        cameraManager.cameraInstance.playerCamera = true;
        SecondHouseManager.secondHouse_instance.light2D.intensity = 1.0f;
        GameManager.m_instance.stopSwitch = false;
        SoundManager.sound_Instance.StopBgm(eventBGMClip);
    }
}

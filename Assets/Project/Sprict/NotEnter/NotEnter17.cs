using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class NotEnter17 : MonoBehaviour
{
    /*3軒目の居間のスクリプト
    アイテムを持っていない状態で初めて入ったらイベント
    2回目以降は近づかないようにしようって出るだけ。
    アイテムを持っている状態なら普通に枯らすイベントがスタートする。
    イベントが終わったらウィンドウで詳細説明*/
    private bool isTouch;
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
    private List<string> defeatMgs;
    [SerializeField]
    private List<string> defeatNam;
    [SerializeField]
    private List<Sprite> defeatImg;
    [SerializeField]
    private List<string> defeatMgs2;
    [SerializeField]
    private List<string> defeatNam2;
    [SerializeField]
    private List<Sprite> defeatImg2;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag.Equals("Player"))
        {
            if (ItemDateBase.itemDate_instance.GetItemId(29).geted)//gas瓶持ってる時のイベント
            {
                DefeatEnemy().Forget();
            }
            else if (isTouch == false)//ガス入り瓶を持たず初めて触るとき。
            {
                FirstEncounter().Forget();
            }
            else if (isTouch == true)//ガス入り瓶を持たず再び触るとき。
            {
                MessageManager.message_instance.MessageWindowActive(messages3, names3, images3, ct: destroyCancellationToken).Forget();
            }
        }
    }
    private async UniTask FirstEncounter()
    {
        GameManager.m_instance.notSaveSwitch = true;
        GameManager.m_instance.stopSwitch = true;
        await MessageManager.message_instance.MessageWindowActive(messages, names, images, ct: destroyCancellationToken);
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        cameraManager.cameraInstance.playerCamera = false;//本来は晴カメラ、実験でPlayer用にしてる。
        GameManager.m_instance.mainCamera.transform.DOLocalMove(new Vector3(278.4f, 53.1f, -10), 2.5f);
        await UniTask.Delay(TimeSpan.FromSeconds(2f));
        while (cameraManager.cameraInstance.cameraSize > 2.5f)
        {
            cameraManager.cameraInstance.cameraSize -= 0.02f;
            await UniTask.Delay(3);
        }
        await MessageManager.message_instance.MessageWindowActive(messages2, names2, images2, ct: destroyCancellationToken);
        await SecondHouseManager.secondHouse_instance.Blackout();
        SecondHouseManager.secondHouse_instance.light2D.intensity = 1;
        cameraManager.cameraInstance.cameraSize = 5;
        cameraManager.cameraInstance.playerCamera = true;
        GameManager.m_instance.notSaveSwitch = false;
        GameManager.m_instance.stopSwitch = false;
        isTouch = true;
    }
    private async UniTask DefeatEnemy()
    {
        // メッセージ出してガラスの音出す。（投げるアニメーションが必要かどうかは検討）そのあと化け物の悲鳴出してメッセージで終わり
        GameManager.m_instance.notSaveSwitch = true;
        GameManager.m_instance.stopSwitch = true;
        await MessageManager.message_instance.MessageWindowActive(defeatMgs,defeatNam,defeatImg, ct: destroyCancellationToken);
        SoundManager.sound_Instance.PlaySe(ThirdHouseManager.thirdHouse_instance.glassBreak);
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        await MessageManager.message_instance.MessageWindowActive(defeatMgs2, defeatNam2, defeatImg2, ct: destroyCancellationToken);
        GameManager.m_instance.notSaveSwitch = false;
        GameManager.m_instance.stopSwitch = false;
        gameObject.SetActive(false);
    }
}

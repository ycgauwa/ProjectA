using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TunnelSeiitirouEvent : MonoBehaviour
{
    //����Y���B���ɂĈ�l��������C�x���g
    //�Z���t���o���āA�J��������Ɉڂ��Ă���
    //�J������߂��ăZ���t���o���ĈÓ]���Đi�߂�B
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

    public GameObject cameraObject;
    public GameObject skullObject;
    public Light2D light2D;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Seiitirou"))
        {
            TunnelEvent().Forget();
        }
        else if(collider.gameObject.tag.Equals("Player")) gameObject.SetActive(false);
    }
    private async UniTask TunnelEvent()
    {
        GameManager.m_instance.stopSwitch = true;
        //�Z���t���e�i���̍B����������x�ʂ邾�Ȃ�đz�������Ă��Ȃ������B
        //���X�����Ƃ͂�΂��z�������̂ɓ����o������ł�����ȉ����������邾�Ȃ�āc�c�B
        //���S�����߂Ĉړ��𑱂��Ă��邪�A��͂�ʂ̏o���ɂ�������󂷂����Ȃ��������ȁB�j
        await MessageManager.message_instance.MessageWindowActive(messages, names, images, ct: destroyCancellationToken);
        cameraManager.seiitirouCamera = false;
        cameraObject.transform.DOLocalMove(new Vector3(190, 11, -10), 4f);
        await UniTask.Delay(TimeSpan.FromSeconds(4f));
        await MessageManager.message_instance.MessageWindowActive(messages2, names2, images2, ct: destroyCancellationToken);
        await Blackout();
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
        cameraManager.seiitirouCamera = true;
        //�I�u�W�F�N�g�̑�����ւ�
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

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
    // �����ɓ��������ɃC�x���g���邩��G���o�Ă��Ȃ��悤�ɂ��āA����ŃC�x���g���I�������
    // ������G���o�Ă��Ă������悤�ɂ���B���ׂ����ɂ͉��y���~�߂đI�������o���ĕʂ̉��y���o���B

    public GameObject player;
    public bool event4flag;
    public ToEvent3 toevent3;

    // ���b�Z�[�W�E�B���h�E�p�̕ϐ�
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
        if(!event4flag) //�t���O�������ĂȂ��Ƃ�
        {
            if(collider.gameObject.tag.Equals("Player"))
            {
                // �����������傢�Â��Ă��ǂ��B�J�����߂Â��ĉ�������BGM�𗬂��Ă��悢
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

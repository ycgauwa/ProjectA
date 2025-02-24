using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SeiitirouMeetToHaru : MonoBehaviour
{
    //���̃X�N���v�g�͐���Y��ԂŐ��ɉ���Ƃ��ăh�A�������ɉ����������ĊJ���Ȃ��B���̂��߂ɉ�������K�v����
    //�l���鐪��Y�B�ړI�̃A�C�e������肵����C�x���g�̓X�^�[�g����
    //�A�C�e������肵�����ƃ��b�Z�[�W������A�h�A���J���鉹������B���̂��ƂɈÓ]���ĕ����Ɉړ����Ƃ̉�b�C�x���g���n�܂�B
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> messages2;
    [SerializeField]
    private List<string> messages3;
    [SerializeField]
    private List<string> messages4;
    [SerializeField]
    private List<string> messages5;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<string> names2;
    [SerializeField]
    private List<string> names3;
    [SerializeField]
    private List<string> names4;
    [SerializeField]
    private List<string> names5;
    [SerializeField]
    private List<Sprite> images;
    [SerializeField]
    private List<Sprite> images2;
    [SerializeField]
    private List<Sprite> images3;
    [SerializeField]
    private List<Sprite> images4;
    [SerializeField]
    private List<Sprite> images5;
    public GameObject haru;
    public Item�@metalBlade;
    public Item key;
    public AudioClip crying;
    public AudioClip doorSound;
    public Light2D light2D;
    private async void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Seiitirou"))
        {
            if(!metalBlade.checkPossession)
            {
                if(!enabled) return;
                gameObject.tag = "Untagged";
                GameManager.m_instance.stopSwitch = true;
                await HaruCryingEvent();
            }
            else if(metalBlade.checkPossession)
            {
                GameManager.m_instance.inventry.Delete(metalBlade);
                await OpendKey();
            }
        }
    }
    private async UniTask HaruCryingEvent()
    {
        await MessageManager.message_instance.MessageWindowActive(messages, names, images, ct: destroyCancellationToken);
        SoundManager.sound_Instance.PlaySe(crying);
        await UniTask.Delay(TimeSpan.FromSeconds(9f));
        //�N�������Ă���H
        await MessageManager.message_instance.MessageWindowActive(messages2, names2, images2, ct: destroyCancellationToken);
        GameManager.m_instance.stopSwitch = false;
        SecondHouseManager.secondHouse_instance.metalBlade.gameObject.SetActive(true);
    }
    private async UniTask OpendKey()
    {
        // �R�����g�u�����J������B�v�̂ŉ������킸���Ȍ��Ԃɋ������������ނƃh�A�������������J�����B
        await MessageManager.message_instance.MessageWindowActive(messages3, names3, images3, ct: destroyCancellationToken);
        SoundManager.sound_Instance.PlaySe(doorSound);
        //GameManager.m_instance.inventry.Delete(metalBlade);
        GameManager.m_instance.stopSwitch = true;
        await UniTask.Delay(TimeSpan.FromSeconds(4f));
        await Blackout();
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
        // ���v���H�d�C���邼�H�̃Z���t��������
        await MessageManager.message_instance.MessageWindowActive(messages4, names4, images4, ct: destroyCancellationToken);
        GameManager.m_instance.seiitirou.gameObject.transform.position = new Vector3(130, 142.2f, 0);
        haru.gameObject.transform.position = new Vector3(141, 144.5f, 0);
        haru.gameObject.transform.DOLocalMove(new Vector3(141, 144, 0), 0.3f);
        light2D.intensity = 1.0f;
        GameManager.m_instance.seiitirou.gameObject.transform.DOLocalMove(new Vector3(141, 142.2f, 0), 5f);
        await UniTask.Delay(TimeSpan.FromSeconds(5f));
        // ���݂��̎����b�������A�q���g��Ⴄ�B
        await MessageManager.message_instance.MessageWindowActive(messages5, names5, images5, ct: destroyCancellationToken);
        await Blackout();
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
        light2D.intensity = 1.0f; 
        GameManager.m_instance.stopSwitch =false;
        gameObject.tag = "Minnka2-15";
        enabled = false;
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
    }
}

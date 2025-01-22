using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class NotEnter15 : MonoBehaviour
{
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> messages2;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<Sprite> images;
    [SerializeField]
    private List<Sprite> images2;
    private bool keyOpened;
    public Canvas secondHouseCanvas;
    public Image buttonGrid;
    public AudioClip keyOpen;
    public AudioClip defuseLocked;
    public AudioClip runSound;

    private async void OnTriggerEnter2D(Collider2D collider)
    {
        if(keyOpened) return;
        else if(collider.gameObject.tag.Equals("Seiitirou"))
        {
            gameObject.tag = "Untagged";
            await MessageManager.message_instance.MessageWindowActive(messages, names, images, ct: destroyCancellationToken);
            GameManager.m_instance.stopSwitch = true;
            secondHouseCanvas.gameObject.SetActive(true);
            buttonGrid.gameObject.SetActive(true);
        }
    }
    public async void DefuseLocked()
    {
        SoundManager.sound_Instance.PlaySe(defuseLocked);
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        SoundManager.sound_Instance.PlaySe(keyOpen);
        buttonGrid.gameObject.SetActive(false);
        secondHouseCanvas.gameObject.SetActive(false);
        await MessageManager.message_instance.MessageWindowActive(messages2, names, images, ct: destroyCancellationToken);
        await Blackout();
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        GameManager.m_instance.seiitirou.gameObject.transform.position = new Vector3(108, 102,0);
        keyOpened = true;
        gameObject.tag = "Minnka2-9";
        SecondHouseManager.secondHouse_instance.light2D.intensity = 1.0f;
        GameManager.m_instance.stopSwitch = false;
    }
    private async UniTask Blackout()
    {
        SecondHouseManager.secondHouse_instance.light2D.intensity = 1.0f;
        GameManager.m_instance.stopSwitch = true;
        SoundManager.sound_Instance.PlaySe(runSound);
        while(SecondHouseManager.secondHouse_instance.light2D.intensity > 0.01f)
        {
            SecondHouseManager.secondHouse_instance.light2D.intensity -= 0.012f;
            await UniTask.Delay(1);
        }
    }
}

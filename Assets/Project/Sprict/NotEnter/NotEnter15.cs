using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    public bool keyOpened;
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
            Debug.Log("非同期処理を開始します");

            if(MessageManager.message_instance == null)
            {
                Debug.LogError("MessageManager.message_instance is null.");
                return;
            }

            if(GameManager.m_instance == null)
            {
                Debug.LogError("GameManager.m_instance is null.");
                return;
            }

            if(secondHouseCanvas == null)
            {
                Debug.LogError("secondHouseCanvas is null.");
                return;
            }

            await MessageManager.message_instance.MessageWindowActive(messages, names, images);

            Debug.Log("awaitが完了しました");
            GameManager.m_instance.stopSwitch = true;
            Debug.Log("GameManager の状態を更新しました");

            secondHouseCanvas.gameObject.SetActive(true);
            buttonGrid.gameObject.SetActive(true);
            Debug.Log("Canvas の状態を更新しました");
            /*gameObject.tag = "Untagged";
            await MessageManager.message_instance.MessageWindowActive(messages, names, images);
            GameManager.m_instance.stopSwitch = true;
            secondHouseCanvas.gameObject.SetActive(true);
            buttonGrid.gameObject.SetActive(true);*/
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
        GameManager.m_instance.seiitirou.gameObject.transform.position = new Vector3(108, 102.8f,0);
        keyOpened = true;
        FlagsManager.flag_Instance.seiitirouFlagBools[4] = true;
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

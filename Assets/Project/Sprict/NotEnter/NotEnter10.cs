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
    public GameObject cameraObject;
    public GameObject sleptAjure;
    public Homing2 ajure;
    public SoundManager soundManager;
    public AudioClip clip;
    //特定のオブジェクトに話しかけ終わったら進めるようになる仕組み
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
        {
            if(secondHouseManager.CalenderInteriors.talked == true)
            {
                gameObject.SetActive(true);
            }
            for(i = 0; i < 4; i++)
            {
                Debug.Log(i);
                if(i == 3)
                {
                    //鍵が開く処理
                    BoxCollider2D box = GetComponent<BoxCollider2D>();
                    box.enabled = false;
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
                cameraManager.playerCamera = false;
                GameManager.m_instance.stopSwitch = true;
                sleptAjure.SetActive(false);
                ajure.gameObject.SetActive(true);
                cameraObject.transform.DOLocalMove(new Vector3(78, 149, -10), 2f);
                Invoke("MessageActive", 2f);
            }
        }
    }
    public async void MessageActive()
    {
        await MessageManager.message_instance.MessageWindowActive(messages2, names2, images2, ct: destroyCancellationToken);
        soundManager.PlaySe(clip);
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
        await MessageManager.message_instance.MessageWindowActive(messages3, names3, images3, ct: destroyCancellationToken);
        soundManager.PlayBgm(SecondHouseManager.secondHouse_instance.fearMusic);
        GameManager.m_instance.stopSwitch = false;
        ajure.enemyEmerge = true;
        cameraManager.playerCamera = true;
        SecondHouseManager.secondHouse_instance.meat.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}

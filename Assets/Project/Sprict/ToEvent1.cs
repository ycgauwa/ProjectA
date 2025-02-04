using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using UnityEditor.Rendering;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System;


public class ToEvent1 : MonoBehaviour
{
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<Sprite> images;
    public Canvas window;
    public Text target;
    public Text nameText;
    public Image Chara;
    public static bool one;
    public GameObject player;
    public GameObject anotherDoor;
    public Light2D light2D;
    // Start is called before the first frame update
    void Start()
    {
        one = false;
    }

    // Update is called once per frame

    private async void OnTriggerEnter2D(Collider2D collider)
    {
        //一回しか作動しないための仕組み
        if(collider.gameObject.tag.Equals("Player"))
            if (!one) await Event1();
    }
    //イベント１のためのコルーチン。大枠の役割を果たしてくれる。
    private async UniTask Event1()
    {
        PlayerManager.m_instance.soundManager.PlaySe(PlayerManager.m_instance.teleportManager.schoolDoor);
        GameManager.m_instance.stopSwitch = true;
        one = true;
        await Blackout();
        SecondHouseManager.secondHouse_instance.haru.transform.DOLocalMove(new Vector3(-35.1f, -34.17f, 0), 0.1f);
        Rigidbody2D rb = SecondHouseManager.secondHouse_instance.haru.GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        light2D.intensity = 1;
        player.transform.position = new Vector3(-33, -34, 0);
        GameManager.m_instance.stopSwitch = false;
        await MessageManager.message_instance.MessageWindowOnceActive(messages, names, images, ct: destroyCancellationToken);
        await Blackout();
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        light2D.intensity = 1;
        if(gameObject.name == "SchoolWarp4")
        {
            gameObject.gameObject.tag = "School8";
            anotherDoor.gameObject.tag = "School7";
        }
        else
        {
            gameObject.gameObject.tag = "School7";
            anotherDoor.gameObject.tag = "School8";
        }
    }

    private IEnumerator Blackout()
    {
        while(light2D.intensity > 0.01f)
        {
            light2D.intensity -= 0.012f;
            yield return null; //ここで１フレーム待ってくれてる
        }
    }
}

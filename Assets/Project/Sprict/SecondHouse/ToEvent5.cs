using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System;

public class ToEvent5 : MonoBehaviour
{
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
    public Canvas window;
    public Text target;
    public Text nameText;
    public Image characterImage;
    public GameObject eventcamera;
    public GameObject haru;
    CapsuleCollider2D capsuleCollider;
    public bool event5Start = false;

    //まず晴は本棚と向き合ってる状態なので後ろ向きの状態で晴が気づく。
    //その状態から晴がこちらに近づいてきて会話が始まる。
    //会話が終わった後に晴は「ずっと僕はここにいるから何かあったら報告に来てね！」
    //イベントが終わるときにはフェードインアウトでいい感じにする
    //イベントが終わった時に晴と話せるのだが、その時に選択肢での質問形式にして話せるようにする。
    //また雑談という項目を作成して展開ごとに雑談の内容が変わるようにしたい。
    //イベントの流れ
    //・晴との邂逅
    //・二人での会話
    //・（ここで晴からアイテムも貰う？事も考え中）
    //・動けるようになる→話しかけられるようになる
    private void Start()
    {
        capsuleCollider = haru.GetComponent<CapsuleCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag.Equals("Player") && event5Start == false)
        {
            capsuleCollider.isTrigger = true;
            haru.transform.position = new Vector2(80,75);
            GameManager.m_instance.stopSwitch = true;
            GameManager.m_instance.notSaveSwitch = true;
            FlagsManager.flag_Instance.navigationPanel.gameObject.SetActive(false);
            EncountHaru().Forget();
        }
    }
    public void ColliderTrigger()
    {
        capsuleCollider = haru.GetComponent<CapsuleCollider2D>();
        capsuleCollider.isTrigger = true;
    }
    private async UniTask EncountHaru()
    {
        await MessageManager.message_instance.MessageWindowActive(messages, names, images, ct: destroyCancellationToken);
        cameraManager.cameraInstance.playerCamera = false;
        GameManager.m_instance.mainCamera.gameObject.transform.DOLocalMove(new Vector3(80, 72, -10), 4f);
        await UniTask.Delay(TimeSpan.FromSeconds(4f));

        await MessageManager.message_instance.MessageWindowActive(messages2, names2, images2, ct: destroyCancellationToken);

        GameManager.m_instance.mainCamera.gameObject.transform.DOLocalMove(new Vector3(80, 69, -10), 4f);
        haru.gameObject.transform.DOLocalMove(new Vector3(80, 69, 0), 4f);
        await UniTask.Delay(TimeSpan.FromSeconds(4f));

        window.gameObject.SetActive(true);
        await OnAction3();
    }
    protected void showMessage(string message, string name, Sprite image)
    {
        target.text = message;
        nameText.text = name;
        characterImage.sprite = image;
    }
    IEnumerator OnAction2()
    {
        for (int i = 0; i < messages2.Count; ++i)
        {
            yield return null;
            showMessage(messages2[i], names2[i], images2[i]);
            yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
        }
        target.text = "";
        window.gameObject.SetActive(false);

        yield break;
    }
    IEnumerator OnAction3()
    {
        for (int i = 0; i < messages3.Count; ++i)
        {
            yield return null;
            showMessage(messages3[i], names3[i], images3[i]);
            yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
        }
        target.text = "";
        window.gameObject.SetActive(false);
        StartCoroutine("Sleep");
        yield break;
    }
    private async UniTask Sleep()
    {
        while(SecondHouseManager.secondHouse_instance.light2D.intensity > 0.01f)
        {
            SecondHouseManager.secondHouse_instance.light2D.intensity -= 0.007f;
            await UniTask.Delay(1);
        }
        SecondHouseManager.secondHouse_instance.light2D.intensity = 1;
        event5Start = true;
        FlagsManager.flag_Instance.flagBools[5] = true;
        haru.transform.position = new Vector2(80, 75);
        cameraManager.cameraInstance.playerCamera = true;
        GameManager.m_instance.stopSwitch = false;
        GameManager.m_instance.notSaveSwitch = false;
        FlagsManager.flag_Instance.navigationPanel.gameObject.SetActive(true);
        FlagsManager.flag_Instance.ChangeUIDestnation(6, "Yukito");
        gameObject.SetActive(false);
    }
}

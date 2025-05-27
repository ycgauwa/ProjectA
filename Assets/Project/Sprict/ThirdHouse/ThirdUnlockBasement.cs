using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class ThirdUnlockBasement : MonoBehaviour
{
    //地下室に行くためのスクリプト。幸人だとそのまま会話で終わるけど晴が話したらフェードが入って地下への階段が現れる。
    //布団が持ってるスクリプトね。

    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<Sprite> image;
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
    private List<string> messages4;
    [SerializeField]
    private List<string> names4;
    [SerializeField]
    private List<Sprite> images4;
    public Canvas selectionCanvas;
    public Image selection;
    public GameObject firstSelect;
    private int isContactedAndChara = 0;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //幸人の時に表示されるメッセージ
        if (collider.gameObject.tag.Equals("Player"))
            isContactedAndChara = 2;
        else if (collider.gameObject.name == "Matiba Haru")
            isContactedAndChara = 2;
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag.Equals("Player"))
            isContactedAndChara = 0;
        else if (collider.gameObject.name == "Matiba Haru")
            isContactedAndChara = 0;
    }

    private void Update()//入力チェックはUpdateに書く
    {
        if (isContactedAndChara > 0 && (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)))
        {
            switch (isContactedAndChara)
            {
                case 1://幸人の場合
                    MessageManager.message_instance.MessageWindowActive(messages, names, image, ct: destroyCancellationToken).Forget();
                    return;
                case 2://晴の場合
                    CheckFuton().Forget();
                    isContactedAndChara = 0;
                    return;
                default:
                    return;
            }
        }
    }
    //このメソッドでは晴が布団を調べて血に焦点を向けながらカメラを動かして↑ここで選択肢！布団を動かすか？動かさないか？（同時に布団も動かす）、さらにメッセージを表示させながら選択肢を出すorそのまま
    //フェードアウトして地下室に降りていくかにする。
    private async UniTask CheckFuton()
    {
        GameManager.m_instance.notSaveSwitch = true;
        GameManager.m_instance.stopSwitch = true;
        await MessageManager.message_instance.MessageWindowActive(messages2, names2, images2, ct: destroyCancellationToken);//調べて驚く
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        cameraManager.cameraInstance.playerCamera = false;//本来は晴カメラ、実験でPlayer用にしてる。
        GameManager.m_instance.mainCamera.transform.DOLocalMove(new Vector3(252.5f, 34.68f, -10), 2f);
        while (cameraManager.cameraInstance.cameraSize > 2)
        {
            cameraManager.cameraInstance.cameraSize -= 0.02f;
            await UniTask.Delay(2);
        }
        await MessageManager.message_instance.MessageSelectWindowActive(messages3, names3, images3, selectionCanvas, selection, firstSelect, ct: destroyCancellationToken);//ここで選択肢が来る。
        cameraManager.cameraInstance.cameraSize = 5;
        cameraManager.cameraInstance.playerCamera = true;
    }
    private void DownBasementMethod()//階段を降りる選択のメソッド
    {
        DescendToBasement().Forget();
    }
    private void StayWasituMethod()//階段を降りないメソッド
    {
        MessageManager.message_instance.MessageWindowActive(messages3, names3, images3,ct: destroyCancellationToken).Forget();
        GameManager.m_instance.stopSwitch = false;
        GameManager.m_instance.notSaveSwitch = false;
    }
    private async UniTask DescendToBasement()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
    }
    private async UniTask Blackout()
    {
        SecondHouseManager.secondHouse_instance.light2D.intensity = 1.0f;
        while (SecondHouseManager.secondHouse_instance.light2D.intensity > 0.01f)
        {
            SecondHouseManager.secondHouse_instance.light2D.intensity -= 0.012f;
            await UniTask.Delay(1);
        }
    }
}

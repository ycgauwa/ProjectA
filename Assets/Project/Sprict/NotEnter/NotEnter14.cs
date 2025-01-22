using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class NotEnter14 : MonoBehaviour
{
    /*大岩で通れないスクリプト
    そのままと爆弾を持っている状態での処理は異なる。
    そのままはメッセージが表示されるだけ持っていたら。
    イベント発生
    イベントはメッセージが発生。その後画面暗転からの2つの音で出してからメッセージ出す。
    アイテムは消費。
    ここで踏むとデモ版clear！！！！
    */
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<Sprite> image;
    [SerializeField]
    private List<Sprite> image2;
    [SerializeField]
    private List<Sprite> image3;
    public Item bomb;
    public Inventry inventry;
    public GameObject bigStone;

    public SoundManager soundManager;
    public AudioClip bombTimer;
    public AudioClip bombSound;
    private int isContactedAndChara = 0;
    private int savedNam;
    public Light2D light2D;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        //幸人の時に表示されるメッセージ
        if(collider.gameObject.tag.Equals("Player"))
            isContactedAndChara = 1;
        else if(collider.gameObject.name == "Matiba Haru")
            isContactedAndChara = 2;
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
            isContactedAndChara = 0;
        else if(collider.gameObject.name == "Matiba Haru")
            isContactedAndChara = 0;
    }
    private void Update()//入力チェックはUpdateに書く
    {
        if(isContactedAndChara > 0 && (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)))
        {
            savedNam = isContactedAndChara;
            isContactedAndChara = 0;
            if(!bomb.checkPossession)
                NotUseBomb();
            else
            {
                UseBomb().Forget();
            }
        }
    }

    private void NotUseBomb()
    {
        //　爆弾なしだからmessegeだけ。主人公だけ。
        MessageManager.message_instance.MessageWindowActive(messages, names, image, ct: destroyCancellationToken).Forget();
    }
    private async UniTask UseBomb()
    {
        //爆弾ありだからイベントあり
        if(savedNam == 1)
            await MessageManager.message_instance.MessageWindowActive(messages, names, image2, ct: destroyCancellationToken);
        else if(savedNam == 2)
            await MessageManager.message_instance.MessageWindowActive(messages, names, image3, ct: destroyCancellationToken);

        inventry.Delete(bomb);
        Blackout().Forget();
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
        soundManager.PlaySe(bombTimer);
        await UniTask.Delay(TimeSpan.FromSeconds(4.5f));
        soundManager.PlaySe(bombSound);
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));

        bigStone.transform.position = new Vector3(0f, 0f, 0f);
        light2D.intensity = 1.0f;

        if(savedNam == 1)
            await MessageManager.message_instance.MessageWindowActive(messages, names, image2, ct: destroyCancellationToken);
        else if(savedNam == 2)
            await MessageManager.message_instance.MessageWindowActive(messages, names, image3, ct: destroyCancellationToken);
        
        GameManager.m_instance.stopSwitch = false;
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

using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemyEncounter : MonoBehaviour
{
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
    private List<Sprite> image2;
    [SerializeField]
    private List<string> messages3;
    [SerializeField]
    private List<string> names3;
    [SerializeField]
    private List<Sprite> image3;
    [SerializeField]
    private List<string> messages4;
    [SerializeField]
    private List<string> names4;
    [SerializeField]
    private List<Sprite> image4;
    public Item bomb;
    public bool afterEncounted;

    public GameObject cameraObject;
    public GameObject player;
    public GameObject haru;
    public GameObject woodStair;
    public BombDefuse bombDefuse;
    public Homing2 ajure;
    public Light2D light2D;

    public SoundManager soundManager;
    public AudioClip heartSound;
    public AudioClip stepSound;
    public AudioClip clip;
    //”š’e‚ð‰ðœ‚µ‚ÄƒAƒCƒeƒ€‚Æ‚µ‚Ä“üŽè‚µ‚½Œã“Á’è‚ÌƒGƒŠƒA‚ð“¥‚Þ‚Æ°‚Æ‡—¬‚·‚é
    //‚»‚Ì‚Ü‚ÜƒAƒjƒ[ƒVƒ‡ƒ“‚Åˆê‚Éi‚Þ(ˆê‰ñ–Ú°A‚Q‰ñ–Ú°‰EA‚R‰ñ–ÚKlA‚S‰ñ“r’†‚©‚ç°)TP‚µ‚½‚Æ‚«‚ÉƒJƒƒ‰‚ª—¬‚ê‚Ä“G‚ªP‚Á‚Ä—ˆ‚é
    //‚»‚ÌÛ‚É°‚Íˆê‚És“®‚·‚é‚æI‚ÆŒ¾‚Á‚ÄÁ‚¦‚éB

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player") && !afterEncounted)
        {
            GameManager.m_instance.teleportManager.enemyTeleportTime += 2;
            GameManager.m_instance.stopSwitch = true;
            GameManager.m_instance.notSaveSwitch = true;
            afterEncounted = true;
            EnemyEncount().Forget();
            haru.transform.position = new Vector2(117, 142);
        }

    }
    private async UniTask EnemyEncount()
    {
        // ‚ñH‰½‚©—ˆ‚éIƒRƒƒ“ƒg
        await MessageManager.message_instance.MessageWindowActive(messages, names, image, ct: destroyCancellationToken);
        cameraManager.cameraInstance.playerCamera = false;
        soundManager.PlayBgm(heartSound);
        cameraObject.transform.DOLocalMove(new Vector3(106, 136, -10), 3f);
        await UniTask.Delay(TimeSpan.FromSeconds(3f));
        haru.transform.position = new Vector3(106, 133, 0);

        soundManager.StopBgm(heartSound);
        await MessageManager.message_instance.MessageWindowActive(messages2, names2, image2, ct: destroyCancellationToken);
        haru.transform.DOLocalMove(new Vector3(106, 136.44f, 0), 3f);

        await UniTask.Delay(TimeSpan.FromSeconds(3f));
        await MessageManager.message_instance.MessageWindowActive(messages3, names3, image3, ct: destroyCancellationToken);
        haru.transform.DOLocalMove(new Vector3(107, 136.44f, 0), 0.5f);

        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
        player.transform.DOLocalMove(new Vector3(106, 139, 0),1f);
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        player.transform.DOLocalMove(new Vector3(106, 134, 0), 3f);

        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
        haru.transform.DOLocalMove(new Vector3(107, 134, 0),1.8f);
        await Blackout();
        await UniTask.Delay(TimeSpan.FromSeconds(1.6f));
        cameraManager.cameraInstance.playerCamera = true;

        light2D.intensity = 1.0f;
        player.transform.position = new Vector3(113, 72, 0);

        haru.transform.position = new Vector3(114, 72, 0);
        player.transform.DOLocalMove(new Vector3(113, 69, 0), 1f);
        haru.transform.DOLocalMove(new Vector3(114, 69, 0), 1f);

        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        player.transform.DOLocalMove(new Vector3(106, 69, 0), 2.5f);
        haru.transform.DOLocalMove(new Vector3(107, 69, 0), 2.5f);

        await UniTask.Delay(TimeSpan.FromSeconds(2.5f));
        cameraManager.cameraInstance.playerCamera = false;
        ajure.gameObject.transform.position = new Vector3(106, 82, 0);
        cameraObject.transform.DOLocalMove(new Vector3(106, 81, -10), 3f);
        await UniTask.Delay(TimeSpan.FromSeconds(3f));

        //‚±‚±’Ç‰Á‚Å‰½•b‚©‘Ò‚Â+ƒZƒŠƒt‚Æ°‚ÌŽpÁ‚·B
        soundManager.PlaySe(clip);
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        await MessageManager.message_instance.MessageWindowActive(messages4, names4, image4, ct: destroyCancellationToken);
        Blackout().Forget();
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
        haru.gameObject.transform.position = new Vector2(0,0);
        soundManager.PlayBgm(SecondHouseManager.secondHouse_instance.fearMusic);
        GameManager.m_instance.stopSwitch = false;
        player.transform.position = new Vector3(108, 68, 0);
        ajure.speed = 5f;
        ajure.gameObject.transform.position = new Vector3(103, 80, 0);
        light2D.intensity = 1.0f;
        ajure.enemyEmerge = true;
        cameraManager.cameraInstance.playerCamera = true;
        woodStair.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
    //°‚ª1ŠK‚ÉˆÚ“®‚·‚éŽž‚ÉuŠÔˆÚ“®‚·‚é
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

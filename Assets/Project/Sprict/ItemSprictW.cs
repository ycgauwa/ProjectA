using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSprictW : MonoBehaviour
{
    //藁人形のスクリプト
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
    public ItemDateBase itemDateBase;
    public GameObject enemy;
    public GameTeleportManager gameTeleportManager;
    public AudioClip bellSound;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // 敵と接触した時に起こすスプリクト
    public void ItemEffect()
    {
        //サウンドと演出を流していっかいだけするーする
        itemDateBase.Items9Delete();
        audioSource.PlayOneShot(bellSound);
        MessageManager.message_instance.MessageWindowActive(messages, names, images);
        gameTeleportManager.StopChased();
        Invoke("Stop", 3f);
    }
    public void ItemDelete()
    {
        itemDateBase.Items9Delete();
        audioSource.PlayOneShot(bellSound);
        MessageManager.message_instance.MessageWindowActive(messages2, names2, images2);
    }
    public void Stop()
    {
        gameTeleportManager.StopChased();
    }
}

using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class ItemSprictW : MonoBehaviour
{
    //�m�l�`�̃X�N���v�g
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
    // �G�ƐڐG�������ɋN�����X�v���N�g
    public void ItemEffect()
    {
        //�T�E���h�Ɖ��o�𗬂��Ă���������������[����
        Inventry.instance.Delete(itemDateBase.GetItemId(301));
        audioSource.PlayOneShot(bellSound);
        MessageManager.message_instance.MessageWindowActive(messages, names, images, ct: destroyCancellationToken).Forget();
        gameTeleportManager.StopChased();
        Invoke("Stop", 3f);
    }
    public void ItemDelete()
    {
        Inventry.instance.Delete(itemDateBase.GetItemId(301));
        audioSource.PlayOneShot(bellSound);
        MessageManager.message_instance.MessageWindowActive(messages2, names2, images2, ct: destroyCancellationToken).Forget();
    }
    public void Stop()
    {
        gameTeleportManager.StopChased();
    }
}

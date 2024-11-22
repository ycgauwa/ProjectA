using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class DemoScene : MonoBehaviour
{
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<Sprite> images;
    public GameObject cameraObject;
    public SoundManager soundManager;
    public AudioClip clip;

    private void Start()
    {

    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
        {
            cameraManager.playerCamera = false;
            GameManager.m_instance.stopSwitch = true;
            cameraObject.transform.DOLocalMove(new Vector3(78, 149, -10), 2f);
            Invoke("MessageActive", 2f);
        }
    }
    public async void MessageActive()
    {
        await MessageManager.message_instance.MessageWindowActive(messages, names, images, ct: destroyCancellationToken);
        soundManager.PlaySe(clip);
    }
}

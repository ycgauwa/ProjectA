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
    public static bool one = false;
    public GameObject player;
    public GameObject anotherDoor;
    public Light2D light2D;
    private async void OnTriggerEnter2D(Collider2D collider)
    {
        //��񂵂��쓮���Ȃ����߂̎d�g��
        if(gameObject.tag == "Untagged")
        {
            if(collider.gameObject.tag.Equals("Player"))
                if(!one) await Event1();
            if(one)
            {
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
        }
        else
        {
            return;
        }
    }
    //�C�x���g�P�̂��߂̃R���[�`���B��g�̖������ʂ����Ă����B
    private async UniTask Event1()
    {
        PlayerManager.m_instance.soundManager.PlaySe(PlayerManager.m_instance.teleportManager.schoolDoor);
        GameManager.m_instance.stopSwitch = true;
        GameManager.m_instance.notSaveSwitch = true;
        one = true;
        FlagsManager.flag_Instance.flagBools[0] = true;
        FlagsManager.flag_Instance.navigationPanel.gameObject.SetActive(false);
        await Blackout();
        FlagsManager.flag_Instance.ChangeUILocation("School7");
        SecondHouseManager.secondHouse_instance.haru.transform.DOLocalMove(new Vector3(-35.1f, -34.17f, 0), 0.1f);
        Rigidbody2D rb = SecondHouseManager.secondHouse_instance.haru.GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        light2D.intensity = 1;
        player.transform.position = new Vector3(-33, -34, 0);
        await MessageManager.message_instance.MessageWindowOnceActive(messages, names, images, ct: destroyCancellationToken);
        await Blackout();
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        GameManager.m_instance.stopSwitch = false;
        GameManager.m_instance.notSaveSwitch = false;
        light2D.intensity = 1;
        FlagsManager.flag_Instance.navigationPanel.gameObject.SetActive(true);
        FlagsManager.flag_Instance.ChangeUIDestnation(2, "Yukito");
        if (gameObject.name == "SchoolWarp4")
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
            yield return null; //�����łP�t���[���҂��Ă���Ă�
        }
    }
}

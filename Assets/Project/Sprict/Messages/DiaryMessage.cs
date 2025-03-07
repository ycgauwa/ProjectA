using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

//using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class DiaryMessage : MonoBehaviour
{
    //やりたいこと
    //調べてボタン押したらメッセージが表示される。そのあとに日記が表示されてからメッセージが出る。
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<Sprite> image;
    [SerializeField]
    private List<string> seimessages;
    [SerializeField]
    private List<string> seinames;
    [SerializeField]
    private List<Sprite> seiimages;
    [SerializeField]
    private List<string> sentences;
    [SerializeField]
    private List<string> dates;
    [SerializeField]
    private List<string> seisentences;
    [SerializeField]
    private List<string> seidates;

    public Canvas diaryWindow;
    public Canvas window;
    public Text target;
    public Text nameText;
    public Text sentence;
    public Text date;
    public Image characterImage;
    public Image diary;
    public Image panel;
    private IEnumerator coroutine;
    private bool isContacted = false;
    private bool seiContacted = false;
    public SoundManager soundManager;
    public AudioClip pageSound;
    public AudioClip pageTojiSound;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
            isContacted = true;
        else if(collider.gameObject.tag.Equals("Seiitirou"))
            seiContacted = true;
    }

    // colliderをもつオブジェクトの領域外にでたとき(下記で説明1)
    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
            isContacted = false;
        else if(collider.gameObject.tag.Equals("Seiitirou"))
            seiContacted = false;
    }
    private void Update()
    {
        if(isContacted && diaryWindow.gameObject.activeInHierarchy == false)
        {
            if(Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return))
            {
                WindowAction().Forget();
                isContacted = false;
            }
        }
        else if(seiContacted && seiimages.Count > 0 && diaryWindow.gameObject.activeInHierarchy == false)
        {
            if(Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return))
            {
                SeiitirouWindowAction().Forget();
                seiContacted = false;
            }
        }
    }
    protected void showDiaryMessage(string message, string name)
    {
        sentence.text = message;
        date.text = name;
    }
    private async UniTask WindowAction()
    {
        await MessageManager.message_instance.MessageWindowActive(messages, names, image, ct: destroyCancellationToken);
        await UniTask.WaitUntil(() => !MessageManager.message_instance.talking);
        //何回か押すとテキストが消えて、日記の表示がされる。
        diaryWindow.gameObject.SetActive(true);
        
        for(int i = 0; i < sentences.Count; ++i)
        {
            await UniTask.Delay(1);
            showDiaryMessage(sentences[i], dates[i]);
            soundManager.PlaySe(pageSound);
            await UniTask.WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
            if (i == sentences.Count - 1)
            {
                soundManager.PlaySe(pageTojiSound);
            }

        }
        sentence.text = "";
        date.text = "";
        Homing.m_instance.speed = 2 + GameManager.m_instance.difficultyLevelManager.addEnemySpeed;
        diaryWindow.gameObject.SetActive(false);
    }
    private async UniTask SeiitirouWindowAction()
    {
        await MessageManager.message_instance.MessageWindowActive(seimessages, seinames, seiimages, ct: destroyCancellationToken);
        await UniTask.WaitUntil(() => !MessageManager.message_instance.talking);
        //何回か押すとテキストが消えて、日記の表示がされる。
        diaryWindow.gameObject.SetActive(true);

        for(int i = 0; i < sentences.Count; ++i)
        {
            await UniTask.Delay(1);
            showDiaryMessage(seisentences[i], seidates[i]);
            soundManager.PlaySe(pageSound);
            await UniTask.WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
            if(i == sentences.Count - 1)
            {
                soundManager.PlaySe(pageTojiSound);
            }

        }
        sentence.text = "";
        date.text = "";
        Homing.m_instance.speed = 2 + GameManager.m_instance.difficultyLevelManager.addEnemySpeed;
        diaryWindow.gameObject.SetActive(false);
    }
}

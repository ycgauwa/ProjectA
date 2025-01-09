using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;
using System;
using static UnityEditor.Progress;

public class BombDefuse : MonoBehaviour
{
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<Sprite> image;
    private List<int> difuseNum = new List<int>();

    public Light2D light2D;
    public Item defusedBomb;
    public Inventry inventry;

    public Canvas secondHouseCanvas;
    public Image bombImage;
    public Text minuteTimerText;
    public Text secondTimerText;
    public Text bombAnswerText;
    float limitTime = 60;
    private bool timerStartSwitch;
    private bool isContacted = false;

    public SoundManager soundManager;
    public AudioClip clockSound;
    public AudioClip difuseBgm;
    public AudioClip lastClockSound;
    public AudioClip explosion;
    public AudioClip difuseClockAudio2;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //幸人の時に表示されるメッセージ
        if(collider.gameObject.tag.Equals("Player"))
            isContacted = true;
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
            isContacted = false;
    }
    void Update()
    {
        if(isContacted && (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)))
        {
            isContacted = false;
            BombDifuse().Forget();
        }
        if(timerStartSwitch)
            limitTime -= Time.deltaTime;

        if(limitTime < 0)
        {
            limitTime = 0;
            timerStartSwitch = false;
            ExplodeBomb().Forget();
        }

        secondTimerText.text = "0:" + limitTime.ToString("F0"); // 残り時間を整数で表示
    }
    private async UniTask BombDifuse()
    {
        GameManager.m_instance.stopSwitch = true;
        await MessageManager.message_instance.MessageWindowActive(messages, names, image, ct: destroyCancellationToken);
        secondHouseCanvas.gameObject.SetActive(true);
        bombImage.gameObject.SetActive(true);
        timerStartSwitch = true;
        soundManager.PlayBgm(clockSound);
    }
    private async UniTask AfterDifuse()
    {
        soundManager.StopBgm(clockSound);
        await MessageManager.message_instance.MessageWindowActive(messages, names, image, ct: destroyCancellationToken);
        inventry.Add(defusedBomb);
        defusedBomb.checkPossession = true;
        GameManager.m_instance.stopSwitch = false;
    }
    private async UniTask ExplodeBomb()
    {
        limitTime += 5;
        await MessageManager.message_instance.MessageWindowActive(messages, names, image, ct: destroyCancellationToken);
        soundManager.StopBgm(clockSound);
        soundManager.PlaySe(lastClockSound);
        bombImage.gameObject.SetActive(false);
        secondHouseCanvas.gameObject.SetActive(false);
        await Blackout();
        await UniTask.Delay(TimeSpan.FromSeconds(2.0f));
        soundManager.PlaySe(explosion);
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
    public void PressNumberButton(int i)
    {
        difuseNum.Add(i);
        if(difuseNum.Count > 2)
            return;
        else if(difuseNum.Count == 1)
            bombAnswerText.text = difuseNum[0].ToString();
        else if(difuseNum.Count == 2)
        {
            bombAnswerText.text = difuseNum[0] + difuseNum[1].ToString();
        }
    }
    public void PressClearButton()
    {
        difuseNum.Clear();
        bombAnswerText.text = "";
    }
    public void PressAnswerButton()
    {
        if(bombAnswerText.text == "15")
        {
            Debug.Log("正解");
            timerStartSwitch = false;
            bombImage.gameObject.SetActive(false);
            secondHouseCanvas.gameObject.SetActive(false);
            AfterDifuse().Forget();
        }
        else 
        {
            Debug.Log("不正解");
            limitTime -= 5;
        }
    }
}

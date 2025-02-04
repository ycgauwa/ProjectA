using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.Rendering.Universal;
using System;

public class WeightObjectSwitch : MonoBehaviour
{
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<Sprite> images;
    public Canvas selectionCanvas;
    public Image selection;
    public GameObject firstSelect;
    public GameObject weightObject;
    public AudioClip pressedButton;
    private bool SeiContacted = false;
    //スイッチを押すと選択肢が出現してその選択肢で暗転かも、
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Seiitirou"))
            SeiContacted = true;
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Seiitirou"))
            SeiContacted = false;
    }
    private void Update()//入力チェックはUpdateに書く
    {
        if(SeiContacted && (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)))
            MessageManager.message_instance.MessageSelectWindowActive(messages, names, images, selectionCanvas, selection, firstSelect, ct: destroyCancellationToken).Forget();
    }
    public void SwitchOn()
    {
        ResetSwitchOn().Forget();
        GameManager.m_instance.stopSwitch = true;
        SeiContacted = false;
    }
    public void SwitchOff()
    {
        selectionCanvas.gameObject.SetActive(false);
        selection.gameObject.SetActive(false);
        MessageManager.message_instance.isOpenSelect = false;
    }
    private async UniTask ResetSwitchOn()
    {
        selectionCanvas.gameObject.SetActive(false);
        selection.gameObject.SetActive(false);
        MessageManager.message_instance.isOpenSelect = false;
        SoundManager.sound_Instance.PlaySe(pressedButton);
        await Blackout();
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
        weightObject.gameObject.transform.position = new Vector3(144, 102, 0);
        SecondHouseManager.secondHouse_instance.light2D.intensity = 1.0f;
        GameManager.m_instance.stopSwitch = false;

    }
    private async UniTask Blackout()
    {
        SecondHouseManager.secondHouse_instance.light2D.intensity = 1.0f;
        while(SecondHouseManager.secondHouse_instance.light2D.intensity > 0.01f)
        {
            SecondHouseManager.secondHouse_instance.light2D.intensity -= 0.012f;
            await UniTask.Delay(1);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.AI;
using Cysharp.Threading.Tasks;

public class NotEnter16 : MonoBehaviour
{
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> messages2;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<Sprite> images;
    [SerializeField]
    private List<Sprite> images2;
    public bool keyOpened = false;
    private bool isContacted = false;
    public Canvas thirdHouseCanvas;
    public GameObject buttonGrid;
    public AudioClip keyOpen;
    public AudioClip defuseLocked;
    public AudioClip runSound;

    //�K�l���a���̕�������o�悤�Ƃ����Ƃ��̃X�N���v�g
    private void Start()
    {
        if(!keyOpened)
            return;
        else
        {
            name = "Warp3-9";
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        //�K�l�̎��ɕ\������郁�b�Z�[�W
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
        if(!keyOpened && isContacted && (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)))
        {
            //�����J���ĂȂ��Ƃ�
            isContacted = false;
            TatamiPuzzle().Forget();
        }
        else if(isContacted && (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)))
        {
            //�����J�����������̂܂ܒʂ��
        }

        if(thirdHouseCanvas.gameObject.activeSelf && (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)))
        {
            buttonGrid.SetActive(false);
            thirdHouseCanvas.gameObject.SetActive(false);
            GameManager.m_instance.stopSwitch = false;
            GameManager.m_instance.notSaveSwitch = false;
        }

    }
    private async UniTask TatamiPuzzle()
    {
        GameManager.m_instance.stopSwitch = true;
        GameManager.m_instance.notSaveSwitch = true;
        await MessageManager.message_instance.MessageWindowActive(messages, names, images, ct: destroyCancellationToken);
        thirdHouseCanvas.gameObject.SetActive(true);
        buttonGrid.SetActive(true);
    }
}

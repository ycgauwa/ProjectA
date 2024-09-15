using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DishMessage : MonoBehaviour
{
    // �b�����������Ƀ��b�Z�[�W�E�B���h�E��\���B�E�B���h�E����\���ɂȂ�����ɃJ�����_�[���A�N�e�B�u�ɂ���
    // �b�������ĉ摜���o�Ă��āA�摜���o�Ă�Ƃ��̓^�C���X�P�[�����O�ɂ���B�����ăG���^�[�������ƃe�L�X�g
    // ���b�Z�[�W���o�Ă��āA�\�����I�������摜�������ă^�C���X�P�[�������ɖ߂��B

    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<Sprite> image;
    public Canvas window;
    public Text target;
    public Text nameText;
    public Image characterImage;
    private IEnumerator coroutine;
    public bool messageSwitch = false;
    public bool isContacted = false;
    public Canvas Selectwindow;
    public Image selection;
    public bool isOpenSelect = false;
    public Inventry inventry;
    public Item shrimp;
    public Item chicken;
    public Item fish;
    public GameObject dish;
    private void Start()
    {
        dish = gameObject;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
        {
            isContacted = true;
        }
    }
    // ���̏�Ԃ��ƕ�����摜�͏o��B�ł��摜������Ȃ��Ȃ��Ă��܂��B�㓮���Ȃ�
    //�@�摜��������Ă��������x�摜���o�����߂�
    private void OnTriggerExit2D(Collider2D collider)
    {
        messageSwitch = false;
        if(collider.gameObject.tag.Equals("Player"))
        {
            isContacted = false;
        }
    }

    protected void showMessage(string message, string name, Sprite image)
    {
        target.text = message;
        nameText.text = name;
        characterImage.sprite = image;
    }
    IEnumerator OnAction()
    {
        window.gameObject.SetActive(true);
        for(int i = 0; i < messages.Count; ++i)
        {
            // 1�t���[���� ������ҋ@(���L����1)
            yield return null;
            // ��b��window��text�t�B�[���h�ɕ\��
            showMessage(messages[i], names[i], image[i]);
            if(i == messages.Count - 1)
            {
                Selectwindow.gameObject.SetActive(true);
                selection.gameObject.SetActive(true);
                isOpenSelect = true;
                break;
            }
            yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
        }
        yield return new WaitUntil(() => !isOpenSelect);
        target.text = "";
        window.gameObject.SetActive(false);
        coroutine = null;
        yield break;
    }
    private void Update()
    {
        if(isContacted && messageSwitch == false && (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)))
        {
            messageSwitch = true;
            coroutine = OnAction();
            StartCoroutine(coroutine);
        }
    }

    /*public void DishTaken()
    {
        selection.gameObject.SetActive(false);
        Selectwindow.gameObject.SetActive(false);
        if(dish.name == ("CookedShrimp"))
        {
            Debug.Log("test1");
            shrimp.checkPossession = true;
            inventry.Add(shrimp);
            isOpenSelect = false;
            window.gameObject.SetActive(false);
            dish.SetActive(false);
        }
        //�Ƃ����A�C�e�������̊ۏĂ��̎�
        else if(dish.name == ("CookedChicken"))
        {
            Debug.Log("test2");
            chicken.checkPossession = true;
            inventry.Add(chicken);
            isOpenSelect = false;
            window.gameObject.SetActive(false);
            dish.SetActive(false);
        }
        else if(dish.name == ("CookedFish"))
        {
            Debug.Log("test3");
            fish.checkPossession = true;
            inventry.Add(fish);
            isOpenSelect = false;
            window.gameObject.SetActive(false);
            dish.SetActive(false);
        }
    }
    public void DishNotTaken()
    {
        selection.gameObject.SetActive(false);
        Selectwindow.gameObject.SetActive(false);
        isOpenSelect = false;
    }*/
}

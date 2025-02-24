using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Cysharp.Threading.Tasks;

public class Cooktop : MonoBehaviour
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
    private List<string> dish1Messages;
    [SerializeField]
    private List<string> dish1Names;
    [SerializeField]
    private List<Sprite> dish1Image;
    [SerializeField]
    private List<string> dish2Messages;
    [SerializeField]
    private List<string> dish2Names;
    [SerializeField]
    private List<Sprite> dish2Image;
    [SerializeField]
    private List<string> dish3Messages;
    [SerializeField]
    private List<string> dish3Names;
    [SerializeField]
    private List<Sprite> dish3Image;
    [SerializeField]
    private List<string> dishFailMessages;
    [SerializeField]
    private List<string> dishFailNames;
    [SerializeField]
    private List<Sprite> dishFailImage;
    [SerializeField]
    private List<string> dishNotMessages;
    [SerializeField]
    private List<string> dishNotNames;
    [SerializeField]
    private List<Sprite> dishNotImage;
    public Canvas window;
    public Text target;
    public Text nameText;
    public TextMeshProUGUI ingredients1Text;
    public TextMeshProUGUI ingredients2Text;
    public TextMeshProUGUI ingredients3Text;
    public Image characterImage;
    public Image cooked;
    public Image interrupt;
    public Image ingredients;
    private IEnumerator coroutine;
    public bool messageSwitch = false;
    public bool isContacted = false;
    public Canvas Selectwindow;
    public Image selection;
    public bool isOpenSelect = false;
    public bool isCooked = false;
    public bool selectedDish1 = false;
    public bool selectedDish2 = false;
    public bool selectedDish3 = false;
    public AudioClip decision;
    public AudioClip cookingMusic;
    public Inventry inventry;
    public ItemDateBase itemDateBase;
    public SoundManager soundManager;
    public Refrigerator refrigerator;
    public GameManager gameManager;
    public GameObject firstSelect;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player")) isContacted = true;
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        messageSwitch = false;
        if(collider.gameObject.tag.Equals("Player")) isContacted = false;
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
            yield return null;
            showMessage(messages[i], names[i], image[i]);
            EventSystem.current.SetSelectedGameObject(firstSelect);
            yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
        }
        Selectwindow.gameObject.SetActive(true);
        cooked.gameObject.SetActive(true);
        selection.gameObject.SetActive(true);
        soundManager.PlayBgm(cookingMusic);
        isOpenSelect = true;
        target.text = "";
        window.gameObject.SetActive(false);
        GameManager.m_instance.stopSwitch = true;
        coroutine = null;
        yield break;
    }
    private void Update()
    {
        if(!cooked.gameObject.activeSelf && isContacted && messageSwitch == false && (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)))
        {
            if(isCooked == true)
            {
                messageSwitch = true;
                MessageManager.message_instance.MessageWindowActive(messages3, names3, image3, messageSwitch, ct: destroyCancellationToken).Forget();
            }
            else if(refrigerator.isTaken == true)
            {
                messageSwitch = true;
                coroutine = OnAction();
                StartCoroutine(coroutine);
            }         
            else
            {
                messageSwitch = true;
                MessageManager.message_instance.MessageWindowActive(messages2, names2, image2, messageSwitch, ct: destroyCancellationToken).Forget();
            }
        }

        if(itemDateBase.GetItemId(15).geted && itemDateBase.GetItemId(16).geted && itemDateBase.GetItemId(17).geted)
        {
            if(!itemDateBase.GetItemId(15).checkPossession && !itemDateBase.GetItemId(16).checkPossession && !itemDateBase.GetItemId(17).checkPossession)
                isCooked = true;
        }
    }

    //�{�^���̃X�N���v�g�̍쐬������B�K�v�ȃX�N���v�g��interrupt�̒��f����Ƃ��Ȃ��{�^��
    //���̎���Cooked�{�^�����������Ƃ���ingredients���łĂ���{�^��
    public void OnClickinterruptButton()
    {
        soundManager.StopBgm(cookingMusic);
        GameManager.m_instance.stopSwitch = false;
        cooked.gameObject.SetActive(false);
        interrupt.gameObject.SetActive(false);
        target.text = "";
        window.gameObject.SetActive(false);
        selection.gameObject.SetActive(false);
        Selectwindow.gameObject.SetActive(false);
    }
    public void OnClickNotinterruptButton()
    {
        interrupt.gameObject.SetActive(false);
        target.text = "";
        window.gameObject.SetActive(false);
    }
    public void OnClickDish1CookedButton() 
    {
        if (selectedDish2 == true || selectedDish3 == true)
        {
            selectedDish2 = false;
            selectedDish3 = false;
        }
        if(itemDateBase.GetItemId(15).checkPossession == true)
        {
            ingredients.gameObject.SetActive(true);
            soundManager.PlaySe(decision);
            selectedDish1 = true;
        }
        //�������̃{�^���Ƃ��Ԃ��Ă邩��ȗ��ł��Ȃ��������B
        if(itemDateBase.GetItemId(6).checkPossession == true)
        {
            ingredients1Text.text = "�p�N�`�[";
        }
        if(itemDateBase.GetItemId(7).checkPossession == true)
        {
            ingredients1Text.text = "�V���E�K";
        }
        if(itemDateBase.GetItemId(8).checkPossession == true)
        {
            ingredients1Text.text = "�j���j�N";
        }
        if(itemDateBase.GetItemId(9).checkPossession == true)
        {
            ingredients2Text.text = "������";
        }
        if(itemDateBase.GetItemId(10).checkPossession == true)
        {
            ingredients2Text.text = "�I�����W";
        }
        if(itemDateBase.GetItemId(11).checkPossession == true)
        {
            ingredients2Text.text = "���C��";
        }
        if(itemDateBase.GetItemId(12).checkPossession == true)
        {
            ingredients3Text.text = "�Ԃ��\�[�X";
        }
        if(itemDateBase.GetItemId(13).checkPossession == true)
        {
            ingredients3Text.text = "���\�[�X";
        }
        if(itemDateBase.GetItemId(14).checkPossession == true)
        {
            ingredients3Text.text = "���F���\�[�X";
        }
    }
    public void OnClickDish2CookedButton()
    {
        if (selectedDish1 == true || selectedDish3 == true)
        {
            selectedDish1 = false;
            selectedDish3 = false;
        }
        if (itemDateBase.GetItemId(16).checkPossession == true)
        {
            ingredients.gameObject.SetActive(true);
            soundManager.PlaySe(decision);
            selectedDish2 = true;
        }
        if(itemDateBase.GetItemId(6).checkPossession == true)
        {
            ingredients1Text.text = "�p�N�`�[";
        }
        if(itemDateBase.GetItemId(7).checkPossession == true)
        {
            ingredients1Text.text = "�V���E�K";
        }
        if(itemDateBase.GetItemId(8).checkPossession == true)
        {
            ingredients1Text.text = "�j���j�N";
        }
        if(itemDateBase.GetItemId(9).checkPossession == true)
        {
            ingredients2Text.text = "������";
        }
        if(itemDateBase.GetItemId(10).checkPossession == true)
        {
            ingredients2Text.text = "�I�����W";
        }
        if(itemDateBase.GetItemId(11).checkPossession == true)
        {
            ingredients2Text.text = "���C��";
        }
        if(itemDateBase.GetItemId(12).checkPossession == true)
        {
            ingredients3Text.text = "�Ԃ��\�[�X";
        }
        if(itemDateBase.GetItemId(13).checkPossession == true)
        {
            ingredients3Text.text = "���\�[�X";
        }
        if(itemDateBase.GetItemId(14).checkPossession == true)
        {
            ingredients3Text.text = "���F���\�[�X";
        }
    }
    public void OnClickDish3CookedButton()
    {
        if (selectedDish2 == true || selectedDish1 == true)
        {
            selectedDish2 = false;
            selectedDish1 = false;
        }
        if (itemDateBase.GetItemId(17).checkPossession == true)
        {
            ingredients.gameObject.SetActive(true);
            soundManager.PlaySe(decision);
            selectedDish3 = true;
        }
        if(itemDateBase.GetItemId(6).checkPossession == true)
        {
            ingredients1Text.text = "�p�N�`�[";
        }
        if(itemDateBase.GetItemId(7).checkPossession == true)
        {
            ingredients1Text.text = "�V���E�K";
        }
        if(itemDateBase.GetItemId(8).checkPossession == true)
        {
            ingredients1Text.text = "�j���j�N";
        }
        if(itemDateBase.GetItemId(9).checkPossession == true)
        {
            ingredients2Text.text = "������";
        }
        if(itemDateBase.GetItemId(10).checkPossession == true)
        {
            ingredients2Text.text = "�I�����W";
        }
        if(itemDateBase.GetItemId(11).checkPossession == true)
        {
            ingredients2Text.text = "���C��";
        }
        if(itemDateBase.GetItemId(12).checkPossession == true)
        {
            ingredients3Text.text = "�Ԃ��\�[�X";
        }
        if(itemDateBase.GetItemId(13).checkPossession == true)
        {
            ingredients3Text.text = "���\�[�X";
        }
        if(itemDateBase.GetItemId(14).checkPossession == true)
        {
            ingredients3Text.text = "���F���\�[�X";
        }
    }
    public void OnclickDish1IngredientsButton()
    {
        Debug.Log("a");
        if(window.gameObject.activeSelf == true)
        {
            Debug.Log("1");
            target.text = "";
            window.gameObject.SetActive(false);
            GameManager.m_instance.ImageErase(characterImage);
        }
        //�G�r�`���p�̃{�^���B�������Ă���f�ނɂ���Đ������ʂ��ς��B�����G�r�`���I��������ԂŐ����̃G�r�`��������ȊO�ɕω�����B
        if(itemDateBase.GetItemId(15).checkPossession && selectedDish1 && ingredients1Text.text == "�p�N�`�[")
        {
            MessageManager.message_instance.MessageWindowActive(dish1Messages, dish1Names, dish1Image, messageSwitch, ct: destroyCancellationToken).Forget();
            ingredients.gameObject.SetActive(false);
            inventry.Delete(itemDateBase.GetItemId(6));
            inventry.Delete(itemDateBase.GetItemId(15));
            inventry.Add(itemDateBase.GetItemId(21));
            selectedDish1 = false;
            ingredients1Text.text = "�g�p�ς�";
        }
        else if(selectedDish2 == true || selectedDish3 == true)//�G�r�`����I�����Ă��Ȃ���Ԃő���Dish�̑f�ނ�I������Ƒf�ނ��K���Ă��Ȃ��Əo��
        {
            MessageManager.message_instance.MessageWindowActive(dishNotMessages, dishNotNames, dishNotImage, messageSwitch, ct: destroyCancellationToken).Forget();
        }
        else//�G�r�`����I��ł��邯�ǂ������������Ă���f�ނ��Ⴄ�������͊������邯�ǎ��s���������ɂȂ�B
        {
            MessageManager.message_instance.MessageWindowActive(dishFailMessages, dishFailNames, dishFailImage, messageSwitch, ct: destroyCancellationToken).Forget();
            ingredients.gameObject.SetActive(false);
            inventry.Delete(itemDateBase.GetItemId(15));
            if(itemDateBase.GetItemId(7).checkPossession == true)
            {
                inventry.Delete(itemDateBase.GetItemId(7));
                inventry.Add(itemDateBase.GetItemId(18));
                selectedDish1 = false;
                ingredients1Text.text = "�g�p�ς�";
            }
            else if(itemDateBase.GetItemId(8).checkPossession == true)
            {
                inventry.Delete(itemDateBase.GetItemId(8));
                inventry.Add(itemDateBase.GetItemId(18));
                selectedDish1 = false;
                ingredients1Text.text = "�g�p�ς�";
            }
        }
    }
    public void OnclickDish2IngredientsButton()
    {
        Debug.Log("b");
        if(window.gameObject.activeSelf == true)
        {
            Debug.Log("2");
            target.text = "";
            window.gameObject.SetActive(false);
            GameManager.m_instance.ImageErase(characterImage);
        }
        if(itemDateBase.GetItemId(16).checkPossession && selectedDish2 && ingredients2Text.text == "������")
        {
            MessageManager.message_instance.MessageWindowActive(dish2Messages, dish2Names, dish2Image, messageSwitch, ct: destroyCancellationToken).Forget();
            ingredients.gameObject.SetActive(false);
            inventry.Delete(itemDateBase.GetItemId(16));
            inventry.Delete(itemDateBase.GetItemId(9));
            inventry.Add(itemDateBase.GetItemId(22));
            selectedDish2 = false;
            ingredients2Text.text = "�g�p�ς�";
        }
        else if(selectedDish1 == true || selectedDish3 == true)
        {
            MessageManager.message_instance.MessageWindowActive(dishNotMessages, dishNotNames, dishNotImage, messageSwitch, ct: destroyCancellationToken).Forget();
        }
        else
        {
            MessageManager.message_instance.MessageWindowActive(dishFailMessages, dishFailNames, dishFailImage, messageSwitch, ct: destroyCancellationToken).Forget();
            ingredients.gameObject.SetActive(false);
            inventry.Delete(itemDateBase.GetItemId(16));
            if(itemDateBase.GetItemId(10).checkPossession == true)
            {
                inventry.Delete(itemDateBase.GetItemId(10));
                inventry.Add(itemDateBase.GetItemId(19));
                selectedDish2 = false;
                ingredients2Text.text = "�g�p�ς�";
            }
            else if(itemDateBase.GetItemId(11).checkPossession == true)
            {
                inventry.Delete(itemDateBase.GetItemId(11));
                inventry.Add(itemDateBase.GetItemId(19));
                selectedDish2 = false;
                ingredients2Text.text = "�g�p�ς�";
            }
        }
    }
    public void OnclickDish3IngredientsButton()
    {
        Debug.Log("c");
        if(window.gameObject.activeSelf == true)
        {
            Debug.Log("3");
            target.text = "";
            window.gameObject.SetActive(false);
            GameManager.m_instance.ImageErase(characterImage);
        }
        if(itemDateBase.GetItemId(17).checkPossession && selectedDish3 && ingredients3Text.text == "�Ԃ��\�[�X")
        {
            MessageManager.message_instance.MessageWindowActive(dish3Messages, dish3Names, dish3Image, messageSwitch, ct: destroyCancellationToken).Forget();
            ingredients.gameObject.SetActive(false);
            inventry.Delete(itemDateBase.GetItemId(17));
            inventry.Delete(itemDateBase.GetItemId(12));
            inventry.Add(itemDateBase.GetItemId(23));
            selectedDish3 = false;
            ingredients3Text.text = "�g�p�ς�";
        }
        else if(selectedDish1 == true || selectedDish2 == true)
        {
            MessageManager.message_instance.MessageWindowActive(dishNotMessages, dishNotNames, dishNotImage, messageSwitch, ct: destroyCancellationToken).Forget();
        }
        else
        {
            MessageManager.message_instance.MessageWindowActive(dishFailMessages, dishFailNames, dishFailImage, messageSwitch, ct: destroyCancellationToken).Forget();
            ingredients.gameObject.SetActive(false);
            inventry.Delete(itemDateBase.GetItemId(17));
            if(itemDateBase.GetItemId(13).checkPossession == true)
            {
                inventry.Delete(itemDateBase.GetItemId(13));
                inventry.Add(itemDateBase.GetItemId(20));
                selectedDish3 = false;
                ingredients3Text.text = "�g�p�ς�";
            }
            else if(itemDateBase.GetItemId(14).checkPossession == true)
            {
                inventry.Delete(itemDateBase.GetItemId(14));
                inventry.Add(itemDateBase.GetItemId(20));
                selectedDish3 = false;
                ingredients3Text.text = "�g�p�ς�";
            }
        }
    }
}
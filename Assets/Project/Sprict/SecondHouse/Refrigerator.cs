using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class Refrigerator : MonoBehaviour
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
    public ItemDateBase itemDateBase;
    public Canvas window;
    public Text target;
    public Text nameText;
    public Text select1;
    public Text select2;
    public Text select3;
    public Image characterImage;
    private IEnumerator coroutine;
    public static bool messageSwitch = false;
    private bool isContacted = false;
    public bool isTaken = false;
    public Canvas Selectwindow;
    public Image selection;
    public bool isOpenSelect = false;
    public Inventry inventry;
    public GameObject firstSelect1;

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
            yield return null;
            // ��b��window��text�t�B�[���h�ɕ\��
            showMessage(messages[i], names[i], image[i]);
            if(i == messages.Count - 1)
            {
                Selectwindow.gameObject.SetActive(true);
                selection.gameObject.SetActive(true);
                EventSystem.current.SetSelectedGameObject(firstSelect1);
                isOpenSelect = true;
                break;
            }
            yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
        }
        yield return new WaitForSeconds(0.2f);
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
            if(itemDateBase.GetItemId(15).checkPossession && itemDateBase.GetItemId(16).checkPossession && itemDateBase.GetItemId(17).checkPossession == true && isTaken == false)
            {
                messageSwitch = true;
                coroutine = OnAction();
                StartCoroutine(coroutine);
            }
            else if(isTaken == true)
            {
                messageSwitch = true;
                MessageManager.message_instance.MessageWindowActive(messages3, names3, image3, messageSwitch, ct: destroyCancellationToken).Forget();
            }
            else
            {
                messageSwitch = true;
                MessageManager.message_instance.MessageWindowActive(messages2, names2, image2, messageSwitch, ct: destroyCancellationToken).Forget();
            }
        }
    }

    public void RefrigeratorSelection1()
    {
        if(target.text == "�u�G�r�`�������������ȗǂ��f�ނ͂Ȃ����ȁH�v")
        {
            inventry.Add(itemDateBase.GetItemId(6));
            target.text = "���̊ۏĂ��p�̑f�ނ́H";
            select1.text = "������";
            select2.text = "�I�����W";
            select3.text = "���C��";
        }
        else if(target.text == "���̊ۏĂ��p�̑f�ނ́H")
        {
            inventry.Add(itemDateBase.GetItemId(9));
            target.text = "���̃\�e�[�p�̑f�ނ́H";
            select1.text = "�Ԃ��\�[�X";
            select2.text = "���\�[�X";
            select3.text = "���F���\�[�X";
        }
        else if(target.text == "���̃\�e�[�p�̑f�ނ́H")
        {
            inventry.Add(itemDateBase.GetItemId(12));
            selection.gameObject.SetActive(false);
            Selectwindow.gameObject.SetActive(false);
            target.text = "����ŐH�ނ̗p�ӂ��ł����B�����Ɍ�������";
            isOpenSelect = false;
            isTaken = true;
        }
    }
    public void RefrigeratorSelection2()
    {
        if(target.text == "�u�G�r�`�������������ȗǂ��f�ނ͂Ȃ����ȁH�v")
        {
            inventry.Add(itemDateBase.GetItemId(7));
            target.text = "���̊ۏĂ��p�̑f�ނ́H";
            select1.text = "������";
            select2.text = "�I�����W";
            select3.text = "���C��";
        }
        else if(target.text == "���̊ۏĂ��p�̑f�ނ́H")
        {
            inventry.Add(itemDateBase.GetItemId(10));
            target.text = "���̃\�e�[�p�̑f�ނ́H";
            select1.text = "�Ԃ��\�[�X";
            select2.text = "���\�[�X";
            select3.text = "���F���\�[�X";
        }
        else if(target.text == "���̃\�e�[�p�̑f�ނ́H")
        {
            inventry.Add(itemDateBase.GetItemId(13));
            selection.gameObject.SetActive(false);
            Selectwindow.gameObject.SetActive(false);
            target.text = "����ŐH�ނ̗p�ӂ��ł����B�����Ɍ�������";
            isOpenSelect = false;
            isTaken = true;
        }
    }
    public void RefrigeratorSelection3()
    {
        if(target.text == "�u�G�r�`�������������ȗǂ��f�ނ͂Ȃ����ȁH�v")
        {
            inventry.Add(itemDateBase.GetItemId(8));
            target.text = "���̊ۏĂ��p�̑f�ނ́H";
            select1.text = "������";
            select2.text = "�I�����W";
            select3.text = "���C��";
        }
        else if(target.text == "���̊ۏĂ��p�̑f�ނ́H")
        {
            inventry.Add(itemDateBase.GetItemId(11));
            target.text = "���̃\�e�[�p�̑f�ނ́H";
            select1.text = "�Ԃ��\�[�X";
            select2.text = "���\�[�X";
            select3.text = "���F���\�[�X";
        }
        else if(target.text == "���̃\�e�[�p�̑f�ނ́H")
        {
            inventry.Add(itemDateBase.GetItemId(14));
            selection.gameObject.SetActive(false);
            Selectwindow.gameObject.SetActive(false);
            target.text = "����ŐH�ނ̗p�ӂ��ł����B�����Ɍ�������";
            isOpenSelect = false;
            isTaken = true;
        }
    }
}

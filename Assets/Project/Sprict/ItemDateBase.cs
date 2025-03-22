using System.Collections;
using System.Collections.Generic;
//using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class ItemDateBase : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public Inventry inventry;
    public NotEnter4 notEnter4;
    public Text itemTextMessage;
    public Canvas inventryCanvas;
    public static ItemDateBase itemDate_instance;

    private void Awake()
    {
        if(itemDate_instance == null)
            itemDate_instance = this;
        else Destroy(gameObject);
    }
    private void Start()
    {
        //�@�G�f�B�^�[��œ������Ƃ����߂���̏ꍇ�ϐ��������������B
        if(SaveSlotsManager.save_Instance.saveState.loadIndex == 0)
        {
            for(int i = 0; i < items.Count; ++i)
            {
                items[i].checkPossession = false;
                items[i].geted = false;
            }
        }
        else return;
    }
    //ItemID����ItemDatebase���Q�Ƃ��ăA�C�e���̃f�[�^������Ă���郁�\�b�h
    public Item GetItemId(int itemid)
    {
        Item item = new Item();
        for(int i = 0; i < items.Count; i++)
        {
            if(items[i].itemID == itemid)
                item = items[i];
        }
        return item;
    }
    //�@�A�C�e���̃Z���N�g��ԉ���
    public void SelectDiff()
    {
        for (int i = 0; i < items.Count; ++i)
        {
            items[i].selectedItem = false;
        }
    }
    //�@�A�C�e�������̃��\�b�h
    public void synthesis()
    {
        //�@�n���}�[�Ɛ�܂ŕ��ʂ̃n���}�[�ɂȂ�
        if(GetItemId(3).selectedItem == true && GetItemId(4).selectedItem == true)
        {
            inventry.Add(GetItemId(5));
            inventry.Delete(GetItemId(3));
            inventry.Delete(GetItemId(4));
            // �����ō����̃��b�Z�[�W���o���悤�ɂ���B
            itemTextMessage.gameObject.SetActive(true);
            itemTextMessage.text = "��������";
        }
        //�@���ʂ̃n���}�[�Ɛl�`�Ō����o�Ă���B
        if(GetItemId(5).selectedItem == true && GetItemId(2).selectedItem == true)
        {
            inventry.Add(GetItemId(251));
            inventry.Delete(GetItemId(5));
            inventry.Delete(GetItemId(2));
            itemTextMessage.gameObject.SetActive(true);
            itemTextMessage.text = "��������";
        }
        
    }
    private void Update()
    {
        if(inventryCanvas.gameObject.activeSelf)
        {
            if(Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return))
            {
                itemTextMessage.gameObject.SetActive(false);
            }
        }
    }
}

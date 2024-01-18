using System.Collections;
using System.Collections.Generic;
//using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;
//using static UnityEditor.Progress;

public class ItemDateBase : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public Inventry inventry;
    public NotEnter4 notEnter4;
    public Text itemTextMessage;
    public Canvas inventryCanvas;

    private void Start()
    {
        for(int i = 0; i < items.Count; ++i)
        {
            items[i].checkPossession = false;
        }
    }
    //�@�A�C�e�������̃��\�b�h
    public void synthesis()
    {
        //�@�n���}�[�Ɛ�܂ŕ��ʂ̃n���}�[�ɂȂ�
        if(items[1].selectedItem == true && items[2].selectedItem == true && notEnter4.getKey1 == false)
        {
            inventry.Add(items[3]);
            inventry.Delete(items[1]);
            inventry.Delete(items[2]);
            // �����ō����̃��b�Z�[�W���o���悤�ɂ���B
            itemTextMessage.gameObject.SetActive(true);
            itemTextMessage.text = "��������";
        }
        //�@���ʂ̃n���}�[�Ɛl�`�Ō����o�Ă���B
        if(items[0].selectedItem == true && items[3].selectedItem == true && notEnter4.getKey1 == false)
        {
            inventry.Add(items[5]);
            inventry.Delete(items[0]);
            inventry.Delete(items[3]);
            itemTextMessage.gameObject.SetActive(true);
            itemTextMessage.text = "��������";
            notEnter4.getKey1 = true;
        }
        
    }
    private void Update()
    {
        if(inventryCanvas.gameObject.activeSelf)
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                itemTextMessage.gameObject.SetActive(false);
            }
        }
    }
    public void Items4Delete()
    {
        inventry.Delete(items[3]);
    }
    public void Items5Delete()
    {
        inventry.Delete(items[3]);
    }
}

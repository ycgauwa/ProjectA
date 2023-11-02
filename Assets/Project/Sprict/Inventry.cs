using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventry : MonoBehaviour
{
    public static Inventry instance;
    public InventryUI inventryUI;

    public List<Item> items = new List<Item>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        inventryUI = GetComponent<InventryUI>();
        //inventryUI.UpdateUI();
    }
    public void Add(Item item)
    {
        items.Add(item);
        inventryUI.UpdateUI();
    }
    /*2023�N11��2��
     �C���x���g���ɂ���
     UI�n�Ŕ�A�N�e�B�u������Start�֐��ŏ����������Ȃ��̃I�u�W�F�N�g������Ă�����UI�n�̃X�N���v�g�����Ă�����
    �Q�[�����Ƀ��X�g�𑝂₵�Ă��������Ƃ��́A�����Ɉ��v���n�u�𕡐�������Ɂi�������A�A�C�e���̏����X���b�g��
    ����Ă����邱�Ɓj������SetActive��True�ɂ���΂��������ɂȂ�INull�ŃG���[�ɂȂ鎞�͎Q�ƌ^���m�肵�Ă��邩�炻������
    ���ׂĂ�����΂����B�I�u�W�F�N�g����A�N�e�B�u�̎�Start��Awake�͓����Ă���Ȃ�
    �Q�Ƃ�����Ƃ����������܂��g���Ă����ăX�N���v�g���m��������Ƃ��ݍ����Ă����悤�ɍ��*/
}

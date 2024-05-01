using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventryUI : MonoBehaviour
{
    public Transform slotsParent;
    public Slot[] slots;
    public Inventry inventry;

    private void Awake()
    {
        //  �������邱�ƂŐe�v�f�ɂ������Ă���Slot���܂񂾎q�v�f��S���擾���邱�Ƃ��ł���
        //�@�܂�InventryParent�̎q�v�f�i�P�Q��Slot�j�����[��Ԏ擾���邱�Ƃ��ł���B
        slots = slotsParent.GetComponentsInChildren<Slot>(true);
    }
    public void UpdateUI()
    {
        //�@�C���x���g���ɃA�C�e���������Ă�Ȃ�\����������ĂȂ��Ȃ�null�ɂ���
        //�@�͂��߂Ƀp�X���[�h����͂���ƃG���[��f��������G���^�[�����ƍ��܂ł̉񐔕��ȏ�o�Ă���
        for(int i = 0; i< slots.Length; i++)
        {
            if(i < inventry.items.Count)//UI�ł͂Ȃ��C���x���g���̃f�[�^�̒���
            {
                slots[i].AddItem(inventry.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
            
            if (slots[i].iconImage.sprite == null)
            {
                slots[i].iconImage.color = new Color32(56, 56, 56, 0);
            }
        }
    }
}

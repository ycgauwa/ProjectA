using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventryManager : MonoBehaviour
{
    // �C���x���g���̃A�C�e���ŏ��������Ǘ����ăC�x���g���b�ɂ��܂��Ȃ���
    // �v���ŃA�C�e������肵�����Ƀ��X�g�𑝂₷�A�A�C�e��������A�������H����
    // �A�C�e�������������A���̃A�C�e����CheckPossession��True�ɂ��Ă�����B
    public List<Item> inventryItems = new List<Item>();

    private void Start()
    {
        //items[0].checkPossession = true;
    }
    public void CheckPossess(Item item)
    {
        inventryItems.Add(item);
        item.checkPossession = true;
    }

}

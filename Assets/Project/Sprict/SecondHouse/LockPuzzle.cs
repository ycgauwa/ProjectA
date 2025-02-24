using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockPuzzle : MonoBehaviour
{
    public Button[] buttons;  // �{�^���̔z��
    public Image[] lamps;  // �����v�̔z��
    public Sprite lampOnSprite;  // �����������v�̃X�v���C�g
    public Sprite lampOffSprite;  // �����������v�̃X�v���C�g
    private bool[] lampStates;  // �����v�̏�Ԃ��Ǘ�����z��
    public NotEnter15 notEnter;
    public AudioClip clickSound;
    void Start()
    {
        lampStates = new bool[lamps.Length];
        for(int i = 0; i < lamps.Length; i++)
        {
            lamps[i].sprite = lampOffSprite;  // ������ԂŃ����v�͏���
        }

        for(int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            buttons[i].onClick.AddListener(() => ToggleLamps(index));  // �e�{�^���ɃN���b�N�C�x���g��o�^
        }
    }

    public void ToggleLamps(int buttonIndex)
    {
        SoundManager.sound_Instance.PlaySe(clickSound);
        // �{�^���ɑΉ����郉���v�̃C���f�b�N�X���擾
        int[] lampIndexes = GetLampIndexesForButton(buttonIndex);
        // �e�����v���g�O���i���]�j����
        foreach(int index in lampIndexes)
        {
            lampStates[index] = !lampStates[index];  // ��Ԃ𔽓]
            lamps[index].sprite = lampStates[index] ? lampOnSprite : lampOffSprite;  // ���]������Ԃɉ����ăX�v���C�g��ύX
        }

        // ���ׂẴ����v�������Ă��邩�m�F
        if(AllLampsOn())
        {
            notEnter.DefuseLocked();
        }
    }

    int[] GetLampIndexesForButton(int buttonIndex)
    {
        // �e�{�^���ɑΉ����郉���v�̃C���f�b�N�X
        switch(buttonIndex)
        {
            case 0: return new int[] { 0, 1, 3 };  // �{�^��1
            case 1: return new int[] { 0, 1, 2, 4 };  // �{�^��2
            case 2: return new int[] { 1, 2, 5 };  // �{�^��3
            case 3: return new int[] { 0, 3, 4, 6 };  // �{�^��4
            case 4: return new int[] { 1, 3, 4, 5, 7 };  // �{�^��5
            case 5: return new int[] { 2, 4, 5, 8 };  // �{�^��6
            case 6: return new int[] { 3, 6, 7 };  // �{�^��7
            case 7: return new int[] { 4, 6, 7, 8 };  // �{�^��8
            case 8: return new int[] { 5, 7, 8 };  // �{�^��9
            default: return new int[] { };
        }
    }

    bool AllLampsOn()
    {
        foreach(bool state in lampStates)
        {
            if(!state) return false;  // 1�ł������Ă��郉���v�������false
        }
        return true;  // ���ׂẴ����v�������Ă����true
    }
}

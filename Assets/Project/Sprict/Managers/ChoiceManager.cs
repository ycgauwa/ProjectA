using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceManager : MonoBehaviour
{
    public Canvas window;
    public Button yes;
    public Button no;
    //�Ăяo���ꂽ�Ƃ��Ƀ{�^����\�����郁�\�b�h�����

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SelectChoice()
    {
        window.gameObject.SetActive(true);
        /*�����I������ĉ����ꂽ��I�����ɂ����bool�ϐ����ω�����
        SetActive(false)�ɂȂ������e�L�X�g���b�Z�[�W���\������遨�����Selection.cs�������
        2�̃}�l�[�W���[���烁�\�b�h���Ăэ��ނƏ�̍s�̃X�N���v�g�őI�����@�\�����������
        �܂�e�L�X�g�����\�������I�u�W�F�N�g��Test1�őI�������݂Ńe�L�X�g���\�������悤�ɂ���
        �{�^�����\�������O�Ƀe�L�X�g�\���B�Ōオ���b�Z�[�W���o�Ă��ăG���^�[�������烁�b�Z�[�W���O��
        ���������Ƀ{�^����UI�L�����o�X���o�Ă��ĉ������{�^���ɂ���ă��b�Z�[�W���ς��
        �܂��{�^��UI����������o�Ă���e�L�X�g���ύX����邽��SetActive(Window)���������Ƃ͍Ō�܂ł��Ȃ�*/
        this.window.gameObject.SetActive(false);
    }
}

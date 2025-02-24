using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using static UnityEditor.Progress;
using static UnityEngine.GraphicsBuffer;

public class TVCharadeManager : MonoBehaviour
{
    public Canvas diaryCanvas;
    public Image panel1;
    public Image panel2;
    public Image panel3;
    public Image panel4;
    public Image TVScreen;
    public Image TVCharade;
    private bool Panel1Answer;
    private bool Panel2Answer;
    private bool Panel3Answer;
    private bool Panel4Answer;
    [SerializeField]
    private string messages;
    [SerializeField]
    private string messages2;
    public Text screenText;

    // ������������������A�C�e�������炦�āA���b�Z�[�W���\��������d�g�݂̍쐬
    // �s������I��������Ō�̃��b�Z�[�W���łăA�C�e���͂��炦�Ȃ��B
    // �ŏI�I�ɂ�bool�̕ϐ��͌��ɖ߂��悤�ɂ���B
    public void OnClickPanel1Answer()
    {
        Panel1Answer = true;
        panel1.gameObject.SetActive(false);
        panel2.gameObject.SetActive(true);
    }

    public void OnClickPanel1Miss()
    {
        panel1.gameObject.SetActive(false);
        panel2.gameObject.SetActive(true);
    }

    public void OnClickPanel2Answer()
    {
        Panel2Answer = true;
        panel2.gameObject.SetActive(false);
        panel3.gameObject.SetActive(true);
    }

    public void OnClickPanel2Miss()
    {
        panel2.gameObject.SetActive(false);
        panel3.gameObject.SetActive(true);
    }
    public void OnClickPanel3Answer()
    {
        Panel3Answer = true;
        panel3.gameObject.SetActive(false);
        panel4.gameObject.SetActive(true);
    }

    public void OnClickPanel3Miss()
    {
        panel3.gameObject.SetActive(false);
        panel4.gameObject.SetActive(true);
    }
    public void OnClickPanel4Answer()
    {
        Panel4Answer = true;
        panel4.gameObject.SetActive(false);
        if(Panel1Answer == true && Panel2Answer == true && Panel3Answer == true && Panel4Answer == true)
        {
            TVMessageAnswer();
        }
        else
        {
            TVMessageMiss();
        }
    }

    public void OnClickPanel4Miss()
    {
        panel4.gameObject.SetActive(false);
        TVMessageMiss();
    }

    // miss���o�����ɂ�����x�I������\������悤�ɂ�������answer�Ȃ�A�C�e������ł���悤�ɂ�����

    private void TVMessageAnswer()
    {
        TVScreen.gameObject.SetActive(true);
        screenText.text = messages;
        Debug.Log(Inventry.instance);
        Debug.Log(ItemDateBase.itemDate_instance);
        if(ItemDateBase.itemDate_instance.GetItemId(252).geted == false)
            GameManager.m_instance.inventry.Add(ItemDateBase.itemDate_instance.GetItemId(252));
    }
    private void TVMessageMiss() 
    {
        TVScreen.gameObject.SetActive(true);
        screenText.text = messages2;
        panel1.gameObject.SetActive(true);
        Panel1Answer = false;
        Panel2Answer = false;
        Panel3Answer = false;
        Panel4Answer = false;
    }
}

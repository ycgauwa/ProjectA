using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public PlayerManager playerManager;
    public GameObject enemy;
    public Homing homing;
    public Canvas menuCanvas;
    public Canvas inventryCanvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!menuCanvas.gameObject.activeSelf)
        {
            if(Input.GetKeyUp(KeyCode.Escape))
            {
                Debug.Log("menu");
                Time.timeScale = 0;
                menuCanvas.gameObject.SetActive(true);

            }
        }
        else if(menuCanvas.gameObject.activeSelf)
        {
            if(Input.GetKeyUp(KeyCode.Escape))
            {
                Time.timeScale = 1;
                menuCanvas.gameObject.SetActive(false);
            }
        }
        if(inventryCanvas.gameObject.activeSelf)
        {
            if(Input.GetKeyUp(KeyCode.Escape))
            {
                inventryCanvas.gameObject.SetActive(false);
                menuCanvas.gameObject.SetActive(true);
            }
        }
        //  ���j���[��ʂ�ESC�ŌĂׂ�B�Ă񂾂���ESC�ŕ����B�����ǃ��j���[��ʂ���
    }
    public void OnClickInventryButton()
    {
        //�@�C���x���g���{�^�����N���b�N�����Ƃ����j���[�L�����o�X�������ăC���x���g�����Ăяo��
        //�@�C���x���g�������o�Ă�̂�ESC��������C���x���g���������ă��j���[���Ăׂ΂悢
        menuCanvas.gameObject.SetActive(false);
        inventryCanvas.gameObject.SetActive(true);
    }
}

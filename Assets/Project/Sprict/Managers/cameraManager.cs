using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class cameraManager : MonoBehaviour
{
    public GameObject player; // �Ǐ]����Ώۂ����߂�ϐ�
    public GameObject seiitirou;
    public GameObject girl;
    public GameObject haru;
    Vector3 pos;              // �J�����̏����ʒu���L�����邽�߂̕ϐ�
    public static bool playerCamera = true;
    public static bool girlCamera = false;
    public static bool seiitirouCamera = false;
    public static bool haruCamera = false;
    public static bool event5Camera = false;
    public float cameraSize = 0.0f;
    public Camera cam;
    public NotEnter6 notEnter6;
    public HaruDeathQuestion question;

    // Start is called before the first frame update
    void Start()
    {
        pos = Camera.main.gameObject.transform.position;
        cam = GetComponent<Camera>();
        cameraSize = cam.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        cam.orthographicSize = cameraSize;
        if(playerCamera == true)
        {
            Vector3 cameraPos = player.transform.position; // cameraPos�Ƃ����ϐ������A�Ǐ]����Ώۂ̈ʒu������


            cameraPos.x = player.transform.position.x;
            cameraPos.y = player.transform.position.y;   // �J�����̏c�ʒu�ɑΏۂ̈ʒu������


            cameraPos.z = -10; // �J�����̉��s���̈ʒu��-10������
            Camera.main.gameObject.transform.position = cameraPos; //�@�J�����̈ʒu�ɕϐ�cameraPos�̈ʒu������

            //�v���C���[��TP�����Ƃ��J������TP����
            //�v���C���[�̍��W���i67,64,0�j�̎��ǂݍ���
        }
        if(girlCamera == true)
        {
            Vector3 cameraPos = girl.transform.position; // cameraPos�Ƃ����ϐ������A�Ǐ]����Ώۂ̈ʒu������


            cameraPos.x = girl.transform.position.x;
            cameraPos.y = girl.transform.position.y;   // �J�����̏c�ʒu�ɑΏۂ̈ʒu������


            cameraPos.z = -10; // �J�����̉��s���̈ʒu��-10������
            Camera.main.gameObject.transform.position = cameraPos; //�@�J�����̈ʒu�ɕϐ�cameraPos�̈ʒu������
        }
        if (seiitirouCamera == true)
        {
            Vector3 cameraPos = seiitirou.transform.position;


            cameraPos.x = seiitirou.transform.position.x;
            cameraPos.y = seiitirou.transform.position.y;


            cameraPos.z = -10;
            Camera.main.gameObject.transform.position = cameraPos;
        }
        if(haruCamera == true)
        {
            Vector3 cameraPos = haru.transform.position;


            cameraPos.x = haru.transform.position.x;
            cameraPos.y = haru.transform.position.y;


            cameraPos.z = -10;
            Camera.main.gameObject.transform.position = cameraPos;
        }
        if(notEnter6.cameraSwitch || question.cameraSwitch)
        {
            if(cameraSize > 0.5)
            {
                cameraSize -= 0.01f;
            }
        }
        else
        {
            cameraSize = 5f;
        }
    }

}

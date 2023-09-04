using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homing : MonoBehaviour
{
    Transform playerTr; //�v���C���[��Transform
    public GameTeleportManager teleportManager;
    [SerializeField] float speed = 2; //�G�̓����X�s�[�h
    // Start is called before the first frame update
    private void Start()
    {
        // �v���C���[��Transform���擾�i�v���C���[�̃^�OPlayer�ɐݒ�K�v�j
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
    }



    // Update is called once per frame
    private void Update()
    {
        if (Vector2.Distance(transform.position, playerTr.position) < 0.1f)
            return;

        // �v���C���[�Ɍ����Đi��
        transform.position = Vector2.MoveTowards(
            transform.position,
            new Vector2(playerTr.position.x, playerTr.position.y),
            speed * Time.deltaTime);
       
    }
    //�G�����ԍ��e���|�[�g���郁�\�b�h
    public void TimerTeleport()
    {
        var teleportAddress = teleportManager.FindTeleportAddress("House");
        
    }
    //�v���C���[��TP���Ƃ�F�m������
    

}

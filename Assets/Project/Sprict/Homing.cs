using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevel;

public class Homing : MonoBehaviour
{
    //�v���C���[��Transform
    Transform playerTr;

    //Static���g���Ă���A�C���X�^���X�����Ă���
    public static Homing m_instance;
    public GameTeleportManager teleportManager;
    [SerializeField] 
    public float speed = 2; //�G�̓����X�s�[�h
    public ToEvent3 toevent3;
    public float enemyCount = 0.0f; //�@�G���ǂ������Ă��鎞��
    public AudioClip chaseSound;

    private void Start()
    {
        m_instance = this;
        // �v���C���[��Transform���擾�i�v���C���[�̃^�OPlayer�ɐݒ�K�v�j
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {

        if(toevent3.event3flag)
        {
            if(Vector2.Distance(transform.position, playerTr.position) < 0.1f)
                return;

            // �v���C���[�Ɍ����Đi��
            transform.position = Vector2.MoveTowards(
                transform.position,
                new Vector2(playerTr.position.x, playerTr.position.y),
                speed * Time.deltaTime);
        }
        if(speed > 0)
        {
            enemyCount += Time.deltaTime;
        }
    }
    // �G�����̋����𓮂�����ԂŃv���C���[�����[�v������ǂ������Ă��Ȃ��Ȃ�
    // �������������L�^�i���邢�͎��Ԃ��L�^����ϐ����쐬�j���̌㓮�������������ȏ�ɂȂ��
    // ���̕ϐ��̒l���O�ƂȂ�B�O�̎��́i���̐��ȉ��Ȃ�j���[�v���Ȃ���ǉ�����


    //�G�����ԍ��e���|�[�g���郁�\�b�h
    public void TimerTeleport()
    {
        if(toevent3.event3flag)
        {
            var teleportAddress = teleportManager.FindTeleportAddress("House");
        }
    }
    //�v���C���[��TP���Ƃ�F�m������
    

}

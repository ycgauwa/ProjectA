using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UI;
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
    public RescueEvent rescueEvent;
    public ItemDateBase itemDateBase;
    public ItemSprictW itemSprictW;
    public GameObject player;
    public float enemyCount = 0.0f; //�@�G���ǂ������Ă��鎞��
    public Canvas gameoverWindow;
    public Image buttonPanel;
    public bool enemyEmerge;
    public bool isMove;
    Rigidbody2D rbody;
    NPCAnimationController cr;
    public Vector2 enemyPosition;
    private Vector2 enemyMovement;

    private void Start()
    {
        m_instance = this;
        // �v���C���[��Transform���擾�i�v���C���[�̃^�OPlayer�ɐݒ�K�v�j
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
        cr = GetComponentInChildren<NPCAnimationController>();
        isMove = true;
        rbody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if(toevent3.event3flag && enemyEmerge)
        {
            if(Vector2.Distance(transform.position, playerTr.position) < 0.1f)
                return;
            // �v���C���[�Ɍ����Đi��
            enemyMovement = Vector2.MoveTowards(
                transform.position,
                new Vector2(playerTr.position.x, playerTr.position.y),
                speed);

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
    void FixedUpdate()
    {
        if (isMove)
        {
            Vector2 currentPos = rbody.position;

            enemyPosition.x = transform.position.x - enemyPosition.x;
            enemyPosition.y = transform.position.y - enemyPosition.y;
            enemyMovement = new Vector2(enemyPosition.x,enemyPosition.y);
            cr.SetDirection(enemyMovement);
        }
        enemyPosition.x = transform.position.x;
        enemyPosition.y = transform.position.y;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag.Equals("Player"))
        {
            // �m�l�`��l�`�������Ă���Ƃ��̏���
            if(itemDateBase.items[8].checkPossession == true)
            {
                itemSprictW.ItemEffect();
            }
            else
            {
                // �H�ׂ�ꂽ���̃T�E���h�𗬂�
                // �Q�[���I�[�o�[��ʂ��o�����߂̃L�����o�X�Ƃ��̐��b��Ƀ{�^�����o��
                gameoverWindow.gameObject.SetActive(true);
                GameManager.m_instance.stopSwitch = true;
                Invoke("AppearChoice", 2.5f);
            }
        }
    }

    public void AppearChoice()
    {
        buttonPanel.gameObject.SetActive(true);
    }

    // �G�����̋����𓮂�����ԂŃv���C���[�����[�v������ǂ������Ă��Ȃ��Ȃ�
    // �������������L�^�i���邢�͎��Ԃ��L�^����ϐ����쐬�j���̌㓮�������������ȏ�ɂȂ��
    // ���̕ϐ��̒l���O�ƂȂ�B�O�̎��́i���̐��ȉ��Ȃ�j���[�v���Ȃ���ǉ�����


    //�G�����ԍ��e���|�[�g���郁�\�b�h
    public void TimerTeleport()
    {
        if(toevent3.event3flag && enemyEmerge)
        {
            var teleportAddress = teleportManager.FindTeleportAddress("House");
        }
    }
    //�v���C���[��TP���Ƃ�F�m������
    

}

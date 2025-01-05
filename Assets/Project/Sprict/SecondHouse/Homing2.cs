using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Homing2 : MonoBehaviour
{
    Transform playerTr;
    public static Homing2 m_instance;
    public GameTeleportManager teleportManager;
    [SerializeField]
    public float speed ; //���x�̂��߁A�����x�Ɗ֌W������(max5)
    public int count = 0;
    public GameObject player;
    public float enemyCount = 0.0f; 
    public float acceleration = 0.0f;//�����x(max1.5)
    public Canvas gameoverWindow;
    public Image buttonPanel;
    public bool enemyEmerge;
    public bool isMove;
    Rigidbody2D rbody;
    //NPCAnimationController cr;
    public Vector2 enemyPosition;
    private Vector2 enemyMovement;
    //2���ڂ̌��̓���X�N���v�g�B����Ƃ��Ă͗��߂Ĉ�C�ɓ��������B
    private void Start()
    {
        m_instance = this;
        // �v���C���[��Transform���擾�i�v���C���[�̃^�OPlayer�ɐݒ�K�v�j
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
        //cr = GetComponentInChildren<NPCAnimationController>();
        isMove = true;
        rbody = GetComponent<Rigidbody2D>();
        enemyCount = 0;
        GameTeleportManager.chasedTime = true;
    }
    private void Update()
    {
        if(enemyEmerge)
        {
            if(count % 60 == 0)
            {
                if(acceleration < 1f)
                    acceleration += 0.3f;
                if(speed < 5.0f)
                    speed += acceleration;
            }
            count++; // �J�E���g�A�b�v
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
        if(enemyEmerge == true && speed > 0 && PlayerManager.m_instance.playerstate != PlayerManager.PlayerState.Talk && PlayerManager.m_instance.playerstate != PlayerManager.PlayerState.Stop)
        {
            enemyCount += Time.deltaTime;
        }
    }
    void FixedUpdate()
    {
        if(isMove)
        {
            Vector2 currentPos = rbody.position;

            enemyPosition.x = transform.position.x - enemyPosition.x;
            enemyPosition.y = transform.position.y - enemyPosition.y;
            enemyMovement = new Vector2(enemyPosition.x, enemyPosition.y);
            //cr.SetDirection(enemyMovement);
        }
        enemyPosition.x = transform.position.x;
        enemyPosition.y = transform.position.y;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
        {
            /* �m�l�`��l�`�������Ă���Ƃ��̏���
            if(itemDateBase.items[8].checkPossession == true)
            {
                itemSprictW.ItemEffect();
            }
            else*/
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
        if(gameObject.activeSelf) teleportManager.StopChased();
    }

    // �G�����̋����𓮂�����ԂŃv���C���[�����[�v������ǂ������Ă��Ȃ��Ȃ�
    // �������������L�^�i���邢�͎��Ԃ��L�^����ϐ����쐬�j���̌㓮�������������ȏ�ɂȂ��
    // ���̕ϐ��̒l���O�ƂȂ�B�O�̎��́i���̐��ȉ��Ȃ�j���[�v���Ȃ���ǉ�����


    //�G�����ԍ��e���|�[�g���郁�\�b�h
    public void TimerTeleport()
    {
        if(enemyEmerge)
        {
            var teleportAddress = teleportManager.FindTeleportAddress("House");
        }
    }
    //�v���C���[��TP���Ƃ�F�m������


}

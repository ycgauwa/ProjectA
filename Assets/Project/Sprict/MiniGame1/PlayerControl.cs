using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public static float ACCELERATION = 10.0f;               //�����x
    public static float SPEED_MIN = 2.0f;                   // ���x�̍ŏ��l
    public static float SPEED_MAX = 4.0f;                   //���x�̍ő�l
    public static float JUMP_HEIGHT_MAX = 3.0f;             //�W�����v�̍���
    public static float JUMP_KEY_RELEASE_REDUSE = 0.5f;     //�W�����v����̌����l

    private Rigidbody2D rb;

    public enum STEP        //�@Player�̊e���Ԃ�\���f�[�^�^
    {
        NONE = -1,          //�@��ԏ��Ȃ��B
        RUN = 0,            //�@����B
        JUMP,               //�@�W�����v�B
        MISS,               //�@�~�X�B
        NUM,                //�@��Ԃ�����ނ��邩�������@�i��3�j
    };

    public STEP step = STEP.NONE;       //Player�̌��݂̏��
    public STEP next_step = STEP.NONE;  //Player�̎��̏�ԁB


    public float step_timer = 0.0f;         //�o�ߎ��� 
    private bool is_landed = false;         //���n���Ă��邩�ǂ���
    private bool is_collided = false;       //�����ƂԂ����Ă��邩
    private bool is_key_released = false;   //�{�^����������Ă��邩�ǂ���

    public static float NARAKU_HEIGHT = -5.0f;

    void Start()
    {
        //�Q�[���J�n���X���瑖��o���悤�ɂ������B
        next_step = STEP.RUN;

        rb = gameObject.AddComponent<Rigidbody2D>() as Rigidbody2D;
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    void Update()
    {
        Debug.Log(step);
        this.transform.Translate(new Vector3(0.001f, 0.0f, 0.0f )* Time.deltaTime);
        
        //Vector2 velocity = rigidbady.velocity;  ���x��ݒ�B
        Vector2 velocity = rb.velocity;
        check_landed();                          // ���n��Ԃ��ǂ����`�F�b�N�B

        switch(this.step)
        {
            case STEP.RUN:
            case STEP.JUMP:
                // ���݂̈ʒu���������l�������Ȃ��
                if(transform.position.y < NARAKU_HEIGHT)
                {
                    this.next_step = STEP.MISS;    //�@�u�~�X�v��Ԃɂ���
                }
                break;
        }

        step_timer += Time.deltaTime;            // �o�ߎ��Ԃ�i�߂�B

        // �u���̏�Ԃ����܂��Ă��Ȃ���΁A��Ԃ̕ω��𒲂ׂ�B�v
        if(next_step == STEP.NONE)
        {
            Debug.Log("next_step == STEP.NONE");
            switch(this.step)// Player�̌��݂̏�Ԃŕ���B
            {
                case STEP.RUN:�@//���s���̏ꍇ
                    if(!is_landed)
                    {
                        Debug.Log("!is_landed_only");
                        //�@���s���A���n���Ă��Ȃ��ꍇ�A�������Ȃ�
                    }
                    else
                    {
                        if(Input.GetMouseButtonDown(0))
                        {
                            // ���s���ŁA���n���Ă��āA���{�^���������ꂽ��B
                            // ���̏�Ԃ��W�����v�ɕύX�B
                            next_step = STEP.JUMP;
                            Debug.Log("!is_landed0");
                        }
                    }
                    break;
                case STEP.JUMP:
                    if(is_landed)
                    {
                        Debug.Log("!is_landed1");
                        // �W�����v���A���n���Ă�����A���̏�Ԃ𑖍s���ɕύX�B
                        next_step = STEP.RUN;
                    }
                    break;
            }
        }

        while(next_step != STEP.NONE)
        {
            step = next_step;
            next_step = STEP.NONE;
            switch(step)
            {
                case STEP.JUMP:
                    //�@�W�����v�̍�������W�����v�̏������v�Z
                    velocity.y = Mathf.Sqrt(2.0f * 9.8f * PlayerControl.JUMP_HEIGHT_MAX);
                    // �u�{�^���������ꂽ�t���O�v���N���A����B
                    is_key_released = false;
                    break;
            }
            step_timer = 0.0f; //�@��Ԃ��ω������̂ŁA�o�ߎ��Ԃ��[���Ƀ��Z�b�g�B
        }

        //�@��Ԃ��Ƃ́A���t���[���̍X�V�����B
        switch(step)
        {
            case STEP.RUN:

                velocity.x += PlayerControl.ACCELERATION * Time.deltaTime;
                //�@���x���ō����x�̐�����������B
                if(Mathf.Abs(velocity.x) > PlayerControl.SPEED_MAX)
                {
                    //�@�ō����x�̐����ȉ��ɕۂB
                    velocity.x *= PlayerControl.SPEED_MAX / Mathf.Abs(velocity.x);
                }
                break;
            case STEP.JUMP:
                do
                {
                    // �u�{�^���������ꂽ�u�ԁv����Ȃ������B
                    if(!Input.GetMouseButton(0))
                    {
                        break;      // ���������Ƀ��[�v�𔲂���
                    }
                    //�@�����ς݂Ȃ�A�i�Q��ȏ㌸�����Ȃ��悤�Ɂj
                    if(is_key_released)
                    {
                        break;      // ���������Ƀ��[�v�𔲂���
                    }
                    //�@�㉺�����̑��x���O�ȉ��Ȃ�i���~���Ȃ�j
                    if(velocity.y <= 0.0f)
                    {
                        break; // ���������Ƀ��[�v
                    }
                    //�@�{�^����������Ă��āA�㏸���Ȃ�A�����J�n
                    //�@�W�����v�̏㏸�͂����ł����܂�
                    velocity.y *= JUMP_KEY_RELEASE_REDUSE;

                    is_key_released = true;
                } while(false);
                break;

            case STEP.MISS:
                //�@�����x�iACCELERATION�j�������Z���āAPlayer�̑��x��x�����Ă����B
                velocity.x -= PlayerControl.ACCELERATION * Time.deltaTime;
                if(velocity.x < 0.0f)       //�v���C���[�̑��x�����Ȃ�
                {
                    velocity.x = 0.0f;      //�[���ɂ���
                }
                break;
        }
        //  Rigidbody�̑��x���A��L�ŋ��߂����x�ōX�V�B
        //�@(���̍s�́A��Ԃɂ�����炸������s�����)
        rb.velocity = velocity;
    }
    private void check_landed()
    {
        is_landed = false;      //�Ƃ肠����false�ɂ��Ă����B
        do
        {
            Vector3 s = this.transform.position;    //Player�̌��݂̈ʒu�B
            Vector3 e = s +  Vector3.down * 1.0f;    //s���牺��1.0f�Ɉړ������ʒu�B

            RaycastHit2D hit = Physics2D.Linecast(s, e);
            if(! hit)    //s����e�̊Ԃɉ����Ȃ���ʁB
            {
                Debug.Log("nanimonai");
                break;�@ //����������do�`while���[�v�𔲂���i�E�o���ցj
            }

            //�@s����e�̊Ԃɉ������������ꍇ�A�ȉ��̏������s����
            if(this.step == STEP.JUMP)      // ���݁A�W�����v��ԂȂ�΁A
            {
                Debug.Log("nankaaru1");
                //�@�o�ߎ��Ԃ�3.0f�����Ȃ��
                if(step_timer < Time.deltaTime * 3.0f)
                {
                    break;�@//��������do�`while���[�v�𔲂���i�E�o���ցj
                }
            }
            Debug.Log("nankaaru2");
            //�@s����e�̊Ԃɉ���������AJUMP����łȂ��ꍇ�̂݁A�ȉ������s�����B
            is_landed = true;

        } while(false);
        //���[�v�̒E�o���B
    }
}

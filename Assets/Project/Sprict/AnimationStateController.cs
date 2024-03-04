using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        // ������
        // �R���g���[�����Z�b�g�����I�u�W�F�N�g�ɕR�t���Ă���
        // Animator���擾����
        this.animator = GetComponent<Animator>();
    }

    /**
     * Update �� MonoBehaviour ���L���̏ꍇ�ɁA���t���[���Ăяo����܂�
     */
    void Update()
    {

        if(Input.anyKeyDown)
        {
            Vector2? action = this.actionKeyDown();
            if(action.HasValue)
            {
                // �L�[���͂������Animator��state���Z�b�g����
                setStateToAnimator(vector: action.Value);
                return;
            }
        }
        // ���͂���Vector2�C���X�^���X���쐬
        Vector2 vector = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical"));

        // �L�[���͂������Ă���ꍇ�́A���͂���쐬����Vector2��n��
        // �L�[���͂��Ȃ���� null
        setStateToAnimator(vector: vector != Vector2.zero ? vector : (Vector2?)null);

    }

    /**
     * Animator�ɏ�Ԃ��Z�b�g����
     *    
     */
    private void setStateToAnimator(Vector2? vector)
    {
        if(!vector.HasValue)
        {
            this.animator.speed = 0.0f;
            return;
        }
        this.animator.speed = 1.0f;
        this.animator.SetFloat("x", vector.Value.x);
        this.animator.SetFloat("y", vector.Value.y);

    }

    /**
     * ����̃L�[�̓��͂�����΃L�[�ɂ��킹��Vector2�C���X�^���X��Ԃ�
     * �Ȃ����null��Ԃ�   
     */
    private Vector2? actionKeyDown()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow)) return Vector2.up;
        if(Input.GetKeyDown(KeyCode.LeftArrow)) return Vector2.left;
        if(Input.GetKeyDown(KeyCode.DownArrow)) return Vector2.down;
        if(Input.GetKeyDown(KeyCode.RightArrow)) return Vector2.right;
        return null;
    }
}

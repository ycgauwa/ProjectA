using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private GameObject player = null;
    private Vector3 position_offset = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        //�����o�[�ϐ�player�ɁAPlayer�I�u�W�F�N�g���擾�B
        player = GameObject.FindGameObjectWithTag("Player");
        //�J�����̈ʒu�ithis.transform.position�j��
        //�v���C���[�̈ʒu�iplayer.transform.position�j�Ƃ̍������m�ہB
        position_offset = player.transform.position - this.transform.position;
    }
    void LateUpdate()
    {
        //�J�����̌��݈ʒu��new_position�Ɏ擾�B
        Vector3 new_position = transform.position;
        // �v���C���[��x���W�ɍ����𑫂��āA�ϐ�new_position��x�ɑ������B
        new_position.x = player.transform.position.x + position_offset.x;
        //�J�����̈ʒu���A�V�����ʒu�inew_position�j�ɍX�V�B
        this.transform.position = new_position;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControl : MonoBehaviour
{
    //�@���ׂ��u���b�N�Ɋւ�������W�߂��\����
    public struct CreationInfo
    {
        public Block.TYPE block_type;       // �u���b�N�̎��
        public int max_count;               //�@�u���b�N�̍ő��
        public int height;                  //�@�u���b�N��z�u���鍂��
        public int current_count;           //�@�쐬�����u���b�N�̌�

    }

    public CreationInfo previous_block;     // �O��A�ǂ������u���b�N���������
    public CreationInfo current_block;      //�@����A�ǂ������u���b�N�����ׂ���
    public CreationInfo next_block;         //�@����A�ǂ������u���b�N�����ׂ���

    public int block_count = 0;             // �쐬�����u���b�N�̑����B
    public int level = 0;                   //�@��Փx�B

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Update()
    {
        
    }

    // Update is called once per frame
    public void update()
    {
        //�@�u���������u���b�N�̌��v���C���N�������g
        current_block.current_count++;

        //�@�u���������u���b�N�̌��v���\�萔�imax_count�j�ȏ�Ȃ�B
        if(current_block.current_count >= current_block.max_count)
        {
            previous_block = current_block;
            current_block = next_block;

            //�@���ɍ��ׂ��u���b�N�̓��e���������B
            clear_next_block(ref next_block);
            //�@���ɍ��ׂ��u���b�N��ݒ�B
            update_level(ref next_block, current_block);
        }
        block_count++;          //�@�u�u���b�N�̑����v���C���N�������g�B
    }
    private void clear_next_block(ref CreationInfo block)
    {
        // �󂯎�����u���b�N�iblock�j�̒��g���������B
        block.block_type = Block.TYPE.FLOOR;
        block.max_count = 15;
        block.height = 0;
        block.current_count = 0;
    }

    public void initialize()
    {
        this.block_count = 0;       //�u���b�N�̑������O�ɁB

        // �O��A����A����̃u���b�N�����ꂼ����B
        // clear_next_block()�ɓn���ď��������Ă��炤�B
        this.clear_next_block(ref this.previous_block);
        this.clear_next_block(ref this.current_block);
        this.clear_next_block(ref this.next_block);
    }

    private void update_level(ref CreationInfo current,CreationInfo previous)
    {
        switch(previous.block_type)
        {
            case Block.TYPE.FLOOR:                      // ����̃u���b�N�����̏ꍇ
                current.block_type = Block.TYPE.FLOOR;�@// ����͌������
                current.max_count = 5;                  //�@���͂T���
                current.height = previous.height;       //�@������O��Ɠ����ɂ���B
                break;

            case Block.TYPE.HOLE:                       //�@����̃u���b�N�����̏ꍇ
                current.block_type = Block.TYPE.FLOOR;  //�@����͏������
                current.max_count = 10;                 //�@���͂P�O���
                break;                                  

        }
    }
}

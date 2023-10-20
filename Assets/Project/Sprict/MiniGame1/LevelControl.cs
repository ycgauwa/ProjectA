using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData
{
    public struct Range         //�@�͈͂�\���\���́B
    {
        public int min;         //  �͈͂̍ŏ��l�B
        public int max;         //�@�͈͂̍ő�l�B
    };

    public float end_time;      //  �I�����ԁB
    public float player_speed;  //  �v���C���[�̑��x�B

    public Range floor_count;   //  ����u���b�N���͈̔́B
    public Range hole_count;    //  ���̌��͈̔́B
    public Range height_diff;   //  ����̍����͈̔́B

    public LevelData()
    {
        end_time = 15.0f;       //�@�I�����Ԃ�������
        player_speed = 6.0f;    //�@�v���C���[�̑��x��������
        floor_count.min = 10;   //�@����u���b�N�̐��̍ŏ��l��������
        floor_count .max = 10;  //�@����u���b�N�̐��̍ő�l��������
        hole_count.min = 2;     //�@���̌��̍ŏ��l��������
        hole_count.max = 6;     //�@���̌��̍ő�l��������
        height_diff.min = 0;    //�@����̍����ω��̍ŏ��l��������
        height_diff .max = 0;   //�@����̍����ω��̍ő�l��������
    }
}

public class LevelControl : MonoBehaviour
{
    private List<LevelData> level_datas = new List<LevelData>();

    public int HEIGHT_MAX = 20;
    public int HEIGHT_MIN = -4;

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


    //public void update()
    public void update(float passage_time)
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
            //update_level(ref next_block, current_block);
            update_level(ref next_block, current_block,passage_time);

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

    private void update_level(ref CreationInfo current,CreationInfo previous, float passage_time) // �V�݂̈���passage_time�ŁA�v���C�̌o�ߎ��Ԃ��󂯎��B
    {
        // �u���x��1�`���x��5�v�̌J��Ԃ��ɁB
        float local_time = Mathf.Repeat(passage_time, level_datas[level_datas.Count - 1].end_time);

        // ���݂̃��x�������߂�B
        int i;
        for(i = 0; i < level_datas.Count - 1; i++)
        {
            if(local_time <= level_datas[i].end_time)
            {
                break;
            }
        }
        this.level = i;

        current.block_type = Block.TYPE.FLOOR;
        current.max_count = 1;

        if(block_count >= 10)
        {
            // ���݂̃��x���p�̃��x���f�[�^���擾�B
            LevelData level_data;
            level_data = level_datas[level];

            switch(previous.block_type)
            {
                case Block.TYPE.FLOOR:                      // �̃u���b�N�����̏ꍇ
                    current.block_type = Block.TYPE.HOLE; �@// ����͌������

                    // ���̒����̍ŏ��l�`�ő�l�̊Ԃ́A�����_���Ȓl�B
                    current.max_count = Random.Range(level_data.hole_count.min, level_data.hole_count.max);

                    current.height = previous.height;       //�@������O��Ɠ����ɂ���B
                    break;

                case Block.TYPE.HOLE:                       //�@�O��̃u���b�N�����̏ꍇ
                    current.block_type = Block.TYPE.FLOOR;  //�@����͏������

                    // ���̒����̍ŏ��l�`�ő�l�̊Ԃ́A�����_���Ȓl�B
                    current.max_count = Random.Range(level_data.floor_count.min, level_data.floor_count.max);

                    // ���̍����̍ŏ��l�ƍő�l�����߂�B
                    int height_min = previous.height + level_data.height_diff.min;
                    int height_max = previous.height + level_data.height_diff.max;
                    height_min = Mathf.Clamp(height_min, HEIGHT_MIN, HEIGHT_MAX);
                    height_max = Mathf.Clamp(height_max, HEIGHT_MIN, HEIGHT_MAX);

                    // ���̍����̍ŏ��l�`�ő�l�̊Ԃ́A�����_���Ȓl�B
                    current.height = Random.Range(height_min, height_max);
                    break;

            }

        }

        switch(previous.block_type)
        {
            case Block.TYPE.FLOOR:                      // ����̃u���b�N�����̏ꍇ
                current.block_type = Block.TYPE.HOLE;�@ // ����͌������
                current.max_count = 5;                  //�@���͂T���
                current.height = previous.height;       //�@������O��Ɠ����ɂ���B
                break;

            case Block.TYPE.HOLE:                       //�@����̃u���b�N�����̏ꍇ
                current.block_type = Block.TYPE.FLOOR;  //�@����͏������
                current.max_count = 10;                 //�@���͂P�O���
                break;                                  

        }
    }

    public void loadLevelData(TextAsset level_data_text)
    {
        //�@�e�L�X�g�f�[�^���A������Ƃ��Ď�荞�ށB
        string level_texts = level_data_text.text;

        //�@���s�R�[�h'\'���Ƃɕ������A������̔z��ɓ����B
        string[] lines = level_texts.Split('\n');

        //�@line���̊e�s�ɑ΂��āA���Ԃɏ������Ă������[�v�B
        foreach(var line in lines)
        {
            if(line == "")      //�@�s������ۂȂ�
            {
                continue;       //�@�ȉ��̏����͂����Ƀ��[�v�̐擪�ɃW�����v�B
            };
            Debug.Log(line);
            string[] words = line.Split();
            int n = 0;

            //  LevelData�^�̕ϐ����쐬�B
            //�@�����ɁA���ݏ������Ă���s�̃f�[�^�����Ă����B
            LevelData level_data = new LevelData();

            //�@word���̊e���[�h�ɑ΂��āA���Ԃɏ������Ă������[�v�B
            foreach(var word in words)          
            {
                if(word.StartsWith("#"))        //�@���[�h�̐擪������#�Ȃ�B
                {
                    break;                      //�@���[�v��E�o�B
                }
                if(word == "")                  //�@���[�h������ہB
                {
                    continue;                   //�@���[�v�̐擪�ɃW�����v�B
                }

                //�@�un�v�̒l0�C1�C2�C�c�c7�ƕω������Ă������ƂŁA�W���ڂ������B
                //�@�e���[�h��float�l�ɕϊ����Alevel_data�Ɋi�[����B
                switch(n)
                {
                    case 0: level_data.end_time    = float.Parse(word); 
                            break;
                    case 1: level_data.player_speed = float.Parse(word);
                            break;
                    case 2: level_data.floor_count.min = int.Parse(word);
                            break;
                    case 3: level_data.floor_count.max = int.Parse(word);
                            break;
                    case 4: level_data.hole_count.min = int.Parse(word);
                            break;
                    case 5: level_data.hole_count.max = int.Parse (word);
                            break;
                    case 6: level_data.height_diff.min = int.Parse(word);
                            break;
                    case 7: level_data.height_diff.max = int.Parse(word);
                            break;
                }
                n++;
            }

            if(n >= 8)      //�@8���ځi�ȏ�j��������Ə������ꂽ�Ȃ�B
            {
                //�@List�\����level_datas��level_data��ǉ��B
                level_datas.Add(level_data);
            }
            else            //�@�����łȂ��Ȃ�i�G���[�̉\��������j
            {
                if(n == 0)  //�@�P���[�h���������Ă��Ȃ��ꍇ�̓R�����g�Ȃ̂ŁA�B
                {
                            //�@���Ȃ��B�������Ȃ��B
                }
                else        //�@����ȊO�Ȃ�G���[
                {
                    //�@�f�[�^�̌��������ĂȂ����������G���[���b�Z�[�W��\������B
                    Debug.LogError("[LevelData] Out of parameter.\n");
                }
            }

        }

        // level_datas�Ƀf�[�^������Ȃ��Ȃ�΁B
        if(level_datas.Count == 0)
        {
            //�@�G���[���b�Z�[�W��\��
            Debug.LogError("[LevelData] Has no data.\n");
            // level_datas�ɁA�f�t�H���g��LevelData���P�ǉ����Ă���
            level_datas.Add(new LevelData());

        }
    }

    public float getPlayerSpeed()
    {
        return (level_datas[level].player_speed);
    }
}

using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class Block
{
    // �u���b�N�̎�ނ�\���񋓑́B
    public enum TYPE
    {
        NONE = -1,          // �Ȃ��B
        FLOOR = 0,          //�@���B
        HOLE,               //�@���B
        NUM,                //�@�u���b�N������ނ��邩�������B
    };
};

public class MapCreater : MonoBehaviour
{
    public static float BLOCK_WIDTH = 1.0f;     // �u���b�N�̕�
    public static float BLOCK_HEIGHT = 0.2f;    //�@�u���b�N�̍���
    public static int BLOCK_NUM_IN_SCREEN = 24;// ��ʓ��Ɏ��܂�u���b�N�̌��B

    private LevelControl level_control = null;

    //�@�u���b�N�Ɋւ�������܂Ƃ߂ĊǗ����邽�߂̍\���́B
    private struct FloorBlock
    {
        public bool is_created;         /*�u���b�N���쐬�ς݂��ۂ�*/
        public Vector3 position;        /*�u���b�N�̈ʒu*/
    };

    private FloorBlock last_block;
    private PlayerControl player = null;
    private BlockCreator block_creator;

    public TextAsset level_data_text = null;
    private GameRoot game_root = null;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        last_block.is_created = false;
        block_creator = this.gameObject.GetComponent<BlockCreator>();

        level_control = new LevelControl();
        level_control.initialize();
        level_control.loadLevelData(level_data_text);

        game_root = gameObject.GetComponent<GameRoot>();

        player.level_control = level_control;
    }

    // Update is called once per frame
    void Update()
    {
        //�@�v���C���[��x�ʒu���擾�B
        float block_generate_x = player.transform.position.x;
        // ��������A���悻����ʕ��A�E�ֈړ��B
        // ���̈ʒu���A�u���b�N�𐶂ݏo���������l�ɂȂ�B
        block_generate_x += BLOCK_WIDTH * ((float)BLOCK_NUM_IN_SCREEN + 1) / 2.0f;

        // �Ō�ɍ�����u���b�N�̈ʒu���������l��菬�����ԁB
        while (last_block.position.x < block_generate_x) 
        {
            //�u���b�N�����B
            create_floor_block();
        }
    }

    private void create_floor_block()
    {
        Vector3 block_position;     //���ꂩ����u���b�N�̈ʒu


        if(! last_block.is_created)//last_block�����쐬�̏ꍇ�B
        {
            //�u���b�N�̈ʒu���Ƃ肠�����v���C���[�Ɠ����ɂ���B
            block_position = player.transform.position;
            //�@���ꂩ��u���b�N��x�ʒu�𔼉�ʕ��A���Ɉړ��B
            block_position.x -= BLOCK_WIDTH * ((float)BLOCK_NUM_IN_SCREEN / 2.0f);
            //�@�u���b�N��y���W�͂O�ɂ���B
            block_position.y = 0.0f;
        }
        else�@  //last_block���쐬�ς݂̏ꍇ
        {
            //������u���b�N�̈ʒu���A�O�������u���b�N�Ɠ����ɁB
            block_position = last_block.position;
        }

        //�u���b�N���P�u���b�N���A�E�Ɉړ��B
        block_position.x += BLOCK_WIDTH;
        // BlockCreator�X�N���v�g��createBlock()���\�b�h�ɍ쐬�w���B
        //����܂ł̃R�[�h�Őݒ肵��block_position��n���B
        //(�R�����g�A�E�g)block_creator.createBlock(block_position);

        //(�R�����g�A�E�g)this.level_control.update();        //�@LevelControl���X�V�B
        level_control.update(game_root.getPlayTime());

        //�@level_control�ɒu���ꂽcurrent_block(�������̃u���b�N)�́B
        //�@height�i�����j���A�V�[����̍��W�ɕϊ��B
        block_position.y = level_control.current_block.height * BLOCK_HEIGHT;

        //�@������u���b�N�Ɋւ������ϐ�current�Ɏ��߂�
        LevelControl.CreationInfo current = level_control.current_block;

        //�@������u���b�N�����Ȃ�B
        if(current.block_type == Block.TYPE.FLOOR)
        {
            //�@block_position�̈ʒu�ɁA�u���b�N�����ۂɍ쐬�B
            block_creator.createBlock(block_position);
        }



        //last_block�̈ʒu���A����Ɉʒu�ɍX�V�B
        last_block.position = block_position;
        //�@�u���b�N�쐬�ς݂Ȃ̂ŁAlast_block��is_created��true�ɂ���B
        last_block.is_created = true;
    }
    public bool isDelete(GameObject block_object)
    {
        bool ret = false;   //�߂�l

        //�@Player����A����ʕ��A���̈ʒu
        //�@���ꂪ�A������ׂ����ۂ������߂邵�����l�ƂȂ�B
        float left_limit = this.player.transform.position.x - BLOCK_WIDTH * ((float)BLOCK_NUM_IN_SCREEN / 2.0f);

        // �u���b�N�̈ʒu���������l��菬�����i���j�Ȃ�B
        if(block_object.transform.position.x < left_limit)
        {
            ret = true;�@// �߂�l��true(�����Ă悵)�ɂ���B
        }
        return ret;     //���茋�ʂ�Ԃ��B
    }
}

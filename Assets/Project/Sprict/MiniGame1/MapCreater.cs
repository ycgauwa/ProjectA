using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class Block
{
    // ブロックの種類を表す列挙体。
    public enum TYPE
    {
        NONE = -1,          // なし。
        FLOOR = 0,          //　床。
        HOLE,               //　穴。
        NUM,                //　ブロックが何種類あるかを示す。
    };
};

public class MapCreater : MonoBehaviour
{
    public static float BLOCK_WIDTH = 1.0f;     // ブロックの幅
    public static float BLOCK_HEIGHT = 0.2f;    //　ブロックの高さ
    public static int BLOCK_NUM_IN_SCREEN = 24;// 画面内に収まるブロックの個数。

    private LevelControl level_control = null;

    //　ブロックに関する情報をまとめて管理するための構造体。
    private struct FloorBlock
    {
        public bool is_created;         /*ブロックが作成済みか否か*/
        public Vector3 position;        /*ブロックの位置*/
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
        //　プレイヤーのx位置を取得。
        float block_generate_x = player.transform.position.x;
        // そこから、およそ半画面分、右へ移動。
        // この位置が、ブロックを生み出すしきい値になる。
        block_generate_x += BLOCK_WIDTH * ((float)BLOCK_NUM_IN_SCREEN + 1) / 2.0f;

        // 最後に作ったブロックの位置がしきい値より小さい間。
        while (last_block.position.x < block_generate_x) 
        {
            //ブロックを作る。
            create_floor_block();
        }
    }

    private void create_floor_block()
    {
        Vector3 block_position;     //これから作るブロックの位置


        if(! last_block.is_created)//last_blockが未作成の場合。
        {
            //ブロックの位置をとりあえずプレイヤーと同じにする。
            block_position = player.transform.position;
            //　それからブロックのx位置を半画面分、左に移動。
            block_position.x -= BLOCK_WIDTH * ((float)BLOCK_NUM_IN_SCREEN / 2.0f);
            //　ブロックのy座標は０にする。
            block_position.y = 0.0f;
        }
        else　  //last_blockが作成済みの場合
        {
            //今回作るブロックの位置を、前回作ったブロックと同じに。
            block_position = last_block.position;
        }

        //ブロックを１ブロック分、右に移動。
        block_position.x += BLOCK_WIDTH;
        // BlockCreatorスクリプトのcreateBlock()メソッドに作成指示。
        //これまでのコードで設定したblock_positionを渡す。
        //(コメントアウト)block_creator.createBlock(block_position);

        //(コメントアウト)this.level_control.update();        //　LevelControlを更新。
        level_control.update(game_root.getPlayTime());

        //　level_controlに置かれたcurrent_block(今作る情報のブロック)の。
        //　height（高さ）を、シーン上の座標に変換。
        block_position.y = level_control.current_block.height * BLOCK_HEIGHT;

        //　今回作るブロックに関する情報を変数currentに収める
        LevelControl.CreationInfo current = level_control.current_block;

        //　今回作るブロックが床なら。
        if(current.block_type == Block.TYPE.FLOOR)
        {
            //　block_positionの位置に、ブロックを実際に作成。
            block_creator.createBlock(block_position);
        }



        //last_blockの位置を、今回に位置に更新。
        last_block.position = block_position;
        //　ブロック作成済みなので、last_blockのis_createdをtrueにする。
        last_block.is_created = true;
    }
    public bool isDelete(GameObject block_object)
    {
        bool ret = false;   //戻り値

        //　Playerから、半画面分、左の位置
        //　これが、消えるべきか否かを決めるしきい値となる。
        float left_limit = this.player.transform.position.x - BLOCK_WIDTH * ((float)BLOCK_NUM_IN_SCREEN / 2.0f);

        // ブロックの位置がしきい値より小さい（左）なら。
        if(block_object.transform.position.x < left_limit)
        {
            ret = true;　// 戻り値をtrue(消えてよし)にする。
        }
        return ret;     //判定結果を返す。
    }
}

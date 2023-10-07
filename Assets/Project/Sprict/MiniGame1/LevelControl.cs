using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControl : MonoBehaviour
{
    //　作るべきブロックに関する情報を集めた構造体
    public struct CreationInfo
    {
        public Block.TYPE block_type;       // ブロックの種類
        public int max_count;               //　ブロックの最大個数
        public int height;                  //　ブロックを配置する高さ
        public int current_count;           //　作成したブロックの個数

    }

    public CreationInfo previous_block;     // 前回、どういうブロックを作ったか
    public CreationInfo current_block;      //　今回、どういうブロックを作るべきか
    public CreationInfo next_block;         //　次回、どういうブロックを作るべきか

    public int block_count = 0;             // 作成したブロックの総数。
    public int level = 0;                   //　難易度。

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
        //　「今回作ったブロックの個数」をインクリメント
        current_block.current_count++;

        //　「今回作ったブロックの個数」が予定数（max_count）以上なら。
        if(current_block.current_count >= current_block.max_count)
        {
            previous_block = current_block;
            current_block = next_block;

            //　次に作るべきブロックの内容を初期化。
            clear_next_block(ref next_block);
            //　次に作るべきブロックを設定。
            update_level(ref next_block, current_block);
        }
        block_count++;          //　「ブロックの総数」をインクリメント。
    }
    private void clear_next_block(ref CreationInfo block)
    {
        // 受け取ったブロック（block）の中身を初期化。
        block.block_type = Block.TYPE.FLOOR;
        block.max_count = 15;
        block.height = 0;
        block.current_count = 0;
    }

    public void initialize()
    {
        this.block_count = 0;       //ブロックの総数を０に。

        // 前回、今回、次回のブロックをそれぞれを。
        // clear_next_block()に渡して初期化してもらう。
        this.clear_next_block(ref this.previous_block);
        this.clear_next_block(ref this.current_block);
        this.clear_next_block(ref this.next_block);
    }

    private void update_level(ref CreationInfo current,CreationInfo previous)
    {
        switch(previous.block_type)
        {
            case Block.TYPE.FLOOR:                      // 今回のブロックが床の場合
                current.block_type = Block.TYPE.FLOOR;　// 次回は穴を作る
                current.max_count = 5;                  //　穴は５個作る
                current.height = previous.height;       //　高さを前回と同じにする。
                break;

            case Block.TYPE.HOLE:                       //　今回のブロックが穴の場合
                current.block_type = Block.TYPE.FLOOR;  //　次回は床を作る
                current.max_count = 10;                 //　床は１０個作る
                break;                                  

        }
    }
}

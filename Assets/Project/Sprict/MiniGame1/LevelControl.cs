using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData
{
    public struct Range         //　範囲を表す構造体。
    {
        public int min;         //  範囲の最小値。
        public int max;         //　範囲の最大値。
    };

    public float end_time;      //  終了時間。
    public float player_speed;  //  プレイヤーの速度。

    public Range floor_count;   //  足場ブロック数の範囲。
    public Range hole_count;    //  穴の個数の範囲。
    public Range height_diff;   //  足場の高さの範囲。

    public LevelData()
    {
        end_time = 15.0f;       //　終了時間を初期化
        player_speed = 6.0f;    //　プレイヤーの速度を初期化
        floor_count.min = 10;   //　足場ブロックの数の最小値を初期化
        floor_count .max = 10;  //　足場ブロックの数の最大値を初期化
        hole_count.min = 2;     //　穴の個数の最小値を初期化
        hole_count.max = 6;     //　穴の個数の最大値を初期化
        height_diff.min = 0;    //　足場の高さ変化の最小値を初期化
        height_diff .max = 0;   //　足場の高さ変化の最大値を初期化
    }
}

public class LevelControl : MonoBehaviour
{
    private List<LevelData> level_datas = new List<LevelData>();

    public int HEIGHT_MAX = 20;
    public int HEIGHT_MIN = -4;

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


    //public void update()
    public void update(float passage_time)
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
            //update_level(ref next_block, current_block);
            update_level(ref next_block, current_block,passage_time);

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

    private void update_level(ref CreationInfo current,CreationInfo previous, float passage_time) // 新設の引数passage_timeで、プレイの経過時間を受け取る。
    {
        // 「レベル1～レベル5」の繰り返しに。
        float local_time = Mathf.Repeat(passage_time, level_datas[level_datas.Count - 1].end_time);

        // 現在のレベルを求める。
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
            // 現在のレベル用のレベルデータを取得。
            LevelData level_data;
            level_data = level_datas[level];

            switch(previous.block_type)
            {
                case Block.TYPE.FLOOR:                      // のブロックが床の場合
                    current.block_type = Block.TYPE.HOLE; 　// 今回は穴を作る

                    // 穴の長さの最小値～最大値の間の、ランダムな値。
                    current.max_count = Random.Range(level_data.hole_count.min, level_data.hole_count.max);

                    current.height = previous.height;       //　高さを前回と同じにする。
                    break;

                case Block.TYPE.HOLE:                       //　前回のブロックが穴の場合
                    current.block_type = Block.TYPE.FLOOR;  //　今回は床を作る

                    // 床の長さの最小値～最大値の間の、ランダムな値。
                    current.max_count = Random.Range(level_data.floor_count.min, level_data.floor_count.max);

                    // 床の高さの最小値と最大値を求める。
                    int height_min = previous.height + level_data.height_diff.min;
                    int height_max = previous.height + level_data.height_diff.max;
                    height_min = Mathf.Clamp(height_min, HEIGHT_MIN, HEIGHT_MAX);
                    height_max = Mathf.Clamp(height_max, HEIGHT_MIN, HEIGHT_MAX);

                    // 床の高さの最小値～最大値の間の、ランダムな値。
                    current.height = Random.Range(height_min, height_max);
                    break;

            }

        }

        switch(previous.block_type)
        {
            case Block.TYPE.FLOOR:                      // 今回のブロックが床の場合
                current.block_type = Block.TYPE.HOLE;　 // 次回は穴を作る
                current.max_count = 5;                  //　穴は５個作る
                current.height = previous.height;       //　高さを前回と同じにする。
                break;

            case Block.TYPE.HOLE:                       //　今回のブロックが穴の場合
                current.block_type = Block.TYPE.FLOOR;  //　次回は床を作る
                current.max_count = 10;                 //　床は１０個作る
                break;                                  

        }
    }

    public void loadLevelData(TextAsset level_data_text)
    {
        //　テキストデータを、文字列として取り込む。
        string level_texts = level_data_text.text;

        //　改行コード'\'ごとに分割し、文字列の配列に入れる。
        string[] lines = level_texts.Split('\n');

        //　line内の各行に対して、順番に処理していくループ。
        foreach(var line in lines)
        {
            if(line == "")      //　行が空っぽなら
            {
                continue;       //　以下の処理はせずにループの先頭にジャンプ。
            };
            Debug.Log(line);
            string[] words = line.Split();
            int n = 0;

            //  LevelData型の変数を作成。
            //　ここに、現在処理している行のデータを入れていく。
            LevelData level_data = new LevelData();

            //　word内の各ワードに対して、順番に処理していくループ。
            foreach(var word in words)          
            {
                if(word.StartsWith("#"))        //　ワードの先頭文字が#なら。
                {
                    break;                      //　ループを脱出。
                }
                if(word == "")                  //　ワードが空っぽ。
                {
                    continue;                   //　ループの先頭にジャンプ。
                }

                //　「n」の値0，1，2，……7と変化させていくことで、８項目を処理。
                //　各ワードをfloat値に変換し、level_dataに格納する。
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

            if(n >= 8)      //　8項目（以上）がきちんと処理されたなら。
            {
                //　List構造のlevel_datasにlevel_dataを追加。
                level_datas.Add(level_data);
            }
            else            //　そうでないなら（エラーの可能性があり）
            {
                if(n == 0)  //　１ワードも処理していない場合はコメントなので、。
                {
                            //　問題なし。何もしない。
                }
                else        //　それ以外ならエラー
                {
                    //　データの個数が合ってない事を示すエラーメッセージを表示する。
                    Debug.LogError("[LevelData] Out of parameter.\n");
                }
            }

        }

        // level_datasにデータが一つもないならば。
        if(level_datas.Count == 0)
        {
            //　エラーメッセージを表示
            Debug.LogError("[LevelData] Has no data.\n");
            // level_datasに、デフォルトのLevelDataを１つ追加しておく
            level_datas.Add(new LevelData());

        }
    }

    public float getPlayerSpeed()
    {
        return (level_datas[level].player_speed);
    }
}

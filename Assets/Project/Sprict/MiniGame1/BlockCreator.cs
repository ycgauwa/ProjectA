using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCreator : MonoBehaviour
{
    //MapCreatorが指示し、その指示に応えてBlockCreatorがブロックを作るすなわち、ここで作成したメソッドが
    //MapCreatorのスクリプトで呼ばれて処理されるということ。
    
    public GameObject[] blockPrehubs;　/*ブロックを格納する配列*/
    private int block_count = 0;　　　/*作成したブロックの個数*/

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void createBlock(Vector3 block_position)
    {
        //　作成すべきブロックの種類（白か赤か）を求める（０か１しか入らない）
        int next_block_type = this.block_count % this.blockPrehubs.Length;

        //　ブロックを生成し、goに保管。ブロックが入っている配列の１か０が呼ばれ続ける
        GameObject go = GameObject.Instantiate(blockPrehubs[next_block_type])as GameObject;

        go.transform.position = block_position;　/*ブロックの位置を移動（生成したブロックの位置を引数で指定された所に変更）*/
        this.block_count++;　　　　　　　　　　　/*ブロックの個数をインクリメント*/
    }
}

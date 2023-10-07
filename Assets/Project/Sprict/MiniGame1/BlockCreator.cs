using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCreator : MonoBehaviour
{
    //MapCreator���w�����A���̎w���ɉ�����BlockCreator���u���b�N����邷�Ȃ킿�A�����ō쐬�������\�b�h��
    //MapCreator�̃X�N���v�g�ŌĂ΂�ď��������Ƃ������ƁB
    
    public GameObject[] blockPrehubs;�@/*�u���b�N���i�[����z��*/
    private int block_count = 0;�@�@�@/*�쐬�����u���b�N�̌�*/

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void createBlock(Vector3 block_position)
    {
        //�@�쐬���ׂ��u���b�N�̎�ށi�����Ԃ��j�����߂�i�O���P��������Ȃ��j
        int next_block_type = this.block_count % this.blockPrehubs.Length;

        //�@�u���b�N�𐶐����Ago�ɕۊǁB�u���b�N�������Ă���z��̂P���O���Ă΂ꑱ����
        GameObject go = GameObject.Instantiate(blockPrehubs[next_block_type])as GameObject;

        go.transform.position = block_position;�@/*�u���b�N�̈ʒu���ړ��i���������u���b�N�̈ʒu�������Ŏw�肳�ꂽ���ɕύX�j*/
        this.block_count++;�@�@�@�@�@�@�@�@�@�@�@/*�u���b�N�̌����C���N�������g*/
    }
}

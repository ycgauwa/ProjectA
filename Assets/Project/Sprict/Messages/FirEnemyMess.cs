using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class FirEnemyMess : MonoBehaviour
{
    [SerializeField]
    private List<string> messages2;
    [SerializeField]
    private List<string> names2;
    [SerializeField]
    private List<Sprite> images2;
    public GameObject enemy;
    public bool firstDeath;
    void Update()
    {
        if(!GameManager.m_instance.homing.enemyEmerge)
        {
            if(GameManager.m_instance.homing.enemyEmerge)
            {
                MessageManager.message_instance.MessageWindowActive(messages2, names2, images2, ct: destroyCancellationToken).Forget();
                enabled = false;
                gameObject.SetActive(false);
            }
        }
    }
    public void Message()
    {
        //一番最初のチェイス時に死んでしまったときのメッセージ
        //このオブジェクトがtrueの時に死んだら条件達成→でも死んだらこのオブジェクトは存在が消える
        //つまり場合分けとして、死んだときと死んでない時で分ける必要あり
        //死んでないときは上記で十分だが死んだときはエネミーが消えてるときにリトライボタンで出てほしいから
        
    }
}

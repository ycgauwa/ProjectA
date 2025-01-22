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
    [SerializeField]
    private List<string> messages3;
    [SerializeField]
    private List<string> names3;
    [SerializeField]
    private List<Sprite> images3;
    public GameObject enemy;
    public bool firstDeath;
    FirEnemyMess firEnemy;
    // Start is called before the first frame update
    void Start()
    {
        firEnemy = GetComponent<FirEnemyMess>();
    }
    // Update is called once per frame
    void Update()
    {

        if(!enemy.activeSelf)
        {
            if(firstDeath)
            {
                Debug.Log(firstDeath);
                MessageManager.message_instance.MessageWindowActive(messages3, names3, images3, ct: destroyCancellationToken).Forget();
                firEnemy.enabled = false;
                firstDeath = false;
                gameObject.SetActive(false);
            }
                
            MessageManager.message_instance.MessageWindowActive(messages2, names2, images2, ct: destroyCancellationToken).Forget();
            firEnemy.enabled = false;
            gameObject.SetActive(false);
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

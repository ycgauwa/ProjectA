using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homing : MonoBehaviour
{
    Transform playerTr; //プレイヤーのTransform
    public GameTeleportManager teleportManager;
    [SerializeField] float speed = 2; //敵の動くスピード
    // Start is called before the first frame update
    private void Start()
    {
        // プレイヤーのTransformを取得（プレイヤーのタグPlayerに設定必要）
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
    }



    // Update is called once per frame
    private void Update()
    {
        if (Vector2.Distance(transform.position, playerTr.position) < 0.1f)
            return;

        // プレイヤーに向けて進む
        transform.position = Vector2.MoveTowards(
            transform.position,
            new Vector2(playerTr.position.x, playerTr.position.y),
            speed * Time.deltaTime);
       
    }
    //敵が時間差テレポートするメソッド
    public void TimerTeleport()
    {
        var teleportAddress = teleportManager.FindTeleportAddress("House");
        
    }
    //プレイヤーがTPことを認知させる
    

}

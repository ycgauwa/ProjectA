using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private GameObject player = null;
    private Vector3 position_offset = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        //メンバー変数playerに、Playerオブジェクトを取得。
        player = GameObject.FindGameObjectWithTag("Player");
        //カメラの位置（this.transform.position）と
        //プレイヤーの位置（player.transform.position）との差分を確保。
        position_offset = player.transform.position - this.transform.position;
    }
    void LateUpdate()
    {
        //カメラの現在位置をnew_positionに取得。
        Vector3 new_position = transform.position;
        // プレイヤーのx座標に差分を足して、変数new_positionのxに代入する。
        new_position.x = player.transform.position.x + position_offset.x;
        //カメラの位置を、新しい位置（new_position）に更新。
        this.transform.position = new_position;
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class cameraManager : MonoBehaviour
{
    public GameObject player; // 追従する対象を決める変数
    public GameObject girl;
    Vector3 pos;              // カメラの初期位置を記憶するための変数
    public static bool playerCamera = true;
    public static bool girlCamera = false;
    public float cameraSize = 0.0f;
    public Camera cam;
    public NotEnter6 notEnter6;

    // Start is called before the first frame update
    void Start()
    {
        pos = Camera.main.gameObject.transform.position;
        cam = GetComponent<Camera>();
        cameraSize = cam.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        cam.orthographicSize = cameraSize;
        if(playerCamera == true)
        {
            Vector3 cameraPos = player.transform.position; // cameraPosという変数を作り、追従する対象の位置を入れる


            cameraPos.x = player.transform.position.x;
            cameraPos.y = player.transform.position.y;   // カメラの縦位置に対象の位置を入れる


            cameraPos.z = -10; // カメラの奥行きの位置に-10を入れる
            Camera.main.gameObject.transform.position = cameraPos; //　カメラの位置に変数cameraPosの位置を入れる

            //プレイヤーがTPしたときカメラもTPする
            //プレイヤーの座標が（67,64,0）の時読み込む
        }
        if(girlCamera == true)
        {
            Vector3 cameraPos = girl.transform.position; // cameraPosという変数を作り、追従する対象の位置を入れる


            cameraPos.x = girl.transform.position.x;
            cameraPos.y = girl.transform.position.y;   // カメラの縦位置に対象の位置を入れる


            cameraPos.z = -10; // カメラの奥行きの位置に-10を入れる
            Camera.main.gameObject.transform.position = cameraPos; //　カメラの位置に変数cameraPosの位置を入れる
        }
        if(notEnter6.cameraSwitch)
        {
            if(cameraSize > 0.5)
            {
                cameraSize -= 0.01f;
            }
        }
        else
        {
            cameraSize = 5f;
        }
    }

}

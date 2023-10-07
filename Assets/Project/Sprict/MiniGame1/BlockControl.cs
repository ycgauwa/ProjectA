using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockControl : MonoBehaviour
{
    public MapCreater map_createor = null;

    void Start()
    {
        //MapCreateorを取得して、メンバー変数map_creatorに保管。
        map_createor = GameObject.Find("GameRoot").GetComponent<MapCreater>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (map_createor.isDelete(this.gameObject))
        {
            GameObject.Destroy(this.gameObject);
        }


    }
}

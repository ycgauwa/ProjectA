using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockControl : MonoBehaviour
{
    public MapCreater map_createor = null;

    void Start()
    {
        //MapCreateor���擾���āA�����o�[�ϐ�map_creator�ɕۊǁB
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ToEvent1 : MonoBehaviour
{
    static bool one = false;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(one);
    }

    // Update is called once per frame
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!one)
        {
            Debug.Log("aaa");
            one = true;
            Debug.Log(one);
            
        }
       
    }
}

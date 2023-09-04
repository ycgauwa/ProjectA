using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void ClickStartButton()
    {
        SceneManager.LoadScene("StartFirst");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

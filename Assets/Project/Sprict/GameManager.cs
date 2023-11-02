using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public PlayerManager playerManager;
    public GameObject enemy;
    public Homing homing;
    public Canvas menuCanvas;
    public Canvas inventryCanvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!menuCanvas.gameObject.activeSelf)
        {
            if(Input.GetKeyUp(KeyCode.Escape))
            {
                Debug.Log("menu");
                Time.timeScale = 0;
                menuCanvas.gameObject.SetActive(true);

            }
        }
        else if(menuCanvas.gameObject.activeSelf)
        {
            if(Input.GetKeyUp(KeyCode.Escape))
            {
                Time.timeScale = 1;
                menuCanvas.gameObject.SetActive(false);
            }
        }
        if(inventryCanvas.gameObject.activeSelf)
        {
            if(Input.GetKeyUp(KeyCode.Escape))
            {
                inventryCanvas.gameObject.SetActive(false);
                menuCanvas.gameObject.SetActive(true);
            }
        }
        //  メニュー画面をESCで呼べる。呼んだあとESCで閉じれる。だけどメニュー画面から
    }
    public void OnClickInventryButton()
    {
        //　インベントリボタンをクリックしたときメニューキャンバスを消してインベントリを呼び出す
        //　インベントリだけ出てるのでESC押したらインベントリを消してメニューを呼べばよい
        menuCanvas.gameObject.SetActive(false);
        inventryCanvas.gameObject.SetActive(true);
    }
}

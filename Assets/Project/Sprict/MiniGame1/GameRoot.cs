using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameRoot : MonoBehaviour
{
    public float step_timer = 0.0f;         //　経過時間を保持
    public static int   score = 0;                 //  スコアを記録
    public Text scoreBoard;                 //　スコアを表示するテキスト
    public Canvas titleCanvas;              //  タイトルを表示するキャンバス

    private void Start()
    {
        Time.timeScale = 0;
    }
    // Update is called once per frame
    void Update()
    {
        step_timer += Time.deltaTime;       //　刻々と経過時間を足していく
        score = (int)step_timer;
        score = score * 100;
        scoreBoard.text = string.Format("Score {0}", score);


    }

    public float getPlayTime()
    {
        float time;
        time = step_timer;
        return (time);                      // 呼び出し元に、経過時間を教えてあげる。
    }
    public void ClickGameStart()
    {
        titleCanvas.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void ClickGameEnd()
    {
        SceneManager.LoadScene("Game");
        Time.timeScale = 1.0f;
    }

    public void ClickGameLoad()
    {
        SceneManager.LoadScene("MiniGame1");
    }
}

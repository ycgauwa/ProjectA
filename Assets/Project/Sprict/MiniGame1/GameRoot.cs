using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameRoot : MonoBehaviour
{
    public float step_timer = 0.0f;         //�@�o�ߎ��Ԃ�ێ�
    public static int   score = 0;                 //  �X�R�A���L�^
    public Text scoreBoard;                 //�@�X�R�A��\������e�L�X�g
    public Canvas titleCanvas;              //  �^�C�g����\������L�����o�X

    private void Start()
    {
        Time.timeScale = 0;
    }
    // Update is called once per frame
    void Update()
    {
        step_timer += Time.deltaTime;       //�@���X�ƌo�ߎ��Ԃ𑫂��Ă���
        score = (int)step_timer;
        score = score * 100;
        scoreBoard.text = string.Format("Score {0}", score);


    }

    public float getPlayTime()
    {
        float time;
        time = step_timer;
        return (time);                      // �Ăяo�����ɁA�o�ߎ��Ԃ������Ă�����B
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

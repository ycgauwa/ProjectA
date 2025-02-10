using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public static Canvas saveCanvas;
    private void Awake()
    {
    }
    public void ClickStartButton()
    {
        SceneManager.LoadScene("Game");
    }
    public void ClickLoadButton()
    {
        saveCanvas.gameObject.SetActive(true);
    }

}

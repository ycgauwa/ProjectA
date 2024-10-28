using System.Collections;
using System.Collections.Generic;
//using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.LowLevel;

public class DemoFinish : MonoBehaviour
{
    public Canvas DemoFinishCanvas;
    public GameObject player;
    public Canvas gameModeCanvas;
    public PlayerMessage playerMessage;
    public string proMessage;
    public string norMessage;
    public GameObject firstSelect2;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        PlayerManager.m_instance.m_speed = 0;
        Time.timeScale = 0.0f;
        DemoFinishCanvas.gameObject.SetActive(true);
    }
    // Start is called before the first frame update
    void Awake()
    {
        gameModeCanvas.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void prologue()
    {
        player.transform.position = new Vector2(30,-35);
        gameModeCanvas.gameObject.SetActive(false);
        EventSystem.current.SetSelectedGameObject(firstSelect2);
        //playerMessage.
    }
    public void normal() 
    {
        player.transform.position = new Vector2(70, -45);
        gameModeCanvas.gameObject.SetActive(false);
        EventSystem.current.SetSelectedGameObject(firstSelect2);
        playerMessage.StartActive = true;
        playerMessage.Demo.gameObject.SetActive(true);
    }
}

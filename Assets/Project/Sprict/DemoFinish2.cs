using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoFinish2 : MonoBehaviour
{
    public NotEnter6 notEnter6;
    public Canvas DemoFinishCanvas;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (notEnter6.choiced == true)
        {
            if (collider.gameObject.tag.Equals("Player"))
            {
                PlayerManager.m_instance.m_speed = 0;
                Time.timeScale = 0.0f;
                DemoFinishCanvas.gameObject.SetActive(true);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirEnemyMess : MonoBehaviour
{
    [SerializeField]
    private List<string> messages2;
    [SerializeField]
    private List<string> names2;
    [SerializeField]
    private List<Sprite> images2;
    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!enemy.activeSelf)
        {
            MessageManager.message_instance.MessageWindowActive(messages2, names2, images2);
            Destroy(this);
        }
    }
}

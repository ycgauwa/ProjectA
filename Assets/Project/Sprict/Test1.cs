using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test1 : MonoBehaviour
{
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    public Canvas window;
    public Text target;
    public Text nameText;
    // Start is called before the first frame update
    private void Awake()
    {
        Debug.Log("Awake");
    }
    void Start()
    {
        Debug.Log("TestStart");
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Debug.Log($"colloder: {collider.gameObject.name} ");
            /*（）の中に引数を入れてあげると実行元のメソッドが渡した変数で処理を行ってくれる。
            ただし、データを渡す側は変数だけでよい*/
            MessageManager.message_instance.MessageWindowActive(messages, names);
        }
       
    }
}

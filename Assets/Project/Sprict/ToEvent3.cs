using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

//using UnityEditor.VersionControl;
using UnityEngine;

public class ToEvent3 : MonoBehaviour
{
    //指定の座標に行ったらカメラが移動し敵を補足
    //そのあとにメッセージウィンドウを表示してボタンを押すたびに
    //効果音を発生 一定の会話まで進むとBGMが鳴り響き追っかけてくる

    public GameObject eventcamera;
    public GameObject enemy;
    public GameObject player;
    //特定のエリアに初めて入りイベントが開始される
    public bool event3flag;

    // メッセージウィンドウ用の変数
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<Sprite> images;
    //[SerializeField]
    //private List<string> messages2;
    //[SerializeField]
    //private List<string> names2;
    //[SerializeField]
    //private List<Sprite> images2;
    public AudioClip chasedBGM;
    public Homing homing;
    public bool firstchased = false;

    public SoundManager soundManager;
    // 効果音
    AudioSource audioSound;
    public AudioClip eatSound;

    // Start is called before the first frame update
    void Start()
    {
        event3flag = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(!event3flag) //フラグが立ってないとき
        {
            if(collider.gameObject.tag.Equals("Player"))
            {
                MessageManager.message_instance.MessageWindowActive(messages, names, images, ct: destroyCancellationToken).Forget();
                soundManager.PlaySe(eatSound);
                event3flag = true; //フラグが立つ
                GameTeleportManager.chasedTime = true;
                homing.enemyEmerge = true; // 敵が出現する
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
        {
            if(!firstchased)
            {
                soundManager.StopSe(eatSound);
                soundManager.PlayBgm(chasedBGM);
                firstchased = true;
            }
        }
    }
}

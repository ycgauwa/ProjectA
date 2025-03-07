using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class NotEnterLadder : MonoBehaviour
{
    //梯子で移動する感じ。最初の敵と会うまで移動できない
    //一番初めと会ってからでメッセージが違う。上るときはギシギシと梯子の音をつける。
    //上る前にメッセージを一番初めにつける感じ。次からメッセージが必要ない感じにする。
    //フラグを回収した後に調べるとまず音とともに移動する。そのあとにメッセージが出てくる。
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<Sprite> images;
    [SerializeField]
    private List<string> messages2;
    [SerializeField]
    private List<string> names2;
    [SerializeField]
    private List<Sprite> images2;
    public GameObject player;
    public ToEvent3 toevent3;
    private bool isContacted = false;
    public SoundManager soundManager;
    public AudioClip ladderSound;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag.Equals("Player"))
        {
            isContacted = true;
            if(gameObject.name == "Ladder1-1") soundManager.PlaySe(ladderSound);
        }
        // イベントが終わった後にTPできるようにしたい
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag.Equals("Player"))
        {
            isContacted = false;
        }
    }
    private async void Update()
    {
        if (isContacted && (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)))
        {
            if (toevent3.event3flag == false) MessageManager.message_instance.MessageWindowActive(messages, names, images, ct: destroyCancellationToken).Forget();
            else if (toevent3.event3flag == true)
            {
                isContacted = false;
                await MessageManager.message_instance.MessageWindowActive(messages2, names2, images2, ct: destroyCancellationToken);
                Homing.m_instance.speed = 0;
                soundManager.PlaySe(ladderSound);
                await SecondHouseManager.secondHouse_instance.Blackout();
                gameObject.name = "Ladder1-1";
                GameManager.m_instance.player.transform.position = GameManager.m_instance.teleportManager.FindTeleportAddress("Ladder1-1").playerPosition;
                GameManager.m_instance.teleportManager.OnPlayerTeleport(GameManager.m_instance.teleportManager.FindTeleportAddress("Ladder1-1"));
                SecondHouseManager.secondHouse_instance.light2D.intensity = 1;
                GameManager.m_instance.stopSwitch = false;
                Homing.m_instance.speed = 2 + GameManager.m_instance.difficultyLevelManager.addEnemySpeed;
            }
        }
    }
}

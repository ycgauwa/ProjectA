using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ImagesMessage : MonoBehaviour
{
    //やりたいこと
    //調べてボタン押したらメッセージが表示される。そのあとに日記が表示されてからメッセージが出る。
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<Sprite> charaImages;
    [SerializeField]
    private List<Sprite> materialImages;
    public Canvas diaryWindow;
    public Canvas window;
    public Text target;
    public Text nameText;
    public GameObject pictureImage;
    public Image materialImage;
    public Image charaImage;
    private IEnumerator coroutine;
    private bool isContacted = false;
    public Homing homing;
    public SoundManager soundManager;
    public PlayerManager playerManager;
    public AudioClip pageSound;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
        {
            isContacted = true;
        }
    }

    // colliderをもつオブジェクトの領域外にでたとき(下記で説明1)
    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
        {
            isContacted = false;
        }
    }
    private void Update()
    {
        if(isContacted == true && coroutine == null && diaryWindow.gameObject.activeInHierarchy == false)
        {
            //話しかける(条件は動的なものと今回のboolのように恒常的なもので分けた方がいい)
            if(Input.GetKeyDown(KeyCode.Return))
            {
                coroutine = WindowAction();
                PlayerManager.m_instance.m_speed = 0;
                // コルーチンの起動(下記説明2)
                StartCoroutine(coroutine);
            }
        }
    }
    protected void showImage(Sprite image)
    {
        materialImage.sprite = image;
    }
    IEnumerator WindowAction()
    {
        MessageManager.message_instance.MessageWindowActive(messages, names, charaImages, ct: destroyCancellationToken).Forget();
        GameManager.m_instance.stopSwitch = true;
        yield return new WaitUntil(() => !MessageManager.message_instance.talking);
        //何回か押すとテキストが消えて、日記の表示がされる。
        diaryWindow.gameObject.SetActive(true);
        pictureImage.gameObject.SetActive(true);

        for(int i = 0; i < materialImages.Count; ++i)
        {
            playerManager.playerstate = PlayerManager.PlayerState.Talk;
            // 1フレーム分 処理を待機(下記説明1)
            yield return null;
            soundManager.PlaySe(pageSound);
            showImage(materialImages[i]);
            yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
        }
        materialImage.sprite = materialImages[0];
        pictureImage.gameObject.SetActive(false);
        diaryWindow.gameObject.SetActive(false);
        playerManager.playerstate = PlayerManager.PlayerState.Idol;
        coroutine = null;
        GameManager.m_instance.stopSwitch = false;
        homing.speed = 2 + GameManager.m_instance.difficultyLevelManager.addEnemySpeed;
        yield break;
    }
}

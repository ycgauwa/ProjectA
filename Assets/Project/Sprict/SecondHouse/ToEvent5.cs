using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class ToEvent5 : MonoBehaviour
{
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
    [SerializeField]
    private List<string> messages3;
    [SerializeField]
    private List<string> names3;
    [SerializeField]
    private List<Sprite> images3;
    public Canvas window;
    public Text target;
    public Text nameText;
    public Image characterImage;
    public float speed;
    private IEnumerator coroutine;
    public GameObject eventcamera;
    public GameObject haru;
    public ToEvent2 event2;
    public Light2D light2D;
    CapsuleCollider2D capsuleCollider;
    private bool phase1;
    private bool event5Start = false;

    //まず晴は本棚と向き合ってる状態なので後ろ向きの状態で晴が気づく。
    //その状態から晴がこちらに近づいてきて会話が始まる。
    //会話が終わった後に晴は「ずっと僕はここにいるから何かあったら報告に来てね！」
    //イベントが終わるときにはフェードインアウトでいい感じにする
    //イベントが終わった時に晴と話せるのだが、その時に選択肢での質問形式にして話せるようにする。
    //また雑談という項目を作成して展開ごとに雑談の内容が変わるようにしたい。
    //イベントの流れ
    //・晴との邂逅
    //・二人での会話
    //・（ここで晴からアイテムも貰う？事も考え中）
    //・動けるようになる→話しかけられるようになる
    private void Start()
    {
        capsuleCollider = haru.GetComponent<CapsuleCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag.Equals("Player") && event5Start == false)
        {
            capsuleCollider.isTrigger = true;
            haru.transform.position = new Vector2(80,75);
            GameManager.m_instance.stopSwitch = true;
            event2.enabled = false;
            coroutine = CreateCoroutine();
            StartCoroutine(coroutine);
        }
    }
    private void FixedUpdate()
    {
        if (eventcamera.transform.position.y < 72 && cameraManager.event5Camera == true)
        {
            eventcamera.transform.position += new Vector3(0f,0.05f,0f);
            //eventcamera.transform.Translate(new Vector3(0.0f, 0.1f, 0.0f * Time.deltaTime * speed));
        }
        if(phase1 == true && event5Start == false)
        {
            if (eventcamera.transform.position.y > 69)
            {
                eventcamera.transform.position += new Vector3(0f, -0.05f, 0f);
            }
            if (haru.transform.position.y > 69)
            {
                haru.transform.position += new Vector3(0f, -0.05f, 0f);
            }
        }

    }
    IEnumerator CreateCoroutine()
    {
        window.gameObject.SetActive(true);
        yield return OnAction();

        Event5Camera();
        yield return new WaitForSeconds(4.0f);
        window.gameObject.SetActive(true);
        yield return OnAction2();

        yield return new WaitForSeconds(4.0f);
        window.gameObject.SetActive(true);
        yield return OnAction3();
    }
    private void Event5Camera()
    {
        cameraManager.playerCamera = false;
        cameraManager.event5Camera = true;
    }
    protected void showMessage(string message, string name, Sprite image)
    {
        target.text = message;
        nameText.text = name;
        characterImage.sprite = image;
    }
    IEnumerator OnAction()
    {
        for (int i = 0; i < messages.Count; ++i)
        {
            yield return null;
            showMessage(messages[i], names[i], images[i]);
            yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
        }
        target.text = "";
        window.gameObject.SetActive(false);
        yield break;
    }
    IEnumerator OnAction2()
    {
        for (int i = 0; i < messages2.Count; ++i)
        {
            yield return null;
            showMessage(messages2[i], names2[i], images2[i]);
            yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
        }
        target.text = "";
        window.gameObject.SetActive(false);
        phase1 = true;
        cameraManager.event5Camera = false;
        yield break;
    }
    IEnumerator OnAction3()
    {
        for (int i = 0; i < messages3.Count; ++i)
        {
            yield return null;
            showMessage(messages3[i], names3[i], images3[i]);
            yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
        }
        target.text = "";
        window.gameObject.SetActive(false);
        StartCoroutine("Sleep");
        yield break;
    }
    private IEnumerator Sleep()
    {
        light2D.intensity = 1.0f;
        GameManager.m_instance.stopSwitch = true;
        while(light2D.intensity > 0.01f)
        {
            light2D.intensity -= 0.007f;
            yield return null; //ここで１フレーム待ってくれてる
        }
        light2D.intensity = 1.0f;
        event5Start = true;
        haru.transform.position = new Vector2(80, 75);
        cameraManager.event5Camera = false;
        cameraManager.playerCamera = true;
        GameManager.m_instance.stopSwitch = false;
        gameObject.SetActive(false);
    }
}

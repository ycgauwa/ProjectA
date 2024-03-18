using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    private bool phase1;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag.Equals("Player"))
        {
            haru.transform.position = new Vector2(79,75);
            PlayerManager.m_instance.m_speed = 0;
            coroutine = CreateCoroutine();
            StartCoroutine(coroutine);
        }
    }
    private void FixedUpdate()
    {
        Debug.Log(cameraManager.playerCamera);
        if (eventcamera.transform.position.y < 72 && cameraManager.event5Camera == true)
        {
            eventcamera.transform.position += new Vector3(0f,0.1f,0f);
            //eventcamera.transform.Translate(new Vector3(0.0f, 0.1f, 0.0f * Time.deltaTime * speed));
        }
        //if (eventcamera.transform.position.y > 72 && phase1 == true)
        //{
        //    if (eventcamera.transform.position.y > 69)
        //    {
        //        eventcamera.transform.Translate(new Vector2(0f, 0.05f * Time.deltaTime * speed));
        //    }
        //    if (haru.transform.position.y > 69)
        //    {
        //        haru.transform.Translate(new Vector2(0f, 0.05f * Time.deltaTime));
        //    }
        //}
        
    }
    IEnumerator CreateCoroutine()
    {
        window.gameObject.SetActive(true);
        yield return OnAction();

        Event5Camera();
        yield return new WaitForSeconds(4.0f);
        yield return OnAction2();
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
        yield break;
    }
}

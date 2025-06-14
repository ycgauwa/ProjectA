using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AnimalsMessages : MonoBehaviour
{
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<Sprite> image;
    [SerializeField]
    private List<string> messages2;
    [SerializeField]
    private List<string> names2;
    [SerializeField]
    private List<Sprite> image2;
    public Canvas window;
    public Text target;
    public Text nameText;
    public Image characterImage;
    private IEnumerator coroutine;
    public bool messageSwitch = false;
    public bool isContacted = false;
    public Canvas Selectwindow;
    public Image selection;
    public bool isOpenSelect = false;
    public ItemDateBase itemDate;
    public AnimalsMessages bear;
    public AnimalsMessages chicken;
    public AnimalsMessages mushroom;
    public Cooktop cooktop;
    public SecondHouseManager secondHouseManager;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
            isContacted = true;
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        messageSwitch = false;
        if(collider.gameObject.tag.Equals("Player"))
            isContacted = false;
    }

    protected void showMessage(string message, string name, Sprite image)
    {
        target.text = message;
        nameText.text = name;
        characterImage.sprite = image;
    }
    IEnumerator OnAction() //このコードきしょいけど一旦放置
    {
        window.gameObject.SetActive(true);
        for(int i = 0; i < messages.Count; ++i)
        {
            yield return null;
            showMessage(messages2[i], names2[i], image2[i]);
            if(i == messages2.Count - 1)
            {
                Selectwindow.gameObject.SetActive(true);
                selection.gameObject.SetActive(true);
                isOpenSelect = true;
                break;
            }
            yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
        }
        yield return new WaitUntil(() => !isOpenSelect);
        target.text = "";
        //セカンドハウスの方でウィンドウ表示させても直後にこっちで消される。
        if (secondHouseManager.window.gameObject.activeSelf)
        {
            yield break;
        }
        else
        {
            window.gameObject.SetActive(false);
        }
        coroutine = null;
        yield break;
    }
    private void Update()
    {
        if(isContacted && messageSwitch == false && (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)))
        {
            if(cooktop.isCooked == true)　//料理後
            {
                messageSwitch = true;
                coroutine = OnAction();
                StartCoroutine(coroutine);
            }
            else
            {
                messageSwitch = true; //料理前
                MessageManager.message_instance.MessageWindowActive(messages, names, image, ct: destroyCancellationToken).Forget();
            }
        }
    }
}

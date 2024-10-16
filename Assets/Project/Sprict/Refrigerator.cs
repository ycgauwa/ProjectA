using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class Refrigerator : MonoBehaviour
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
    [SerializeField]
    private List<string> messages3;
    [SerializeField]
    private List<string> names3;
    [SerializeField]
    private List<Sprite> image3;
    public ItemDateBase itemDateBase;
    public Canvas window;
    public Text target;
    public Text nameText;
    public Text select1;
    public Text select2;
    public Text select3;
    public Image characterImage;
    private IEnumerator coroutine;
    public static bool messageSwitch = false;
    private bool isContacted = false;
    public bool isTaken = false;
    public Canvas Selectwindow;
    public Image selection;
    public bool isOpenSelect = false;
    public Inventry inventry;
    public GameObject firstSelect1;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
        {
            isContacted = true;
        }
    }
    // この状態だと文字や画像は出る。でも画像が閉じれなくなってしまう。後動けない
    //　画像が一周してからもう一度画像を出すために
    private void OnTriggerExit2D(Collider2D collider)
    {
        messageSwitch = false;
        if(collider.gameObject.tag.Equals("Player"))
        {
            isContacted = false;
        }
    }

    protected void showMessage(string message, string name, Sprite image)
    {
        target.text = message;
        nameText.text = name;
        characterImage.sprite = image;
    }

    IEnumerator OnAction()
    {
        window.gameObject.SetActive(true);
        for(int i = 0; i < messages.Count; ++i)
        {
            yield return null;
            // 会話をwindowのtextフィールドに表示
            showMessage(messages[i], names[i], image[i]);
            if(i == messages.Count - 1)
            {
                Selectwindow.gameObject.SetActive(true);
                selection.gameObject.SetActive(true);
                EventSystem.current.SetSelectedGameObject(firstSelect1);
                isOpenSelect = true;
                break;
            }
            yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
        }
        yield return new WaitForSeconds(0.2f);
        yield return new WaitUntil(() => !isOpenSelect);
        target.text = "";
        window.gameObject.SetActive(false);
        coroutine = null;
        yield break;
    }
    private void Update()
    {
        if(isContacted && messageSwitch == false && (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)))
        {
            if(itemDateBase.items[11].checkPossession && itemDateBase.items[12].checkPossession && itemDateBase.items[13].checkPossession == true && isTaken == false)
            {
                messageSwitch = true;
                coroutine = OnAction();
                StartCoroutine(coroutine);
            }
            else if(isTaken == true)
            {
                messageSwitch = true;
                MessageManager.message_instance.MessageWindowActive(messages3, names3, image3);
            }
            else
            {
                messageSwitch = true;
                MessageManager.message_instance.MessageWindowActive(messages2, names2, image2);
            }
        }
    }

    public void RefrigeratorSelection1()
    {
        if(target.text == "「エビチリ似合いそうな良い素材はないかな？」")
        {
            inventry.Add(itemDateBase.items[14]);
            itemDateBase.items[14].checkPossession = true;
            target.text = "鳥の丸焼き用の素材は？";
            select1.text = "レモン";
            select2.text = "オレンジ";
            select3.text = "ライム";
        }
        else if(target.text == "鳥の丸焼き用の素材は？")
        {
            inventry.Add(itemDateBase.items[17]);
            itemDateBase.items[17].checkPossession = true;
            target.text = "魚のソテー用の素材は？";
            select1.text = "赤いソース";
            select2.text = "青いソース";
            select3.text = "黄色いソース";
        }
        else if(target.text == "魚のソテー用の素材は？")
        {
            inventry.Add(itemDateBase.items[20]);
            itemDateBase.items[20].checkPossession = true;
            selection.gameObject.SetActive(false);
            Selectwindow.gameObject.SetActive(false);
            target.text = "これで食材の用意ができた。調理に向かおう";
            isOpenSelect = false;
            isTaken = true;
        }
    }
    public void RefrigeratorSelection2()
    {
        if(target.text == "「エビチリ似合いそうな良い素材はないかな？」")
        {
            inventry.Add(itemDateBase.items[15]);
            itemDateBase.items[15].checkPossession = true;
            target.text = "鳥の丸焼き用の素材は？";
            select1.text = "レモン";
            select2.text = "オレンジ";
            select3.text = "ライム";
        }
        else if(target.text == "鳥の丸焼き用の素材は？")
        {
            inventry.Add(itemDateBase.items[18]);
            itemDateBase.items[18].checkPossession = true;
            target.text = "魚のソテー用の素材は？";
            select1.text = "赤いソース";
            select2.text = "青いソース";
            select3.text = "黄色いソース";
        }
        else if(target.text == "魚のソテー用の素材は？")
        {
            inventry.Add(itemDateBase.items[21]);
            itemDateBase.items[21].checkPossession = true;
            selection.gameObject.SetActive(false);
            Selectwindow.gameObject.SetActive(false);
            target.text = "これで食材の用意ができた。調理に向かおう";
            isOpenSelect = false;
            isTaken = true;
        }
    }
    public void RefrigeratorSelection3()
    {
        if(target.text == "「エビチリ似合いそうな良い素材はないかな？」")
        {
            inventry.Add(itemDateBase.items[16]);
            itemDateBase.items[16].checkPossession = true;
            target.text = "鳥の丸焼き用の素材は？";
            select1.text = "レモン";
            select2.text = "オレンジ";
            select3.text = "ライム";
        }
        else if(target.text == "鳥の丸焼き用の素材は？")
        {
            inventry.Add(itemDateBase.items[19]);
            itemDateBase.items[19].checkPossession = true;
            target.text = "魚のソテー用の素材は？";
            select1.text = "赤いソース";
            select2.text = "青いソース";
            select3.text = "黄色いソース";
        }
        else if(target.text == "魚のソテー用の素材は？")
        {
            inventry.Add(itemDateBase.items[22]);
            itemDateBase.items[22].checkPossession = true;
            selection.gameObject.SetActive(false);
            Selectwindow.gameObject.SetActive(false);
            target.text = "これで食材の用意ができた。調理に向かおう";
            isOpenSelect = false;
            isTaken = true;
        }
    }
}

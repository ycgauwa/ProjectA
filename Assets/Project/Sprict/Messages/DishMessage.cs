using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DishMessage : MonoBehaviour
{
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<Sprite> image;
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
    public Item shrimp;
    public Item chicken;
    public Item fish;
    public GameObject firstSelect;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
            isContacted = true;
    }
    // この状態だと文字や画像は出る。でも画像が閉じれなくなってしまう。後動けない
    //　画像が一周してからもう一度画像を出すために
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
    IEnumerator OnAction()//改善の余地は大ありだけどスルーでもOK
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
                EventSystem.current.SetSelectedGameObject(firstSelect);
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
            messageSwitch = true;
            coroutine = OnAction();
            StartCoroutine(coroutine);
        }
    }

    /*public void DishTaken()
    {
        selection.gameObject.SetActive(false);
        Selectwindow.gameObject.SetActive(false);
        if(dish.name == ("CookedShrimp"))
        {
            Debug.Log("test1");
            shrimp.checkPossession = true;
            inventry.Add(shrimp);
            isOpenSelect = false;
            window.gameObject.SetActive(false);
            dish.SetActive(false);
        }
        //とったアイテムが鳥の丸焼きの時
        else if(dish.name == ("CookedChicken"))
        {
            Debug.Log("test2");
            chicken.checkPossession = true;
            inventry.Add(chicken);
            isOpenSelect = false;
            window.gameObject.SetActive(false);
            dish.SetActive(false);
        }
        else if(dish.name == ("CookedFish"))
        {
            Debug.Log("test3");
            fish.checkPossession = true;
            inventry.Add(fish);
            isOpenSelect = false;
            window.gameObject.SetActive(false);
            dish.SetActive(false);
        }
    }
    public void DishNotTaken()
    {
        selection.gameObject.SetActive(false);
        Selectwindow.gameObject.SetActive(false);
        isOpenSelect = false;
    }*/
}

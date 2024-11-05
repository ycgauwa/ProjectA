using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Cysharp.Threading.Tasks;

public class Cooktop : MonoBehaviour
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
    [SerializeField]
    private List<string> dish1Messages;
    [SerializeField]
    private List<string> dish1Names;
    [SerializeField]
    private List<Sprite> dish1Image;
    [SerializeField]
    private List<string> dish2Messages;
    [SerializeField]
    private List<string> dish2Names;
    [SerializeField]
    private List<Sprite> dish2Image;
    [SerializeField]
    private List<string> dish3Messages;
    [SerializeField]
    private List<string> dish3Names;
    [SerializeField]
    private List<Sprite> dish3Image;
    [SerializeField]
    private List<string> dishFailMessages;
    [SerializeField]
    private List<string> dishFailNames;
    [SerializeField]
    private List<Sprite> dishFailImage;
    [SerializeField]
    private List<string> dishNotMessages;
    [SerializeField]
    private List<string> dishNotNames;
    [SerializeField]
    private List<Sprite> dishNotImage;
    public Canvas window;
    public Text target;
    public Text nameText;
    public TextMeshProUGUI ingredients1Text;
    public TextMeshProUGUI ingredients2Text;
    public TextMeshProUGUI ingredients3Text;
    public Image characterImage;
    public Image cooked;
    public Image interrupt;
    public Image ingredients;
    private IEnumerator coroutine;
    public bool messageSwitch = false;
    public bool isContacted = false;
    public Canvas Selectwindow;
    public Image selection;
    public bool isOpenSelect = false;
    public bool isCooked = false;
    public bool selectedDish1 = false;
    public bool selectedDish2 = false;
    public bool selectedDish3 = false;
    public AudioClip decision;
    public AudioClip cookingMusic;
    public Inventry inventry;
    public ItemDateBase itemDateBase;
    public SoundManager soundManager;
    public Refrigerator refrigerator;
    public GameManager gameManager;
    public GameObject firstSelect;
    private void Start()
    {

    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
        {
            isContacted = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
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
            showMessage(messages[i], names[i], image[i]);
            EventSystem.current.SetSelectedGameObject(firstSelect);
            yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
        }
        Selectwindow.gameObject.SetActive(true);
        cooked.gameObject.SetActive(true);
        selection.gameObject.SetActive(true);
        soundManager.PlayBgm(cookingMusic);
        isOpenSelect = true;
        target.text = "";
        window.gameObject.SetActive(false);
        GameManager.m_instance.stopSwitch = true;
        coroutine = null;
        yield break;
    }
    private void Update()
    {
        if(!cooked.gameObject.activeSelf && isContacted && messageSwitch == false && (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)))
        {
            if(refrigerator.isTaken == true)
            {
                messageSwitch = true;
                coroutine = OnAction();
                StartCoroutine(coroutine);
            }
            else if(isCooked == true)
            {
                messageSwitch = true;
                MessageManager.message_instance.MessageWindowActive(messages3, names3, image3, messageSwitch, ct: destroyCancellationToken).Forget();
            }
            else
            {
                messageSwitch = true;
                MessageManager.message_instance.MessageWindowActive(messages2, names2, image2, messageSwitch, ct: destroyCancellationToken).Forget();
            }
        }

        if(ingredients1Text.text == "使用済み" && ingredients2Text.text == "使用済み" && ingredients3Text.text == "使用済み") isCooked = true;
        
        /*if(cooked.gameObject.activeSelf && !interrupt.gameObject.activeSelf && !ingredients.gameObject.activeSelf)
        {
            if(Input.GetKeyDown("joystick button 1") || Input.GetKeyDown(KeyCode.Escape))
            {
                //中断するかを問う選択肢の出現
                MessageManager.message_instance.MessageWindowActive(messages4, names4, image4);
                interrupt.gameObject.SetActive(true);
                soundManager.PlaySe(gameManager.cancel);
            }
        }
        else if(ingredients.gameObject.activeSelf)
        {
            if(Input.GetKeyDown("joystick button 1") || Input.GetKeyDown(KeyCode.Escape))
            {
                //素材が出てる時に素材ウィンドウを消すメソッド
                ingredients.gameObject.SetActive(false);
                soundManager.PlaySe(gameManager.cancel);
            }
        }*/
    }

    //ボタンのスクリプトの作成をする。必要なスクリプトはinterruptの中断するとしないボタン
    //その次はCookedボタンを押したときにingredientsがでてくるボタン
    public void OnClickinterruptButton()
    {
        soundManager.StopBgm(cookingMusic);
        GameManager.m_instance.stopSwitch = false;
        cooked.gameObject.SetActive(false);
        interrupt.gameObject.SetActive(false);
        selection.gameObject.SetActive(false);
        Selectwindow.gameObject.SetActive(false);
    }
    public void OnClickNotinterruptButton()
    {
        interrupt.gameObject.SetActive(false);
        target.text = "";
        window.gameObject.SetActive(false);
    }
    public void OnClickDish1CookedButton() 
    {
        if (selectedDish2 == true || selectedDish3 == true)
        {
            selectedDish2 = false;
            selectedDish3 = false;
        }
        if(itemDateBase.items[11].checkPossession == true)
        {
            ingredients.gameObject.SetActive(true);
            soundManager.PlaySe(decision);
            selectedDish1 = true;
        }
        if(itemDateBase.items[14].checkPossession == true)
        {
            ingredients1Text.text = "パクチー";
        }
        if(itemDateBase.items[15].checkPossession == true)
        {
            ingredients1Text.text = "ショウガ";
        }
        if(itemDateBase.items[16].checkPossession == true)
        {
            ingredients1Text.text = "ニンニク";
        }
        if(itemDateBase.items[17].checkPossession == true)
        {
            ingredients2Text.text = "レモン";
        }
        if(itemDateBase.items[18].checkPossession == true)
        {
            ingredients2Text.text = "オレンジ";
        }
        if(itemDateBase.items[19].checkPossession == true)
        {
            ingredients2Text.text = "ライム";
        }
        if(itemDateBase.items[20].checkPossession == true)
        {
            ingredients3Text.text = "赤いソース";
        }
        if(itemDateBase.items[21].checkPossession == true)
        {
            ingredients3Text.text = "青いソース";
        }
        if(itemDateBase.items[22].checkPossession == true)
        {
            ingredients3Text.text = "黄色いソース";
        }
    }
    public void OnClickDish2CookedButton()
    {
        if (selectedDish1 == true || selectedDish3 == true)
        {
            selectedDish1 = false;
            selectedDish3 = false;
        }
        if (itemDateBase.items[12].checkPossession == true)
        {
            ingredients.gameObject.SetActive(true);
            soundManager.PlaySe(decision);
            selectedDish2 = true;
        }
        if(itemDateBase.items[14].checkPossession == true)
        {
            ingredients1Text.text = "パクチー";
        }
        if(itemDateBase.items[15].checkPossession == true)
        {
            ingredients1Text.text = "ショウガ";
        }
        if(itemDateBase.items[16].checkPossession == true)
        {
            ingredients1Text.text = "ニンニク";
        }
        if(itemDateBase.items[17].checkPossession == true)
        {
            ingredients2Text.text = "レモン";
        }
        if(itemDateBase.items[18].checkPossession == true)
        {
            ingredients2Text.text = "オレンジ";
        }
        if(itemDateBase.items[19].checkPossession == true)
        {
            ingredients2Text.text = "ライム";
        }
        if(itemDateBase.items[20].checkPossession == true)
        {
            ingredients3Text.text = "赤いソース";
        }
        if(itemDateBase.items[21].checkPossession == true)
        {
            ingredients3Text.text = "青いソース";
        }
        if(itemDateBase.items[22].checkPossession == true)
        {
            ingredients3Text.text = "黄色いソース";
        }
    }
    public void OnClickDish3CookedButton()
    {
        if (selectedDish2 == true || selectedDish1 == true)
        {
            selectedDish2 = false;
            selectedDish1 = false;
        }
        if (itemDateBase.items[13].checkPossession == true)
        {
            ingredients.gameObject.SetActive(true);
            soundManager.PlaySe(decision);
            selectedDish3 = true;
        }
        if(itemDateBase.items[14].checkPossession == true)
        {
            ingredients1Text.text = "パクチー";
        }
        if(itemDateBase.items[15].checkPossession == true)
        {
            ingredients1Text.text = "ショウガ";
        }
        if(itemDateBase.items[16].checkPossession == true)
        {
            ingredients1Text.text = "ニンニク";
        }
        if(itemDateBase.items[17].checkPossession == true)
        {
            ingredients2Text.text = "レモン";
        }
        if(itemDateBase.items[18].checkPossession == true)
        {
            ingredients2Text.text = "オレンジ";
        }
        if(itemDateBase.items[19].checkPossession == true)
        {
            ingredients2Text.text = "ライム";
        }
        if(itemDateBase.items[20].checkPossession == true)
        {
            ingredients3Text.text = "赤いソース";
        }
        if(itemDateBase.items[21].checkPossession == true)
        {
            ingredients3Text.text = "青いソース";
        }
        if(itemDateBase.items[22].checkPossession == true)
        {
            ingredients3Text.text = "黄色いソース";
        }
    }
    public void OnclickDish1IngredientsButton()
    {
        //エビチリ用のボタン。所持している素材によって成功結果が変わる。もしエビチリ選択した状態で正解のエビチリかそれ以外に変化する。
        if(itemDateBase.items[11].checkPossession == true  && selectedDish1 == true && ingredients1Text.text == "パクチー")
        {
            MessageManager.message_instance.MessageWindowActive(dish1Messages, dish1Names, dish1Image, messageSwitch, ct: destroyCancellationToken).Forget();
            ingredients.gameObject.SetActive(false);
            inventry.Delete(itemDateBase.items[11]);
            itemDateBase.items[11].checkPossession = false;
            itemDateBase.items[14].checkPossession = false;
            inventry.Delete(itemDateBase.items[14]);
            inventry.Add(itemDateBase.items[23]);
            itemDateBase.items[23].checkPossession = true;
            selectedDish1 = false;
            ingredients1Text.text = "使用済み";
        }
        else if(selectedDish2 == true || selectedDish3 == true)//エビチリを選択していない状態で他のDishの素材を選択すると素材が適していないと出る
        {
            MessageManager.message_instance.MessageWindowActive(dishNotMessages, dishNotNames, dishNotImage, messageSwitch, ct: destroyCancellationToken).Forget();
        }
        else//エビチリを選んでいるけどそもそも持っている素材が違う→料理は完成するけど失敗した料理になる。
        {
            MessageManager.message_instance.MessageWindowActive(dishFailMessages, dishFailNames, dishFailImage, messageSwitch, ct: destroyCancellationToken).Forget();
            ingredients.gameObject.SetActive(false);
            inventry.Delete(itemDateBase.items[11]);
            itemDateBase.items[11].checkPossession = false;
            if(itemDateBase.items[15].checkPossession == true)
            {
                itemDateBase.items[15].checkPossession = false;
                inventry.Delete(itemDateBase.items[15]);
                inventry.Add(itemDateBase.items[26]);
                itemDateBase.items[26].checkPossession = true;
                selectedDish1 = false;
                ingredients1Text.text = "使用済み";
            }
            else if(itemDateBase.items[16].checkPossession == true)
            {
                itemDateBase.items[16].checkPossession = false;
                inventry.Delete(itemDateBase.items[16]);
                inventry.Add(itemDateBase.items[26]);
                itemDateBase.items[26].checkPossession = true;
                selectedDish1 = false;
                ingredients1Text.text = "使用済み";
            }
        }
    }
    public void OnclickDish2IngredientsButton()
    {
        if(itemDateBase.items[12].checkPossession == true && selectedDish2 == true && ingredients2Text.text == "レモン")
        {
            MessageManager.message_instance.MessageWindowActive(dish2Messages, dish2Names, dish2Image, messageSwitch, ct: destroyCancellationToken).Forget();
            ingredients.gameObject.SetActive(false);
            inventry.Delete(itemDateBase.items[12]);
            itemDateBase.items[12].checkPossession = false;
            itemDateBase.items[17].checkPossession = false;
            inventry.Delete(itemDateBase.items[17]);
            inventry.Add(itemDateBase.items[24]);
            itemDateBase.items[24].checkPossession = true;
            selectedDish2 = false;
            ingredients2Text.text = "使用済み";
        }
        else if(selectedDish1 == true || selectedDish3 == true)
        {
            MessageManager.message_instance.MessageWindowActive(dishNotMessages, dishNotNames, dishNotImage, messageSwitch, ct: destroyCancellationToken).Forget();
        }
        else
        {
            MessageManager.message_instance.MessageWindowActive(dishFailMessages, dishFailNames, dishFailImage, messageSwitch, ct: destroyCancellationToken).Forget();
            ingredients.gameObject.SetActive(false);
            inventry.Delete(itemDateBase.items[12]);
            itemDateBase.items[12].checkPossession = false;
            if(itemDateBase.items[18].checkPossession == true)
            {
                itemDateBase.items[18].checkPossession = false;
                inventry.Delete(itemDateBase.items[18]);
                inventry.Add(itemDateBase.items[27]);
                itemDateBase.items[27].checkPossession = true;
                selectedDish2 = false;
                ingredients2Text.text = "使用済み";
            }
            else if(itemDateBase.items[19].checkPossession == true)
            {
                itemDateBase.items[19].checkPossession = false;
                inventry.Delete(itemDateBase.items[19]);
                inventry.Add(itemDateBase.items[27]);
                itemDateBase.items[27].checkPossession = true;
                selectedDish2 = false;
                ingredients2Text.text = "使用済み";
            }
        }
    }
    public void OnclickDish3IngredientsButton()
    {
        if(itemDateBase.items[13].checkPossession == true && selectedDish3 == true && ingredients3Text.text == "赤いソース")
        {
            MessageManager.message_instance.MessageWindowActive(dish3Messages, dish3Names, dish3Image, messageSwitch, ct: destroyCancellationToken).Forget();
            ingredients.gameObject.SetActive(false);
            inventry.Delete(itemDateBase.items[13]);
            itemDateBase.items[13].checkPossession = false;
            itemDateBase.items[20].checkPossession = false;
            inventry.Delete(itemDateBase.items[20]);
            inventry.Add(itemDateBase.items[25]);
            itemDateBase.items[25].checkPossession = true;
            selectedDish3 = false;
            ingredients3Text.text = "使用済み";
        }
        else if(selectedDish1 == true || selectedDish2 == true)
        {
            MessageManager.message_instance.MessageWindowActive(dishNotMessages, dishNotNames, dishNotImage, messageSwitch, ct: destroyCancellationToken).Forget();
        }
        else
        {
            MessageManager.message_instance.MessageWindowActive(dishFailMessages, dishFailNames, dishFailImage, messageSwitch, ct: destroyCancellationToken).Forget();
            ingredients.gameObject.SetActive(false);
            inventry.Delete(itemDateBase.items[13]);
            itemDateBase.items[13].checkPossession = false;
            if(itemDateBase.items[21].checkPossession == true)
            {
                itemDateBase.items[21].checkPossession = false;
                inventry.Delete(itemDateBase.items[21]);
                inventry.Add(itemDateBase.items[28]);
                itemDateBase.items[28].checkPossession = true;
                selectedDish3 = false;
                ingredients3Text.text = "使用済み";
            }
            else if(itemDateBase.items[22].checkPossession == true)
            {
                itemDateBase.items[22].checkPossession = false;
                inventry.Delete(itemDateBase.items[22]);
                inventry.Add(itemDateBase.items[28]);
                itemDateBase.items[28].checkPossession = true;
                selectedDish3 = false;
                ingredients3Text.text = "使用済み";
            }
        }
    }
}
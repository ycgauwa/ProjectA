using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class SecondHouseManager : MonoBehaviour
{
    [SerializeField]
    private List<string> bearmessages;
    [SerializeField]
    private List<string> bearnames;
    [SerializeField]
    private List<Sprite> bearimage;
    [SerializeField]
    private List<string> bearfailmessages;
    [SerializeField]
    private List<string> bearfailnames;
    [SerializeField]
    private List<Sprite> bearfailimage;
    [SerializeField]
    private List<string> chickenmessages;
    [SerializeField]
    private List<string> chickennames;
    [SerializeField]
    private List<Sprite> chickenimage;
    [SerializeField]
    private List<string> chickenfailmessages;
    [SerializeField]
    private List<string> chickenfailnames;
    [SerializeField]
    private List<Sprite> chickenfailimage;
    [SerializeField]
    private List<string> mushroommessages;
    [SerializeField]
    private List<string> mushroomnames;
    [SerializeField]
    private List<Sprite> mushroomimage;
    [SerializeField]
    private List<string> mushroomfailmessages;
    [SerializeField]
    private List<string> mushroomfailnames;
    [SerializeField]
    private List<Sprite> mushroomfailimage;
    [SerializeField]
    private List<string> keyOpenMessages;
    [SerializeField]
    private List<string> keyOpenNames;
    [SerializeField]
    private List<Sprite> keyOpenImage;
    private IEnumerator coroutine;
    public Canvas window;
    public Text target;
    public Text nameText;
    public Image characterImage;
    private bool bearKey = false;
    private bool chickenKey = false;
    private bool mushroomKey = false;
    public bool secondkey = false;
    public Inventry inventry;
    public ItemDateBase itemDate;
    public DishMessage chickenDish;
    public DishMessage fishDish;
    public DishMessage shrimpDish;
    public AnimalsMessages bear;
    public AnimalsMessages chicken;
    public AnimalsMessages mushroom;
    public Cooktop cooktop;
    public SoundManager soundManager;
    public AudioClip keyOpen;
    //2軒目の選択肢を統括するスクリプト

    private void Update()
    {
        if(chickenKey == true && mushroomKey == true && bearKey == true && secondkey == false)
        {
            //鍵が開いた音
            coroutine = OpenKey();
            StartCoroutine(coroutine);
            //鍵が空いてからメッセージを出す
            secondkey = true;
        }
    }

    protected void showMessage(string message, string name, Sprite image)
    {
        target.text = message;
        nameText.text = name;
        characterImage.sprite = image;
    }
    IEnumerator OpenKey()
    {
        window.gameObject.SetActive(true);
        GameManager.m_instance.stopSwitch = true;
        soundManager.PlaySe(keyOpen);
        yield return new WaitForSeconds(2.0f);
        for(int i = 0; i < keyOpenMessages.Count; ++i)
        {
            yield return null;
            showMessage(keyOpenMessages[i], keyOpenNames[i], keyOpenImage[i]);
            yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
        }
        coroutine = null;
        GameManager.m_instance.stopSwitch = false;
        yield break;
    }
    IEnumerator OnAction2()
    {
        window.gameObject.SetActive(true);
        if (bear.isContacted == true)
        {
            for(int i = 0; i < bearmessages.Count; ++i)
            {
                yield return null;
                showMessage(bearmessages[i], bearnames[i], bearimage[i]);
                yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
            }
        }
        else if(chicken.isContacted == true)
        {
            for(int i = 0; i < chickenmessages.Count; ++i)
            {
                yield return null;
                showMessage(chickenmessages[i], chickennames[i], chickenimage[i]);
                yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
            }
        }
        else if(mushroom.isContacted == true)
        {
            for(int i = 0; i < mushroommessages.Count; ++i)
            {
                yield return null;
                showMessage(mushroommessages[i], mushroomnames[i], mushroomimage[i]);
                yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
            }
        }
        target.text = "";
        window.gameObject.SetActive(false);
        if (bear.isContacted == true) bear.gameObject.SetActive(false);
        else if(chicken.isContacted == true) chicken.gameObject.SetActive(false);
        else if(mushroom.isContacted == true) mushroom.gameObject.SetActive(false);
        coroutine = null;
        yield break;
    }
    IEnumerator OnFailAction()
    {
        if (bear.isContacted == true)
        {
            for (int i = 0; i < bearfailmessages.Count; ++i)
            {
                yield return null;
                showMessage(bearfailmessages[i], bearfailnames[i], bearfailimage[i]);
                yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
            }
        }
        else if (chicken.isContacted == true)
        {
            for (int i = 0; i < chickenfailmessages.Count; ++i)
            {
                yield return null;
                showMessage(chickenfailmessages[i], chickenfailnames[i], chickenfailimage[i]);
                yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
            }
        }
        else if (mushroom.isContacted == true)
        {
            for (int i = 0; i < mushroomfailmessages.Count; ++i)
            {
                yield return null;
                showMessage(mushroomfailmessages[i], mushroomfailnames[i], mushroomfailimage[i]);
                yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
            }
        }
        yield break;
    }
    public void AnimalGiveDish()
    {
        if(bear.isContacted == true)
        {
            bear.selection.gameObject.SetActive(false);
            bear.Selectwindow.gameObject.SetActive(false);
            if(itemDate.items[25].checkPossession == true)
            {//本物持ってた時ドアが空くキーが一個解放する（３つでドアが開く）
                inventry.Delete(itemDate.items[25]);
                itemDate.items[25].checkPossession = false;
                bear.isOpenSelect = false;
                bearKey = true;
                coroutine = OnAction2();
                StartCoroutine(coroutine);
            }
            else if(itemDate.items[28].checkPossession == true)
            {//メッセージが出た後に死ぬ
                inventry.Delete(itemDate.items[28]);
                itemDate.items[28].checkPossession = false;
                bear.isOpenSelect = false;
                coroutine = OnFailAction();
                StartCoroutine(coroutine);
            }
        }
        if(chicken.isContacted == true)
        {
            chicken.selection.gameObject.SetActive(false);
            chicken.Selectwindow.gameObject.SetActive(false);
            if(itemDate.items[24].checkPossession == true)
            {
                inventry.Delete(itemDate.items[24]);
                itemDate.items[24].checkPossession = false;
                chicken.isOpenSelect = false;
                chickenKey = true;
                coroutine = OnAction2();
                StartCoroutine(coroutine);
            }
            else if(itemDate.items[27].checkPossession == true)
            {//メッセージが出た後に死ぬ

            }
        }
        if(mushroom.isContacted == true)
        {
            mushroom.selection.gameObject.SetActive(false);
            mushroom.Selectwindow.gameObject.SetActive(false);
            if(itemDate.items[23].checkPossession == true)
            {
                inventry.Delete(itemDate.items[23]);
                itemDate.items[23].checkPossession = false;
                mushroom.isOpenSelect = false;
                mushroomKey = true;
                coroutine = OnAction2();
                StartCoroutine(coroutine);
            }
            else if(itemDate.items[26].checkPossession == true)
            {//メッセージが出た後に死ぬ

            }
        }
    }
    public void AnimalNotGiveDish()
    {
        if(bear.isContacted == true)
        {
            bear.selection.gameObject.SetActive(false);
            bear.Selectwindow.gameObject.SetActive(false);
            bear.isOpenSelect = false;
        }
        else if(chicken.isContacted == true)
        {
            chicken.selection.gameObject.SetActive(false);
            chicken.Selectwindow.gameObject.SetActive(false);
            chicken.isOpenSelect = false;
        }
        else if(mushroom.isContacted == true)
        {
            mushroom.selection.gameObject.SetActive(false);
            mushroom.Selectwindow.gameObject.SetActive(false);
            mushroom.isOpenSelect = false;
        }
    }
    public void DishTaken()
    {
        if(shrimpDish.isContacted == true)
        {
            shrimpDish.selection.gameObject.SetActive(false);
            shrimpDish.Selectwindow.gameObject.SetActive(false);
            shrimpDish.shrimp.checkPossession = true;
            inventry.Add(shrimpDish.shrimp);
            shrimpDish.isOpenSelect = false;
            shrimpDish.window.gameObject.SetActive(false);
            shrimpDish.dish.SetActive(false);
        }
        //とったアイテムが鳥の丸焼きの時
        else if(chickenDish.isContacted == true)
        {
            chickenDish.selection.gameObject.SetActive(false);
            chickenDish.Selectwindow.gameObject.SetActive(false);
            chickenDish.chicken.checkPossession = true;
            inventry.Add(chickenDish.chicken);
            chickenDish.isOpenSelect = false;
            chickenDish.window.gameObject.SetActive(false);
            chickenDish.dish.SetActive(false);
        }
        else if(fishDish.isContacted == true)
        {
            fishDish.selection.gameObject.SetActive(false);
            fishDish.Selectwindow.gameObject.SetActive(false);
            fishDish.fish.checkPossession = true;
            inventry.Add(fishDish.fish);
            fishDish.isOpenSelect = false;
            fishDish.window.gameObject.SetActive(false);
            fishDish.dish.SetActive(false);
        }
    }
    public void DishNotTaken()
    {
        if(shrimpDish.isContacted == true)
        {
            shrimpDish.selection.gameObject.SetActive(false);
            shrimpDish.Selectwindow.gameObject.SetActive(false);
            shrimpDish.isOpenSelect = false;
        }
        else if(chickenDish.isContacted == true)
        {
            chickenDish.selection.gameObject.SetActive(false);
            chickenDish.Selectwindow.gameObject.SetActive(false);
            chickenDish.isOpenSelect = false;
        }
        else if(fishDish.isContacted == true)
        {
            fishDish.selection.gameObject.SetActive(false);
            fishDish.Selectwindow.gameObject.SetActive(false);
            fishDish.isOpenSelect = false;
        }
    }
}

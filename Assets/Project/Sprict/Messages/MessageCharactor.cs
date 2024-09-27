using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static MessageCharactor;
//using static UnityEditor.Progress;

/**
 * フィールドオブジェクトの基本処理
 */
public class MessageCharactor : MonoBehaviour
{
    // Unityのインスペクタ(UI上)で、前項でつくったオブジェクトをバインドする。
    // （次項 : インスペクタでscriptを追加して、設定をする で説明）
    //　特定のフラグを回収した状態で話しかけると会話内容が変わる。
    //　またインデックスをランダムでとることで会話内容に変化を持たせる。
    
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private string charactername;
    [SerializeField]
    private List<Sprite> images;
    public Canvas window;
    public Text target;
    public Image Chara;
    public Text charaname;
    private Sprite charaImage;
    public Character character;
    private IEnumerator coroutine;
    public NotEnter1 notEnter1;
    public NotEnter4 notEnter4;
    private bool isContacted = false;
    public static bool messageSwitch = false;
    public CharacterItem characterItem;

    private void Start()
    {
        isContacted = false;
        characterItem.thisGameObject = gameObject;
    }

    //プレイヤーが接触する。その時にキャラクターによって呼ぶメソッドを変えたい→メソッドは同じでゲームオブジェクトを
    //取得してそのオブジェクトで条件分岐させる？

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log($"colloder: {gameObject.name}");
        if(collider.gameObject.tag.Equals("Player"))
        {
            coroutine = CreateCoroutine();
            StartCoroutine(coroutine);
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
        {
            isContacted = false;
        }
    }
    public IEnumerator CreateCoroutine()
    {
        // window起動
        window.gameObject.SetActive(true);
        // 抽象メソッド呼び出し 詳細は子クラスで実装
        yield return CharaShowMessage();

        if (!characterItem.selection.gameObject.activeSelf)
        {
            window.gameObject.SetActive(false);
            target.text = "";
        }

        StopCoroutine(coroutine);
        coroutine = null;

    }

    protected void showMessage(string message, string name, Sprite image)
    {
        target.text = message;
        charaname.text = name;
        Chara.sprite = image;
    }

    /*イベントを回収するごとに話す内容を変更する。
    bool変数で条件を変更して話してほしい状況ならそのままShow Message
    違うなら最初の文はContinueで後回しにしてあげる
    会話内容ごとにリストを作ってあげたらいいのでは？
    特定の条件の時にはList1、別の条件ではList2を呼び出すことによってListを呼び出しながらも会話内容を変えることができる
    今悩んでいるのは、foreachを使った場合stringのListは回してくれるけどImageのListはどうなるの？ってはなし
    でもメッセージと画像は一対一対応してるから別にforeachじゃなくてもよくね？*/
    IEnumerator CharaShowMessage()
    {
        // 学校にいる間にしゃべらされる内容
        if(notEnter1.one == false)
        {
            charactername = character.charaName;
            images = character.characterImages1;
            messages = character.messageTexts1;
            int i = 0;
            // 要素の数だけループが行われる。
            foreach (string str in messages)
            {
                yield return null;
                showMessage(str, charactername, images[i]);
                i++;
                if (gameObject.name == "Hosokawa Mitsuki" && i == 3)
                {
                    //こいつの中に初回限定のメッセージ文を（⑤）を入れてあげればいける。
                    characterItem.CharagivedItem();
                }
                yield return new WaitUntil(() => (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)));
            }
            yield break;
        }
        else if(notEnter4.getKey1 == true)//居間に行けるようになってからの内容
        {
            charactername = character.charaName;
            images = character.characterImages3;
            messages = character.messageTexts3;
            int i = 0;
            // 要素の数だけループが行われる。
            foreach(string str in messages)
            {
                yield return null;
                showMessage(str, charactername, images[i]);
                i++;
                yield return new WaitUntil(() => (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)));
            }
            yield break;
        }
        else if(notEnter1.one == true)//バチをとってからの内容
        {
            charactername = character.charaName;
            images = character.characterImages2;
            messages = character.messageTexts2;
            int i = 0;
            // 要素の数だけループが行われる。
            foreach(string str in messages)
            {
                yield return null;
                showMessage(str, charactername, images[i]);
                i++;
                yield return new WaitUntil(() => (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)));
            }
            yield break;
        }
    }
    public void MitsukiCoroutine()
    {
        characterItem.coroutine = characterItem.OnAction();
        StartCoroutine(characterItem.coroutine);
    }
    public void MitsukiItemYes()
    {
        characterItem.soundManager.PlaySe(characterItem.decision);
        characterItem.selection.gameObject.SetActive(false);
        characterItem.Selectwindow.gameObject.SetActive(false);
        characterItem.inventry.Add(characterItem.itemDateBase.items[10]);
        characterItem.givedItem.checkPossession = true;
        characterItem.answer = true;
        characterItem.isOpenSelect = false;
    }
    public void MitsukiItemNo()
    {
        characterItem.soundManager.PlaySe(characterItem.decision);
        characterItem.selection.gameObject.SetActive(false);
        characterItem.Selectwindow.gameObject.SetActive(false);
        characterItem.answer = true;
        characterItem.isOpenSelect = false;
    }
    [System.Serializable]
    public class CharacterItem
    {
        //話しかけたらアイテムがもらえる仕様で、アイテムが所持している場合はもらえない仕様にする。
        //欲を言えばアイテムを持っているときは話す内容が変化してほしい。
        //仕様的に言えば、誰にいつ話しかけたかで、話しかけた後にアイテムをもらうメソッドを起動してほしい
        //条件（誰にOK）（いつのタイミングでOK）（アイテムを持っているかOK）

        public Item givedItem;
        public ItemDateBase itemDateBase;
        public Inventry inventry;
        public Canvas window;
        public Canvas Selectwindow;
        public Text target;
        public Text nameText;
        public Image characterImage;
        public Image selection;
        public SoundManager soundManager;
        public AudioClip decision;
        public bool answer;
        public IEnumerator coroutine;
        public bool isOpenSelect = false;
        public GameObject thisGameObject;
        public static CharacterItem instance;
        [SerializeField]
        private MessageCharactor messageCharactor;
        //やりたいこと、コルーチンを別スクリプトから引っ張ってきて発動する。messagecharactorがnullのままになってる

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }
        public void CharagivedItem()
        {
            if (thisGameObject.name == "Hosokawa Mitsuki")
            {
                if (givedItem.checkPossession == false)
                {
                    messageCharactor.MitsukiCoroutine();
                    Debug.Log("c");
                }
                else if (givedItem.checkPossession == true)
                {
                    return;
                }
            }
        }
        public IEnumerator OnAction()
        {
            window.gameObject.SetActive(true);
            Selectwindow.gameObject.SetActive(true);
            selection.gameObject.SetActive(true);
            isOpenSelect = true;
            yield return new WaitUntil(() => !isOpenSelect);
            Debug.Log("d");
            window.gameObject.SetActive(false);
            coroutine = null;
            yield break;
        }
    }

}
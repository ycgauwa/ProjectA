using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using static MessageCharactor;
using UnityEngine.Rendering.Universal;
using UnityEngine.EventSystems;
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
    public Item bati;
    private IEnumerator coroutine;
    public NotEnter1 notEnter1;
    public NotEnter4 notEnter4;
    private bool isContacted = false;
    public static bool messageSwitch = false;
    public CharacterItem characterItem;
    public SoundManager soundManager;
    public AudioClip decision;
    public AudioClip crisis;
    public PlayableDirector haruTimeline;
    public Canvas Selectwindow;
    public Image selectionPanel;
    public Text selectionText;
    public GameObject firstSelect;
    private bool isOpenSelect = false;
    public int answerNum = 0;//0が答える前1あきらめるな！2もうやめよう
    public Image[] selectionImages = new Image[58];

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
        if (!characterItem.selection.gameObject.activeSelf && characterItem.answerNum != 0)
        {
            target.text = "";
            GameManager.m_instance.ImageErase(Chara);
            window.gameObject.SetActive(false);
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
        // バチを持ってないとき1軒目以降は分岐できず
        if(bati.checkPossession == false)
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
                yield return new WaitUntil(() => (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)));
            }
            if(gameObject.name == "Hosokawa Mitsuki" && characterItem.answerNum == 0)
            {
                //こいつの中に初回限定のメッセージ文を（⑤）を入れてあげればいける。
                characterItem.CharagivedItem();
            }
            else
            {
                target.text = "";
                GameManager.m_instance.ImageErase(Chara);
                window.gameObject.SetActive(false);
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
            target.text = "";
            GameManager.m_instance.ImageErase(Chara);
            window.gameObject.SetActive(false);
            yield break;
        }
        else if(bati.checkPossession == true)//バチをとってからの内容
        {
            charactername = character.charaName;
            images = character.characterImages2;
            if(gameObject.name == "Matiba Haru" && answerNum == 2) images = character.characterImages1;
            if (gameObject.name == "Matiba Haru" && answerNum != 2)
            {
                character.messageTexts2[0] = "「バチ本当に見つかったの！？都市伝説は実在するのかな。」";
                character.messageTexts2[1] = "「やっぱり僕怖くなってきたかも知れない……。ね、ねぇちょっと今回はやめにしない？」";
            }
            messages = character.messageTexts2;
            int i = 0;
            // 要素の数だけループが行われる。
            foreach(string str in messages)
            {
                yield return null;
                showMessage(str, charactername, images[i]);
                i++;
                if (i == messages.Count && gameObject.name == "Matiba Haru" && answerNum == 0)
                {
                    Selectwindow.gameObject.SetActive(true);
                    selectionPanel.gameObject.SetActive(true);
                    EventSystem.current.SetSelectedGameObject(firstSelect);
                    isOpenSelect = true;
                    break;
                }
                yield return new WaitUntil(() => (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)));
            }
            if(gameObject.name == "Hosokawa Mitsuki" && characterItem.answerNum == 0)
            {
                //こいつの中に初回限定のメッセージ文を（⑤）を入れてあげればいける。
                characterItem.CharagivedItem();
            }
            else
            {
                yield return new WaitUntil(() => !isOpenSelect);
                Chara.sprite = null;
                target.text = "";
                GameManager.m_instance.ImageErase(Chara);
                window.gameObject.SetActive(false);
            }
            yield break;
        }
    }
    public IEnumerator HaruSelectionCoroutine()
    {
        answerNum = 2;
        int i = 0;
        while (i <= 58)
        {
            if(i>=0 && i<=9)
            {
                selectionImages[i].gameObject.SetActive(true);
                yield return new WaitForSeconds(0.23f);
            }
            else if (i >= 10 && i <= 26)
            {
                selectionImages[i].gameObject.SetActive(true);
                yield return new WaitForSeconds(0.17f);
            }
            else if (i >= 27 && i <= 57)
            {
                selectionImages[i].gameObject.SetActive(true);
                yield return new WaitForSeconds(0.1f);
            }
            i++;
        }
        character.messageTexts2[0] = "「もう準備が遅いよ……都市伝説の儀式を試すんでしょ？」";
        character.messageTexts2[1] = "「早く行こうよ！もう待ちきれないよー！はやく！はやく！はやク！ハヤク！」";
        selectionText.gameObject.SetActive(true);
        GameManager.m_instance.stopSwitch = true;
        yield return new WaitForSeconds(1f);
        haruTimeline.Play();
        selectionText.gameObject.SetActive(false);
        foreach (Image images in selectionImages)
        {
            images.gameObject.SetActive(false);
        }
        isOpenSelect = false;
        notEnter1.player.gameObject.transform.position = new Vector3(-11, -72, 0);
        yield return new WaitForSeconds(4f);
        soundManager.StopBgm(crisis);
        GameManager.m_instance.stopSwitch = false;
    }
    public IEnumerator MitsukiNotGivedMessage()
    {
        charactername = character.charaName;
        images =character.itemGiveImage2;
        messages = character.itemGiveMessage2;
        int i = 0;
        foreach (string str in messages)
        {
            yield return null;
            showMessage(str, charactername, images[i]);
            i++;
            yield return new WaitUntil(() => (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)));
        }
        target.text = "";
        GameManager.m_instance.ImageErase(Chara);
        window.gameObject.SetActive(false);
        yield break;
    }
    public void HaruSelectionYes()
    {
        if (answerNum == 0)
        {
            soundManager.PlaySe(decision);
            answerNum = 1;
            selectionPanel.gameObject.SetActive(false);
            Selectwindow.gameObject.SetActive(false);
            isOpenSelect = false;
        }
    }
    public void HaruSelectionNo()
    {
        if (answerNum == 0)
        {
            StartCoroutine(HaruSelectionCoroutine());
            soundManager.PlayBgm(crisis);
        }
    }
    public void MitsukiCoroutine()
    {
        window.gameObject.SetActive(true);
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
        characterItem.answerNum = 2;
        characterItem.isOpenSelect = false;
    }
    public void MitsukiItemNo()
    {
        characterItem.soundManager.PlaySe(characterItem.decision);
        characterItem.selection.gameObject.SetActive(false);
        characterItem.Selectwindow.gameObject.SetActive(false);
        characterItem.answerNum = 1;
        characterItem.isOpenSelect = false;
        StartCoroutine("MitsukiNotGivedMessage");
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
        public int answerNum;
        public IEnumerator coroutine;
        public bool isOpenSelect = false;
        public GameObject thisGameObject;
        public GameObject itemFirstSelect;
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
                }
                else if (givedItem.checkPossession == true)
                {
                    return;
                }
            }
        }
        public IEnumerator OnAction()
        {
            messageCharactor.charactername = messageCharactor.character.charaName;
            messageCharactor.images = messageCharactor.character.itemGiveImage1;
            messageCharactor.messages = messageCharactor.character.itemGiveMessage1;
            int i = 0;
            // 要素の数だけループが行われる。
            foreach(string str in messageCharactor.messages)
            {
                yield return null;
                messageCharactor.showMessage(str, messageCharactor.charactername, messageCharactor.images[i]);
                i++;
                yield return new WaitUntil(() => (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)));
            }
            window.gameObject.SetActive(true);
            Selectwindow.gameObject.SetActive(true);
            selection.gameObject.SetActive(true);
            EventSystem.current.SetSelectedGameObject(itemFirstSelect);
            isOpenSelect = true;
            yield return new WaitUntil(() => !isOpenSelect);
            if (answerNum == 2)
            {
                messageCharactor.charactername = messageCharactor.character.charaName;
                messageCharactor.images = messageCharactor.character.itemGivedImage;
                messageCharactor.messages = messageCharactor.character.itemGivedMessage;
                i = 0;
                foreach (string str in messageCharactor.messages)
                {
                    yield return null;
                    messageCharactor.showMessage(str, messageCharactor.charactername, messageCharactor.images[i]);
                    i++;
                    yield return new WaitUntil(() => (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)));
                }
                target.text = "";
                GameManager.m_instance.ImageErase(characterImage);
                window.gameObject.SetActive(false);
            }
            coroutine = null;
            yield break;
        }
    }

}
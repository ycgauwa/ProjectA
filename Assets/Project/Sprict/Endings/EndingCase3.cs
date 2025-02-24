using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.EventSystems;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;
using Cysharp.Threading.Tasks;

public class EndingCase3 : MonoBehaviour
{
    //一軒目のタンスにて起こるエンドのスクリプト
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
    private List<string> hidingMessages;
    [SerializeField]
    private List<string> hidingNames;
    [SerializeField]
    private List<Sprite> hidingImage;
    [SerializeField]
    private List<string> hidingMessages2;
    [SerializeField]
    private List<string> hidingNames2;
    [SerializeField]
    private List<Sprite> hidingImage2;
    [SerializeField]
    private List<string> debuffMessages;
    [SerializeField]
    private List<string> debuffNames;
    [SerializeField]
    private List<Sprite> debuffImage;
    [SerializeField]
    private List<string> exitMessages;
    [SerializeField]
    private List<string> exitNames;
    [SerializeField]
    private List<Sprite> exitImage;
    [SerializeField]
    private List<string> end3Messages;
    [SerializeField]
    private List<string> end3Names;
    [SerializeField]
    private List<Sprite> end3Images;
    [SerializeField]
    private List<string> end4Messages;
    [SerializeField]
    private List<string> end4Names;
    [SerializeField]
    private List<Sprite> end4Images;
    public Canvas window;
    public Canvas end3Canvas;
    public Canvas end4Canvas;
    public Text target;
    public Text nameText;
    public Image characterImage;
    public Light2D light2D;
    private IEnumerator coroutine;
    public bool messageSwitch = false;
    public bool isContacted = false;
    public Canvas Selectwindow;
    public Image selection;
    public Image selection2;
    public Image enemyImage;
    public Image end3Image;
    public Image end4Image;
    public Image end4Image2;
    public Color color;
    public bool isOpenSelect = false;
    private int questionCount = 0;
    private int debuffPercent;
    private int exitPercent;
    public int debuffcount = 0;
    public GameObject enemy;
    public GameObject firstSelect;
    public GameObject firstSelect2;
    public PlayerManager playerManager;
    public Homing homing;
    public GameTeleportManager gameTeleportManager;
    public SoundManager soundManager;
    public AudioClip ending3Sound;
    public AudioClip chestsSound;
    public AudioClip decision;
    public AudioClip heartSound;
    public AudioClip shortnessSound;
    public AudioClip sighSound;
    public AudioClip eatSound;
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
    IEnumerator OnAction()
    {
        window.gameObject.SetActive(true);
        homing.speed = 0;
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
        homing.speed = 2 + GameManager.m_instance.difficultyLevelManager.addEnemySpeed;
        yield break;
    }
    private void Update()
    {
        if(isContacted && messageSwitch == false && (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)))
        {
            //これでも2回目以降の梯子からタンスかくれるは通用しない。
            if(questionCount == 0 && homing.enemyCount > 0)
            {
                messageSwitch = true;
                coroutine = OnAction();
                StartCoroutine(coroutine);
            }
            else
            {
                messageSwitch = true;
                MessageManager.message_instance.MessageWindowActive(messages2, names2, image2, ct: destroyCancellationToken).Forget();
            }
        }
    }
    //何回も出てくる選択肢、何回目かによって条件分岐をつけたい。
    public void SelectedExiting()
    {//Yesの選択肢
        Selectwindow.gameObject.SetActive(false);
        selection2.gameObject.SetActive(false);
        coroutine = ExitToDeath();
        StartCoroutine(coroutine);
    }
    public void SelectedRemaing()
    {//Noの選択肢
        if(questionCount >= 1) soundManager.StopBgm(shortnessSound);
        target.text = "";
        window.gameObject.SetActive(false);
        coroutine = null;
        Selectwindow.gameObject.SetActive(false);
        selection2.gameObject.SetActive(false);
        ++questionCount;
        debuffPercent = Random.Range(1, 101);
        exitPercent = Random.Range(1, 101);
        coroutine = Hiding();
        StartCoroutine(coroutine);
    }
    public void HideIn()
    {
        //隠れる選択肢
        Selectwindow.gameObject.SetActive(false);
        selection.gameObject.SetActive(false);
        soundManager.PlaySe(decision);
        StartCoroutine("Blackout");
    }
    public void NotHide() 
    {
        Selectwindow.gameObject.SetActive(false);
        selection.gameObject.SetActive(false);
        isOpenSelect = false;
    }
    private IEnumerator Blackout()
    {
        light2D.intensity = 1.0f;
        GameManager.m_instance.stopSwitch = true;
        soundManager.PlaySe(chestsSound);
        while (light2D.intensity > 0.01f)
        {
            light2D.intensity -= 0.006f;
            yield return null; //ここで１フレーム待ってくれてる
        }
        GameManager.m_instance.stopSwitch = false;
        soundManager.StopSe(chestsSound);
        coroutine = Hiding();
        StartCoroutine(coroutine);
    }
    private IEnumerator Hiding()
    {
        Debug.Log(exitPercent);
        Debug.Log(debuffPercent);
        if (questionCount == 0)
        {
            for (int i = 0; i < hidingMessages.Count; ++i)
            {
                window.gameObject.SetActive(true);
                yield return null;
                // 会話をwindowのtextフィールドに表示
                showMessage(hidingMessages[i], hidingNames[i], hidingImage[i]);
                if (i == hidingMessages.Count - 1)
                {
                    Selectwindow.gameObject.SetActive(true);
                    selection2.gameObject.SetActive(true);
                    soundManager.PlayBgm(heartSound);
                    soundManager.StopBgm(Homing.m_instance.chasedBGM);
                    EventSystem.current.SetSelectedGameObject(firstSelect2);
                    isOpenSelect = true;
                    break;
                }
                yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
            }
            yield return new WaitForSeconds(0.2f);
            yield return new WaitUntil(() => !isOpenSelect);
            yield break;
        }
        if (questionCount == 1)
        {
            soundManager.PlayBgm(shortnessSound);
            yield return new WaitForSeconds(2f);
            if (exitPercent <= 20)
            {
                yield return ExitEvent();
                yield break;
            }
            else if(debuffPercent <= 3 + GameManager.m_instance.difficultyLevelManager.hideDifficultyFactor)
            {
                yield return debuffEvent();
            }
            hidingMessages[0] = "（まだ、外に気配がする。……さすがに狭苦しいな）";
            // 会話をwindowのtextフィールドに表示
            window.gameObject.SetActive(true);
            showMessage(hidingMessages[0], hidingNames[0], hidingImage[0]);
            Selectwindow.gameObject.SetActive(true);
            selection2.gameObject.SetActive(true);
            EventSystem.current.SetSelectedGameObject(firstSelect2);
            isOpenSelect = true;
            yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
            yield return new WaitForSeconds(0.2f);
            yield return new WaitUntil(() => !isOpenSelect);
            yield break;
        }
        if (questionCount == 2)
        {
            if (exitPercent <= 20)
            {
                yield return ExitEvent();
                yield break;
            }
            else if (debuffPercent <= 7 + GameManager.m_instance.difficultyLevelManager.hideDifficultyFactor)
            {
                yield return debuffEvent();
                if (debuffcount == 3) yield break;
            }
            soundManager.PlayBgm(shortnessSound);
            yield return new WaitForSeconds(2f);
            hidingMessages[0] = "（まだ、外に気配がする。……すこし息苦しさを感じてきたな）";
            // 会話をwindowのtextフィールドに表示
            window.gameObject.SetActive(true);
            showMessage(hidingMessages[0], hidingNames[0], hidingImage[0]);
            Selectwindow.gameObject.SetActive(true);
            selection2.gameObject.SetActive(true);
            EventSystem.current.SetSelectedGameObject(firstSelect2);
            isOpenSelect = true;
            yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
            yield return new WaitForSeconds(0.2f);
            yield return new WaitUntil(() => !isOpenSelect);
            yield break;
        }
        if (questionCount == 3)
        {
            if (exitPercent <= 20)
            {
                yield return ExitEvent();
                yield break;
            }
            else if (debuffPercent <= 13 + GameManager.m_instance.difficultyLevelManager.hideDifficultyFactor)
            {
                yield return debuffEvent();
                if (debuffcount == 3) yield break;
            }
            soundManager.PlayBgm(shortnessSound);
            yield return new WaitForSeconds(2f);
            hidingMessages[0] = "（まだ、外に気配がする。ハァ……ハァ……いつになったら出れるんだ。）";
            // 会話をwindowのtextフィールドに表示
            window.gameObject.SetActive(true);
            showMessage(hidingMessages[0], hidingNames[0], hidingImage[0]);
            Selectwindow.gameObject.SetActive(true);
            selection2.gameObject.SetActive(true);
            EventSystem.current.SetSelectedGameObject(firstSelect2);
            isOpenSelect = true;
            yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
            yield return new WaitForSeconds(0.2f);
            yield return new WaitUntil(() => !isOpenSelect);
            yield break;
        }
        if (questionCount == 4)
        {
            if (exitPercent <= 20)
            {
                yield return ExitEvent();
                yield break;
            }
            else if (debuffPercent <= 20 + GameManager.m_instance.difficultyLevelManager.hideDifficultyFactor)
            {
                yield return debuffEvent();
                if (debuffcount == 3) yield break;
            }
            soundManager.PlayBgm(shortnessSound);
            yield return new WaitForSeconds(2f);
            hidingMessages[0] = "（まだ、外に気配がする。さすがに息が苦しくなってきた。）";
            // 会話をwindowのtextフィールドに表示
            window.gameObject.SetActive(true);
            showMessage(hidingMessages[0], hidingNames[0], hidingImage[0]);
            Selectwindow.gameObject.SetActive(true);
            selection2.gameObject.SetActive(true);
            EventSystem.current.SetSelectedGameObject(firstSelect2);
            isOpenSelect = true;
            yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
            yield return new WaitForSeconds(0.2f);
            yield return new WaitUntil(() => !isOpenSelect);
            yield break;
        }
        if (questionCount == 5)
        {
            if (exitPercent <= 30)
            {
                yield return ExitEvent();
                yield break;
            }
            else if (debuffPercent <= 30 + GameManager.m_instance.difficultyLevelManager.hideDifficultyFactor)
            {
                yield return debuffEvent();
                if (debuffcount == 3) yield break;
            }
            soundManager.PlayBgm(sighSound);
            yield return new WaitForSeconds(2f);
            hidingMessages[0] = "（まだ、外に気配がする。まずい、そろそろ出ないと息苦しさで倒れそうだ）";
            // 会話をwindowのtextフィールドに表示
            window.gameObject.SetActive(true);
            showMessage(hidingMessages[0], hidingNames[0], hidingImage[0]);
            Selectwindow.gameObject.SetActive(true);
            selection2.gameObject.SetActive(true);
            EventSystem.current.SetSelectedGameObject(firstSelect2);
            isOpenSelect = true;
            yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
            yield return new WaitForSeconds(0.2f);
            yield return new WaitUntil(() => !isOpenSelect);
            target.text = "";
            window.gameObject.SetActive(false);
            coroutine = null;
            yield break;
        }
        if (questionCount >= 6)
        {
            if (exitPercent <= 40)
            {
                yield return ExitEvent();
                yield break;
            }
            else if (debuffPercent <= 40 + GameManager.m_instance.difficultyLevelManager.hideDifficultyFactor)
            {
                yield return debuffEvent();
                if (debuffcount == 3) yield break;
            }
            yield return new WaitForSeconds(2f);
            hidingMessages[0] = "（いつまであいつはいるんだ！？まさか出られないのを知って遊んでるのか！？）";
            // 会話をwindowのtextフィールドに表示
            window.gameObject.SetActive(true);
            showMessage(hidingMessages[0], hidingNames[0], hidingImage[0]);
            Selectwindow.gameObject.SetActive(true);
            selection2.gameObject.SetActive(true);
            EventSystem.current.SetSelectedGameObject(firstSelect2);
            isOpenSelect = true;
            yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
            yield return new WaitForSeconds(0.2f);
            yield return new WaitUntil(() => !isOpenSelect);
            target.text = "";
            window.gameObject.SetActive(false);
            coroutine = null;
            yield break;
        }

    }
    private IEnumerator debuffEvent()
    {
        //debuffカウント１でダッシュが出来ない、2で歩く速度減少3で死ぬ
        ++debuffcount;
        if (debuffcount == 1)
        {
            playerManager.playercondition = PlayerManager.PlayerCondition.Suffocation;
            for (int i = 0; i < debuffMessages.Count; ++i)
            {
                window.gameObject.SetActive(true);
                yield return null;
                // 会話をwindowのtextフィールドに表示
                showMessage(debuffMessages[i], debuffNames[i], debuffImage[i]);
                yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
            }
            coroutine = null;
        }
        if (debuffcount == 2)
        {
            playerManager.playercondition = PlayerManager.PlayerCondition.Suffocation2;
            for (int i = 0; i < debuffMessages.Count; ++i)
            {
                debuffMessages[0] = "「ハァハァ……。不味い、息が少ししか出来ない。なんだこれは……？落ち着け……！焦るな！」";
                debuffMessages[1] = "状態異常　酸欠Ⅱとなりました。健康状態に戻るまで歩行速度が低下し、ダッシュが出来なくなります。";
                window.gameObject.SetActive(true);
                yield return null;
                // 会話をwindowのtextフィールドに表示
                showMessage(debuffMessages[i], debuffNames[i], debuffImage[i]);
                yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
            }
            coroutine = null;

        }
        if (debuffcount == 3)
        {
            playerManager.playercondition = PlayerManager.PlayerCondition.Suffocation3;
            for (int i = 0; i < debuffMessages.Count; ++i)
            {
                debuffMessages[0] = "「息が……！酸素が吸えない！短時間でこんなことになるのか！？不味い……頭が……真っ白に…。」";
                debuffMessages[1] = "状態異常　酸欠Ⅲとなりました。あなたは呼吸が出来なくなりました。";
                window.gameObject.SetActive(true);
                yield return null;
                showMessage(debuffMessages[i], debuffNames[i], debuffImage[i]);
                yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
            }
            window.gameObject.SetActive(false);
            coroutine = null;
            Selectwindow.gameObject.SetActive(false);
            selection2.gameObject.SetActive(false);
            StartCoroutine("Suffocation");
        }
        yield break;
    }
    private IEnumerator Suffocation()
    {
        GameManager.m_instance.stopSwitch = true;
        soundManager.StopBgm(shortnessSound);
        soundManager.StopBgm(heartSound);
        soundManager.PlayBgm(ending3Sound);
        for (int i = 0; i < end3Messages.Count; ++i)
        {
            window.gameObject.SetActive(true);
            yield return null;
            // 会話をwindowのtextフィールドに表示
            showMessage(end3Messages[i], end3Names[i], end3Images[i]);
            yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
        }
        target.text = "";
        window.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        end3Canvas.gameObject.SetActive(true);
        isContacted = false;
        yield break;
    }
    public void OnclickEnd3Retry()
    {
        end3Canvas.gameObject.SetActive(false);
        light2D.intensity = 1.0f;
        soundManager.StopBgm(ending3Sound);
        soundManager.StopBgm(shortnessSound);
        soundManager.StopBgm(sighSound);
        GameManager.m_instance.stopSwitch = false;
        GameManager.m_instance.player.transform.position = new Vector2(69, -46);
        EndingGalleryManager.m_gallery.endingGallerys[2].sprite = end3Image.sprite;
        EndingGalleryManager.m_gallery.endingFlag[2] = true;
        homing.speed = 2 + GameManager.m_instance.difficultyLevelManager.addEnemySpeed;
        gameTeleportManager.StopChased();
        homing.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
    public void OnclickEnd4Retry()
    {
        end4Image.gameObject.SetActive(false);
        end4Canvas.gameObject.SetActive(false);
        light2D.intensity = 1.0f;
        soundManager.StopSe(eatSound);
        GameManager.m_instance.stopSwitch = false;
        GameManager.m_instance.player.transform.position = new Vector2(69, -46);
        EndingGalleryManager.m_gallery.endingGallerys[3].sprite = end4Image.sprite;
        EndingGalleryManager.m_gallery.endingFlag[3] = true;
        homing.speed = 2 + GameManager.m_instance.difficultyLevelManager.addEnemySpeed;
        gameTeleportManager.StopChased();
        homing.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
    private IEnumerator ExitEvent()
    {
        soundManager.StopBgm(shortnessSound);
        soundManager.StopBgm(sighSound);
        for (int i = 0; i < exitMessages.Count; ++i)
        {
            window.gameObject.SetActive(true);
            yield return null;
            // 会話をwindowのtextフィールドに表示
            showMessage(exitMessages[i], exitNames[i], exitImage[i]);
            if(i == exitMessages.Count -1)
                soundManager.PlaySe(chestsSound);
            yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
        }
        soundManager.StopBgm(heartSound);
        gameTeleportManager.StopChased();
        while (light2D.intensity < 1f)
        {
            light2D.intensity += 0.01f;
            yield return null; //ここで１フレーム待ってくれてる
        }
        messages2[0] = "「なんとか生きて戻れたな。あんな思いするのはもうこりごりだ。」";
        messages2[1] = "「にしても、タンスに張り付いてたみたいなこと言っていたが……もし苦しさに負けて開けていたらと考えると恐ろしいな。」";
        for (int i = 0; i < messages2.Count; ++i)
        {
            window.gameObject.SetActive(true);
            yield return null;
            // 会話をwindowのtextフィールドに表示
            showMessage(messages2[i], names2[i], image2[i]);
            yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
        }
        target.text = "";
        window.gameObject.SetActive(false);
        coroutine = null;
        homing.speed = 2 + GameManager.m_instance.difficultyLevelManager.addEnemySpeed;
        yield break;
    }
    public IEnumerator ExitToDeath()
    {
        //画像が出て死にます。
        soundManager.StopBgm(shortnessSound);
        soundManager.StopBgm(sighSound);
        for (int i = 0; i < end4Messages.Count; ++i)
        {
            window.gameObject.SetActive(true);
            yield return null;
            showMessage(end4Messages[i], end4Names[i], end4Images[i]);
            yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
        }
        soundManager.StopBgm(heartSound);
        soundManager.PlaySe(chestsSound);
        color = enemyImage.GetComponent<Image>().color;
        end4Canvas.gameObject.SetActive(true);
        enemyImage.gameObject.SetActive(true);
        color.a = 0f;
        enemyImage.GetComponent<Image>().color = color;
        while (color.a < 1)
        {
            color.a += 0.006f;
            enemyImage.color = color;
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        end4Messages[0] = "……はは。嘘だろ";
        end4Messages[1] = "きっと悪い夢だ。こんな悪夢覚めなきゃおかしいな。目を瞑ったらきっと……。";
        for (int i = 0; i < end4Messages.Count; ++i)
        {
            window.gameObject.SetActive(true);
            yield return null;
            showMessage(end4Messages[i], end4Names[i], end4Images[i]);
            yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
        }
        while (color.a > 0.01f)
        {
            color.a -= 0.007f;
            enemyImage.color = color;
            yield return null;
        }
        //食われる音。ここからエンディング画像出す
        soundManager.PlaySe(eatSound);
        yield return new WaitForSeconds(2f);
        end4Image.gameObject.SetActive(true);
        target.text = "";
        enemyImage.gameObject.SetActive(false);
        window.gameObject.SetActive(false);
        yield break;
    }
}

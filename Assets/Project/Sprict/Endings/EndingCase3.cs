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
    //�ꌬ�ڂ̃^���X�ɂċN����G���h�̃X�N���v�g
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
    // ���̏�Ԃ��ƕ�����摜�͏o��B�ł��摜������Ȃ��Ȃ��Ă��܂��B�㓮���Ȃ�
    //�@�摜��������Ă��������x�摜���o�����߂�
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
            // ��b��window��text�t�B�[���h�ɕ\��
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
            //����ł�2��ڈȍ~�̒�q����^���X�������͒ʗp���Ȃ��B
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
    //������o�Ă���I�����A����ڂ��ɂ���ď���������������B
    public void SelectedExiting()
    {//Yes�̑I����
        Selectwindow.gameObject.SetActive(false);
        selection2.gameObject.SetActive(false);
        coroutine = ExitToDeath();
        StartCoroutine(coroutine);
    }
    public void SelectedRemaing()
    {//No�̑I����
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
        //�B���I����
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
            yield return null; //�����łP�t���[���҂��Ă���Ă�
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
                // ��b��window��text�t�B�[���h�ɕ\��
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
            hidingMessages[0] = "�i�܂��A�O�ɋC�z������B�c�c�������ɋ��ꂵ���ȁj";
            // ��b��window��text�t�B�[���h�ɕ\��
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
            hidingMessages[0] = "�i�܂��A�O�ɋC�z������B�c�c���������ꂵ���������Ă����ȁj";
            // ��b��window��text�t�B�[���h�ɕ\��
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
            hidingMessages[0] = "�i�܂��A�O�ɋC�z������B�n�@�c�c�n�@�c�c���ɂȂ�����o���񂾁B�j";
            // ��b��window��text�t�B�[���h�ɕ\��
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
            hidingMessages[0] = "�i�܂��A�O�ɋC�z������B�������ɑ����ꂵ���Ȃ��Ă����B�j";
            // ��b��window��text�t�B�[���h�ɕ\��
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
            hidingMessages[0] = "�i�܂��A�O�ɋC�z������B�܂����A���낻��o�Ȃ��Ƒ��ꂵ���œ|�ꂻ�����j";
            // ��b��window��text�t�B�[���h�ɕ\��
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
            hidingMessages[0] = "�i���܂ł����͂���񂾁I�H�܂����o���Ȃ��̂�m���ėV��ł�̂��I�H�j";
            // ��b��window��text�t�B�[���h�ɕ\��
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
        //debuff�J�E���g�P�Ń_�b�V�����o���Ȃ��A2�ŕ������x����3�Ŏ���
        ++debuffcount;
        if (debuffcount == 1)
        {
            playerManager.playercondition = PlayerManager.PlayerCondition.Suffocation;
            for (int i = 0; i < debuffMessages.Count; ++i)
            {
                window.gameObject.SetActive(true);
                yield return null;
                // ��b��window��text�t�B�[���h�ɕ\��
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
                debuffMessages[0] = "�u�n�@�n�@�c�c�B�s�����A�������������o���Ȃ��B�Ȃ񂾂���́c�c�H���������c�c�I�ł�ȁI�v";
                debuffMessages[1] = "��Ԉُ�@�_���U�ƂȂ�܂����B���N��Ԃɖ߂�܂ŕ��s���x���ቺ���A�_�b�V�����o���Ȃ��Ȃ�܂��B";
                window.gameObject.SetActive(true);
                yield return null;
                // ��b��window��text�t�B�[���h�ɕ\��
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
                debuffMessages[0] = "�u�����c�c�I�_�f���z���Ȃ��I�Z���Ԃł���Ȃ��ƂɂȂ�̂��I�H�s�����c�c�����c�c�^�����Ɂc�B�v";
                debuffMessages[1] = "��Ԉُ�@�_���V�ƂȂ�܂����B���Ȃ��͌ċz���o���Ȃ��Ȃ�܂����B";
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
            // ��b��window��text�t�B�[���h�ɕ\��
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
            // ��b��window��text�t�B�[���h�ɕ\��
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
            yield return null; //�����łP�t���[���҂��Ă���Ă�
        }
        messages2[0] = "�u�Ȃ�Ƃ������Ė߂ꂽ�ȁB����Ȏv������̂͂������育�肾�B�v";
        messages2[1] = "�u�ɂ��Ă��A�^���X�ɒ���t���Ă��݂����Ȃ��ƌ����Ă������c�c�����ꂵ���ɕ����ĊJ���Ă�����ƍl����Ƌ��낵���ȁB�v";
        for (int i = 0; i < messages2.Count; ++i)
        {
            window.gameObject.SetActive(true);
            yield return null;
            // ��b��window��text�t�B�[���h�ɕ\��
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
        //�摜���o�Ď��ɂ܂��B
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
        end4Messages[0] = "�c�c�͂́B�R����";
        end4Messages[1] = "�����ƈ��������B����Ȉ����o�߂Ȃ��Ⴈ�������ȁB�ڂ��҂����炫���Ɓc�c�B";
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
        //�H���鉹�B��������G���f�B���O�摜�o��
        soundManager.PlaySe(eatSound);
        yield return new WaitForSeconds(2f);
        end4Image.gameObject.SetActive(true);
        target.text = "";
        enemyImage.gameObject.SetActive(false);
        window.gameObject.SetActive(false);
        yield break;
    }
}

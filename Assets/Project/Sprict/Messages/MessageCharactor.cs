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
 * �t�B�[���h�I�u�W�F�N�g�̊�{����
 */
public class MessageCharactor : MonoBehaviour
{
    // Unity�̃C���X�y�N�^(UI��)�ŁA�O���ł������I�u�W�F�N�g���o�C���h����B
    // �i���� : �C���X�y�N�^��script��ǉ����āA�ݒ������ �Ő����j
    //�@����̃t���O�����������ԂŘb��������Ɖ�b���e���ς��B
    //�@�܂��C���f�b�N�X�������_���łƂ邱�Ƃŉ�b���e�ɕω�����������B
    
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
    private bool seiIsContacted = false;
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
    public int answerNum = 0;//0��������O1������߂�ȁI2������߂悤
    public Image[] selectionImages = new Image[58];

    private void Start()
    {
        isContacted = false;
        characterItem.thisGameObject = gameObject;
        
    }

    //�v���C���[���ڐG����B���̎��ɃL�����N�^�[�ɂ���ČĂԃ��\�b�h��ς����������\�b�h�͓����ŃQ�[���I�u�W�F�N�g��
    //�擾���Ă��̃I�u�W�F�N�g�ŏ������򂳂���H

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
            isContacted = true;
        else if(collider.gameObject.tag.Equals("Seiitirou"))
            seiIsContacted = true;
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
            isContacted = false;
        else if(collider.gameObject.tag.Equals("Seiitirou"))
            seiIsContacted = false;
    }
    private void Update()
    {
        if (isContacted && (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)))
        {
            isContacted = false;
            coroutine = CreateCoroutine();
            StartCoroutine(coroutine);
        }
    }
    public IEnumerator CreateCoroutine()
    {
        window.gameObject.SetActive(true);
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

    /*�C�x���g��������邲�Ƃɘb�����e��ύX����B
    bool�ϐ��ŏ�����ύX���Ęb���Ăق����󋵂Ȃ炻�̂܂�Show Message
    �Ⴄ�Ȃ�ŏ��̕���Continue�Ō�񂵂ɂ��Ă�����
    ��b���e���ƂɃ��X�g������Ă������炢���̂ł́H
    ����̏����̎��ɂ�List1�A�ʂ̏����ł�List2���Ăяo�����Ƃɂ����List���Ăяo���Ȃ������b���e��ς��邱�Ƃ��ł���
    ���Y��ł���̂́Aforeach���g�����ꍇstring��List�͉񂵂Ă���邯��Image��List�͂ǂ��Ȃ�́H���Ă͂Ȃ�
    �ł����b�Z�[�W�Ɖ摜�͈�Έ�Ή����Ă邩��ʂ�foreach����Ȃ��Ă��悭�ˁH
    1��22���ǋL
    ��̊K�w�ŃC�x���g���Ƃ̃t���O�Ői�s�x�ɂ�郁�b�Z�[�W�Ǘ��B����{�I�ɐ�̓��e�قǏサ�Ƃ��B
    ���̃X�C�b�`���ōD���x�ɂ��ω��������b�Z�[�W���̊Ǘ��B
     */
    IEnumerator CharaShowMessage()
    {
        if(notEnter4.getKey1 == true)//���Ԃɍs����悤�ɂȂ��Ă���̓��e
        {
            charactername = character.charaName;
            images = character.characterImages3;
            messages = character.messageTexts3;
            yield return CharacterMessageActive(messages, charactername, images);
            yield break;
        }
        if(bati.checkPossession == true)//�o�`���Ƃ��Ă���̓��e
        {
            charactername = character.charaName;
            images = character.characterImages2;
            if(gameObject.name == "Matiba Haru" && answerNum == 2) images = character.characterImages1;
            if(gameObject.name == "Matiba Haru" && answerNum != 2)
            {
                character.messageTexts2[0] = "�u�o�`�{���Ɍ��������́I�H�s�s�`���͎��݂���̂��ȁB�v";
                character.messageTexts2[1] = "�u����ς�l�|���Ȃ��Ă��������m��Ȃ��c�c�B�ˁA�˂�������ƍ���͂�߂ɂ��Ȃ��H�v";
            }
            messages = character.messageTexts2;
            
            int i = 0;
            // �v�f�̐��������[�v���s����B
            foreach(string str in messages)
            {
                yield return null;
                showMessage(str, charactername, images[i]);
                i++;
                if(i == messages.Count && gameObject.name == "Matiba Haru" && answerNum == 0)
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
                //�����̒��ɏ������̃��b�Z�[�W�����i�D�j�����Ă�����΂�����B
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
        else if(bati.checkPossession == false)
        {
            charactername = character.charaName;
            switch (character.FavorabilityCount)
            {
                case >80:
                    Debug.Log("81�ȏ�");
                    images = character.characterImages1;
                    messages = character.messageTexts1;
                    yield return CharacterMessageActive(messages, charactername, images);
                    yield break;
                case >60:
                    Debug.Log("61");
                    images = character.characterImages1;
                    messages = character.highAffinityMessages;
                    yield return CharacterMessageActive(messages, charactername, images);
                    yield break;
                case >40:
                    Debug.Log("41");
                    images = character.characterImages1;
                    messages = character.moderateAffinityMessages;
                    yield return CharacterMessageActive(messages, charactername, images);
                    yield break;
                case >20:
                    Debug.Log("21");
                    break;
                case >0:
                    Debug.Log("1");
                    break;
                case 0:
                    Debug.Log("0");
                    break;


                default:
                    break;
            }
        }
    }
    // �L�����N�^�[�̃��b�Z�[�W���̃��\�b�h�B
    public IEnumerator CharacterMessageActive(List<string> message,string name,List<Sprite> charaimage)
    {
        int i = 0;
        foreach (string str in message)
        {
            Debug.Log(name);
            yield return null;
            showMessage(str, name, charaimage[i]);
            i++;
            yield return new WaitUntil(() => (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)));
        }
        target.text = "";
        GameManager.m_instance.ImageErase(Chara);
        window.gameObject.SetActive(false);
        yield break;
    }

    // �ȉ��̓L�����N�^�[�̃C�x���g���̓���ȃ��\�b�h
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
        character.messageTexts2[0] = "�u�����������x����c�c�s�s�`���̋V����������ł���H�v";
        character.messageTexts2[1] = "�u�����s������I�����҂�����Ȃ���I�͂₭�I�͂₭�I�͂�N�I�n���N�I�v";
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
        images = character.freeImage1;
        messages = character.freeText1;
        charactername = "�K�l";
        yield return new WaitForSeconds(0.5f);
        window.gameObject.SetActive(true);
        i = 0;
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
        characterItem.inventry.Add(characterItem.itemDateBase.GetItemId(201));
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
        //�b����������A�C�e�������炦��d�l�ŁA�A�C�e�����������Ă���ꍇ�͂��炦�Ȃ��d�l�ɂ���B
        //�~�������΃A�C�e���������Ă���Ƃ��͘b�����e���ω����Ăق����B
        //�d�l�I�Ɍ����΁A�N�ɂ��b�����������ŁA�b����������ɃA�C�e�������炤���\�b�h���N�����Ăق���
        //�����i�N��OK�j�i���̃^�C�~���O��OK�j�i�A�C�e���������Ă��邩OK�j

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
        //��肽�����ƁA�R���[�`����ʃX�N���v�g������������Ă��Ĕ�������Bmessagecharactor��null�̂܂܂ɂȂ��Ă�

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
            // �v�f�̐��������[�v���s����B
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
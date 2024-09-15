using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
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
    private IEnumerator coroutine;
    public NotEnter1 notEnter1;
    public NotEnter4 notEnter4;
    private bool isContacted = false;
    public static bool messageSwitch = false;
    public CharacterItem characterItem;

    private void Start()
    {
        isContacted = false;
        GameObject myGameObject = gameObject;
    }

    //�v���C���[���ڐG����B���̎��ɃL�����N�^�[�ɂ���ČĂԃ��\�b�h��ς����������\�b�h�͓����ŃQ�[���I�u�W�F�N�g��
    //�擾���Ă��̃I�u�W�F�N�g�ŏ������򂳂���H

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log($"colloder: {gameObject.name} ");
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
        // window�N��
        window.gameObject.SetActive(true);

        // ���ۃ��\�b�h�Ăяo�� �ڍׂ͎q�N���X�Ŏ���
        yield return CharaShowMessage();

        // window�I��
        target.text = "";
        window.gameObject.SetActive(false);

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
    �ł����b�Z�[�W�Ɖ摜�͈�Έ�Ή����Ă邩��ʂ�foreach����Ȃ��Ă��悭�ˁH*/

    IEnumerator CharaShowMessage()
    {
        // �w�Z�ɂ���Ԃɂ���ׂ炳�����e
        if(notEnter1.one == false)
        {
            charactername = character.charaName;
            images = character.characterImages1;
            messages = character.messageTexts1;
            int i = 0;
            if(gameObject.name == "Hosokawa Mitsuki")
            {
                characterItem.CharagivedItem();
            }
            // �v�f�̐��������[�v���s����B
            foreach(string str in messages)
            {
                yield return null;
                showMessage(str, charactername, images[i]);
                i++;
                yield return new WaitUntil(() => (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)));
            }
            yield break;
        }
        else if(notEnter4.getKey1 == true)//���Ԃɍs����悤�ɂȂ��Ă���̓��e
        {
            charactername = character.charaName;
            images = character.characterImages3;
            messages = character.messageTexts3;
            int i = 0;
            // �v�f�̐��������[�v���s����B
            foreach(string str in messages)
            {
                yield return null;
                showMessage(str, charactername, images[i]);
                i++;
                yield return new WaitUntil(() => (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)));
            }
            yield break;
        }
        else if(notEnter1.one == true)//�o�`���Ƃ��Ă���̓��e
        {
            charactername = character.charaName;
            images = character.characterImages2;
            messages = character.messageTexts2;
            int i = 0;
            // �v�f�̐��������[�v���s����B
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
    IEnumerator OnAction()
    {
        int i = 0;
        charactername = character.charaName;
        //�@�Ƃ��ƂɃZ���t�����蓖�Ă�B
        if(notEnter1.one == false)
        {
            for(i = 0; i < messages.Count; ++i)
            {
                messages[i] = character.messageTexts1[i];
                images = character.characterImages1;
                // 1�t���[���� ������ҋ@(���L����1)
                yield return null;

                // ��b��window��text�t�B�[���h�ɕ\��
                showMessage(messages[i], charactername, images[i]);
                yield return new WaitUntil(() => (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)));
            }
            yield break;
        }
        else if(notEnter1.one == true)
        {
            for(i = 0; i < messages.Count; ++i)
            {
                messages[i] = character.messageTexts2[i];
                images = character.characterImages2;
                // 1�t���[���� ������ҋ@(���L����1)
                yield return null;

                // ��b��window��text�t�B�[���h�ɕ\��
                showMessage(messages[i], charactername, images[i]);

                yield return new WaitUntil(() => (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)));
            }
            yield break;
        }
        yield break;
    }
    [System.Serializable]
    public class CharacterItem:MonoBehaviour
    {
        //�b����������A�C�e�������炦��d�l�ŁA�A�C�e�����������Ă���ꍇ�͂��炦�Ȃ��d�l�ɂ���B
        //�~�������΃A�C�e���������Ă���Ƃ��͘b�����e���ω����Ăق����B
        //�d�l�I�Ɍ����΁A�N�ɂ��b�����������ŁA�b����������ɃA�C�e�������炤���\�b�h���N�����Ăق���
        //�����i�N��OK�j�i���̃^�C�~���O��OK�j�i�A�C�e���������Ă��邩OK�j

        public Item mitsukiItem;
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
        private IEnumerator coroutine;
        private bool isOpenSelect = false;

        public void CharagivedItem()
        {
            if(mitsukiItem.checkPossession == false)
            {
                coroutine = OnAction();
                StartCoroutine(coroutine);
                inventry.Add(itemDateBase.items[10]);
                mitsukiItem.checkPossession = true;
            }
        }
        IEnumerator OnAction()
        {
            window.gameObject.SetActive(true);
            yield return new WaitUntil(() => !isOpenSelect);
            window.gameObject.SetActive(false);
            PlayerManager.m_instance.m_speed = 0.075f;
            coroutine = null;
            yield break;

        }
    }

}
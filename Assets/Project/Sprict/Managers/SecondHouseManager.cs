using System.Collections;
using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using DG.Tweening;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;
using static UnityEngine.GraphicsBuffer;
using System;

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
    private List<string> bearfailedmessages;
    [SerializeField]
    private List<string> bearfailednames;
    [SerializeField]
    private List<Sprite> bearfailedimage;
    [SerializeField]
    private List<string> chickenfailedmessages;
    [SerializeField]
    private List<string> chickenfailednames;
    [SerializeField]
    private List<Sprite> chickenfailedimage;
    [SerializeField]
    private List<string> mushroomfailedmessages;
    [SerializeField]
    private List<string> mushroomfailednames;
    [SerializeField]
    private List<Sprite> mushroomfailedimage;
    [SerializeField]
    private List<string> bearfailedMetamessages;
    [SerializeField]
    private List<string> bearfailedMetanames;
    [SerializeField]
    private List<Sprite> bearfailedMetaimages;
    [SerializeField]
    private List<string> chickenfailedMetamessages;
    [SerializeField]
    private List<string> chickenfailedMetanames;
    [SerializeField]
    private List<Sprite> chickenfailedMetaimages;
    [SerializeField]
    private List<string> mushroomfailedMetamessages;
    [SerializeField]
    private List<string> mushroomfailedMetanames;
    [SerializeField]
    private List<Sprite> mushroomfailedMetaimages;
    [SerializeField]
    private List<string> failedmessages1;
    [SerializeField]
    private List<string> failednames1;
    [SerializeField]
    private List<Sprite> failedimages1;
    [SerializeField]
    private List<string> failedmessages2;
    [SerializeField]
    private List<string> failednames2;
    [SerializeField]
    private List<Sprite> failedimages2;
    [SerializeField]
    private List<string> failedmessages3;
    [SerializeField]
    private List<string> failednames3;
    [SerializeField]
    private List<Sprite> failedimages3;
    [SerializeField]
    private List<string> failedmessages4;
    [SerializeField]
    private List<string> failednames4;
    [SerializeField]
    private List<Sprite> failedimages4;
    [SerializeField]
    private List<string> keyOpenMessages;
    [SerializeField]
    private List<string> keyOpenNames;
    [SerializeField]
    private List<Sprite> keyOpenImage;
    public Test1[] interiors = new Test1[4];
    public CalenderMessage CalenderInteriors;
    private IEnumerator coroutine;
    public Canvas window;
    public Canvas end6Canvas;
    public Text target;
    public Text nameText;
    public Image characterImage;
    public Image[] end6Image;
    private bool bearKey = false;
    private bool chickenKey = false;
    private bool mushroomKey = false;
    public bool firstkey = false;
    public bool haruTakedKey = false;
    public GameObject meat;
    public GameObject haru;
    public GameObject metalBlade;
    public GameObject weightObject;
    public GameObject weightSwitch;
    public GameObject sleepAjure;
    public NotEnter9 notEnter9;
    public EndingCase9 endingCase9;
    public Inventry inventry;
    public ItemDateBase itemDate;
    public DishMessage chickenDish;
    public DishMessage fishDish;
    public DishMessage shrimpDish;
    public AnimalsMessages bear;
    public AnimalsMessages chicken;
    public AnimalsMessages mushroom;
    public Light2D light2D;
    public ToEvent5 toEvent5;
    public EnemyEncounter enemyEncounter;
    public HaruImportantEvent haruImportant;
    public NotEnter13 notEnter13;
    public TalkingWithHaru talkingWithHaru;
    public TunnelSeiitirouEvent tunnelSeiitirou;
    public Cooktop cooktop;
    public Homing2 ajure;
    public static SecondHouseManager secondHouse_instance;
    public SoundManager soundManager;
    public Item clinicKey;
    public AudioClip heartSound;
    public AudioClip dogSound;
    public AudioClip doorSound;
    public AudioClip keyOpen;
    public AudioClip meatEat;
    public AudioClip muvingAnimals;
    public AudioClip ending6Music;
    public AudioClip noiseSound;
    public AudioClip laughing;
    public AudioClip bigSoundSE;
    public AudioClip runSound;
    public AudioClip fearMusic;
    //2���ڂ̑I�����𓝊�����X�N���v�g

    private void Start()
    {
        secondHouse_instance = this;
    }
    private void Update()
    {
        if(chickenKey == true && mushroomKey == true && bearKey == true && firstkey == false && coroutine == null)
        {
            //�����J������
            coroutine = OpenKey();
            StartCoroutine(coroutine);
            //�����󂢂Ă��烁�b�Z�[�W���o��
            firstkey = true;
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
        GameManager.m_instance.ImageErase(characterImage);
        GameManager.m_instance.stopSwitch = true;
        soundManager.PlaySe(keyOpen);
        yield return new WaitForSeconds(2.0f);
        window.gameObject.SetActive(true);
        for(int i = 0; i < keyOpenMessages.Count; ++i)
        {
            yield return null;
            showMessage(keyOpenMessages[i], keyOpenNames[i], keyOpenImage[i]);
            yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
        }
        target.text = "";
        window.gameObject.SetActive(false);
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
    private IEnumerator Blackout()
    {
        light2D.intensity = 1.0f;
        GameManager.m_instance.stopSwitch = true;
        while(light2D.intensity > 0.01f)
        {
            light2D.intensity -= 0.012f;
            yield return null;
        }
    }
    IEnumerator OnFailAction()
    {
        if (bear.isContacted == true)
        {
            Debug.Log("bear");
            for (int i = 0; i < bearfailmessages.Count; ++i)
            {
                yield return null;
                showMessage(bearfailmessages[i], bearfailnames[i], bearfailimage[i]);
                yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
            }
            // ���b�Z�[�W������邪�A����I��������Ƃ���͈Ⴄ�Ƃ����A�s���������B
            //���̂��ߌ��������K�v���Ƃ����Ɠ���H�ׂ鉹�y�ƂƂ��Ɏ��S����B
            SoundManager.sound_Instance.PlaySe(meatEat);
            yield return Blackout();
            yield return new WaitForSeconds(1.5f);
            // �Q�[���I�[�o�[��ʂ��o�����߂̃L�����o�X�Ƃ��̐��b��Ƀ{�^�����o��
            GameManager.m_instance.gameoverWindow.gameObject.SetActive(true);
            GameManager.m_instance.stopSwitch = true;
            yield return new WaitForSeconds(2.0f);
            GameManager.m_instance.buttonPanel.gameObject.SetActive(true);
            light2D.intensity = 1.0f;
        }
        else if (chicken.isContacted == true)
        {
            Debug.Log("chiken");
            for (int i = 0; i < chickenfailmessages.Count; ++i)
            {
                yield return null;
                showMessage(chickenfailmessages[i], chickenfailnames[i], chickenfailimage[i]);
                yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
            }
            SoundManager.sound_Instance.PlaySe(meatEat);
            yield return Blackout();
            yield return new WaitForSeconds(1.5f);
            GameManager.m_instance.gameoverWindow.gameObject.SetActive(true);
            GameManager.m_instance.stopSwitch = true;
            yield return new WaitForSeconds(2.0f);
            GameManager.m_instance.buttonPanel.gameObject.SetActive(true);
            light2D.intensity = 1.0f;
        }
        else if (mushroom.isContacted == true)
        {
            Debug.Log("kinoko");
            for (int i = 0; i < mushroomfailmessages.Count; ++i)
            {
                yield return null;
                showMessage(mushroomfailmessages[i], mushroomfailnames[i], mushroomfailimage[i]);
                yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
            }
            SoundManager.sound_Instance.PlaySe(meatEat);
            yield return Blackout();
            yield return new WaitForSeconds(1.5f);
            GameManager.m_instance.gameoverWindow.gameObject.SetActive(true);
            GameManager.m_instance.stopSwitch = true;
            yield return new WaitForSeconds(2.0f);
            GameManager.m_instance.buttonPanel.gameObject.SetActive(true);
            light2D.intensity = 1.0f;
        }
        yield break;
    }
    public void AnimalGiveDish()
    {
        if(bear.isContacted == true)
        {
            bear.selection.gameObject.SetActive(false);
            bear.Selectwindow.gameObject.SetActive(false);
            if(itemDate.GetItemId(23).checkPossession == true)
            {//�{�������Ă����h�A���󂭃L�[����������i�R�Ńh�A���J���j
                inventry.Delete(itemDate.GetItemId(23));
                bear.isOpenSelect = false;
                bearKey = true;
                coroutine = OnAction2();
                StartCoroutine(coroutine);
            }
            else if(itemDate.GetItemId(20).checkPossession == true)
            {//���b�Z�[�W���o����Ɏ���
                inventry.Delete(itemDate.GetItemId(20));
                bear.isOpenSelect = false;
                coroutine = OnFailAction();
                StartCoroutine(coroutine);
            }
        }
        if(chicken.isContacted == true)
        {
            chicken.selection.gameObject.SetActive(false);
            chicken.Selectwindow.gameObject.SetActive(false);
            if(itemDate.GetItemId(22).checkPossession == true)
            {
                inventry.Delete(itemDate.GetItemId(22));
                chicken.isOpenSelect = false;
                chickenKey = true;
                coroutine = OnAction2();
                StartCoroutine(coroutine);
            }
            else if(itemDate.GetItemId(19).checkPossession == true)
            {//���b�Z�[�W���o����Ɏ���
                inventry.Delete(itemDate.GetItemId(19));
                bear.isOpenSelect = false;
                coroutine = OnFailAction();
                StartCoroutine(coroutine);
            }
        }
        if(mushroom.isContacted == true)
        {
            mushroom.selection.gameObject.SetActive(false);
            mushroom.Selectwindow.gameObject.SetActive(false);
            if(itemDate.GetItemId(21).checkPossession == true)
            {
                inventry.Delete(itemDate.GetItemId(21));
                mushroom.isOpenSelect = false;
                mushroomKey = true;
                coroutine = OnAction2();
                StartCoroutine(coroutine);
            }
            else if(itemDate.GetItemId(18).checkPossession == true)
            {//���b�Z�[�W���o����Ɏ���
                inventry.Delete(itemDate.GetItemId(18));
                bear.isOpenSelect = false;
                coroutine = OnFailAction();
                StartCoroutine(coroutine);
            }
        }
    }
    public void AnimalNotGiveDishBotton()
    {
        GameManager.m_instance.stopSwitch = true;
        AnimalNotGiveDish().Forget();
        
    }
    public async UniTask AnimalNotGiveDish()
    {
        /* �����ɗ�����n���Ȃ��ƃG���f�B���O���n�܂�B
           �G���f�B���O���e�͗�����������n�Y�̎�l���ɑ΂���
        ������񋟂��Ȃ��͉̂������H�ƃL���Ă��̂������̂Q�C���o�Ă���B�ŏI�I�ɂ͂R�C�ɎE����Ď���ł��܂��B
        �ꉞ���\�b�h���̂͂R�쐬���邪�㔼����͋��ʉ����Ă��悢�B
         */
        if(bear.isContacted == true)
        {
            if(EndingGalleryManager.m_gallery.endingFlag[5])
            {
                //���łɃG���h���������Ă���Ȃ烁�^�����B���̂��Ƃɗ����̑I��ɓ���B
                MessageManager.message_instance.MessageWindowActive(bearfailedMetamessages, bearfailedMetanames, bearfailedMetaimages, ct: destroyCancellationToken).Forget();
                AnimalGiveDish();
                return;
            }
            bear.selection.gameObject.SetActive(false);
            bear.Selectwindow.gameObject.SetActive(false);
            bear.isOpenSelect = false;
            bear.enabled = false;
            await MessageManager.message_instance.MessageWindowActive(bearfailedmessages, bearfailednames, bearfailedimage, ct: destroyCancellationToken);
            soundManager.PlaySe(muvingAnimals);
            chicken.gameObject.transform.DOLocalMove(new Vector3(138, 88, 0), 2f);
            mushroom.gameObject.transform.DOLocalMove(new Vector3(144, 89, 0), 0.7f);
            await UniTask.Delay(TimeSpan.FromSeconds(0.7f));
            mushroom.gameObject.transform.DOLocalMove(new Vector3(138, 89, 0), 4.9f);
            soundManager.PlaySe(muvingAnimals);
            await UniTask.Delay(TimeSpan.FromSeconds(1.9f));
            soundManager.PlaySe(muvingAnimals);
            await UniTask.Delay(TimeSpan.FromSeconds(3f));
            Ending6().Forget();
        }
        else if(chicken.isContacted == true)
        {
            if(EndingGalleryManager.m_gallery.endingFlag[5])
            {
                //���łɃG���h���������Ă���Ȃ烁�^�����B���̂��Ƃɗ����̑I��ɓ���B
                MessageManager.message_instance.MessageWindowActive(chickenfailedMetamessages, chickenfailedMetanames, chickenfailedMetaimages, ct: destroyCancellationToken).Forget();
                AnimalGiveDish();
                return;
            }
            chicken.selection.gameObject.SetActive(false);
            chicken.Selectwindow.gameObject.SetActive(false);
            chicken.isOpenSelect = false;
            chicken.enabled = false;
            await MessageManager.message_instance.MessageWindowActive(chickenfailedmessages, chickenfailednames, chickenfailedimage, ct: destroyCancellationToken);
            soundManager.PlaySe(muvingAnimals);
            bear.gameObject.transform.DOLocalMove(new Vector3(138, 88, 0), 2f);
            mushroom.gameObject.transform.DOLocalMove(new Vector3(142, 88, 0), 2f);
            await UniTask.Delay(TimeSpan.FromSeconds(2f));
            Ending6().Forget();
        }
        else if(mushroom.isContacted == true)
        {
            if(EndingGalleryManager.m_gallery.endingFlag[5])
            {
                //���łɃG���h���������Ă���Ȃ烁�^�����B���̂��Ƃɗ����̑I��ɓ���B
                MessageManager.message_instance.MessageWindowActive(mushroomfailedMetamessages, mushroomfailedMetanames, mushroomfailedMetaimages, ct: destroyCancellationToken).Forget();
                AnimalGiveDish();
                return;
            }
            mushroom.selection.gameObject.SetActive(false);
            mushroom.Selectwindow.gameObject.SetActive(false);
            mushroom.isOpenSelect = false;
            mushroom.enabled = false;
            await MessageManager.message_instance.MessageWindowActive(mushroomfailedmessages, mushroomfailednames, mushroomfailedimage, ct: destroyCancellationToken);
            soundManager.PlaySe(muvingAnimals);
            chicken.gameObject.transform.DOLocalMove(new Vector3(142, 88, 0), 2f);
            bear.gameObject.transform.DOLocalMove(new Vector3(136, 89, 0), 0.7f);
            await UniTask.Delay(TimeSpan.FromSeconds(0.7f));
            bear.gameObject.transform.DOLocalMove(new Vector3(142, 89, 0), 4.9f);
            soundManager.PlaySe(muvingAnimals);
            await UniTask.Delay(TimeSpan.FromSeconds(1.9f));
            soundManager.PlaySe(muvingAnimals);
            await UniTask.Delay(TimeSpan.FromSeconds(3f));
            Ending6().Forget();
        }
    }
    public async UniTask Ending6()
    {
        //�@�����ȁu���O�͒f�߂��B�v�̃Z���t
        await MessageManager.message_instance.MessageWindowActive(failedmessages1,failednames1,failedimages1, ct: destroyCancellationToken);
        await Blackout();
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
        soundManager.PlayBgm(ending6Music);
        end6Canvas.gameObject.SetActive(true);
        end6Image[0].gameObject.SetActive(true);
        //�u�Ȃ񂾂悱��A��߂�߂Â��ȁI�v�u�����ȁv*�R
        await MessageManager.message_instance.MessageWindowActive(failedmessages2, failednames2, failedimages2, ct: destroyCancellationToken);
        Color color = end6Image[0].GetComponent<Image>().color;
        end6Image[0].gameObject.SetActive(true);
        color.a = 1f;
        end6Image[0].GetComponent<Image>().color = color;
        while(color.a > 0.01f)
        {
            Debug.Log(color.a);
            color.a -= 0.004f;
            end6Image[0].color = color;
            await UniTask.DelayFrame(1);
        }
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
        soundManager.PlaySe(muvingAnimals);
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
        soundManager.PlaySe(muvingAnimals);
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
        soundManager.PlaySe(muvingAnimals);
        await UniTask.Delay(TimeSpan.FromSeconds(2.5f));
        end6Image[1].gameObject.SetActive(true);
        end6Image[0].gameObject.SetActive(false);

        await MessageManager.message_instance.MessageWindowActive(failedmessages3, failednames3, failedimages3, ct: destroyCancellationToken);
        color = end6Image[1].GetComponent<Image>().color;
        end6Image[1].gameObject.SetActive(true);
        color.a = 1f;
        end6Image[1].GetComponent<Image>().color = color;
        while(color.a > 0.01f)
        {
            Debug.Log(color.a);
            color.a -= 0.004f;
            end6Image[1].color = color;
            await UniTask.DelayFrame(1);
        }
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
        soundManager.PlaySe(muvingAnimals);
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
        soundManager.PlaySe(muvingAnimals);
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
        soundManager.PlaySe(muvingAnimals);
        await UniTask.Delay(TimeSpan.FromSeconds(2.5f));
        end6Image[2].gameObject.SetActive(true);
        end6Image[1].gameObject.SetActive(false);

        await MessageManager.message_instance.MessageWindowActive(failedmessages4, failednames4, failedimages4, ct: destroyCancellationToken);
        color = end6Image[2].GetComponent<Image>().color;
        end6Image[2].gameObject.SetActive(true);
        color.a = 1f;
        end6Image[2].GetComponent<Image>().color = color;
        while(color.a > 0.01f)
        {
            color.a -= 0.004f;
            end6Image[2].color = color;
            await UniTask.DelayFrame(1);
        }
        soundManager.PlaySe(laughing);
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
        soundManager.PlaySe(muvingAnimals);
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
        soundManager.PlaySe(muvingAnimals);
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
        soundManager.PlaySe(muvingAnimals);
        await UniTask.Delay(TimeSpan.FromSeconds(2.5f));
        end6Image[3].gameObject.SetActive(true);
        soundManager.PlaySe(bigSoundSE);
        end6Image[2].gameObject.SetActive(false);
    }
    public void DishTaken()
    {
        if(shrimpDish.isContacted == true)
        {
            shrimpDish.selection.gameObject.SetActive(false);
            shrimpDish.Selectwindow.gameObject.SetActive(false);
            inventry.Add(shrimpDish.shrimp);
            shrimpDish.isOpenSelect = false;
            shrimpDish.window.gameObject.SetActive(false);
            shrimpDish.dish.SetActive(false);
        }
        //�Ƃ����A�C�e�������̊ۏĂ��̎�
        else if(chickenDish.isContacted == true)
        {
            chickenDish.selection.gameObject.SetActive(false);
            chickenDish.Selectwindow.gameObject.SetActive(false);
            inventry.Add(chickenDish.chicken);
            chickenDish.isOpenSelect = false;
            chickenDish.window.gameObject.SetActive(false);
            chickenDish.dish.SetActive(false);
        }
        else if(fishDish.isContacted == true)
        {
            fishDish.selection.gameObject.SetActive(false);
            fishDish.Selectwindow.gameObject.SetActive(false);
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
    public void OnclickEndRetry()
    {
        end6Image[3].gameObject.SetActive(false);
        end6Canvas.gameObject.SetActive(false);
        light2D.intensity = 1.0f;
        soundManager.StopBgm(ending6Music);
        //GameManager.m_instance.OnclickRetryButton();
        EndingGalleryManager.m_gallery.endingGallerys[5].sprite = end6Image[3].sprite;
        EndingGalleryManager.m_gallery.endingFlag[5] = true;
    }
}

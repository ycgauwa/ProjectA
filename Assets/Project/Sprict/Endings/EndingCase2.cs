using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using UnityEngine;
using UnityEngine.EventSystems;
using Cysharp.Threading.Tasks;

public class EndingCase2 : MonoBehaviour
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
    private List<string> messages4;
    [SerializeField]
    private List<string> names4;
    [SerializeField]
    private List<Sprite> image4;
    [SerializeField]
    private List<string> messages5;
    [SerializeField]
    private List<string> names5;
    [SerializeField]
    private List<Sprite> image5;
    [SerializeField]
    private List<string> messages6;
    [SerializeField]
    private List<string> names6;
    [SerializeField]
    private List<Sprite> image6;
    public Canvas window;
    public Canvas Selectwindow;
    public Canvas endWindow;
    public Text target;
    public Text nameText;
    public Image characterImage;
    public Image selection;
    public Image end2Image;
    public Image end2Image2;
    public Image end2Image3;
    public Image end2retry;
    public Color color;
    public SoundManager soundManager;
    public Homing homing;
    public AudioClip decision;
    public AudioClip ending2Sound;
    public static bool messageSwitch = false;
    private bool isContacted = false;
    public bool answer;
    private IEnumerator coroutine;
    public GameObject firstSelect;
    public GameObject firstSelect2;
    public GameObject entrance;
    public GameObject wall;

    //����Ƃ��Ă͊O�ɏo�悤�Ƃ��āA���މ摜�̏o�����̂��Ƃɓ��邻�̂��Ƃɔ���������̉쎀�G���h�B
    //�ω����N����O�ɁA�t�F�[�h�A�E�g���͂��ޗ\��ł͂���B�I���������މ摜����u�����ĉ摜�����ւ������b�Z�[�W����
    //�̂��ƃt�F�[�h�A�E�g���摜�����ւ��Ŕ����������摜���o���Ċ�����\������B
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag.Equals("Player"))
            isContacted = true;
    }

    // collider�����I�u�W�F�N�g�̗̈�O�ɂł��Ƃ�(���L�Ő���1)
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag.Equals("Player"))
            isContacted = false;
    }
    private void Update()//���̓`�F�b�N��Update�ɏ���
    {
        if (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return))
        {
            if (isContacted == true && coroutine == null)
            {
                if (homing.enemyCount > 0.2 && messageSwitch == false)
                {
                    messageSwitch = true;
                    MessageManager.message_instance.MessageWindowActive(messages5, names5, image5, ct: destroyCancellationToken).Forget();
                }
                else if (answer == true && isContacted)
                {
                    MessageManager.message_instance.MessageWindowActive(messages, names, image, ct: destroyCancellationToken).Forget();
                    isContacted = false;
                }
                else if(answer == false && isContacted)
                {
                    MessageManager.message_instance.MessageSelectWindowActive(messages2, names2, image2, Selectwindow, selection, firstSelect, ct: destroyCancellationToken).Forget();
                    isContacted = false;
                }
                    
            }
        }
    }
    protected void showMessage(string message, string name, Sprite image)
    {
        target.text = message;
        nameText.text = name;
        characterImage.sprite = image;
    }
    private IEnumerator Blackout()
    {
        yield return new WaitForSeconds(0.1f);
        window.gameObject.SetActive(true);
        end2Image2.gameObject.SetActive(true);
        for(int i = 0; i < messages3.Count; ++i)
        {
            yield return null;
            showMessage(messages3[i], names3[i], image3[i]);
            yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
        }
        Debug.Log("Image3");
        end2Image3.gameObject.SetActive(true);
        color = end2Image3.GetComponent<Image>().color;
        color.a = 0f;
        while(color.a <= 1f)
        {
            if(color.a >= 0.299f && color.a <= 0.303f) SoundManager.sound_Instance.PlaySe(EndingGalleryManager.m_gallery.freezeSound);
            color.a += 0.004f;
            end2Image3.color = color;
            yield return null;
            GameManager.m_instance.stopSwitch = true;
        }
        //�摜�̖��邳�������Đ^���Âɂ���B
        SecondHouseManager.secondHouse_instance.light2D.intensity = 0f;
        GameManager.m_instance.gameUICanvas.gameObject.SetActive(false);
        end2Image2.gameObject.SetActive(false);
        yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
        for (int i = 0; i < messages4.Count; ++i)
        {
            yield return null;
            showMessage(messages4[i], names4[i], image4[i]);
            yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
        }
        window.gameObject.SetActive(false);
        while(color.a > 0.01f)
        {
            color.a -= 0.004f;
            end2Image3.color = color;
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        window.gameObject.SetActive(true);
        for(int i = 0; i < messages6.Count; ++i)
        {
            yield return null;
            showMessage(messages6[i], names6[i], image6[i]);
            yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
        }
        window.gameObject.SetActive(false);
        end2retry.gameObject.SetActive(true);
        GameManager.m_instance.gameUICanvas.gameObject.SetActive(true);
        end2Image3.gameObject.SetActive(false);
        coroutine = null;
        entrance.gameObject.SetActive(false);
        wall.gameObject.SetActive(true);
        gameObject.SetActive(false);

    }
    public void OnclickEnd2Retry()
    {
        end2Image.gameObject.SetActive(true);
        endWindow.gameObject.SetActive(false);
        soundManager.StopBgm(ending2Sound);
        GameManager.m_instance.stopSwitch = false;
        GameManager.m_instance.player.transform.position = new Vector2(69, -46);
        SecondHouseManager.secondHouse_instance.light2D.intensity = 1f;
        EndingGalleryManager.m_gallery.endingGallerys[1].sprite = end2retry.sprite;
        EndingGalleryManager.m_gallery.endingFlag[1] = true;
        EndingGalleryManager.m_gallery.GetedEndings(EndingGalleryManager.m_gallery.getedEndTotalNumber).Forget();
    }
    public void End2SelectYes()
    {
        //�I�����Ă���A�P�C�Q�񃁃b�Z�[�W�𑗂��ĉ摜���o���Ă��̌ォ��ꂵ�ݏo���i�~�������Ώł�悤��BGM���ق����j
        soundManager.PlaySe(decision);
        StartCoroutine("Blackout");
        MessageManager.message_instance.isOpenSelect = false;
        selection.gameObject.SetActive(false);
        Selectwindow.gameObject.SetActive(false);
        endWindow.gameObject.SetActive(true);
        answer = true;
    }
    public void End2SelectNo()
    {
        //���b�Z�[�W���o���āA�E�B���h�E�����B���̂��Ƃ͂����I�������o�Ȃ��悤�ɂ���B
        soundManager.PlaySe(decision);
        selection.gameObject.SetActive(false);
        Selectwindow.gameObject.SetActive(false);
        answer = true;
        coroutine = null;
        MessageManager.message_instance.isOpenSelect = false;
    }
}

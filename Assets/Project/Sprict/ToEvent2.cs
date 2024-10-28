using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.Playables;

public class ToEvent2 : MonoBehaviour
{
    //�I����ʂ��o�ĉ�b���n�܂肻�̂��Ƃɑ��ۂ̉��𗬂��B���̌㔒�����̉��o���o����ɒN�����Ȃ��Ȃ�
    //���W�Œ肵���J�������ړ����āi�������ڂɁj��l��NPC�������ɓ��葾�ۂ̑O�܂ōs��
    //�Z���t��b������ɂ܂��J�������ړ��A�x�����Ə������f���p�Ɉړ������ŉ�b���I�������
    //���̎q���G�t�F�N�g���o���ď��ŁB�x�����̃Z���t�������Ă���I���B
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<Sprite> images;
    [SerializeField]
    private List<string> messages2;
    [SerializeField]
    private List<string> names2;
    [SerializeField]
    private List<Sprite> images2;
    [SerializeField]
    private List<string> messages3;
    [SerializeField]
    private List<string> names3;
    [SerializeField]
    private List<Sprite> images3;
    [SerializeField]
    private List<string> messages4;
    [SerializeField]
    private List<string> names4;
    [SerializeField]
    private List<Sprite> images4;
    [SerializeField]
    private List<string> messages5;
    [SerializeField]
    private List<string> names5;
    [SerializeField]
    private List<Sprite> images5;
    [SerializeField]
    private List<string> beforeMessages;
    [SerializeField]
    private List<string> beforeNames;
    [SerializeField]
    private List<Sprite> beforeImages;
    public Canvas window;
    public Text target;
    public Text nameText;
    public Image characterImage;
    public static bool one;
    private IEnumerator coroutine;
    private bool isContacted = false;
    public SoundManager soundManager;
    public AudioClip sound;
    public AudioClip doorSound;
    public AudioClip flashSe;
    public AudioClip suspiciousBgm;
    public Light2D light2D;
    public GameObject player;
    public GameObject[] friends;
    public GameObject girl;
    public GameObject guards;
    public GameObject eventcamera;
    public GameObject gameMenuUI;
    public float speed;
    public Inventry inventry;
    public Item item;
    private bool playerCamera;
    public PlayableDirector playableDirector;
    public Animator cameraAnimator;

    //���ʉ���BGM�̒ǉ��B
    IEnumerator Event2()
    {
        yield return new WaitForSeconds(1);
        coroutine = CreateCoroutine();
        // �R���[�`���̋N��(���L����2)
        StartCoroutine(coroutine);

    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        isContacted = collider.gameObject.tag.Equals("Player");
    }

    // collider�����I�u�W�F�N�g�̗̈�O�ɂł��Ƃ�(���L�Ő���1)
    private void OnTriggerExit2D(Collider2D collider)
    {
        isContacted = !collider.gameObject.tag.Equals("Player");
    }
    private void FixedUpdate()
    {
        if(isContacted && coroutine == null && Input.GetButton("Submit") && Input.GetKeyDown(KeyCode.Return))
        {
            if (item.checkPossession)
            {
                light2D = gameObject.GetComponent<Light2D>();
                item.checkPossession = false;
                coroutine = CreateCoroutine();
                StartCoroutine(coroutine);
            }
        }
        if(eventcamera.transform.position.y > 6 && cameraManager.playerCamera == false)
        {
            eventcamera.transform.Translate(new Vector3(0.0f, -0.05f, 0.0f * Time.deltaTime * speed));
        }
        if(girl.transform.position.x > -86 && cameraManager.girlCamera == true)
        {
            // girl�������v���O����
            girl.transform.Translate(new Vector3(-0.05f, 0, 0.0f * Time.deltaTime * speed));
        }
        if(eventcamera.transform.position.x < -80 && cameraManager.playerCamera == false && cameraManager.girlCamera == false)
        {
            eventcamera.transform.Translate(new Vector3(0.07f, 0.0f, 0.0f * Time.deltaTime * speed));
        }
    }
    IEnumerator CreateCoroutine()
    {

        inventry.Delete(item);
        yield return OnAction6();
        StartCoroutine("Blackout");
        yield return new WaitForSeconds(1.5f);
        light2D.intensity = 1.0f;
        window.gameObject.SetActive(true);

        yield return OnAction();

        target.text = "";
        window.gameObject.SetActive(false);
        soundManager.PlaySe(sound);
       
        yield return new WaitForSeconds(2.0f);
        yield return Flash();
        
        //startCoroutine�ł͂Ȃ���yield�@return �������Ă�����Ɠ����iyield return�̈Ӗ��𒲂ׂĂ����j
        yield return new WaitForSeconds(3.0f);
        light2D.intensity = 1.0f;
        soundManager.StopSe(flashSe);
        soundManager.PlayBgm(suspiciousBgm);
        //camera�̏���
        Event2Camera();

        yield return new WaitForSeconds(6.0f);

        window.gameObject.SetActive(true);
        yield return OnAction2();
        
        target.text = "";
        window.gameObject.SetActive(false);
        
        cameraManager.girlCamera = true;
        yield return new WaitForSeconds(2.0f);
        soundManager.PlaySe(doorSound);
        yield return new WaitForSeconds(2.0f);
        
        guards.transform.position = new Vector3(-76, 5, 0);
        cameraManager.girlCamera = false;
        
        yield return new WaitForSeconds(3.0f);
        
        window.gameObject.SetActive(true);
        yield return OnAction3();

        target.text = "";
        window.gameObject.SetActive(false);
        
        yield return Flash();

        yield return new WaitForSeconds(3.0f);

        girl.transform.position = new Vector3(0, 0, 0);
        light2D.intensity = 1.0f;
        soundManager.StopSe(flashSe);
        window.gameObject.SetActive(true);
        yield return OnAction4();

        target.text = "";
        window.gameObject.SetActive(false);
        
        yield return new WaitForSeconds(1.0f);
        StartCoroutine("Blackout2");
        yield return new WaitForSeconds(1.5f);
        soundManager.StopBgm(suspiciousBgm);
        cameraManager.playerCamera = true;
        cameraAnimator.enabled = true;
        light2D.intensity = 1.0f;
        gameMenuUI.SetActive(false);
        playableDirector.Play();
        yield return new WaitForSeconds(70f);
        StartCoroutine("Blackout2");
        yield return new WaitForSeconds(1.5f);
        cameraAnimator.enabled = false;
        light2D.intensity = 1.0f;
        gameMenuUI.SetActive(true);

        window.gameObject.SetActive(true);
        yield return OnAction5();

        target.text = "";
        GameManager.m_instance.ImageErase(characterImage);
        window.gameObject.SetActive(false);
        GameManager.m_instance.stopSwitch = false;

        StopCoroutine(coroutine);
        coroutine = null;
    }
    private IEnumerator Flash()
    {
        light2D = gameObject.GetComponent<Light2D>();
        light2D.intensity = 1.0f;
        soundManager.PlaySe(flashSe);
        while(light2D.intensity < 7.0f)
        {
            light2D.intensity += 0.1f;
            yield return null;//�����łP�t���[���҂��Ă���Ă�
                              //yield return null �����邩����Ƃ��Ƃ̃��\�b�h��
                              //void ����Ȃ��� IEnumerator �ɂ��Ȃ��Ⴂ���Ȃ��B
        }
        /*Light2D x = light2D;
        x.intensity = 1.0f;
        while(x.intensity < 7.0f)
        {
           light2D.intensity += 0.1f;
        }
        
        ���͊ԈႦ����B�ォ��Q�s�ڂ̕����ԈႢ�Ⴂ�Ƃ��Ă̓R�s�[����̂��V���[�g�J�b�g�̈Ⴂ
        float int string�̂R���댯�B��̗��Light2D x = light2D���R�s�[�łȂ��V���[�g�J�b�g
        �V���ȋ^��_�Ƃ��ĉ���float����Ȃ���var�ɂ��Ă��_���݂��������炻�̗��R���E�ӂɂ���̂��`�F�b�N���Ă���
        
        light2D = this.gameObject.GetComponent<Light2D>();
        float brightness = light2D.intensity;
        Debug.Log(light2D.intensity);
        brightness = 1.0f;

        while(brightness < 7.0f)
        {
            light2D.intensity += 0.1f;
        }
        
        */
    }
    private void Event2Camera()
    {
        playerCamera = cameraManager.playerCamera;
        cameraManager.playerCamera = false;
        player.transform.position = new Vector3(70, -45, 0);
        friends[0].transform.position = new Vector3(0, 0, 0);
        friends[1].transform.position = new Vector3(0, 0, 0);
        friends[2].transform.position = new Vector3(0, 0, 0);
        friends[3].transform.position = new Vector3(0, 0, 0);
        girl.transform.position = new Vector3(-80, 6, 0);

    }
    protected void showMessage(string message, string name , Sprite image)
    {
        target.text = message;
        nameText.text = name;
        characterImage.sprite = image;
    }


    IEnumerator OnAction()
    {
        for(int i = 0; i < messages.Count; ++i)
        {
            // 1�t���[���� ������ҋ@(���L����1)
            yield return null;
            // ��b��window��text�t�B�[���h�ɕ\��
            showMessage(messages[i], names[i], images[i]);
            // �L�[���͂�ҋ@ (���L����1)
            yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
        }
        yield break;
    }
    IEnumerator OnAction2()
    {

        for(int i = 0; i < messages2.Count; ++i)
        {
            yield return null;
            showMessage(messages2[i], names2[i], images2[i]);
            yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
        }
        yield break;
    }
    IEnumerator OnAction3()
    {
        for(int i = 0; i < messages3.Count; ++i)
        {
            yield return null;
            showMessage(messages3[i], names3[i], images3[i]);
            yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
        }
        yield break;
    }
    IEnumerator OnAction4()
    {

        for(int i = 0; i < messages4.Count; ++i)
        {
            yield return null;
            showMessage(messages4[i], names4[i], images4[i]);
            yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
        }
        yield break;
    }
    IEnumerator OnAction5()
    {
        for(int i = 0; i < messages5.Count; ++i)
        {
            yield return null;
            showMessage(messages5[i], names5[i], images5[i]);
            yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
        }
        yield break;
    }
    IEnumerator OnAction6()
    {
        window.gameObject.SetActive(true);
        for (int i = 0; i < beforeMessages.Count; ++i)
        {
            yield return null;
            showMessage(beforeMessages[i], beforeNames[i], beforeImages[i]);
            yield return new WaitUntil(() => Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return));
        }
        window.gameObject.SetActive(false);
        yield break;
    }
    private IEnumerator Blackout()
    {
        light2D.intensity = 1.0f;
        GameManager.m_instance.stopSwitch = true;
        while (light2D.intensity > 0.01f)
        {
            light2D.intensity -= 0.012f;
            yield return null; //�����łP�t���[���҂��Ă���Ă�
        }
        friends[0].transform.position = new Vector3(-81, 19, 0);
        friends[1].transform.position = new Vector3(-81, 21, 0);
        friends[2].transform.position = new Vector3(-79, 21, 0);
        friends[3].transform.position = new Vector3(-78, 20, 0);
    }
    private IEnumerator Blackout2()
    {
        light2D.intensity = 1.0f;
        GameManager.m_instance.stopSwitch = true;
        while (light2D.intensity > 0.01f)
        {
            light2D.intensity -= 0.012f;
            yield return null; //�����łP�t���[���҂��Ă���Ă�
        }
    }
}

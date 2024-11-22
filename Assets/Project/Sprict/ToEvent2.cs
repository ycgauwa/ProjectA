using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.Playables;
using Cysharp.Threading.Tasks;
using System;

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
    public Volume volume;
    private ColorAdjustments colorAdjustments;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
            isContacted = true;
    }

    // collider�����I�u�W�F�N�g�̗̈�O�ɂł��Ƃ�(���L�Ő���1)
    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
            isContacted = false;
    }
    private void FixedUpdate()
    {
        if(isContacted == true  && Input.GetButton("Submit") && Input.GetKeyDown(KeyCode.Return))
        {
            if (item.checkPossession)
            {
                light2D = gameObject.GetComponent<Light2D>();
                item.checkPossession = false;
                CreateCoroutine().Forget();
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
    async UniTask CreateCoroutine()
    {
        volume.profile.TryGet<ColorAdjustments>(out colorAdjustments);
        inventry.Delete(item);
        await MessageManager.message_instance.MessageWindowOnceActive(beforeMessages, beforeNames, beforeImages, ct: destroyCancellationToken);

        await Blackout();
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
        light2D.intensity = 1.0f;
        window.gameObject.SetActive(true);

        await MessageManager.message_instance.MessageWindowOnceActive(messages, names, images, ct: destroyCancellationToken);

        target.text = "";
        window.gameObject.SetActive(false);
        soundManager.PlaySe(sound);

        await UniTask.Delay(TimeSpan.FromSeconds(2.0f));
        await Flash();

        //startCoroutine�ł͂Ȃ���yield�@return �������Ă�����Ɠ����iyield return�̈Ӗ��𒲂ׂĂ����j
        await UniTask.Delay(TimeSpan.FromSeconds(2.0f));
        light2D.intensity = 1.0f;
        soundManager.StopSe(flashSe);
        soundManager.PlayBgm(suspiciousBgm);
        //camera�̏���
        Event2Camera();

        await UniTask.Delay(TimeSpan.FromSeconds(6.0f));

        window.gameObject.SetActive(true);
        await MessageManager.message_instance.MessageWindowOnceActive(messages2, names2, images2, ct: destroyCancellationToken);
        
        target.text = "";
        window.gameObject.SetActive(false);
        
        cameraManager.girlCamera = true;
        await UniTask.Delay(TimeSpan.FromSeconds(2.0f));
        soundManager.PlaySe(doorSound);
        await UniTask.Delay(TimeSpan.FromSeconds(2.0f));
        
        guards.transform.position = new Vector3(-76, 5, 0);
        cameraManager.girlCamera = false;

        await UniTask.Delay(TimeSpan.FromSeconds(3.0f));
        
        window.gameObject.SetActive(true);
        await MessageManager.message_instance.MessageWindowOnceActive(messages3, names3, images3, ct: destroyCancellationToken);

        target.text = "";
        window.gameObject.SetActive(false);

        await Flash();

        await UniTask.Delay(TimeSpan.FromSeconds(3.0f));

        girl.transform.position = new Vector3(0, 0, 0);
        light2D.intensity = 1.0f;
        soundManager.StopSe(flashSe);
        window.gameObject.SetActive(true);
        await MessageManager.message_instance.MessageWindowOnceActive(messages4, names4, images4, ct: destroyCancellationToken);

        target.text = "";
        window.gameObject.SetActive(false);
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        await Blackout2();
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
        soundManager.StopBgm(suspiciousBgm);
        cameraManager.playerCamera = true;
        cameraAnimator.enabled = true;
        light2D.intensity = 1.0f;
        gameMenuUI.SetActive(false);
        playableDirector.Play();
        await UniTask.Delay(TimeSpan.FromSeconds(68f));
        await Blackout2();
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
        cameraAnimator.enabled = false;
        colorAdjustments.active = true;
        light2D.intensity = 1.0f;
        gameMenuUI.SetActive(true);

        window.gameObject.SetActive(true);
        await MessageManager.message_instance.MessageWindowOnceActive(messages5, names5, images5, ct: destroyCancellationToken);

        target.text = "";
        GameManager.m_instance.ImageErase(characterImage);
        window.gameObject.SetActive(false);
        GameManager.m_instance.stopSwitch = false;
    }
    private async UniTask Flash()
    {
        light2D = gameObject.GetComponent<Light2D>();
        light2D.intensity = 1.0f;
        soundManager.PlaySe(flashSe);
        while(light2D.intensity < 7.0f)
        {
            light2D.intensity += 0.1f;
            await UniTask.Delay(1);
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
    private async UniTask Blackout()
    {
        light2D.intensity = 1.0f;
        GameManager.m_instance.stopSwitch = true;
        while (light2D.intensity > 0.01f)
        {
            light2D.intensity -= 0.012f;
            await UniTask.Delay(1);
        }
        friends[0].transform.position = new Vector3(-81, 19, 0);
        friends[1].transform.position = new Vector3(-81, 21, 0);
        friends[2].transform.position = new Vector3(-79, 21, 0);
        friends[3].transform.position = new Vector3(-78, 20, 0);
    }
    private async UniTask Blackout2()
    {
        light2D.intensity = 1.0f;
        GameManager.m_instance.stopSwitch = true;
        while (light2D.intensity > 0.01f)
        {
            light2D.intensity -= 0.012f;
            await UniTask.Delay(1);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class RescueEvent : MonoBehaviour
{
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
    public Image characterImage;
    public Canvas window;
    public Text target;
    public Text nameText;

    public SoundManager soundManager;
    public AudioClip doorSound;
    public AudioClip ChasedBGM;
    public NotEnter6 notEnter6;
    public ToEvent3 toEvent3;
    public GameTeleportManager gameTeleportManager;
    public GameObject Seiitirou;
    public GameObject Enemy;
    public GameObject Player;
    public GameObject colisionBox;
    private bool SeiitirouMove;
    public bool RescueSwitch;
    private IEnumerator coroutine;
    public Light2D light2D;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Seiitirou.transform.position.y > 70 && SeiitirouMove == true)
        {
            Seiitirou.transform.Translate(new Vector3(0f, -0.05f, 0.0f * Time.deltaTime));
        }
        // ���̃C�x���g���͓G�͏����Ȃ����G���o�Ă���Ԃ�BGM�𗬂�������B
        if (RescueSwitch == true)
        {
            gameTeleportManager.enemyRndNum = 99;

            soundManager.PlayBgm(ChasedBGM);
        }
        if (SeiitirouMove == true && RescueSwitch == false)
        {
            Player.gameObject.tag = "Untagged";
        }
        else
        {
            Player.gameObject.tag = "Player";
        }
        // �Q�[���I�[�o�[��5�񂵂����ʂ��^���ÂɂȂ�C�x���g���쐬����i�h�A�̕����ω������艽������̕ω��͂��Ă��������j
        // ��񎀂ʂ��Ƃɉ�ʂ��Â��Ȃ�BBGM�͂����܂܂œG��Player�̈ʒu�̓C�x���g���n�܂��������̈ʒu�Ɉړ����Ă���B
        // ���g���C�{�^���������Ǝ����I�ɗ�̏ꏊ�ɖ߂����B
        if (GameManager.m_instance.deathCount == 1)
        {
            light2D.intensity = 0.8f;
        }
        else if (GameManager.m_instance.deathCount == 2)
        {
            light2D.intensity = 0.6f;
        }
        else if (GameManager.m_instance.deathCount == 3)
        {
            light2D.intensity = 0.4f;
        }
        else if (GameManager.m_instance.deathCount == 4)
        {
            light2D.intensity = 0.2f;
        }
        else if (GameManager.m_instance.deathCount == 5)
        {
            light2D.intensity = 0.0f;
        }
        else if (GameManager.m_instance.deathCount == 6)
        {
            light2D.intensity = 1.0f;
        }
        if(notEnter6.rescued == true && Homing.m_instance.enemyEmerge == false)
        {
            colisionBox.SetActive(true);
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag.Equals("Player"))
        {
            if (notEnter6.rescued == false)
            {
                return;
            }
            if (notEnter6.rescued == true && RescueSwitch == false)
            {
                CapsuleCollider2D capsuleCollider = Seiitirou.GetComponent<CapsuleCollider2D>();
                PlayerManager.m_instance.m_speed = 0;
                capsuleCollider.enabled = false;
                coroutine = RescueSeiitirouEvent();
                StartCoroutine(coroutine);
                Homing.m_instance.enemyEmerge = true;
                toEvent3.event3flag = true;
            }
        }
    }
    IEnumerator RescueSeiitirouEvent()
    {
        // �Ƃ茾

        window.gameObject.SetActive(true);
        yield return OnMessage1();
        window.gameObject.SetActive(false);

        yield return new WaitForSeconds(2.0f);
        //����Y���o�Ă��ĉ�b����
        colisionBox.SetActive(false);
        soundManager.PlaySe(doorSound);
        Seiitirou.transform.position = new Vector2(35,71);
        yield return new WaitForSeconds(0.5f);
        SeiitirouMove = true;

        window.gameObject.SetActive(true);
        yield return OnMessage2();
        target.text = "";
        window.gameObject.SetActive(false);
        //����Y�������Ă����B�h�A�̉��ƂƂ��ɉ�������������o�Ă���
        PlayerManager.m_instance.m_speed = 0;
        yield return SeiitirouLeave();
        soundManager.PlaySe(doorSound);
        Seiitirou.transform.position = new Vector2(24, 0);
        yield return new WaitForSeconds(2.0f);
        
        Enemy.gameObject.SetActive(true);
        RescueSwitch = true;
        Homing.m_instance.speed = 0;
        Enemy.transform.position = new Vector2(35, 71);

        window.gameObject.SetActive(true);
        yield return OnMessage3();
        target.text = "";
        window.gameObject.SetActive(false);

        PlayerManager.m_instance.m_speed = 0.075f;
        Homing.m_instance.speed = 2;

        soundManager.PlayBgm(ChasedBGM);

        StopCoroutine(coroutine);
    }
    protected void showMessage(string message, string name, Sprite image)
    {
        target.text = message;
        nameText.text = name;
        characterImage.sprite = image;
    }
    IEnumerator OnMessage1()
    {
        for (int i = 0; i < messages.Count; ++i)
        {
            // 1�t���[���� ������ҋ@(���L����1)
            yield return null;
            showMessage(messages[i], names[i], images[i]);
            yield return new WaitUntil(() => (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)));
        }
        yield break;
    }
    IEnumerator OnMessage2()
    {
        for (int i = 0; i < messages2.Count; ++i)
        {
            // 1�t���[���� ������ҋ@(���L����1)
            yield return null;
            showMessage(messages2[i], names2[i], images2[i]);
            yield return new WaitUntil(() => (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)));
        }
        yield break;
    }
    IEnumerator OnMessage3()
    {
        for (int i = 0; i < messages3.Count; ++i)
        {
            // 1�t���[���� ������ҋ@(���L����1)
            yield return null;
            showMessage(messages3[i], names3[i], images3[i]);
            yield return new WaitUntil(() => (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)));
        }
        yield break;
    }
    IEnumerator SeiitirouLeave()
    {
        while (Seiitirou.transform.position.y > 60)
        {
            yield return null;
            Seiitirou.transform.Translate(new Vector3(0f, -0.05f, 0.0f * Time.deltaTime));
        }
        yield break;
    }
}

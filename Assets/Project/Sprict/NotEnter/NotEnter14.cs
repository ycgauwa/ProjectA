using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class NotEnter14 : MonoBehaviour
{
    /*���Œʂ�Ȃ��X�N���v�g
    ���̂܂܂Ɣ��e�������Ă����Ԃł̏����͈قȂ�B
    ���̂܂܂̓��b�Z�[�W���\������邾�������Ă�����B
    �C�x���g����
    �C�x���g�̓��b�Z�[�W�������B���̌��ʈÓ]�����2�̉��ŏo���Ă��烁�b�Z�[�W�o���B
    �A�C�e���͏���B
    �����œ��ނƃf����clear�I�I�I�I
    */
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<Sprite> image;
    [SerializeField]
    private List<Sprite> image2;
    [SerializeField]
    private List<Sprite> image3;
    public Item bomb;
    public Inventry inventry;
    public GameObject bigStone;

    public SoundManager soundManager;
    public AudioClip bombTimer;
    public AudioClip bombSound;
    private int isContactedAndChara = 0;
    private int savedNam;
    public Light2D light2D;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        //�K�l�̎��ɕ\������郁�b�Z�[�W
        if(collider.gameObject.tag.Equals("Player"))
            isContactedAndChara = 1;
        else if(collider.gameObject.name == "Matiba Haru")
            isContactedAndChara = 2;
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
            isContactedAndChara = 0;
        else if(collider.gameObject.name == "Matiba Haru")
            isContactedAndChara = 0;
    }
    private void Update()//���̓`�F�b�N��Update�ɏ���
    {
        if(isContactedAndChara > 0 && (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)))
        {
            savedNam = isContactedAndChara;
            isContactedAndChara = 0;
            if(!bomb.checkPossession)
                NotUseBomb();
            else
            {
                UseBomb().Forget();
            }
        }
    }

    private void NotUseBomb()
    {
        //�@���e�Ȃ�������messege�����B��l�������B
        MessageManager.message_instance.MessageWindowActive(messages, names, image, ct: destroyCancellationToken).Forget();
    }
    private async UniTask UseBomb()
    {
        //���e���肾����C�x���g����
        if(savedNam == 1)
            await MessageManager.message_instance.MessageWindowActive(messages, names, image2, ct: destroyCancellationToken);
        else if(savedNam == 2)
            await MessageManager.message_instance.MessageWindowActive(messages, names, image3, ct: destroyCancellationToken);

        inventry.Delete(bomb);
        Blackout().Forget();
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
        soundManager.PlaySe(bombTimer);
        await UniTask.Delay(TimeSpan.FromSeconds(4.5f));
        soundManager.PlaySe(bombSound);
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));

        bigStone.transform.position = new Vector3(0f, 0f, 0f);
        light2D.intensity = 1.0f;

        if(savedNam == 1)
            await MessageManager.message_instance.MessageWindowActive(messages, names, image2, ct: destroyCancellationToken);
        else if(savedNam == 2)
            await MessageManager.message_instance.MessageWindowActive(messages, names, image3, ct: destroyCancellationToken);
        
        GameManager.m_instance.stopSwitch = false;
    }
    private async UniTask Blackout()
    {
        light2D.intensity = 1.0f;
        GameManager.m_instance.stopSwitch = true;
        while(light2D.intensity > 0.01f)
        {
            light2D.intensity -= 0.012f;
            await UniTask.Delay(1);
        }
    }
}

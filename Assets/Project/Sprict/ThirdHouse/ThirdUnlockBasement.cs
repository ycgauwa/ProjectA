using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class ThirdUnlockBasement : MonoBehaviour
{
    //�n�����ɍs�����߂̃X�N���v�g�B�K�l���Ƃ��̂܂܉�b�ŏI��邯�ǐ����b������t�F�[�h�������Ēn���ւ̊K�i�������B
    //�z�c�������Ă�X�N���v�g�ˁB

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
    public Canvas selectionCanvas;
    public Image selection;
    public GameObject firstSelect;
    private int isContactedAndChara = 0;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //�K�l�̎��ɕ\������郁�b�Z�[�W
        if (collider.gameObject.tag.Equals("Player"))
            isContactedAndChara = 2;
        else if (collider.gameObject.name == "Matiba Haru")
            isContactedAndChara = 2;
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag.Equals("Player"))
            isContactedAndChara = 0;
        else if (collider.gameObject.name == "Matiba Haru")
            isContactedAndChara = 0;
    }

    private void Update()//���̓`�F�b�N��Update�ɏ���
    {
        if (isContactedAndChara > 0 && (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)))
        {
            switch (isContactedAndChara)
            {
                case 1://�K�l�̏ꍇ
                    MessageManager.message_instance.MessageWindowActive(messages, names, image, ct: destroyCancellationToken).Forget();
                    return;
                case 2://���̏ꍇ
                    CheckFuton().Forget();
                    isContactedAndChara = 0;
                    return;
                default:
                    return;
            }
        }
    }
    //���̃��\�b�h�ł͐����z�c�𒲂ׂČ��ɏœ_�������Ȃ���J�����𓮂����ā������őI�����I�z�c�𓮂������H�������Ȃ����H�i�����ɕz�c���������j�A����Ƀ��b�Z�[�W��\�������Ȃ���I�������o��or���̂܂�
    //�t�F�[�h�A�E�g���Ēn�����ɍ~��Ă������ɂ���B
    private async UniTask CheckFuton()
    {
        GameManager.m_instance.notSaveSwitch = true;
        GameManager.m_instance.stopSwitch = true;
        await MessageManager.message_instance.MessageWindowActive(messages2, names2, images2, ct: destroyCancellationToken);//���ׂċ���
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
        cameraManager.cameraInstance.playerCamera = false;//�{���͐��J�����A������Player�p�ɂ��Ă�B
        GameManager.m_instance.mainCamera.transform.DOLocalMove(new Vector3(252.5f, 34.68f, -10), 2f);
        while (cameraManager.cameraInstance.cameraSize > 2)
        {
            cameraManager.cameraInstance.cameraSize -= 0.02f;
            await UniTask.Delay(2);
        }
        await MessageManager.message_instance.MessageSelectWindowActive(messages3, names3, images3, selectionCanvas, selection, firstSelect, ct: destroyCancellationToken);//�����őI����������B
        cameraManager.cameraInstance.cameraSize = 5;
        cameraManager.cameraInstance.playerCamera = true;
    }
    private void DownBasementMethod()//�K�i���~���I���̃��\�b�h
    {
        DescendToBasement().Forget();
    }
    private void StayWasituMethod()//�K�i���~��Ȃ����\�b�h
    {
        MessageManager.message_instance.MessageWindowActive(messages3, names3, images3,ct: destroyCancellationToken).Forget();
        GameManager.m_instance.stopSwitch = false;
        GameManager.m_instance.notSaveSwitch = false;
    }
    private async UniTask DescendToBasement()
    {
        await UniTask.Delay(TimeSpan.FromSeconds(1f));
    }
    private async UniTask Blackout()
    {
        SecondHouseManager.secondHouse_instance.light2D.intensity = 1.0f;
        while (SecondHouseManager.secondHouse_instance.light2D.intensity > 0.01f)
        {
            SecondHouseManager.secondHouse_instance.light2D.intensity -= 0.012f;
            await UniTask.Delay(1);
        }
    }
}

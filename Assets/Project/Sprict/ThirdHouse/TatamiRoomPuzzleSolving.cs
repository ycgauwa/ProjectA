using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System.Threading;
using static UnityEngine.InputSystem.Controls.AxisControl;
using System;

public class TatamiRoomPuzzleSolving : MonoBehaviour
{
    // ��̕����E�o�̃R�[�h�B
    [SerializeField]
    private List<string> Messages;
    [SerializeField]
    private List<string> Names;
    [SerializeField]
    private List<Sprite> images;
    public Button[] buttons;  // �{�^���̔z��
    public Button DeleteButton;
    public Image[] Answers;  // �E���̓����̋󗓂̔z��
    private string selectedCharacters;
    private Text[] AnswersText; //�����̃e�L�X�g
    private bool[] CharacterStates;  // �󗓂̏�Ԃ��Ǘ�����z��
    public string[] kanaCharacters; // �e�{�^���ɑΉ����镶���i�蓮�ݒ�j
    public NotEnter16 notEnter;
    public AudioClip clickSound;
    void Start()
    {
        AnswersText = new Text[Answers.Length];  // �z���������
        CharacterStates = new bool[Answers.Length];
        for(int i = 0; i < Answers.Length; i++)
        {
            AnswersText[i] = Answers[i]?.GetComponentInChildren<Text>();
            if(AnswersText[i] != null)
            {
                AnswersText[i].text = ""; // ������Ԃŋ�
            }
            else
            {
                Debug.LogError($"Text�R���|�[�l���g��������܂���: {Answers[i]?.name}");
            }

            AnswersText[i].text = "";  // ������Ԃŕ����͋�
        }

        for(int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            buttons[i].onClick.AddListener(() => SetEnteredText(kanaCharacters[index]));  // �e�{�^���ɃN���b�N�C�x���g��o�^
            //Debug.Log($"�{�^�� {buttons[i].name} �ɃC�x���g�o�^: {kanaCharacters[index]}");
        }
        DeleteButton.onClick.AddListener(() => DeleteButtonMethod());
    }
    private void Update()
    {
        if(selectedCharacters == "�����������ɂ�")
        {
            KeyOpenEvent().Forget();
        }
    }

    private async UniTask KeyOpenEvent(CancellationToken ct = default)
    {
        selectedCharacters = "";
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
        notEnter.buttonGrid.gameObject.SetActive(false);
        notEnter.thirdHouseCanvas.gameObject.SetActive(false);
        await MessageManager.message_instance.MessageWindowActive(Messages, Names, images, ct: destroyCancellationToken);
    }
    public void SetEnteredText(string SetedJapanese)//button�ɓo�^���郁�\�b�h
    {
        SoundManager.sound_Instance.PlaySe(clickSound);
        // �󗓂ɕ�������͂���@���������炪�o���Ă��Ȃ��B
        
        foreach(Text selectedText in AnswersText)
        {
            if(selectedText.text == "")
            {
                selectedText.text = SetedJapanese;
                selectedCharacters += SetedJapanese;

                return;
            }
            // �󗓂���Ȃ������ꍇ���̃e�L�X�g�ɓ��ꂽ���B���󗓂���Ȃ��Ȃ牽�����Ȃ��H
        }
    }
    public void DeleteButtonMethod()
    {
        SoundManager.sound_Instance.PlaySe(clickSound);

        for (int i = AnswersText.Length -1; i >= 0; i--)
        {
            Text selectedText = AnswersText[i];
            if (selectedText.text != "")
            {
                selectedText.text = "";
                selectedCharacters = selectedCharacters.Substring(0,selectedCharacters.Length - 1);
                return;
            }
        }
    }
}

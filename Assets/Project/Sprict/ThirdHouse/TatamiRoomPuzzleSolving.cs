using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.InputSystem.Controls.AxisControl;

public class TatamiRoomPuzzleSolving : MonoBehaviour
{
    // ��̕����E�o�̃R�[�h�B

    public Button[] buttons;  // �{�^���̔z��
    public Image[] Answers;  // �E���̓����̋󗓂̔z��
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
    }
    public void SetEnteredText(string SetedJapanese)//button�ɓo�^���郁�\�b�h
    {
        SoundManager.sound_Instance.PlaySe(clickSound);
        // �{�^���ɑΉ����镶�����擾
        string buttonText = GetJananeseIndexForButton(SetedJapanese);
        // �󗓂ɕ�������͂���@���������炪�o���Ă��Ȃ��B
        
        foreach(Text selectedText in AnswersText)
        {
            if(selectedText.text == "")
            {
                selectedText.text = SetedJapanese;
                return;
            }
            // �󗓂���Ȃ������ꍇ���̃e�L�X�g�ɓ��ꂽ���B���󗓂���Ȃ��Ȃ牽�����Ȃ��H

            Debug.Log(SetedJapanese);
        }
        /*
        // ���ׂẴ����v�������Ă��邩�m�F
        if(AllLampsOn())
        {
            notEnter.DefuseLocked();
        }*/
    }

    string GetJananeseIndexForButton(string JanapeseIndex)// �����ꂽ�{�^�����牽����͂��邩�Ԃ�
    {
        switch(JanapeseIndex)
        {
            case "��": return "��";
            case "��": return "��";
            case "��": return "��";
            case "��": return "��";
            case "��": return "��";
            case "��": return "��";
        }
        return null;
    }
}

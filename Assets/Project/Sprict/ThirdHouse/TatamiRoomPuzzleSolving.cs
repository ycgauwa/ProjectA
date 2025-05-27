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
    // 畳の部屋脱出のコード。
    [SerializeField]
    private List<string> Messages;
    [SerializeField]
    private List<string> Names;
    [SerializeField]
    private List<Sprite> images;
    public Button[] buttons;  // ボタンの配列
    public Button DeleteButton;
    public Image[] Answers;  // 右側の答えの空欄の配列
    private string selectedCharacters;
    private Text[] AnswersText; //答えのテキスト
    private bool[] CharacterStates;  // 空欄の状態を管理する配列
    public string[] kanaCharacters; // 各ボタンに対応する文字（手動設定）
    public NotEnter16 notEnter;
    public AudioClip clickSound;
    void Start()
    {
        AnswersText = new Text[Answers.Length];  // 配列を初期化
        CharacterStates = new bool[Answers.Length];
        for(int i = 0; i < Answers.Length; i++)
        {
            AnswersText[i] = Answers[i]?.GetComponentInChildren<Text>();
            if(AnswersText[i] != null)
            {
                AnswersText[i].text = ""; // 初期状態で空白
            }
            else
            {
                Debug.LogError($"Textコンポーネントが見つかりません: {Answers[i]?.name}");
            }

            AnswersText[i].text = "";  // 初期状態で文字は空欄
        }

        for(int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            buttons[i].onClick.AddListener(() => SetEnteredText(kanaCharacters[index]));  // 各ボタンにクリックイベントを登録
            //Debug.Log($"ボタン {buttons[i].name} にイベント登録: {kanaCharacters[index]}");
        }
        DeleteButton.onClick.AddListener(() => DeleteButtonMethod());
    }
    private void Update()
    {
        if(selectedCharacters == "いきしききにみ")
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
    public void SetEnteredText(string SetedJapanese)//buttonに登録するメソッド
    {
        SoundManager.sound_Instance.PlaySe(clickSound);
        // 空欄に文字を入力する　←ここからが出来ていない。
        
        foreach(Text selectedText in AnswersText)
        {
            if(selectedText.text == "")
            {
                selectedText.text = SetedJapanese;
                selectedCharacters += SetedJapanese;

                return;
            }
            // 空欄じゃなかった場合次のテキストに入れたい。←空欄じゃないなら何もしない？
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

using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.InputSystem.Controls.AxisControl;

public class TatamiRoomPuzzleSolving : MonoBehaviour
{
    // 畳の部屋脱出のコード。

    public Button[] buttons;  // ボタンの配列
    public Image[] Answers;  // 右側の答えの空欄の配列
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
    }
    public void SetEnteredText(string SetedJapanese)//buttonに登録するメソッド
    {
        SoundManager.sound_Instance.PlaySe(clickSound);
        // ボタンに対応する文字を取得
        string buttonText = GetJananeseIndexForButton(SetedJapanese);
        // 空欄に文字を入力する　←ここからが出来ていない。
        
        foreach(Text selectedText in AnswersText)
        {
            if(selectedText.text == "")
            {
                selectedText.text = SetedJapanese;
                return;
            }
            // 空欄じゃなかった場合次のテキストに入れたい。←空欄じゃないなら何もしない？

            Debug.Log(SetedJapanese);
        }
        /*
        // すべてのランプが光っているか確認
        if(AllLampsOn())
        {
            notEnter.DefuseLocked();
        }*/
    }

    string GetJananeseIndexForButton(string JanapeseIndex)// 押されたボタンから何を入力するか返す
    {
        switch(JanapeseIndex)
        {
            case "あ": return "あ";
            case "い": return "い";
            case "う": return "う";
            case "え": return "え";
            case "お": return "お";
            case "か": return "か";
        }
        return null;
    }
}

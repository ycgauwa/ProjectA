using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockPuzzle : MonoBehaviour
{
    public Button[] buttons;  // ボタンの配列
    public Image[] lamps;  // ランプの配列
    public Sprite lampOnSprite;  // 光ったランプのスプライト
    public Sprite lampOffSprite;  // 消えたランプのスプライト
    private bool[] lampStates;  // ランプの状態を管理する配列
    public NotEnter15 notEnter;
    public AudioClip clickSound;
    void Start()
    {
        lampStates = new bool[lamps.Length];
        for(int i = 0; i < lamps.Length; i++)
        {
            lamps[i].sprite = lampOffSprite;  // 初期状態でランプは消灯
        }

        for(int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            buttons[i].onClick.AddListener(() => ToggleLamps(index));  // 各ボタンにクリックイベントを登録
        }
    }

    public void ToggleLamps(int buttonIndex)
    {
        SoundManager.sound_Instance.PlaySe(clickSound);
        // ボタンに対応するランプのインデックスを取得
        int[] lampIndexes = GetLampIndexesForButton(buttonIndex);
        // 各ランプをトグル（反転）する
        foreach(int index in lampIndexes)
        {
            lampStates[index] = !lampStates[index];  // 状態を反転
            lamps[index].sprite = lampStates[index] ? lampOnSprite : lampOffSprite;  // 反転した状態に応じてスプライトを変更
        }

        // すべてのランプが光っているか確認
        if(AllLampsOn())
        {
            notEnter.DefuseLocked();
        }
    }

    int[] GetLampIndexesForButton(int buttonIndex)
    {
        // 各ボタンに対応するランプのインデックス
        switch(buttonIndex)
        {
            case 0: return new int[] { 0, 1, 3 };  // ボタン1
            case 1: return new int[] { 0, 1, 2, 4 };  // ボタン2
            case 2: return new int[] { 1, 2, 5 };  // ボタン3
            case 3: return new int[] { 0, 3, 4, 6 };  // ボタン4
            case 4: return new int[] { 1, 3, 4, 5, 7 };  // ボタン5
            case 5: return new int[] { 2, 4, 5, 8 };  // ボタン6
            case 6: return new int[] { 3, 6, 7 };  // ボタン7
            case 7: return new int[] { 4, 6, 7, 8 };  // ボタン8
            case 8: return new int[] { 5, 7, 8 };  // ボタン9
            default: return new int[] { };
        }
    }

    bool AllLampsOn()
    {
        foreach(bool state in lampStates)
        {
            if(!state) return false;  // 1つでも消えているランプがあればfalse
        }
        return true;  // すべてのランプが光っていればtrue
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventry : MonoBehaviour
{
    public static Inventry instance;
    public InventryUI inventryUI;

    public List<Item> items = new List<Item>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        inventryUI = GetComponent<InventryUI>();
        //inventryUI.UpdateUI();
    }
    public void Add(Item item)
    {
        items.Add(item);
        inventryUI.UpdateUI();
    }
    /*2023年11月2日
     インベントリについて
     UI系で非アクティブが多くStart関数で処理したいなら空のオブジェクトを作ってそこにUI系のスクリプトを入れてあげる
    ゲーム中にリストを増やしてあげたいときは、処理に一回プレハブを複製した後に（初期化、アイテムの情報をスロットに
    入れてあげること）をしてSetActiveをTrueにすればいい感じになる！Nullでエラーになる時は参照型が確定しているからそこから
    調べてあげればいい。オブジェクトが非アクティブの時StartやAwakeは動いてくれない
    参照したりとか引数をうまく使ってあげてスクリプト同士がきちんとかみ合ってくれるように作る*/
}

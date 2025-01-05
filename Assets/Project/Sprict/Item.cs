using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="New Item", menuName = "ScriptableObject/Create Item")]

public class Item : ScriptableObject
{
    //new public string name = "New Item";
    public Sprite icon = null;
    public string itemText = null;
    public int itemID;//1～200は謎解き用アイテム201～250はキャラアイテム251～300は鍵301～400はその他アイテム
    public bool checkPossession = false;
    public bool selectedItem = false;

    public void start()
    {
        checkPossession = false;
    }


    // アイテムをGetしたときcheckPossessionをTrueにしてインベントリに追加、
    // GetItemとかの方で操作するときはアタッチして変数として使えば良い

}

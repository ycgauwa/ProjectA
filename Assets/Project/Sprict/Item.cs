using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="New Item", menuName = "ScriptableObject/Create Item")]

public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public string itemText = null;
    public bool checkPossession = false;
    public bool selectedItem = false;

    public void start()
    {
        checkPossession = false;
    }

    // アイテムをGetしたときcheckPossessionをTrueにしてインベントリに追加、
    // GetItemとかの方で操作するときはアタッチして変数として使えば良い

}

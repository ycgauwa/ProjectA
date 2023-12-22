using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObject/Character")]
public class Character : ScriptableObject
{
    //イベント時以外で話す内容や顔はこっちで設定して指定できる。

    public string charaName;
    public string description;
    public List<Sprite> characterImages;
    public List<string> messageTexts;

}

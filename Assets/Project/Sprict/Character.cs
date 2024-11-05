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
    public List<Sprite> characterImages1;
    public List<Sprite> characterImages2;
    public List<Sprite> characterImages3;
    public List<Sprite> characterImages4;
    public List<Sprite> itemGiveImage1;
    public List<Sprite> itemGiveImage2;
    public List<Sprite> itemGivedImage;
    public List<Sprite> freeImage1;
    public List<string> messageTexts1;
    public List<string> messageTexts2;
    public List<string> messageTexts3;
    public List<string> messageTexts4;
    public List<string> itemGiveMessage1;
    public List<string> itemGiveMessage2;
    public List<string> itemGivedMessage;
    public List<string> freeText1;
}

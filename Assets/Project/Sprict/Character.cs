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
    public int FavorabilityCount = 100;
    public List<Sprite> characterImages1;
    public List<Sprite> characterImages2;
    public List<Sprite> characterImages3;
    public List<Sprite> characterImages4;
    public List<Sprite> maxAffinityImages;
    public List<Sprite> highAffinityImages;
    public List<Sprite> moderateAffinityImages;
    public List<Sprite> lowAffinityImages;
    public List<Sprite> minAffinityImages;
    public List<Sprite> negativeAffinityImages;
    public List<Sprite> itemGiveImage1;
    public List<Sprite> itemGiveImage2;
    public List<Sprite> itemGivedImage;
    public List<Sprite> freeImage1;
    public List<string> messageTexts1;
    public List<string> messageTexts2;
    public List<string> messageTexts3;
    public List<string> messageTexts4;
    public List<string> maxAffinityMessages;
    public List<string> highAffinityMessages;
    public List<string> moderateAffinityMessages;
    public List<string> lowAffinityMessages;
    public List<string> minAffinityMessages;
    public List<string> negativeAffinityMessages;

    public List<string> itemGiveMessage1;
    public List<string> itemGiveMessage2;
    public List<string> itemGivedMessage;
    public List<string> freeText1;
    public List<string> messageNames1;
    public List<string> messageNames2;
    public List<string> messageNames3;
    public List<string> messageNames4;
    public List<string> maxAffinityNames;
    public List<string> highAffinityNames;
    public List<string> moderateAffinityNames;
    public List<string> lowAffinityNames;
    public List<string> minAffinityNames;
    public List<string> negativeAffinityNames;

    public List<string> itemGiveNames1;
    public List<string> itemGiveNames2;
    public List<string> itemGivedNames;
    public List<string> freeNames1;
}

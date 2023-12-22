using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObject/Character")]
public class Character : ScriptableObject
{
    //�C�x���g���ȊO�Řb�����e���͂������Őݒ肵�Ďw��ł���B

    public string charaName;
    public string description;
    public List<Sprite> characterImages;
    public List<string> messageTexts;

}

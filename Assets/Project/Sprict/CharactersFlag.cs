using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharactersFlag", menuName = "ScriptableObject/CharactersFlag")]
public class CharactersFlag : ScriptableObject
{
    //要はここにキャラクター毎に変わるであろう目的の内容を取れるようにしていきたいって感じ
    //でも現在地については共通だからそこに関しては別のスクリプトを作成して行くって感じで。
    public string[] JPN_characterDestination;
    public string[] EN_characterDestination;
}

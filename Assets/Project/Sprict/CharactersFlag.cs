using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharactersFlag", menuName = "ScriptableObject/CharactersFlag")]
public class CharactersFlag : ScriptableObject
{
    //�v�͂����ɃL�����N�^�[���ɕς��ł��낤�ړI�̓��e������悤�ɂ��Ă����������Ċ���
    //�ł����ݒn�ɂ��Ă͋��ʂ����炻���Ɋւ��Ă͕ʂ̃X�N���v�g���쐬���čs�����Ċ����ŁB
    public string[] JPN_characterDestination;
    public string[] EN_characterDestination;
}

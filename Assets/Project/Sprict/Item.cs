using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="New Item", menuName = "ScriptableObject/Create Item")]

public class Item : ScriptableObject
{
    //new public string name = "New Item";
    public Sprite icon = null;
    public string itemText = null;
    public int itemID;//1�`200�͓�����p�A�C�e��201�`250�̓L�����A�C�e��251�`300�͌�301�`400�͂��̑��A�C�e��
    public bool checkPossession = false;
    public bool selectedItem = false;

    public void start()
    {
        checkPossession = false;
    }


    // �A�C�e����Get�����Ƃ�checkPossession��True�ɂ��ăC���x���g���ɒǉ��A
    // GetItem�Ƃ��̕��ő��삷��Ƃ��̓A�^�b�`���ĕϐ��Ƃ��Ďg���Ηǂ�

}

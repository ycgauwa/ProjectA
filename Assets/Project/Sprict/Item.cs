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

    // �A�C�e����Get�����Ƃ�checkPossession��True�ɂ��ăC���x���g���ɒǉ��A
    // GetItem�Ƃ��̕��ő��삷��Ƃ��̓A�^�b�`���ĕϐ��Ƃ��Ďg���Ηǂ�

}

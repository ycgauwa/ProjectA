using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassWord : MonoBehaviour
{
    [SerializeField] GameObject fleldObj;
    public InputField passcode;
    public Item item;
    public Inventry inventry;
    private bool getItemDoll = false;
    void Start()
    {
        //����͂���{���摜��G���ċN���邱�Ƃ�����Image���擾���Ă邯�ǉ��̃Q�[���̏ꍇ�́A�A�H
        GetComponent<Image>().sprite = item.icon;
        fleldObj = GameObject.Find("Passcode1");
        //passcode = fleldObj.GetComponent<InputField>();
    }

    public void InputText()
    {
        if(item.checkPossession == false)
        {
            if(passcode.text == "0622")
            {
                // �����Ή����قǃA�C�e��������ł��Ă��܂������x�擾���������ł��Ȃ��d�l�ɂ��Ă�����B
                // Inventry�̒ǉ��i�A�C�e���̎擾�j
                inventry.Add(item);
                item.checkPossession = true;
            }
        }
    }
}

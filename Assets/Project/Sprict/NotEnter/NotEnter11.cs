using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.InputSystem;

public class NotEnter11 : MonoBehaviour
{
    public Item key5;
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<Sprite> images;
    //�f�Ï��ɂ�1�񌢂ɒǂ��Ă���߂�ƌ����Ă�ꏊ�����邽�߁A�����Ŏ�p���̌�����ɂ���B
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(key5.geted == false)
        {
            if(collider.gameObject.tag.Equals("Player"))
            {
                MessageManager.message_instance.MessageWindowActive(messages, names, images, ct: destroyCancellationToken).Forget();
            }
        }
        else if(key5.geted)
        {
            if(collider.gameObject.tag.Equals("Player"))
            {
                name = "Warp2-23";
            }
        }
    }
}

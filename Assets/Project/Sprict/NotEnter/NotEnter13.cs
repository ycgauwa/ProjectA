using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class NotEnter13 : MonoBehaviour
{
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<Sprite> image;
    [SerializeField]
    private List<string> messages2;
    [SerializeField]
    private List<string> names2;
    [SerializeField]
    private List<Sprite> image2;
    /*
     * �b��������L�����N�^�[�ɂ���Č��ʂ��قȂ�B
     * ���Ȃ݂ɂǂ������ʂ�Ȃ����A�������������ǂ���̃��[�g�ł��ʂ��悤�ɂ���B
     * ���̏����͂܂�����̂��ߍ���̃X�N���v�g�͏������Œʂ�Ȃ����邾��
     */
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
        {
            MessageManager.message_instance.MessageWindowActive(messages, names, image, ct: destroyCancellationToken).Forget();
        }
        else if(collider.gameObject.name == "Matiba Haru")
        {
            MessageManager.message_instance.MessageWindowActive(messages2, names2, image2, ct: destroyCancellationToken).Forget();
        }

    }
}

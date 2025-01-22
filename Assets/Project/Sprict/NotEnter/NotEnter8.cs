using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class NotEnter8 : MonoBehaviour
{

    /*
    2���ڂ̈�K�̓䂪������܂�2�K�ɂ͂����Ȃ��d�g�݂����B
    ���������Ȃ��񂶂�Ȃ��čs�����Ƃ����Ƃ��ɉ����y�i���e�B�������̓_���[�W��H�炤���o�������ċ��������̂��{��
    ���������܂ł̓��b�Z�[�W�Ɖ��o���o���āA�����󂢂Ă���͂��̂܂ܒʂ�銴���ɂ��Ă�������
    ���o���o�������Ȃ�A2�K�ɍs�����Ƃ��Ă���ʂ�Ȃ��āA���ɍs�����Ƃ��ɐl�`�����������݂����Ȏd�g�݂ɂ��Ă�����
    */

    public SecondHouseManager secondHouseManager;
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<Sprite> images;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Seiitirou"))
            gameObject.tag = "Minnka2-5";
        if (secondHouseManager.firstkey == false)
        {
            if (collider.gameObject.tag.Equals("Player"))
            {
                MessageManager.message_instance.MessageWindowActive(messages, names, images, ct: destroyCancellationToken).Forget();
            }
        }
        else if (secondHouseManager.firstkey == true)
        {
            if (collider.gameObject.tag.Equals("Player"))
            {
                gameObject.tag = "Minnka2-5";
            }
        }
    }
}

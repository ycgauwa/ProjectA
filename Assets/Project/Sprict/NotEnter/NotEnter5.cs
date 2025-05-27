using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class NotEnter5 : MonoBehaviour
{
    //�@���ƂP�̂Q�K�̈�ڂ̌��t���̃h�A
    // �����Ȃ��Ɠ���Ȃ��h�A�̂��߂̃X�N���v�g
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<Sprite> images;
    [SerializeField]
    private List<string> messages2;
    [SerializeField]
    private List<string> names2;
    [SerializeField]
    private List<Sprite> images2;
    public RescueEvent rescueEvent;
    public GameObject enemy;
    public Homing homing;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(ItemDateBase.itemDate_instance.GetItemId(252).geted == false)
        {
            if(collider.gameObject.tag.Equals("Player"))
            {
                //�u�͂���܂����v
                MessageManager.message_instance.MessageWindowActive(GameManager.m_instance.GetMessages(name, "NotEnter"), GameManager.m_instance.GetSpeakerName(name, "NotEnter"), images, ct: destroyCancellationToken).Forget();
            }
        }
        else if(rescueEvent.RescueSwitch == true)
        {
            if(collider.gameObject.tag.Equals("Player"))
            {
                gameObject.tag = "Untagged";
                //�u�����ɓ����Ă����ʂ���v
                MessageManager.message_instance.MessageWindowActive(GameManager.m_instance.GetMessages(name + "A", "NotEnter"), GameManager.m_instance.GetSpeakerName(name + "A", "NotEnter"), images2, ct: destroyCancellationToken).Forget();
            }
        }
        else if(ItemDateBase.itemDate_instance.GetItemId(252).geted == true)
        {
            if(collider.gameObject.tag.Equals("Player"))
            {
                gameObject.tag = "Minnka1-20";
                if(ItemDateBase.itemDate_instance.GetItemId(252).checkPossession)
                    GameManager.m_instance.inventry.Delete(ItemDateBase.itemDate_instance.GetItemId(252));
                if (enemy.gameObject.activeSelf)
                {
                    homing.teleportManager.StopChased();
                    homing.enemyEmerge = false;
                }
            }
        }
    }
}

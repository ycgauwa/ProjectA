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
    public bool getKey2;
    public ItemDateBase itemDateBase;
    public RescueEvent rescueEvent;
    public GameObject enemy;
    public Homing homing;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(getKey2 == false)
        {
            if(collider.gameObject.tag.Equals("Player"))
            {
                MessageManager.message_instance.MessageWindowActive(GameManager.m_instance.GetMessages(name, "NotEnter"), GameManager.m_instance.GetSpeakerName(name, "NotEnter"), images, ct: destroyCancellationToken).Forget();
            }
        }
        else if(rescueEvent.RescueSwitch == true)
        {
            if(collider.gameObject.tag.Equals("Player"))
            {
                gameObject.tag = "Untagged";
                MessageManager.message_instance.MessageWindowActive(GameManager.m_instance.GetMessages(name + "A", "NotEnter"), GameManager.m_instance.GetSpeakerName(name + "A", "NotEnter"), images2, ct: destroyCancellationToken).Forget();
            }
        }
        else if(getKey2 == true)
        {
            if(collider.gameObject.tag.Equals("Player"))
            {
                this.gameObject.tag = "Minnka1-20";
                Inventry.instance.Delete(itemDateBase.GetItemId(252));
                if (enemy.gameObject.activeSelf)
                {
                    homing.teleportManager.StopChased();
                    homing.enemyEmerge = false;
                }

            }
        }

    }

}

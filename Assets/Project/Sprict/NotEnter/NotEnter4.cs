using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotEnter4 : MonoBehaviour
{
    // �����Ȃ��Ɠ���Ȃ��h�A�̂��߂̃X�N���v�g
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<Sprite> images;
    public bool getKey1;
    public ToEvent3 toevent3;
    public ItemDateBase itemDateBase;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(getKey1 == false)
        {
            if(collider.gameObject.tag.Equals("Player"))
            {
                MessageManager.message_instance.MessageWindowActive(messages, names, images);
            }
        }
        else if(getKey1 == true)
        {
            if(collider.gameObject.tag.Equals("Player"))
            {
                this.gameObject.tag = "Minnka1-17";
                // ��U�G���o�Ă��Ȃ��悤�ɂ���B
                toevent3.event3flag = false;
                itemDateBase.Items4Delete();
            }
        }
    }
}

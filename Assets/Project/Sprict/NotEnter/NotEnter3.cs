using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotEnter3 : MonoBehaviour
{
    //�@����̓t���O���������Ȃ������烁�b�Z�[�W�\���A������ꂽ��^�O���������Ă�����

    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<Sprite> images;
    public GameObject player;
    public GameObject seiitirou;
    public ToEvent3 toevent3;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(toevent3.event3flag == false)
        {
            if(collider.gameObject.tag.Equals("Player"))
            {
                MessageManager.message_instance.MessageWindowActive(messages, names, images);
            }
        }
        // �C�x���g���I��������TP�ł���悤�ɂ�����
        else if(toevent3.event3flag == true)
        {
            this.gameObject.tag = "Minnka1-1";
            if(collider.gameObject.tag.Equals("Seiitirou"))
            {
                seiitirou.transform.position = new Vector2(24, -2);
            }
        }
    }
}

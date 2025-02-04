using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ToEvent4 : MonoBehaviour
{
    // �����ɓ��������ɃC�x���g���邩��G���o�Ă��Ȃ��悤�ɂ��āA����ŃC�x���g���I�������
    // ������G���o�Ă��Ă������悤�ɂ���B���ׂ����ɂ͉��y���~�߂đI�������o���ĕʂ̉��y���o���B

    public GameObject player;
    public bool event4flag;
    public bool playerStop;
    public ToEvent3 toevent3;

    // ���b�Z�[�W�E�B���h�E�p�̕ϐ�
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

    // Start is called before the first frame update
    void Start()
    {
        event4flag = false;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(!event4flag) //�t���O�������ĂȂ��Ƃ�
        {
            if(collider.gameObject.tag.Equals("Player"))
            {
                // �v���C���[�̑��x����~
                playerStop = true;
                // ���ʉ��ƃ��b�Z�[�W�𗬂�
                MessageManager.message_instance.MessageWindowActive(messages, names, images, ct: destroyCancellationToken).Forget();
                event4flag = true; //�t���O������
                toevent3.event3flag = true; //�@�G���o�Ă���悤�ɂ���B
            }
        }
    }
}

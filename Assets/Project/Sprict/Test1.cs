using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test1 : MonoBehaviour
{
    //�@���b�Z�[�W�E�B���h�E���\�����ꑱ���L�[�������Ă��������������Ȃ��s����������Ă���
    //�@�̓E�B���h�E���J���Ă�̂Ƀ��\�b�h�𓮂���������ăE�B���h�E�����遨���������u�ŌJ��Ԃ�����N�����B�����t�������悤
    //���̃X�N���v�g�͔p���B���̃X�N���v�g�Ɋ�������A���ڂ����Ԍo�߁H�ŃE�B���h�E����̂�
    //�Q��ڂ͈ꐶ������������肷��̂��Ӗ��킩��񁨂�����@�Ɠ�������
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<Sprite> image;
    public Canvas window;
    public Text target;
    public Text nameText;
    public static bool messageSwitch = false;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        isContacted = collider.gameObject.tag.Equals("Player");
    }

    // collider�����I�u�W�F�N�g�̗̈�O�ɂł��Ƃ�(���L�Ő���1)
    private void OnTriggerExit2D(Collider2D collider)
    {
        isContacted = !collider.gameObject.tag.Equals("Player");
    }
    private bool isContacted = false;
    //�����I�ȋ����̎���FixedUpdate�ł����I

    private void Update()//���̓`�F�b�N��Update�ɏ���
    {
        //���b�Z�[�W�E�B���h�E����Ƃ��͂��̃��\�b�h��
        if(isContacted && messageSwitch == false && Input.GetKeyDown(KeyCode.Return))
        {
            messageSwitch = true;
            Debug.Log(MessageManager.message_instance);
            Debug.Log(messages);
            Debug.Log(names);
            MessageManager.message_instance.MessageWindowActive(messages, names,image);
        }
    }
    /*private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player") &&Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log($"colloder: {collider.gameObject.name} ");
            /*�i�j�̒��Ɉ��������Ă�����Ǝ��s���̃��\�b�h���n�����ϐ��ŏ������s���Ă����B
            �������A�f�[�^��n�����͕ϐ������ł悢*/
           /* MessageManager.message_instance.MessageWindowActive(messages, names);
        }
       
    }*/
}

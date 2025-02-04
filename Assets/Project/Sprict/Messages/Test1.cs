using System.Collections;
using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

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
    [SerializeField]
    private List<Sprite> seiitirouImage;
    public Canvas window;
    public Text target;
    public Text nameText;
    public bool talked;
    private bool isContacted = false;
    private bool SeiContacted = false;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //�K�l�̎��ɕ\������郁�b�Z�[�W
        if(collider.gameObject.tag.Equals("Player"))
            isContacted = true;
        else if(collider.gameObject.tag.Equals("Seiitirou"))
            SeiContacted = true;
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
            isContacted = false;
        else if(collider.gameObject.tag.Equals("Seiitirou"))
            SeiContacted = false;
    }
    private void Update()//���̓`�F�b�N��Update�ɏ���
    {
        
        //���b�Z�[�W�E�B���h�E����Ƃ��͂��̃��\�b�h��
        if(isContacted && (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)))
        {
            Debug.Log("Yukito");
            MessageManager.message_instance.MessageWindowActive(GameManager.m_instance.GetMessages(name,"Interior"),GameManager.m_instance.GetSpeakerName(name, "Interior"), image, ct: destroyCancellationToken).Forget();
            if(!talked) talked = true;
        }
        else if(SeiContacted && seiitirouImage.Count > 0 && (Input.GetKeyDown("joystick button 0") || Input.GetKeyDown(KeyCode.Return)))
        {
            Debug.Log(seiitirouImage);
            MessageManager.message_instance.MessageWindowActive(GameManager.m_instance.GetMessages(name + "S","Interior"), GameManager.m_instance.GetSpeakerName(name + "S","Interior"), seiitirouImage, ct: destroyCancellationToken).Forget();
            if(!talked) talked = true;
        }
    }
}

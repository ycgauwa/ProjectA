using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * �t�B�[���h�I�u�W�F�N�g�̊�{����
 */
public class MessageCharactor : MonoBehaviour
{

    // Unity�̃C���X�y�N�^(UI��)�ŁA�O���ł������I�u�W�F�N�g���o�C���h����B
    // �i���� : �C���X�y�N�^��script��ǉ����āA�ݒ������ �Ő����j
    //�@����̃t���O�����������ԂŘb��������Ɖ�b���e���ς��B
    //�@�܂��C���f�b�N�X�������_���łƂ邱�Ƃŉ�b���e�ɕω�����������B
    
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private string charactername;
    [SerializeField]
    private Sprite images;
    public Canvas window;
    public Text target;
    public Image Chara;
    public Text charaname;
    private Sprite charaImage;
    public Character character;
    private IEnumerator coroutine;


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
        {
            PlayerManager.m_instance.m_speed = 0;
            coroutine = CreateCoroutine();
            StartCoroutine(coroutine);
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
        {
            
        }
    }
    public IEnumerator CreateCoroutine()
    {
        // window�N��
        window.gameObject.SetActive(true);

        // ���ۃ��\�b�h�Ăяo�� �ڍׂ͎q�N���X�Ŏ���
        yield return OnAction();

        // window�I��
        target.text = "";
        window.gameObject.SetActive(false);

        StopCoroutine(coroutine);
        coroutine = null;
        PlayerManager.m_instance.m_speed = 0.05f;

    }

    protected void showMessage(string message, string name, Sprite image)
    {
        target.text = message;
        charaname.text = name;
        Chara.sprite = image;
    }

    IEnumerator OnAction()
    {
        int i = 0;
        charactername = character.charaName;
        charaImage = character.characterImages[0];
        for(i = 0; i < messages.Count; ++i)
        {
            messages[i] = character.messageTexts[i];
            // 1�t���[���� ������ҋ@(���L����1)
            yield return null;

            // ��b��window��text�t�B�[���h�ɕ\��
            showMessage(messages[i], charactername, charaImage);

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));
        }
        yield break;

    }
}
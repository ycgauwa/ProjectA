using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SaveSlotsManager : MonoBehaviour
{
    public static SaveSlotsManager save_Instance;
    public Sprite[] SaveStageImages = new Sprite[6];
    public Button[] saveSlots = new Button[3];
    public Image[] saveImages = new Image[3];
    public Text[] playTimes = new Text[3];
    public Text[] gameModes = new Text[3];
    public Text[] characters = new Text[3];
    public Text[] Chapters = new Text[3];
    public SaveState saveState;


    void Awake()
    {
        if(save_Instance == null)
        {
            save_Instance = this;
            DontDestroyOnLoad(save_Instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        if(saveState == null)
        {
            saveState = new SaveState();
        }
        /*���̂Ƃ���̓X�N���v�^�u��OB�ōŏ��Ƀf�[�^�������Ɋi�[����B
         �i�[���ꂽ�f�[�^�̓V�[�����ς�邲�Ƃɂ����ɃX�N���v�^�u��OB�̃f�[�^��
        �i�[�B�V�[���̏��߂ɂ�������Q�Ƃ��Ƃ��Ċe�V�[����SaveCanvas��Int��String�ɏ󋵂��L�q

        �ŏ��̓^�C�g���B���̌�Q�[���V�[���B�܂��^�C�g������肽���B
        �f�[�^�̗���́A�ŏ��X�N���v�^�u��OB���炱���ɂ��Ă����Start�֐��Ŏ擾���Ă���B
        ���̂��ƂɃQ�[���V�[���Ɉړ�����Ƃ��́AUpdateSaveDate�֐����g���ăQ�[���V�[���������Ă���ϐ���
        ������Ɋi�[����B�i�������炢���Ȃ�^�C�g���ɖ߂��Ă���̕��@���ƃX�N���v�^�u��OB����l������Ă��ω����Ȃ�����OK�j
        Save����ƃX�N���v�^�u��OB��Json�t�@�C���̗����ɏ������܂��B
        �^�C�g���ɖ߂�������Json�t�@�C������Q�Ƃ��o�ăZ�[�u�̕\�L���㏑�������B
         */

        else return;
    }
}

using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class SoukobanManager : MonoBehaviour
{
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
    public static SoukobanManager s_instance;
    public GameObject positionJudge;
    public GameObject weightObject;
    public float positionxDifference;
    public float positionyDifference;
    private bool positionSwitch;
    private void Start()
    {
        s_instance = this;
        positionSwitch = false;
    }
    private void Update()
    {
        Vector3 pos = positionJudge.transform.position - weightObject.transform.position;
        positionxDifference = pos.x;
        positionyDifference = pos.y;

        if(positionxDifference < 0.3 && positionxDifference > -0.2 && positionyDifference < 0.2 && positionyDifference > -0.68&& positionSwitch == false)
        {
            MoveWeightEvent().Forget();
        }
    }
    private async UniTask MoveWeightEvent()
    {
        if(SecondHouseManager.secondHouse_instance.ajure.enemyEmerge)
        {
            positionSwitch = true;
            SecondHouseManager.secondHouse_instance.ajure.gameObject.transform.position = new Vector3(0,0,0);
            SecondHouseManager.secondHouse_instance.ajure.enemyEmerge = false;
            SoundManager.sound_Instance.StopBgm(SecondHouseManager.secondHouse_instance.fearMusic);
            SoundManager.sound_Instance.PlaySe(SecondHouseManager.secondHouse_instance.muvingAnimals);
            weightObject.gameObject.transform.DOLocalMove(new Vector3(132.4f, 97.9f, 0), 0.5f);
            Rigidbody2D weightRigid = weightObject.GetComponent<Rigidbody2D>();
            weightRigid.constraints = RigidbodyConstraints2D.FreezeAll;
            await MessageManager.message_instance.MessageWindowActive(messages, names, images, ct: destroyCancellationToken);
            SecondHouseManager.secondHouse_instance.endingCase9.gameObject.SetActive(false);
            SecondHouseManager.secondHouse_instance.notEnter9.gameObject.tag = "Minnka2-17";
        }
        else
        {
            return;
        }
        
    }
}

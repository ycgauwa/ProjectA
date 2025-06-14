using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using System.Threading;

public class EndingGalleryManager : MonoBehaviour
{
    public Image[] endingGallerys  = new Image[100];
    public Sprite[] endingSprites = new Sprite[100];
    public bool[] endingFlag = new bool[100];
    public Image[] endingGalleryPages = new Image[16];
    private int activePageNum = 0;
    public int getedEndTotalNumber;
    public Sprite testSprite;
    public EndingCase1 endingCase1;
    public Case1Object case1Object;
    public EndingCase2 endingCase2;
    public EndingCase3 endingCase3;
    public GameObject endingDetail;
    public Image endingDetailImage;
    public Image endedUIPanel;
    public Image noiseImage;
    public Text endingDetailText;
    public Text endCountNumber;
    public Text endCountNextNumber;
    public AudioClip ending5Bgm;
    public AudioClip freezeSound;
    public AudioClip blizzardSound;
    public AudioClip noiseSound;
    public AudioClip endingCountSound;
    public string explainText;
    public static EndingGalleryManager m_gallery;

    private void Awake()
    {
        if(m_gallery == null)
            m_gallery = this;
        else
        {
            Destroy(this);
        }
    }
    private void Start()
    {
        //ロードした時にギャラリー画面のUIを更新また、エンディング回収によってかわる要素をここで加えておく
        if(SaveSlotsManager.save_Instance.saveState.loadIndex > 0)
        {
            for(int i = 0 ; i <  endingFlag.Length; i++)
            {
                if(endingFlag[i] == true)
                {
                    m_gallery.endingGallerys[i].sprite = endingSprites[i];
                    LoadEndingChanges(i);
                }
            }
        }
    }
    public void EndingToDetail(int i)
    {
        //ボタンを押したときに解放してないエンドは開けないようにしたい。
        if(endingGallerys[i].sprite == testSprite) return;
        endingDetail.gameObject.SetActive(true);
        endingDetailImage.sprite = endingGallerys[i].sprite;
        if(i == 0) 
            explainText = "End1 人生最大の選択\n【取得条件】\n最初の選択で学校に行かず寝過ごす";
        else if(i == 1)
            explainText = "End2 逃亡を試みた天罰\n【取得条件】\n飛ばされた場所に玄関から外へ出ようとする";
        else if(i == 2)
            explainText = "End3 弄ばれた小動物\n【取得条件】\nクローゼットに隠れた後に酸欠�Vとなって死亡する";
        else if(i == 3)
            explainText = "End4 隅に追いやられた鼠\n【取得条件】\nクローゼットに隠れてから安全を確認せず外に出ようとする";
        else if(i == 4)
            explainText = "End5 呪われた生命の救済\n【取得条件】\n謎の男を見殺しにして自分のために友人の晴も生贄にする";
        else if(i == 5)
            explainText = "End6 血にまみれた晩餐会\n【取得条件】\n親切なぬいぐるみたちに作った料理を渡さない";
        else if(i == 6)
            explainText = "End7 捕らわれた獲物の慟哭\n【取得条件】\n異界の２軒目の民家にて玄関から出ようとする";//名前変更アリ
        else if(i == 7)
            explainText = "End8 凍てつく世界の中で\n【取得条件】\n生身の状態で玄関のドアを開ける。";
        else if(i == 8)
            explainText = "End9 亡霊と憎悪の目覚め\n【取得条件】\n征一郎が幸人を生贄にした後、晴を見殺しにする。";
        else if(i == 9)
            explainText = "End6 血にまみれた晩餐会\n【取得条件】\n親切なぬいぐるみたちに作った料理を渡さない";
        endingDetailText.text = explainText;
    }
    public  async UniTask GetedEndings(int getedEndingCount, CancellationToken ct = default)
    {
        SecondHouseManager.secondHouse_instance.light2D.intensity = 0f;
        endCountNumber.text = getedEndTotalNumber.ToString();
        getedEndTotalNumber++;
        endCountNextNumber.text = getedEndTotalNumber.ToString();
        await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: ct);
        //この関数を呼び出したら取得後の数字が変わる演出を出す
        //具体的にエンディングの取得個数が変わるのだがそれは小さい数字の透明度が半分を切った段階
        //で新しいほうの数字が徐々に表記される。で最初と最後でいじる変数はセーブとストップのスイッチ
        //あとは２つの文字の透明度を最後は0に変える。欲を言えば怖いSEも欲しい。
        GameManager.m_instance.notSaveSwitch = true;
        GameManager.m_instance.stopSwitch = true;
        endedUIPanel.gameObject.SetActive(true);
        noiseImage.gameObject.SetActive(true);
        SecondHouseManager.secondHouse_instance.light2D.intensity = 1f;
        SoundManager.sound_Instance.PlaySe(noiseSound);
        await UniTask.Delay(TimeSpan.FromSeconds(1f), cancellationToken: ct);
        SoundManager.sound_Instance.StopSe(noiseSound);
        SoundManager.sound_Instance.PlaySe(endingCountSound);
        noiseImage.gameObject.SetActive(false);
        Color color = endCountNumber.GetComponent<Text>().color;
        Color nextColor = endCountNextNumber.GetComponent<Text>().color;

        while (color.a > 0f)
        {
            color.a -= 0.01f;
            endCountNumber.color = color;
            await UniTask.Delay(1);
            if(color.a < 0.5f)
            {
                nextColor.a += 0.007f;
                endCountNextNumber.color = nextColor;
            }
        }
        while (nextColor.a < 1f)
        {
            nextColor.a += 0.007f;
            endCountNextNumber.color = nextColor;
            await UniTask.Delay(1);
        }
        noiseImage.gameObject.SetActive(true);
        SoundManager.sound_Instance.PlaySe(noiseSound);
        await UniTask.Delay(TimeSpan.FromSeconds(1f), cancellationToken: ct);
        SoundManager.sound_Instance.StopSe(noiseSound);

        color.a = 1f;
        nextColor.a = 0f;
        endCountNumber.color = color;
        endCountNextNumber.color = nextColor;
        noiseImage.gameObject.SetActive(false);

        GameManager.m_instance.notSaveSwitch = false;
        GameManager.m_instance.stopSwitch = false;
        endedUIPanel.gameObject.SetActive(false);
    }
    public void LoadEndingChanges(int endNumber)
    {
        switch(endNumber)
        {
            case 0:
                endingCase1.gameObject.transform.position = new Vector3(-35, 29, 0);
                endingCase1.answerNum = 3;
                Destroy(endingCase1.faliedSelect.gameObject);
                break;
            case 1:
                endingCase2.entrance.gameObject.SetActive(false);
                endingCase2.wall.gameObject.SetActive(true);
                endingCase2.gameObject.SetActive(false);
                break;
            case 2:
                endingCase3.gameObject.SetActive(false);
                break;
            case 3:
                endingCase3.gameObject.SetActive(false);
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
            case 8:
                break;
            case 9:
                break;
            case 10:
                break;
            case 11:
                break;
            case 12:
                break;
            case 13:
                break;
            case 14:
                break;
            case 15:
                break;
            case 16:
                break;
            case 17:
                break;
            case 18:
                break;
            case 19:
                break;                   
            case 20:
                break;
            default
                : break;
        }
    }
    public void CloseEndingDetail()
    {
        endingDetail.gameObject.SetActive(false);
        explainText = "";
    }
    public void NextGalleryPage()
    {
        if(endingGalleryPages[activePageNum].gameObject.activeSelf)
        {
            SoundManager.sound_Instance.PlaySe(GameManager.m_instance.decision);
            endingGalleryPages[activePageNum].gameObject.SetActive(false);
            endingGalleryPages[activePageNum + 1].gameObject.SetActive(true);
            activePageNum++;
        }
    }
    public void BackGalleryButton()
    {
        if(endingGalleryPages[activePageNum].gameObject.activeSelf)
        {
            SoundManager.sound_Instance.PlaySe(GameManager.m_instance.decision);
            endingGalleryPages[activePageNum].gameObject.SetActive(false);
            endingGalleryPages[activePageNum - 1].gameObject.SetActive(true);
            activePageNum--;
        }
    }
}

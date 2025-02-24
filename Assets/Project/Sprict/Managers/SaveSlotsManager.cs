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
        /*今のところはスクリプタブルOBで最初にデータをここに格納する。
         格納されたデータはシーンが変わるごとにここにスクリプタブルOBのデータを
        格納。シーンの初めにここから参照をとって各シーンのSaveCanvasのIntやStringに状況を記述

        最初はタイトル。その後ゲームシーン。またタイトルがやりたい。
        データの流れは、最初スクリプタブルOBからここにきてそれをStart関数で取得している。
        そのあとにゲームシーンに移動するときは、UpdateSaveDate関数を使ってゲームシーンが持っている変数を
        こちらに格納する。（ここからいきなりタイトルに戻っても上の方法だとスクリプタブルOBから値を取っても変化しないからOK）
        SaveするとスクリプタブルOBとJsonファイルの両方に書き込まれる。
        タイトルに戻った時にJsonファイルから参照を経てセーブの表記が上書きされる。
         */

        else return;
    }
}

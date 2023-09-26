using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceManager : MonoBehaviour
{
    public Canvas window;
    public Button yes;
    public Button no;
    //呼び出されたときにボタンを表示するメソッドを作る

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SelectChoice()
    {
        window.gameObject.SetActive(true);
        /*もし選択されて押されたら選択肢によってbool変数が変化する
        SetActive(false)になった時テキストメッセージが表示される→それはSelection.csを作って
        2つのマネージャーからメソッドを呼び込むと上の行のスクリプトで選択肢機能が実装される
        つまりテキストだけ表示されるオブジェクトはTest1で選択肢込みでテキストが表示されるようにする
        ボタンが表示される前にテキスト表示。最後がメッセージが出てきてエンター押したらメッセージログが
        消える代わりにボタンのUIキャンバスが出てきて押したボタンによってメッセージが変わる
        またボタンUIが消えたら出てくるテキストも変更されるためSetActive(Window)を消すことは最後までしない*/
        this.window.gameObject.SetActive(false);
    }
}

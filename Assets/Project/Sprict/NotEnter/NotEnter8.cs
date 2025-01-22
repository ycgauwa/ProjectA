using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class NotEnter8 : MonoBehaviour
{

    /*
    2軒目の一階の謎が解けるまで2階にはいけない仕組みを作る。
    ただいけないんじゃなくて行こうとしたときに何かペナルティもしくはダメージを食らう演出を見せて挙げたいのが本音
    鍵があくまではメッセージと演出を出して、鍵が空いてからはそのまま通れる感じにしておきたい
    演出を出したいなら、2階に行こうとしてから通れなくて、一回に行ったときに人形たちが現れるみたいな仕組みにしてもいい
    */

    public SecondHouseManager secondHouseManager;
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<Sprite> images;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Seiitirou"))
            gameObject.tag = "Minnka2-5";
        if (secondHouseManager.firstkey == false)
        {
            if (collider.gameObject.tag.Equals("Player"))
            {
                MessageManager.message_instance.MessageWindowActive(messages, names, images, ct: destroyCancellationToken).Forget();
            }
        }
        else if (secondHouseManager.firstkey == true)
        {
            if (collider.gameObject.tag.Equals("Player"))
            {
                gameObject.tag = "Minnka2-5";
            }
        }
    }
}

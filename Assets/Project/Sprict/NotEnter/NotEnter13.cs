using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class NotEnter13 : MonoBehaviour
{
    [SerializeField]
    private List<string> messages;
    [SerializeField]
    private List<string> names;
    [SerializeField]
    private List<Sprite> image;
    [SerializeField]
    private List<string> messages2;
    [SerializeField]
    private List<string> names2;
    [SerializeField]
    private List<Sprite> image2;
    /*
     * 話しかけるキャラクターによって結果が異なる。
     * ちなみにどっちも通れないが、条件を回収次第どちらのルートでも通れるようにする。
     * その条件はまだ未定のため今回のスクリプトは条件式で通れなくするだけ
     */
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag.Equals("Player"))
        {
            MessageManager.message_instance.MessageWindowActive(messages, names, image, ct: destroyCancellationToken).Forget();
        }
        else if(collider.gameObject.name == "Matiba Haru")
        {
            MessageManager.message_instance.MessageWindowActive(messages2, names2, image2, ct: destroyCancellationToken).Forget();
        }

    }
}

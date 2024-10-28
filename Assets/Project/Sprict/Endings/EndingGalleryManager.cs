using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EndingGalleryManager : MonoBehaviour
{
    public Image[] endingGallerys  = new Image[100];
    public EndingCase1 endingCase1;
    public Case1Object case1Object;
    public EndingCase2 endingCase2;
    public EndingCase3 endingCase3;
    public static EndingGalleryManager m_gallery;

    private void Start()
    {
        m_gallery = this;
    }
}

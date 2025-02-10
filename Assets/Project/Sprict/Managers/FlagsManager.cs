using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;
using UnityEngine.EventSystems;

public class FlagsManager : MonoBehaviour
{
    public static FlagsManager flag_Instance;

    public ToEvent1 toEvent1;
    public ToEvent2 toEvent2;
    public ToEvent3 toEvent3;
    public ToEvent4 toEvent4;
    public ToEvent5 toEvent5;
    public Vector3 playerPosition;
    public int chapterNum;
    public string characterName;
    public string playTime;
    public string gameMode;
    public Sprite playerImage;

    private void Start()
    {
        characterName = "çKêl";
        chapterNum = 0;
        flag_Instance = this;
    }
}

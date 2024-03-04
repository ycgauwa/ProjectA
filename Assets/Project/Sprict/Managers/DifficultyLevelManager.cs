using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyLevelManager : MonoBehaviour
{
    public Homing homing;
    public Canvas DifficultyCanvas;
    public bool ActiveCanvas = false;
    public enum DifficultyLevel
    {
        Easy,
        Normal,
        Hard,
        Extreme
    }
    public DifficultyLevel difficultyLevel;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void EasyButton()
    {
        difficultyLevel = DifficultyLevel.Easy;
        DifficultyCanvas.gameObject.SetActive(false);
        ActiveCanvas = true;
    }
    public void NormalButton()
    {
        difficultyLevel = DifficultyLevel.Normal;
        DifficultyCanvas.gameObject.SetActive(false);
        ActiveCanvas = true;
    }
    public void HardButton()
    {
        difficultyLevel = DifficultyLevel.Hard;
        DifficultyCanvas.gameObject.SetActive(false);
        ActiveCanvas = true;
    }
    public void ExtremeButton()
    {
        difficultyLevel = DifficultyLevel.Extreme;
        DifficultyCanvas.gameObject.SetActive(false);
        ActiveCanvas = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DifficultyLevelManager : MonoBehaviour
{
    public Homing homing;
    public Canvas DifficultyCanvas;
    public GameObject firstSelect;
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
        DifficultyCanvas.gameObject.SetActive(true);
    }
    public void EasyButton()
    {
        difficultyLevel = DifficultyLevel.Easy;
        DifficultyCanvas.gameObject.SetActive(false);
        EventSystem.current.SetSelectedGameObject(firstSelect);
        FlagsManager.flag_Instance.gameMode = "Easy";
        ActiveCanvas = true;
    }
    public void NormalButton()
    {
        difficultyLevel = DifficultyLevel.Normal;
        DifficultyCanvas.gameObject.SetActive(false);
        EventSystem.current.SetSelectedGameObject(firstSelect);
        FlagsManager.flag_Instance.gameMode = "Normal";
        ActiveCanvas = true;
    }
    public void HardButton()
    {
        difficultyLevel = DifficultyLevel.Hard;
        DifficultyCanvas.gameObject.SetActive(false);
        EventSystem.current.SetSelectedGameObject(firstSelect);
        FlagsManager.flag_Instance.gameMode = "Hard";
        ActiveCanvas = true;
    }
    public void ExtremeButton()
    {
        difficultyLevel = DifficultyLevel.Extreme;
        DifficultyCanvas.gameObject.SetActive(false);
        ActiveCanvas = true;
    }
}

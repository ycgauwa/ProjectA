using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DifficultyLevelManager : MonoBehaviour
{
    public Canvas DifficultyCanvas;
    public GameObject firstSelect;
    public bool ActiveCanvas = false;
    public int addEnemySpeed;
    public int hideDifficultyFactor;
    public enum DifficultyLevel
    {
        Easy,
        Normal,
        Hard,
        Extreme
    }
    public DifficultyLevel difficultyLevel;
    public void EasyButton()
    {
        difficultyLevel = DifficultyLevel.Easy;
        DifficultyCanvas.gameObject.SetActive(false);
        EventSystem.current.SetSelectedGameObject(firstSelect);
        SaveSlotsManager.save_Instance.saveState.gameModeString = "Easy";
        addEnemySpeed = 0;
        hideDifficultyFactor = 0;
        ActiveCanvas = true;
    }
    public void NormalButton()
    {
        difficultyLevel = DifficultyLevel.Normal;
        DifficultyCanvas.gameObject.SetActive(false);
        EventSystem.current.SetSelectedGameObject(firstSelect);
        SaveSlotsManager.save_Instance.saveState.gameModeString = "Normal";
        addEnemySpeed = 1;
        hideDifficultyFactor = 10;
        ActiveCanvas = true;
    }
    public void HardButton()
    {
        difficultyLevel = DifficultyLevel.Hard;
        DifficultyCanvas.gameObject.SetActive(false);
        EventSystem.current.SetSelectedGameObject(firstSelect);
        SaveSlotsManager.save_Instance.saveState.gameModeString = "Hard";
        addEnemySpeed = 3;
        hideDifficultyFactor = 20;
        ActiveCanvas = true;
    }
    public void ExtremeButton()
    {
        difficultyLevel = DifficultyLevel.Extreme;
        DifficultyCanvas.gameObject.SetActive(false);
        ActiveCanvas = true;
    }
}

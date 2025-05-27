using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdHouseManager : MonoBehaviour
{
    public static ThirdHouseManager thirdHouse_instance;
    public ThirdUnlockBasement thirdUnlockBasement;
    public GameObject futon;
    public GameObject[] thirdBloodObject;
    private void Awake()
    {
        if(thirdHouse_instance == null)
            thirdHouse_instance = this;
        else
        {
            Destroy(thirdHouse_instance);
        }
    }
}

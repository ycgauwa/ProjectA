using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventry : MonoBehaviour
{
    public static Inventry instance;
    public Canvas inventryCanvas;
    public InventryUI inventryUI;

    public List<Item> items = new List<Item>();

    private void Awake()
    {
        if(!inventryCanvas.gameObject.activeSelf)
            inventryCanvas.gameObject.SetActive(true);
        if (instance == null)
            instance = this;
        else Destroy(gameObject);
    }
    private void Start()
    {
        inventryUI = inventryCanvas.GetComponent<InventryUI>();
        if(SaveSlotsManager.save_Instance.saveState.loadIndex > 0)
        {
            foreach(Item items in ItemDateBase.itemDate_instance.items)
            {
                Debug.Log("InventryStart");
                if(items.checkPossession)
                    Add(items);
            }
        }
        inventryCanvas.gameObject.SetActive(false);
    }
    public void Add(Item item)
    {
        Debug.Log(item);
        item.checkPossession = true;
        item.geted = true;
        items.Add(item);
        inventryUI.UpdateUI();
    }
    public void Delete(Item item)
    {
        item.selectedItem = false;
        item.checkPossession = false;
        items.Remove(item);
        inventryUI.UpdateUI();
    }
}

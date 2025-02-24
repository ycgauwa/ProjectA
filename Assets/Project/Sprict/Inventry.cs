using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventry : MonoBehaviour
{
    public static Inventry instance;
    public InventryUI inventryUI;

    public List<Item> items = new List<Item>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(instance);
    }
    private void Start()
    {
        inventryUI = GetComponent<InventryUI>();
        if(SaveSlotsManager.save_Instance.saveState.loadIndex > 0)
        {
            foreach(Item items in ItemDateBase.itemDate_instance.items)
            {
                if(items.checkPossession)
                    Add(items);
            }
        }
    }
    public void Add(Item item)
    {
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

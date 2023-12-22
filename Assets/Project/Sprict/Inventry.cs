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
        {
            instance = this;
        }
    }
    private void Start()
    {
        inventryUI = GetComponent<InventryUI>();
        //inventryUI.UpdateUI();
    }
    public void Add(Item item)
    {
        items.Add(item);
        inventryUI.UpdateUI();
    }
    public void Delete(Item item)
    {
        item.selectedItem = false;
        items.Remove(item);
        inventryUI.UpdateUI();
    }
}

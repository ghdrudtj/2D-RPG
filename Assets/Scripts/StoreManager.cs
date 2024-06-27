using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    public InventoryItemData[] item;
    public GameObject Purchase_UI;
    public Image ItemImage;
    public Text ItemNameText;
    public Text ItemCoinText;
    public Text ItemExplainText;

    private Dictionary<string, InventoryItemData> itemDictionary;
    public void Start()
    {
        itemDictionary = new Dictionary<string, InventoryItemData>();
        foreach(InventoryItemData item in item)
        {
            itemDictionary[item.itemID] = item;
        }
    }
    public void SelectItem(string itemID)
    {
        if(itemDictionary.TryGetValue(itemID, out InventoryItemData selectedItem))
        {
            Purchase_UI.SetActive(true);
            ItemImage.sprite = selectedItem.itemlmage;
            ItemNameText.text = selectedItem.itemName;
            ItemCoinText.text = $"({selectedItem.itemPrice:N0} Point)";
            ItemExplainText.text = selectedItem.itemDescripion;
        }
        else
        {
            Debug.LogError("Item ID not found:" + itemID);
        }
    }
    void Update()
    {
        
    }
}

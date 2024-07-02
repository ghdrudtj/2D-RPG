using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackPackManager : MonoBehaviour
{
    public static BackPackManager Instance;
    public GameObject BackPack_UI;
    public Text CoinText;

    public Image[] ItemImage;
    private InventoryItemData[] InventoryItemDatas;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        InventoryItemDatas = new InventoryItemData[ItemImage.Length];
    }

    private void Update()
    {
        BackPackUIOn();
        CoinText.text = $"Coin: {GameManager.Instance.Coin:N0}";
    }
    private void BackPackUIOn()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            BackPack_UI.SetActive(!BackPack_UI.activeSelf);
        }
    }
    public bool AddItem(InventoryItemData item)
    {
        for (int i = 0;i < InventoryItemDatas.Length; i++)
        {
            if (ItemImage[i].sprite == null)
            {
                ItemImage[i].sprite = item.itemlmage;
                InventoryItemDatas[i] = item;
                return true;
            }
        }
        return false;
    }
}

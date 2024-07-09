using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BackPackManager : MonoBehaviour
{
    public static BackPackManager Instance;
    public GameObject BackPack_UI;
    public Text CoinText;

    public Image[] ItemImage;
    private InventoryItemData[] InventoryItemDatas;

    private int defitemUsingCount;
    private int speeditemUsingCount;
    private int poweritemUsingCount;
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
    public void ItemUse()
    {
        int sibingIndex = EventSystem.current.currentSelectedGameObject.transform.parent.GetSiblingIndex();
        InventoryItemData inventoryItem = InventoryItemDatas[sibingIndex];
        if (inventoryItem == null) return;

        if (inventoryItem.itemID == "HP")
        {
            GameManager.Instance.PlayerHP += 10f;
            GameManager.Instance.PlayerHP = Mathf.Min(GameManager.Instance.PlayerHP, 100f);
            PopupMsgManager.instance.ShowPopupMessage("체력이 10 회복되었습니다.");
        }
        else if (inventoryItem.itemID == "MP")
        {
            GameManager.Instance.PlayerMP += 10f;
            GameManager.Instance.PlayerMP = Mathf.Min(GameManager.Instance.PlayerMP, 100f);
            PopupMsgManager.instance.ShowPopupMessage("마나가 10 회복되었습니다.");
        }
        else if(inventoryItem.itemID == "HP_Power")
        {
            GameManager.Instance.PlayerHP += 100f;
            PopupMsgManager.instance.ShowPopupMessage("체력 전체가 회복되었습니다.");
        }
        else if(inventoryItem.itemID == "MP_Power")
        {
            GameManager.Instance.PlayerMP += 100f;
            PopupMsgManager.instance.ShowPopupMessage("마나 전체가 회복되었습니다.");
        }
        else if (inventoryItem.itemID == "Def")
        {
            StartCoroutine(Defitem());
        }
        else if(inventoryItem.itemID == "Speed")
        {
            StartCoroutine (Speeditem());
        }
        else if(inventoryItem.itemID == "Power")
        {
            StartCoroutine(Poweritem());
        }
        else
        {
            Debug.Log($"존재하지 않는 itemID[{inventoryItem.itemID}]");
            return;
        }
        InventoryItemDatas[sibingIndex] = null;
        EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite = null;


    }
    IEnumerator Defitem()
    {
        defitemUsingCount++;
        GameManager.Instance.PlayerDef *= 2;
        GameManager.Instance.character.GetComponent<SpriteRenderer>().color = Color.blue;
        Debug.Log("1. PlayerDef: = " + GameManager.Instance.PlayerDef);
        yield return new WaitForSeconds(10f);

        defitemUsingCount--;
        GameManager.Instance.PlayerDef /= 2;
        if (defitemUsingCount == 0)
            GameManager.Instance.character.GetComponent<SpriteRenderer>().color = Color.white;
        Debug.Log("2. PlayerDef : = " + GameManager.Instance.PlayerDef);
    }
    IEnumerator Speeditem()
    {
        speeditemUsingCount++;
        GameManager.Instance.character.Speed *= 2;
        GameManager.Instance.character.GetComponent<SpriteRenderer>().color = Color.red;
        Debug.Log("1. Speed: = " + GameManager.Instance.character.Speed);
        yield return new WaitForSeconds(10f);

        speeditemUsingCount--;
        GameManager.Instance.character.Speed /= 2;
        if (speeditemUsingCount == 0)
            GameManager.Instance.character.GetComponent<SpriteRenderer>().color = Color.white;
        Debug.Log("2. Speed : = " + GameManager.Instance.character.Speed);
    }
    IEnumerator Poweritem()
    {
        poweritemUsingCount++;
        GameManager.Instance.CharacterAttack.AttackDamage *= 2;
        GameManager.Instance.character.GetComponent<SpriteRenderer>().color = Color.green;
        Debug.Log("1. AttackDamage: = " + GameManager.Instance.CharacterAttack.AttackDamage);
        yield return new WaitForSeconds(10f);

        poweritemUsingCount--;
        GameManager.Instance.CharacterAttack.AttackDamage /= 2;
        if (poweritemUsingCount == 0)
            GameManager.Instance.character.GetComponent<SpriteRenderer>().color = Color.white;
        Debug.Log("2. AttackDamage : = " + GameManager.Instance.CharacterAttack.AttackDamage);
    }

}

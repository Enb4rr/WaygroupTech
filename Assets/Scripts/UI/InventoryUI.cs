using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance;

    [SerializeField] private List<TMP_Text> slotsTexts;
    [SerializeField] private List<Image> slotsImages;
    [SerializeField] private Sprite trashIcon;
    [SerializeField] private Sprite keyIcon;
    [SerializeField] private Sprite ebombIcon;
    [SerializeField] private Sprite defaultIcon;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateLabel(PickUpItem item, int ammount, int slotNumber)
    {
        slotsTexts[slotNumber].text = ammount.ToString();

        if(item.ItemType == PickUpItemType.Thrash) slotsImages[slotNumber].sprite = trashIcon;
        else if (item.ItemType == PickUpItemType.Key) slotsImages[slotNumber].sprite = keyIcon;
        else if (item.ItemType == PickUpItemType.EBomb) slotsImages[slotNumber ].sprite = ebombIcon;
    }

    public void RemoveLabel(int slotNumber)
    {
        slotsTexts[slotNumber].text = "0";
        slotsImages[slotNumber].sprite = defaultIcon;
    }
}

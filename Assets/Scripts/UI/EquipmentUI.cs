using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class EquipmentUI : MonoBehaviour
{
    public static EquipmentUI Instance;

    [SerializeField] private float fullFadeAmmount;
    [SerializeField] private float noFadeAmmount;
    [SerializeField] private float fadeDuration;

    [SerializeField] private TMP_Text equippedItemText;
    [SerializeField] private TMP_Text pickupText;
    [SerializeField] private Image pickupImage;
    [SerializeField] private List<TMP_Text> interactionsTexts;
    [SerializeField] private List<Image> inteactionImages;

    [SerializeField] private TMP_Text healthBar;

    private void Awake()
    {
        Instance = this;
    }

    public void HighlightPickUp(bool active)
    {
        if (active)
        {
            pickupText.DOFade(noFadeAmmount, fadeDuration);
            pickupImage.DOFade(noFadeAmmount, fadeDuration);
        }
        else
        {
            pickupText.DOFade(fullFadeAmmount, fadeDuration);
            pickupImage.DOFade(fullFadeAmmount, fadeDuration);
        }
    }

    public void EquipWeapon(string weaponName)
    {
        equippedItemText.text = weaponName;
        foreach (var item in interactionsTexts)
        {
            item.DOFade(noFadeAmmount, fadeDuration);
        }
        foreach (var item in inteactionImages)
        {
            item.DOFade(noFadeAmmount, fadeDuration);
        }
    }

    public void UnnequipWeapon()
    {
        equippedItemText.text = string.Empty;
        foreach (var item in interactionsTexts)
        {
            item.DOFade(fullFadeAmmount, fadeDuration);
        }
        foreach (var item in inteactionImages)
        {
            item.DOFade(fullFadeAmmount, fadeDuration);
        }
    }

    public void UpdateHealthBar(string health)
    {
        healthBar.text = health;
    }
}

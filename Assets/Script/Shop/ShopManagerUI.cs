using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System;
using NaughtyAttributes;
using UnityEngine.UI;


public class ShopManagerUI : MonoBehaviour
{
    [Header("Player Stats Elements")]
    [SerializeField] private RectTransform playerStatsPanel;
    [SerializeField] private RectTransform playerStatsClosePanel;
    private Vector2 playerStatsPanelOpenPosition;
    private Vector2 playerStatsPanelClosedPosition;
        [Header("Inventory Elements")]
    [SerializeField] private RectTransform InventoryPanel;
    [SerializeField] private RectTransform InventoryCloseButton;
    private Vector2 InventoryPanelOpenPosition;
    private Vector2 InventoryPanelClosedPosition;

    [Header("Item Info")]
    [SerializeField] private RectTransform itemInfoSlidePanel;
    [SerializeField] private RectTransform itemInfoCloseButton;
    [SerializeField] private Vector2 itemInfoOpenPosition;
    [SerializeField] private Vector2 itemInfoClosedPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    IEnumerator  Start()
    {
        yield return null;
        ConfigurePlayerStatsPanel();
        ConfigureInventoryPanel();
        configureItemInfoPanel();
    }

    private void configureItemInfoPanel()
    {
       float height = Screen.height /(3 * itemInfoSlidePanel.localScale.x);
       itemInfoSlidePanel.offsetMax = itemInfoSlidePanel.offsetMax.With(y: height);
       itemInfoOpenPosition = itemInfoSlidePanel.anchoredPosition;
       itemInfoClosedPosition = itemInfoOpenPosition + Vector2.down * height;

       itemInfoSlidePanel.anchoredPosition = itemInfoClosedPosition;

       itemInfoSlidePanel.gameObject.SetActive(false);
      
    }

    private void ConfigureInventoryPanel()
    {
        float width = InventoryPanel.rect.width;
        InventoryPanel.offsetMin = InventoryPanel.offsetMin.With(x: -width);
        InventoryPanelOpenPosition = InventoryPanel.anchoredPosition;
        InventoryPanelClosedPosition = InventoryPanelOpenPosition - Vector2.left * width;
        InventoryPanel.anchoredPosition = InventoryPanelClosedPosition;
        HideInventory(false);
    }

    private void ConfigurePlayerStatsPanel()
    {
        float width = playerStatsPanel.rect.width;
        playerStatsPanel.offsetMax = playerStatsPanel.offsetMax.With(x: width);
        playerStatsPanelOpenPosition = playerStatsPanel.anchoredPosition;
        playerStatsPanelClosedPosition = playerStatsPanelOpenPosition + Vector2.left * width;
        playerStatsPanel.anchoredPosition = playerStatsPanelClosedPosition;
        HidePlayerStats();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [NaughtyAttributes.Button("Show Player Stats")]
    public void ShowPlayerStats()
    {
        playerStatsPanel.gameObject.SetActive(true);
        playerStatsClosePanel.gameObject.SetActive(true);
        playerStatsClosePanel.GetComponent<Image>().raycastTarget = true;
        LeanTween.cancel(playerStatsPanel);
        LeanTween.move(playerStatsPanel, playerStatsPanelOpenPosition, 0.5f).setEaseInOutCubic();
        LeanTween.cancel(playerStatsClosePanel);
        LeanTween.alpha(playerStatsClosePanel, 0.8f, 0.5f).setRecursive(false);
    }

    [NaughtyAttributes.Button("Hide Player Stats")]
    public void HidePlayerStats()

    {
            // playerStatsPanel.gameObject.SetActive(false);
        //playerStatsClosePanel.gameObject.SetActive(false);
        playerStatsClosePanel.GetComponent<Image>().raycastTarget = false;
        LeanTween.cancel(playerStatsPanel);
        LeanTween.move(playerStatsPanel, playerStatsPanelClosedPosition, 0.5f).setEaseInOutCubic().setOnComplete(() => playerStatsPanel.gameObject.SetActive(false));
        LeanTween.cancel(playerStatsClosePanel);
        LeanTween.alpha(playerStatsClosePanel, 0f, 0.5f).setRecursive(false).setOnComplete(() => playerStatsClosePanel.gameObject.SetActive(false));
   
    }
    public void ShowInventory()
    {
        InventoryPanel.gameObject.SetActive(true);
        InventoryCloseButton.gameObject.SetActive(true);
        InventoryCloseButton.GetComponent<Image>().raycastTarget = true;
        LeanTween.cancel(InventoryPanel);
        LeanTween.move(InventoryPanel, InventoryPanelOpenPosition, 0.5f).setEaseInOutCubic();
        LeanTween.cancel(InventoryCloseButton);
        LeanTween.alpha(InventoryCloseButton, 0.8f, 0.5f).setRecursive(false);
    }
    public void HideInventory(bool hideItemInfo = true)
    {
        InventoryCloseButton.GetComponent<Image>().raycastTarget = false;
        LeanTween.cancel(InventoryPanel);
        LeanTween.move(InventoryPanel, InventoryPanelClosedPosition, 0.5f).setEaseInOutCubic().setOnComplete(() => InventoryPanel.gameObject.SetActive(false));
        LeanTween.cancel(InventoryCloseButton);
        LeanTween.alpha(InventoryCloseButton, 0f, 0.5f).setRecursive(false).setOnComplete(() => InventoryCloseButton.gameObject.SetActive(false));

        if (hideItemInfo)
        {
            HideItemInfo();
        }
    }
    [NaughtyAttributes.Button("Show Item Info")]
    public void ShowItemInfo()
    {
        itemInfoSlidePanel.gameObject.SetActive(true);
      itemInfoSlidePanel.LeanCancel();
      itemInfoSlidePanel.LeanMove((Vector3)itemInfoOpenPosition,.3f).setEaseInOutCubic();
    }
    [NaughtyAttributes.Button("Hide Item Info")]
    public void HideItemInfo()
    {
        itemInfoSlidePanel.LeanCancel();
        itemInfoSlidePanel.LeanMove((Vector3)itemInfoClosedPosition,.3f).setEase(LeanTweenType.easeInCubic).setOnComplete(() => itemInfoSlidePanel.gameObject.SetActive(false));
    }
}

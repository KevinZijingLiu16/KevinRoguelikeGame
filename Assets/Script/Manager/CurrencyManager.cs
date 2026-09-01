using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager instance;
    [field: SerializeField] public int Currrency { get; private set; }

    public static Action onUpdated;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {       
         if (instance == null)
        {
            instance = this;
           // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        UpdateText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [NaughtyAttributes.Button("Add 500 Currency")]
    private void Add500Currency()
    {
        AddCurrency(500);
    }

    public void AddCurrency(int amount)
    {
        Currrency += amount;
        UpdateText();
        onUpdated?.Invoke();
    }

   public void UpdateText()
    {
       CurrencyText[] currencyTexts = FindObjectsByType<CurrencyText>();
        foreach(CurrencyText currencyText in currencyTexts)
        {
            currencyText.UpdateText(Currrency.ToString());
        }
    }

    public bool HasEnoughCurrency(int amount)
    {
        return Currrency >= amount;
    }

    public void UseCurrency(int amount)
    {
        AddCurrency(-amount);
    }
}

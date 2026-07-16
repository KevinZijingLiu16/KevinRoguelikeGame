using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager instance;
    [field: SerializeField] public int Currrency { get; private set; }
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

    public void AddCurrency(int amount)
    {
        Currrency += amount;
        UpdateText();
    }

    private void UpdateText()
    {
       CurrencyText[] currencyTexts = FindObjectsByType<CurrencyText>();
        foreach(CurrencyText currencyText in currencyTexts)
        {
            currencyText.UpdateText(Currrency.ToString());
        }
    }
}

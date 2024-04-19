using Assets.SimpleLocalization.Scripts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Stats : MonoBehaviour
{
    public int UIMoneyCount;
    public PlayerStats playerStats;
    public TextMeshProUGUI TEXT_Money_Count;

    public static UI_Stats Instance;

    [SerializeField] private Animator CompteurAnimator;

    private Color moneyCountColor;
    float colorTimer = 0.2f;
    [SerializeField] private TextMeshProUGUI DollarText;
    [SerializeField] private Image DollarImage;


    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void Start()
    {
        playerStats = PlayerStats.Instance;
        UIMoneyCount = 0;
        UpdateStats();

        moneyCountColor = TEXT_Money_Count.color;
        Debug.Log(moneyCountColor);

    }

    private void Update()
    {
        if(colorTimer > 0)
        {
            colorTimer -= Time.deltaTime;
        }
        else
        {
            float addColorValue = 0.025f;
            moneyCountColor = new Color(moneyCountColor.r + addColorValue, moneyCountColor.g + addColorValue, moneyCountColor.b + addColorValue);
            UpdateColors();
        }
    }

    public void UpdateStats()
    {
        UIMoneyCount = playerStats.moneyCount;
        TEXT_Money_Count.text = $"{UIMoneyCount}";
        DoColorChange();
        CompteurAnimator.SetTrigger("AddMoney");
    }

    public void DoColorChange()
    {
        moneyCountColor = new Color(0.23f, 0.88f, 0.25f);
        UpdateColors();
        Debug.Log(moneyCountColor);
        colorTimer = 0.2f;
    }

    void UpdateColors()
    {
        TEXT_Money_Count.color = moneyCountColor;
        DollarText.color = moneyCountColor;
        DollarImage.color = moneyCountColor;
    }

}

using UnityEngine;
using TMPro;


public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;
    [SerializeField] private int coins;
    public int Coins
    {
        get{return coins;}
        set{coins = value;}
    }

    [SerializeField] private TextMeshProUGUI coinTxt;
    public TextMeshProUGUI CoinTxt
    {
        get{return coinTxt;}
    }

    void Awake()
    {
        instance = this;
        UpdateCoins(0);
    }

    public void UpdateCoins(int changeAmount)
    {
        Coins += changeAmount;

        CoinTxt.text = Coins.ToString();
    }
}

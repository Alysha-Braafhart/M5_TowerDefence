using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TowerUpgradeUI : MonoBehaviour
{
    [SerializeField] private Tower tower;
    public Tower Tower
    {
        get{return tower;}
        set{tower = value;}
    }

     [SerializeField] private Button upgradeButton;
     [SerializeField] private TextMeshProUGUI priceTxt;
     public TextMeshProUGUI PriceTxt
    {
        get{return priceTxt;}
        set{priceTxt = value;}
    }

     private bool justOpened = true;
     
    

   private void Awake()
    {
        upgradeButton.onClick.AddListener(TryUpgrade);
    }

    private void TryUpgrade()
    {
        if(CoinManager.instance.Coins < tower.UpgradeStages[tower.UpgradeStage].Price) return;

        tower.Upgrade();

        Destroy(gameObject);
    }

    void Update()
    {
        if (justOpened)
        {
            justOpened = false;
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if(UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
                return;

            Destroy(gameObject);
        }

        if(tower.UpgradeStage >= tower.UpgradeStages.Length)
        {
            Destroy(gameObject);
        }
    }
}

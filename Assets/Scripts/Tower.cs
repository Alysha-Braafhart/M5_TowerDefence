using UnityEngine;

[System.Serializable]
public class TowerUpgradeStage
{
   [SerializeField] private float range;
    public float Range 
    {
        get{return range;}
    }


    [SerializeField] private float fireRate;
    public float FireRate
    {
        get{return fireRate;}
    }
    [SerializeField] private Sprite sprite;
    public Sprite Sprite
    {
        get{return sprite;}
    }
    [SerializeField] private int price;
    public int Price
    {
        get{return price;}
    }
}

public class Tower : MonoBehaviour
{
    [SerializeField] private float range = 3f;
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;

    [SerializeField] private TowerUpgradeStage [] upgradeStages;

    public TowerUpgradeStage [] UpgradeStages
    {
        get{return upgradeStages;}
        set{upgradeStages = value;}
    }
    private int upgradeStage = 0;
    public int UpgradeStage
    {
        get{return upgradeStage;}
        set{upgradeStage = value;}
    }
    private SpriteRenderer sr;
    [SerializeField] private GameObject TowerUpgradeUIPrefab;
    private GameObject currentUI;

    [SerializeField] private GameObject cloudPS;

    [SerializeField] private int towerPrice = 1;
    public int TowerPrice
    {
        get{return towerPrice;}
        set{towerPrice = value;}
    }

    private float fireCooldown = 0f;

    [SerializeField] private AudioClip upgradeSFX;

    private void Start()
    {
        
    }

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        GameObject go = Instantiate(cloudPS, transform.position, Quaternion.identity);
        go.GetComponent<ParticleSystem>().Play();

        Debug.Log(go.transform.position);



    }

    void Update()
    {
        fireCooldown -= Time.deltaTime;

        Enemy target = FindBestTarget();

        if (target != null && fireCooldown <= 0f)
        {
            Shoot(target);
            fireCooldown = 1f / fireRate;
        }
    }

    Enemy FindBestTarget()
    {
        Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();
        
        Enemy best = null;
        float bestProgress = -1f;

        foreach(Enemy e in enemies)
        {
            float dist = Vector2.Distance(transform.position, e.transform.position);

            if(dist <= range)
            {
                if(e.CurrentWayPoint > bestProgress)
                {
                    bestProgress = e.CurrentWayPoint;
                    best = e;
                }
            }
        }

        return best;
    }

    void Shoot(Enemy target)
    {
        GameObject p = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Projectile pr = p.GetComponent<Projectile>();
        pr.Target = target.transform;
    }

    public void Upgrade()
    {
        TowerUpgradeStage currentUpgradeStage = upgradeStages[upgradeStage];

        range = currentUpgradeStage.Range;
        fireRate = currentUpgradeStage.FireRate;
        sr.sprite = currentUpgradeStage.Sprite;
        CoinManager.instance.UpdateCoins(-currentUpgradeStage.Price);
        upgradeStage += 1;
        Instantiate(cloudPS, transform.position, Quaternion.identity);
        AudioManager.Instance.PlaySFX(upgradeSFX);
    }

    private void OnMouseDown()
    {
        if(currentUI == null)
        {
            currentUI = Instantiate(TowerUpgradeUIPrefab, FindObjectOfType<Canvas>().transform);
        }

        TowerUpgradeUI currentUpgradeUI = currentUI.GetComponent<TowerUpgradeUI>();
        currentUpgradeUI.Tower = this;

        currentUI.transform.position = Input.mousePosition + new Vector3(50, -50);

        if(upgradeStage >= upgradeStages.Length) return;
        currentUpgradeUI.PriceTxt.text = upgradeStages[upgradeStage].Price.ToString();
    }
}

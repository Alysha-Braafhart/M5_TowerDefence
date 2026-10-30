using UnityEngine;

public class Enemy : MonoBehaviour
{
   [SerializeField] private float speed = 2f;
    [SerializeField] private float health = 1f;
    public float Health
    {
        get{return health;}
        set{health = value;}
    }

    [SerializeField] private int damage = -1;
    
    [SerializeField] private Transform[] waypoints;
    public Transform [] Waypoints
    {
        get{return waypoints;}
        set{waypoints = value;}
    }

   [SerializeField] private int coinEarning = 5;
    public int CoinEarning
    {
        get{return coinEarning;}
    }

    private int currentWayPoint = 0;
    public WaveManager WaveManager { get; set; }
    public int CurrentWayPoint
    {
        get{return currentWayPoint;}

    }
    void Update()
    {
        if (waypoints == null || waypoints.Length == 0) return;

        Transform target = waypoints[currentWayPoint];
        Vector3 dir = (target.position - transform.position).normalized;

        transform.position += dir * speed * Time.deltaTime;

        if(Vector3.Distance(transform.position, target.position) < 0.05f)
        {
            currentWayPoint++;

            Tower e = target.GetComponent<Tower>();

            if(currentWayPoint >= waypoints.Length)
            {
                HealthManager.instance.UpdateHealth(damage);
                Destroy(gameObject);
            }
        }
    }

    void OnDestroy()
    {
        if(WaveManager != null)
        {
            WaveManager.EnemyDestroyed();
        }
    }
}

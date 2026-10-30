using Unity.Mathematics;
using UnityEngine;

public class Projectile : MonoBehaviour
{
   [SerializeField] private float speed = 8f;
    private Transform target;
    public Transform Target
    {
        get{return target;}
        set{target = value;}
    }
    [SerializeField] private GameObject easyEnemies;
    [SerializeField] private GameObject hardEnemies;
    [SerializeField] private GameObject  harderEnemies;
    [SerializeField] private GameObject hitPS;

    [SerializeField] private AudioClip hitSFX;

    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;

        }

        Vector3 dir =(target.position - transform.position).normalized;
        transform.position += dir * speed * Time.deltaTime;

        float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);

        if(Vector2.Distance(transform.position, target.position) < 0.15f)
        {
            Enemy e = target.GetComponent<Enemy>();
            e.Health -= 1;
            if(e.Health <= 0)
            {CoinManager.instance.UpdateCoins(e.CoinEarning);
                Destroy(target.gameObject);
                
            }

            Instantiate(hitPS, transform.position, Quaternion.identity);
            AudioManager.Instance.PlaySFX(hitSFX);
            Destroy(gameObject);
        }
;    }
}

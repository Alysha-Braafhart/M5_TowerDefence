using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    private float time;
    void Start()
    {
        Destroy(gameObject, time);
    }
}

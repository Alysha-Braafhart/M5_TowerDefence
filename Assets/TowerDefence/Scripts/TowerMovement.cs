using UnityEngine;

public class TowerMovement : MonoBehaviour
{

    private Vector3 movement = new Vector3(-1, 0, 0);

    [SerializeField] private float speed = 2.5f;
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += movement * speed * Time.deltaTime;
        
    }
}

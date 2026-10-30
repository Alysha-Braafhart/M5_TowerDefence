using UnityEngine;
using UnityEngine.UI;

public class Wall : MonoBehaviour
{
    [SerializeField] private Button startButton;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startButton.onClick.AddListener(DestroyWall);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DestroyWall()
    {
        Destroy(gameObject);
    }
}

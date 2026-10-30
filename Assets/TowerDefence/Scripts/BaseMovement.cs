using UnityEngine;
using UnityEngine.UI;

public class BaseMovement : MonoBehaviour
{
    [SerializeField] private Button startButton;
    private readonly Vector3 movement = new Vector3(-1, 0, 0);

    [SerializeField] private float speed = 2.5f;
    private bool isMoving;
    public bool IsMoving
    {
        get{return isMoving;}
    }

    void Start()
    {
        startButton.onClick.AddListener(StartMovement);
    }

    public void StartMovement()
    {
        isMoving = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving) return;

        transform.position += movement * speed * Time.deltaTime;
    }

}

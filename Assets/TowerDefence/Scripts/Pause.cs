using UnityEngine;

public class Pause : MonoBehaviour
{
    private bool paused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            paused = !paused;
            Time.timeScale = paused ? 0f : 1f;
        }
    }
}

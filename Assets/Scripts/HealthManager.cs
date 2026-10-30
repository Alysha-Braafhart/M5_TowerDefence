using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;

    private int health = 100;
    [SerializeField] private TextMeshProUGUI healthTxt;

    [SerializeField] private GameObject loseUI;

    void Awake()
    {
        instance = this;
    }

    public void UpdateHealth(int changeAmount)
    {
        health += changeAmount;

        healthTxt.text = health.ToString();

        if(health <= 0)
        {
            Time.timeScale = 0;
            loseUI.SetActive(true);
        }
    }
}

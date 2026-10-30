using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Assemblies;
using Unity.Mathematics;
using TMPro;

[System.Serializable]
public class WaveData
{
    [SerializeField] private float duration = 10f;
    public float Duration
    {
        get{return duration;}
    }
    [SerializeField] private int easyEnemies = 5;
    public int EasyEnemies
    {
        get{return easyEnemies;}
    }
    [SerializeField] private int hardEnemies = 2;
    public int HardEnemies
    {
        get{return hardEnemies;}
    }

    [SerializeField] private int harderEnemies = 1;
    public int HarderEnemies
    {
        get{return harderEnemies;}
    }
}

public class WaveManager : MonoBehaviour
{
    [SerializeField] private WaveData[] waves;
    public WaveData[] Waves
    {
        get{return waves;}
        set{waves = value;}
    }
    [SerializeField] private Button startWavebutton;

    [SerializeField] private GameObject easyEnemyPrefab;
    [SerializeField] private GameObject hardEnemyPrefab;
    [SerializeField] private GameObject harderEnemyPrefab;

    [SerializeField] private Transform[] wayPoints;

    [SerializeField] private TextMeshProUGUI waveTxt;

    private int currentWaveIndex = 0;
    private bool waveRunning = false;
    private int activeEnemyCount = 0;

    [SerializeField] private GameObject winUI;
    void Start()
    {
        startWavebutton.onClick.AddListener(StartWave);
    }

    public void StartWave()
    {
        if(waveRunning || currentWaveIndex >= Waves.Length) return;

        StartCoroutine(RunWave());
    }

    IEnumerator RunWave()
    {
        waveRunning = true;
        startWavebutton.interactable = false;

        WaveData wave = Waves [currentWaveIndex];

        for(int i = 0; i < wave.EasyEnemies; i++)
        {
            SpawnEnemy(easyEnemyPrefab);
            yield return new WaitForSeconds((wave.Duration / 3) / wave.EasyEnemies);
        }

        for(int i = 0; i < wave.HardEnemies; i++)
        {
            SpawnEnemy(hardEnemyPrefab);
            yield return new WaitForSeconds((wave.Duration / 3) / wave.HardEnemies);
        }

        for(int i = 0; i < wave.HarderEnemies; i++)
        {
            SpawnEnemy(harderEnemyPrefab);
            yield return new WaitForSeconds((wave.Duration / 3) / wave.HardEnemies);
        }


        while(activeEnemyCount > 0)
        {
            yield return null;
        }

        waveRunning = false;
        currentWaveIndex++;
        Won();

        if(currentWaveIndex < Waves.Length)
        {
            waveTxt.text = (currentWaveIndex + 1).ToString();
            StartWave();
        }
    }

    void Won()
    {
        if(currentWaveIndex >= Waves.Length)
        {
            Time.timeScale = 0;
            winUI.SetActive(true);
            return;
        }
    }

    void SpawnEnemy(GameObject prefab)
    {
        GameObject e = Instantiate(prefab, wayPoints[0].position, quaternion.identity);
        Enemy enemy = e.GetComponent<Enemy>();
        enemy.Waypoints = wayPoints;
        enemy.WaveManager = this;
        activeEnemyCount++;
    }

    public void EnemyDestroyed()
    {
        activeEnemyCount = Mathf.Max(0, activeEnemyCount - 1);
    }

}

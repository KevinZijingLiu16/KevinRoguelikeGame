using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
[RequireComponent(typeof(WaveManagerUI))]
public class WaveManager : MonoBehaviour, IGameStateListener
{
    [Header(" Settings ")]
    [SerializeField] private float waveDuration;
    private float timer;
    [SerializeField] private Player player;
    private WaveManagerUI waveManagerUI;
    private bool isTimerOn;
    private int currentWaveIndex;

    [Header("Wave")]
    [SerializeField] private Wave[] waves;
    private List<float> localCounters = new List<float>();

    void Awake()
    {
        waveManagerUI = GetComponent<WaveManagerUI>();
       
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       

        //StartWave(currentWaveIndex);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTimerOn)
            return;

        if (timer < waveDuration)
        { 
        
            ManageCurrentWave();
            string timerString = ((int)(waveDuration - timer)).ToString() + "s";
            waveManagerUI.UpdateTimerText(timerString);
        }
        else
            StartWaveTransition();

       // Debug.Log("Timer: " + timer);
    }
    private void StartWave(int waveIndex)
    {
        Debug.Log("Starting wave: " + waveIndex);
        waveManagerUI.UpdateWaveText("Wave " + (currentWaveIndex + 1) + " / "+ waves.Length);
        if (waveIndex >= waves.Length)
        {
            Debug.Log("All waves completed!");
            return;
        }
        localCounters.Clear();
        foreach(WaveSegment segment in waves[waveIndex].segments)
        {
            localCounters.Add(1);
        }
        timer = 0f;
        isTimerOn = true;
       
    }

    private void ManageCurrentWave()
    { 
        
        Wave currentWave = waves[currentWaveIndex]; 
        for (int i = 0; i < currentWave.segments.Count; i++)
        {
            WaveSegment segment = currentWave.segments[i];
           
            float tStart = segment.tStartEnd.x / 100 * waveDuration;
            float tEnd = segment.tStartEnd.y / 100 * waveDuration;

            if(timer < tStart || timer > tEnd)
                continue;

            float timeSinceSegmentStart = timer - tStart;

            float spawnDelay = 1f / segment.spawnFrequency;

        
            if (timeSinceSegmentStart / spawnDelay > localCounters[i])
            { 
                Instantiate(segment.enemyPrefab, GetSpawnPosition(), Quaternion.identity,transform);

                localCounters[i]++;
            }
        }
        timer += Time.deltaTime;
    }

    private void StartWaveTransition()
    {
        isTimerOn = false;
        DefeatAllEnemies();
        currentWaveIndex++;

        if (currentWaveIndex >= waves.Length)
        {
            Debug.Log("All waves completed!");
            waveManagerUI.UpdateTimerText("");
            waveManagerUI.UpdateWaveText("Stage Completed!");

            GameManager.instance.SetGameState(GameState.STAGECOMPLETE);
        }
        else
            GameManager.instance.WaveCompletedCallback();

    }

    private Vector2 GetSpawnPosition()
    {
        Vector2 direction = Random.onUnitSphere;
        Vector2 offset = direction.normalized * Random.Range(6f, 10f); // Random offset from the player
        Vector2 targetPosition = (Vector2)player.transform.position + offset;

        targetPosition.x = Mathf.Clamp(targetPosition.x, -18f, 18f); // Clamp X position
        targetPosition.y = Mathf.Clamp(targetPosition.y, -8f, 8f); // Clamp Y position

        return targetPosition;
    }

    [Button("ClearAllEnemies")]
    private void DefeatAllEnemies()
    { 
        transform.Clear();
    }

    private void StartNextWave()
    {
     
        StartWave(currentWaveIndex);
    }

    public void GameStateChangedCallback(GameState gameState)
    {
        Debug.Log($"WaveManger knows that the new game state is {gameState}");

        switch(gameState)
            {
            case GameState.GAME:
                StartNextWave();
                break;

                case GameState.GAMEOVER:
                isTimerOn = false;
                DefeatAllEnemies();
                break;
        }
    }
}
[System.Serializable]
public struct Wave
{
    public string name;
    public List<WaveSegment> segments;
}
[System.Serializable]
public struct WaveSegment
{
    [MinMaxSlider(0, 100)] public Vector2 tStartEnd; 
    public float spawnFrequency;
    public GameObject enemyPrefab;
}

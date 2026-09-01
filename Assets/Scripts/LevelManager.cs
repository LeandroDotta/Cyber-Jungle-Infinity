using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private EnemyWave[] waves;
    private int currentWave = -1;

    private void Start()
    {
        waves = transform.GetComponentsInChildren<EnemyWave>(true);

        StartNextWave();
    }

    public void StartNextWave()
    {
        int nextWave = currentWave + 1;

        if (nextWave >= waves.Length)
        {
            SendMessage("OnLevelEnd", SendMessageOptions.RequireReceiver);
            currentWave = -1;
            return;
        }
        
        StartWave(nextWave);
    }

    public void StartWave(int waveIndex)
    {
        CancelInvoke();

        if (waveIndex < 0 || waveIndex >= waves.Length)
        {
            Debug.LogError($"[LevelManager] Invalid Wave Index ({waveIndex})");
            return;
        }

        EnemyWave wave = waves[waveIndex];
        wave.gameObject.SetActive(true);
        wave.StartWave();

        currentWave = waveIndex;

        Invoke(nameof(StartNextWave), wave.Duration);
    }
}

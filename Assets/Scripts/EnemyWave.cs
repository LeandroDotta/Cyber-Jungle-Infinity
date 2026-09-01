using UnityEditor;
using UnityEngine;

public class EnemyWave : MonoBehaviour
{
    [field: SerializeField] public float Duration { get; set; } = 2f;
    public int EnemyCount => placeholders.Length;

    private EnemyPlaceholder[] placeholders;

    private void Awake()
    {
        InitializePlaceholders();
    }

    private void InitializePlaceholders()
    {
        if (placeholders != null && placeholders.Length > 0)
            return;

        placeholders = GetComponentsInChildren<EnemyPlaceholder>(true);
    }
    
    [ContextMenu("Start Wave")]
    public void StartWave()
    {
        if (!Application.isPlaying)
        {
            Debug.LogWarning("Starting an enemy wave is only available when the game is running!");
            return;
        }

        foreach (EnemyPlaceholder placeholder in placeholders)
        {
            Enemy enemy = Instantiate(
                placeholder.Data.Prefab,
                placeholder.transform.position,
                Quaternion.identity,
                transform
            );

            enemy.data = placeholder.Data;
        }
    }

    [ContextMenu("Clear Wave")]
    public void ClearWave()
    {
        if (!Application.isPlaying)
        {
            Debug.LogWarning("Clearing the enemy wave is only available when the game is running!");
            return;
        }

        Enemy[] enemies = transform.GetComponentsInChildren<Enemy>();
        foreach (Enemy enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }
    }

    [ContextMenu("Restart Wave")]
    public void RestartWave()
    {
        if (!Application.isPlaying)
        {
            Debug.LogWarning("Restarting an enemy wave is only available when the game is running!");
            return;
        }

        ClearWave();
        StartWave();
    }

#if UNITY_EDITOR
    public GameObject CreateEnemyPlaceholder(EnemyData enemyData, Vector3 position)
    {
        GameObject placeholderGO = new(enemyData.name);
        placeholderGO.transform.parent = transform;
        placeholderGO.transform.localPosition = position;

        var placeholder = placeholderGO.AddComponent<EnemyPlaceholder>();
        placeholder.Data = enemyData;
        
        return placeholderGO;
    }
#endif
}

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyRepository", menuName = "Cyber Jungle Infinity/Enemy Repository")]
public class EnemyRepository : ScriptableObject
{
    [SerializeField] private List<EnemyData> enemyList = new();

    private Dictionary<string, EnemyData> enemyLookupTable;

    public IReadOnlyList<string> AllEnemyNames
    {
        get
        {
            EnsureLookupTableInitialized();
            return enemyLookupTable.Keys.ToList();
        }
    }

    private void OnEnable()
    {
        enemyLookupTable = null;
    }

    private void EnsureLookupTableInitialized()
    {
        if (enemyLookupTable != null) return;

        enemyLookupTable = new Dictionary<string, EnemyData>();

        if (enemyList == null) return;

        foreach (EnemyData data in enemyList)
        {
            if (data == null) continue;

            if (data.Prefab == null)
            {
                Debug.LogWarning($"[EnemyRepository] No prefab set for enemy {data.name}. Review the enemy data asset.");
                continue;
            }

            if (!enemyLookupTable.ContainsKey(data.name))
            {
                enemyLookupTable.Add(data.name, data);
            }
            else
            {
                Debug.LogWarning($"[EnemyRepository] Duplicate enemy name key detected: '{data.name}'");
            }
        }
    }

    public EnemyData GetEnemy(string name)
    {
        EnsureLookupTableInitialized();

        if (enemyLookupTable.TryGetValue(name, out EnemyData enemy))
        {
            return enemy;
        }

        Debug.LogError($"[EnemyRepository] Enemy with name '{name}' not found!");
        return null;
    }

    private void OnValidate()
    {
        enemyLookupTable = null;
    }
}
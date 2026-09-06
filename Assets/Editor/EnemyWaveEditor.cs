using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyWave))]
public class EnemyWaveEditor : Editor
{
    private int selectedIndex = 0;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EnemyWave wave = (EnemyWave)target;

        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("Wave Design Tools", EditorStyles.boldLabel);

        EnemyRepository repository = FindEnemyRepository();

        if (repository == null)
        {
            EditorGUILayout.HelpBox("No EnemyRepository asset found in the project. Please create one.", MessageType.Warning);
        }

        IReadOnlyList<string> enemies = repository.AllEnemyNames;
        if (enemies == null || enemies.Count == 0)
        {
            EditorGUILayout.HelpBox("EnemyRepository found, but it contains no enemies.", MessageType.Info);
            return;
        }

        selectedIndex = EditorGUILayout.Popup("Select Enemy", selectedIndex, enemies.ToArray());
        selectedIndex = Mathf.Clamp(selectedIndex, 0, enemies.Count - 1);

        if (GUILayout.Button("Add Enemy"))
        {
            EnemyData selectedEnemy = repository.GetEnemy(enemies[selectedIndex]);

            GameObject newPlaceholder = wave.CreateEnemyPlaceholder(selectedEnemy, Vector3.zero);
            Undo.RegisterCreatedObjectUndo(newPlaceholder, "Add Enemy Placeholder");

            Selection.activeGameObject = newPlaceholder;
        }
    }
    
    private EnemyRepository FindEnemyRepository()
    {
        // Searches the entire Assets folder for assets of type EnemyRepository
        string[] guids = AssetDatabase.FindAssets("t:EnemyRepository");

        if (guids.Length == 0) return null;

        if (guids.Length > 1)
        {
            Debug.LogWarning("[WaveEditor] Multiple EnemyRepository assets found! Using the first one.");
        }

        string assetPath = AssetDatabase.GUIDToAssetPath(guids[0]);
        return AssetDatabase.LoadAssetAtPath<EnemyRepository>(assetPath);
    }
}

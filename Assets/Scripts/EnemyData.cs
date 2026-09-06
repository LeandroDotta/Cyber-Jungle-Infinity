using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "Cyber Jungle Infinity/Enemy Data")]
public class EnemyData : ScriptableObject
{
    [SerializeField] private Enemy prefab;
    [SerializeField] private Color sampleColor;
    [SerializeField] private int scorePoints;

    public Enemy Prefab => prefab;
    public Color SampleColor => sampleColor;
    public int ScorePoints => scorePoints;
}

using UnityEditor;
using UnityEngine;

public class EnemyPlaceholder : MonoBehaviour
{
    [field: SerializeField] public EnemyData Data { get; set; }

    public void OnDrawGizmos()
    {
        float radius = 0.3f;
        string labelText = Data != null ? Data.name : "Invalid Placeholder";
        Color color = Data ? Data.SampleColor : Color.gray;
        GUIStyle labelStyle = new(EditorStyles.boldLabel)
        {
            normal = { textColor = Color.black },
            fontSize = 12,
            alignment = TextAnchor.MiddleCenter
        };

        Gizmos.color = color;
        Gizmos.DrawWireSphere(transform.position, radius);

#if UNITY_EDITOR
        // Draw the text string slightly above the object's position
        Vector3 textPosition = transform.position + Vector3.up * (radius + 0.2f);
        Handles.Label(textPosition, labelText, labelStyle);
#endif
    }
}
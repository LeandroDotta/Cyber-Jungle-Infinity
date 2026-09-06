using UnityEngine;

public static class TransformExtensions
{
    public static Transform[] GetAllChildren(this Transform transform)
    {
        Transform[] children = new Transform[transform.childCount];

        for (int i = 0; i < children.Length; i++)
        {
            children[i] = transform.GetChild(i);
        }

        return children;
    }
}

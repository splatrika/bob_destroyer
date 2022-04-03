using UnityEngine;
using System.Collections;

public static class BoxCollider2DExtensions
{
    public static Rect GetGlobalRect(this BoxCollider2D collider)
    {
        Rect rect = new Rect();
        rect.size = collider.size * (Vector2)collider.transform.lossyScale;
        rect.center = (Vector2)collider.transform.position + collider.offset;
        return rect;
    }
}

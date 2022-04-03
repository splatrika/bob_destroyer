using UnityEngine;


public static class TransformExtensions
{
    public static void LookAt2D(this Transform transform, Transform target)
    {
        transform.rotation = transform.GetLookAtRotation(target);
    }


    public static Quaternion GetLookAtRotation(this Transform transform, Transform target)
    {
        return transform.GetLookAtRotation(target.position);
    }


    public static Quaternion GetLookAtRotation(this Transform transform, Vector2 target)
    {
        Vector2 direction = target - (Vector2)transform.position;
        direction.Normalize();
        float degress = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        return Quaternion.Euler(0, 0, degress);
    }
}

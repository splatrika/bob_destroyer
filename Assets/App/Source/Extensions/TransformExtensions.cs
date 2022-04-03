using UnityEngine;


public static class TransformExtensions
{
    public static void LookAt2D(this Transform transform, Transform target)
    {
        Vector2 direction = target.position - transform.position;
        direction.Normalize();
        float degress = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, degress);
    }
}

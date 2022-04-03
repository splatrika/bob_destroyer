using System.Collections;
using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using BobDestroyer.App;

public class Follower2DTests
{
    [UnityTest]
    public IEnumerator TestFollow()
    {
        Transform followTarget = new GameObject().transform;
        followTarget.transform.position = Vector2.one * 2;
        Follower2D follower = new GameObject().AddComponent<Follower2D>();
        float speed = 1;
        follower.Init(followTarget, new Vector2(1, 1), Vector2.zero, speed);
        Func<Vector2> currentDistance = () => followTarget.position - follower.transform.position;
        Vector2 latestDistance = currentDistance();
        float timeToArrival = currentDistance().magnitude / speed;
        yield return new WaitForSeconds(timeToArrival / 2);
        Assert.Less(currentDistance().magnitude, latestDistance.magnitude);
        float inaccuracy = 0.1f;
        yield return new WaitForSeconds(timeToArrival / 2 + inaccuracy);
        Assert.AreEqual(currentDistance().magnitude, 0);
    }
}

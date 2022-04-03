using System;
using UnityEngine;

namespace BobDestroyer.App
{

    public class KickTrigger : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            IKickable target = other.attachedRigidbody?.GetComponent<IKickable>();
            if (target != null)
            {
                target.Kick();
            }
        }
    }

}

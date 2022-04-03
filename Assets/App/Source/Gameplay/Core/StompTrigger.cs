using System;
using UnityEngine;

namespace BobDestroyer.App
{

    public class StompTrigger : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            IStompable target = other.attachedRigidbody?.GetComponent<IStompable>();
            if (target != null)
            {
                target.Stomp();
            }
        }
    }

}

using UnityEngine;
using System;


namespace BobDestroyer.App
{

    public class BobController : MonoBehaviour
    {
        [SerializeField] private BobModel _target;
        [SerializeField] private string _walkAxis;
        [SerializeField] private string _kickButton;


        private void Start()
        {
            if (!_target) throw new NullReferenceException(nameof(_target));
        }


        private void Update()
        {
            if (!_target) return;

            float walkVelocity = Input.GetAxis(_walkAxis);
            if (walkVelocity != 0)
            {
                _target.TryWalk(walkVelocity);
            }
            else
            {
                _target.TryStayStill();
            }

            if (Input.GetButtonDown(_kickButton))
            {
                _target.TryKick();
            }
        }
    }

}
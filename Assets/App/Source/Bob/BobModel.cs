using System;
using System.Collections.Generic;
using UnityEngine;

namespace BobDestroyer.App
{

    [RequireComponent(typeof(Rigidbody2D))]
    public class BobModel : MonoBehaviour
    {
        public delegate void FinishKick();
        public delegate void FinishStomp();

        public event Action<IKickable, FinishKick> Kick;
        public event Action<IStompable, FinishStomp> Stomp;
        public event Action<float> Walk;
        public event Action StayStill;

        [SerializeField] private float _speed;

        private bool isWalking => _velocity != 0;
        private float _velocity;
        private IKickable _kickTarget;
        private bool _isKicking = false;
        private bool _isStomping = false;
        private Rigidbody2D _rigidbody;


        public void TryKick()
        {
            if (_kickTarget != null && !_isKicking)
            {
                _isKicking = true;
                Kick?.Invoke(_kickTarget, OnFinishKick);
            }
            TryStayStill();
        }


        public void TryWalk(float velocity)
        {
            if (velocity == 0)
            {
                throw new InvalidOperationException("Velocity can't be zero");
            }
            if (_isKicking || _isStomping)
            {
                return;
            }
            _velocity = velocity;
            Walk?.Invoke(_velocity);
        }


        public void TryStayStill()
        {
            if (!isWalking) return;
            _velocity = 0 * _speed;
            StayStill?.Invoke();
        }


        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }


        private void OnCollisionEnter2D(Collision2D collision)
        {
            IStompable target = collision.rigidbody?.GetComponent<IStompable>();
            if (target != null)
            {
                TryStayStill();
                _isStomping = true;
                Stomp?.Invoke(target, OnFinishStomp);
            }
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            IKickable target = other.attachedRigidbody?.GetComponent<IKickable>();
            if (target != null)
            {
                _kickTarget = target;
            }
        }


        private void OnTriggerExit2D(Collider2D other)
        {
            IKickable target = other.attachedRigidbody?.GetComponent<IKickable>();
            if (target == _kickTarget)
            {
                _kickTarget = null;
            }
        }


        private void Update()
        {
            Vector2 target = transform.position;
            target.x += _velocity * Time.deltaTime;
            transform.position = target;
        }


        private void OnFinishKick()
        {
            _isKicking = false;
        }


        private void OnFinishStomp()
        {
            _isStomping = false;
        }

    }

}

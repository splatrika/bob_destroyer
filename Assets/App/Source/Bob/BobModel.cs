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
        public event Action Idle;

        public float Speed => _speed;
        public float Direction { get; private set; }

        [SerializeField] private float _speed;
        [SerializeField] private Transform _raycastOrigin;
        [SerializeField] private float _raycastLength;

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
            Direction = Mathf.Sign(velocity);
            _velocity = velocity * _speed;
            Vector2 totalVelocity = _rigidbody.velocity;
            totalVelocity.x = velocity * _speed;
            _rigidbody.velocity = totalVelocity;
            Walk?.Invoke(_velocity);
        }


        public void TryStayStill()
        {
            if (!isWalking) return;
            _velocity = 0 * _speed;
            Idle?.Invoke();
            Vector2 totalVelocity = _rigidbody.velocity;
            totalVelocity.x = 0;
            _rigidbody.velocity = totalVelocity;
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


        private void Update()
        {
            RaycastHit2D hit = Physics2D.Raycast(_raycastOrigin.position,
                Vector2.right * Math.Sign(transform.lossyScale.x), _raycastLength);
            _kickTarget = hit.rigidbody?.GetComponent<IKickable>();
        }


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(_raycastOrigin.position,
                _raycastOrigin.position + Vector3.right * _raycastLength);
        }


        private void OnFinishKick()
        {
            _isKicking = false;
            Idle?.Invoke();
        }


        private void OnFinishStomp()
        {
            _isStomping = false;
            Idle?.Invoke();
        }

    }

}

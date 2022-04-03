using UnityEngine;
using System;

namespace BobDestroyer.App
{
    /// <summary>
    /// Init rquired
    /// </summary>
    public class Follower2D : MonoBehaviour
    {
        private Vector2 _totalTarget => (Vector2)_target.position + _offset;
        private Transform _target;
        private Vector2 _mask;
        private Vector2 _offset;
        private float _speed;
        private bool _inited = false;
        private Transform _transform;


        public void Init(Transform target, Vector2 mask, Vector2 offset, float speed)
        {
            if (_inited)
            {
                throw new InvalidOperationException("Already inited");
            }
            _target = target;
            _mask = mask;
            _offset = offset;
            _speed = speed;
            _inited = true;
        }


        private void Awake()
        {
            _transform = transform;
        }


        private void Start()
        {
            if (!_inited)
            {
                Debug.LogError("Init required");
            }
        }


        private void Update()
        {
            if ((Vector2)_transform.position * _mask == _totalTarget * _mask)
            {
                return;
            }
            Vector2 distance = (_totalTarget - (Vector2)_transform.position) * _mask;
            Vector2 direction = distance.normalized;
            Vector2 velocity = direction * _speed;
            if (velocity.magnitude > distance.magnitude)
            {
                _transform.position = _totalTarget;
                return;
            }
            _transform.position += (Vector3)velocity * Time.deltaTime;
        }
    }

}
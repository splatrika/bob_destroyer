using UnityEngine;
using System;

namespace BobDestroyer.App
{

    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _damage;
        [SerializeField] private float _burnTime = 10;

        private bool _inited = false;
        private bool _flag = false;
        private Vector2 _velocity;


        public void Init(Transform target, Transform startPoint)
        {
            if (_inited)
            {
                throw new InvalidOperationException("Already inited");
            }
            transform.position = startPoint.position;
            transform.LookAt2D(target);
            float angle = transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
            _velocity.x = Mathf.Cos(angle);
            _velocity.y = Mathf.Sin(angle);
            _velocity *= _speed;
            _inited = true;
        }


        private void Start()
        {
            Invoke(nameof(OnBurn), _burnTime);
        }


        private void Update()
        {
            transform.position += (Vector3)_velocity * Time.deltaTime;
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            IDamagable target = other.attachedRigidbody?.GetComponent<IDamagable>();
            if (target != null)
            {
                target.ApplyDamage(_damage);
                Destroy(gameObject);
            }
        }


        private void OnBurn()
        {
            Destroy(gameObject);
        }
    }

}

using UnityEngine;
using System.Collections;
using DG.Tweening;
using System;

namespace BobDestroyer.App
{
    /// <summary>
    /// Init required
    /// </summary>
    [RequireComponent(typeof(BoxCollider2D))]
    public class Tank : MonoBehaviour, ICityDefencer, IStompable, IDirected
    {
        public event Action Died;
        public event Action<Side> DirectionChanged;

        public Rect Body { get; private set; }
        public Side Direction { get; private set; }

        [SerializeField] private float _speed;
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private float _shootFrequency;
        [SerializeField] private Transform _shootAnchor;
        [SerializeField] private Transform _gunRotationAnchor;
        [SerializeField] private float _gunRotationTime = 1;

        private BoxCollider2D _collider;
        private BobModel _target;
        private Follower2D _follower;
        private DirectionFlip _flipping;
        private bool _inited;


        public void Init(ILevelData level, float absoluteDistance, Side side)
        {
            if (_inited)
            {
                throw new InvalidOperationException("Already inited");
            }
            _target = level.PlayerCharacter;
            Vector2 offset = new Vector2(absoluteDistance * (int)side, 0);
            Vector2 mask = new Vector2(1, 0);
            _follower.Init(_target.transform, mask, offset, _speed);
            Rect screen = level.ScreenBounds;
            Vector2 startPosition = screen.center;
            startPosition.y = level.GroundHeight;
            startPosition.x += screen.width / 2f * (int)side;
            transform.position = startPosition;
            _flipping.Init(this, transform);
            Direction = side;
            DirectionChanged?.Invoke(side);
            _inited = true;
        }


        public void Stomp()
        {
            Died?.Invoke();
            Destroy(gameObject);
        }


        private void Awake()
        {
            _follower = gameObject.AddComponent<Follower2D>();
            _flipping = gameObject.AddComponent<DirectionFlip>();
            _collider = GetComponent<BoxCollider2D>();
            Body = _collider.GetGlobalRect();
        }


        private void Update()
        {
            Side actualDirection = (Side)Mathf.Sign(
                _target.transform.position.x - transform.position.x);
            if (actualDirection != Direction)
            {
                Direction = actualDirection;
                DirectionChanged?.Invoke(actualDirection);
            }
        }


        private void Start()
        {
            if (!_inited)
            {
                Debug.LogError("Init required");
            }
            StartCoroutine(ShootsCoroutine());
        }


        private IEnumerator ShootsCoroutine()
        {
            while (this)
            {
                yield return new WaitForSeconds(_shootFrequency);
                Vector2 shotPoint = _target.BodyCenterAnchor.position;
                shotPoint.x *= (int)Direction;
                Quaternion gunRotation = _gunRotationAnchor.GetLookAtRotation(shotPoint);
                gunRotation.z *= (int)Direction;
                _gunRotationAnchor.DORotateQuaternion(gunRotation, _gunRotationTime);
                yield return new WaitForSeconds(_gunRotationTime);
                Instantiate(_bulletPrefab)
                    .Init(_target.BodyCenterAnchor, _shootAnchor);
            }
        }    


        private void OnDestroy()
        {
            Destroy(_follower);
            Destroy(_flipping);
        }
    }

}

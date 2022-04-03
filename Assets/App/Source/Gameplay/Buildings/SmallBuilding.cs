using UnityEngine;
using System.Collections;

namespace BobDestroyer.App
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class SmallBuilding : MonoBehaviour, IStompable
    {
        public Rect Body { get; private set; }

        [SerializeField] private Sprite _ruined;
        [SerializeField] private SpriteRenderer _targetRenderer;

        private BoxCollider2D _collider;


        private void Start()
        {
            _collider = GetComponent<BoxCollider2D>();
            Body = _collider.GetGlobalRect();
        }


        public void Stomp()
        {
            _collider.enabled = false;
            _targetRenderer.sprite = _ruined;
        }
    }

}

using UnityEngine;
using System;

namespace BobDestroyer.App
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Animator))]
    public class Skyscraper : MonoBehaviour, IKickable
    {
        [Serializable]
        public class CrackState
        {
            public Sprite BuildingSprite => _sprite;
            [SerializeField] private Sprite _sprite;
        }


        public Rect Body { get; private set; }

        [SerializeField] private CrackState[] _crackStates;
        [SerializeField] private Sprite _ruined;
        [Header("Animations")]
        [SerializeField] private string _kickAnimation;
        [SerializeField] private string _idleAnimation;

        private BoxCollider2D _collider;
        private SpriteRenderer _renderer;
        private int _crackState = -1;
        private Animator _animator;


        private void Start()
        {
            _collider = GetComponent<BoxCollider2D>();
            _renderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
            Body = _collider.GetGlobalRect();
        }


        public void Kick()
        {
            _animator.Play(_idleAnimation); //TODO make after
            _animator.Play(_kickAnimation);
            _crackState++;
            if (_crackState >= _crackStates.Length)
            {
                _collider.enabled = false;
                _renderer.sprite = _ruined;
                return;
            }
            _renderer.sprite = _crackStates[_crackState].BuildingSprite;

        }
    }

}

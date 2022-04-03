using UnityEngine;
using System;

namespace BobDestroyer.App
{
    /// <summary>
    /// Init requared
    /// </summary>
    public class DirectionFlip : MonoBehaviour
    {
        private IDirected _target;
        private Transform _root;
        private bool _inited;


        public void Init(IDirected target, Transform root)
        {
            if (_inited)
            {
                throw new InvalidProgramException("Already inited");
            }
            _target = target;
            _root = root;
            _target.DirectionChanged += OnDirectionChanged;
            _inited = true;
        }


        private void Start()
        {
            if (!_inited)
            {
                Debug.LogError("Init requared");
            }
        }


        private void OnDestroy()
        {
            _target.DirectionChanged -= OnDirectionChanged;
        }


        private void OnDirectionChanged(Side side)
        {
            Vector2 flipped = Vector2.one;
            flipped.x *= (int)side;
            _root.localScale = flipped;
        }
    }

}
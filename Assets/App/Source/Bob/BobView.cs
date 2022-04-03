using System.Collections;
using System;
using UnityEngine;

namespace BobDestroyer.App
{

    [RequireComponent(typeof(Animator))]
    public class BobView : MonoBehaviour
    {
        [SerializeField] private Transform _root;
        [SerializeField] private BobModel _model;
        [SerializeField] private Transform _kickAnchor;
        [SerializeField] private Transform _stompAnchor;
        [Header("AnimationClips")]
        [SerializeField] private string _stompAnimation;
        [SerializeField] private string _kickAnimation;
        [SerializeField] private string _walkAnimation;
        [SerializeField] private string _idleAnimation;
        private Animator _animator;


        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _model.Stomp += OnStomp;
            _model.Kick += OnKick;
            _model.Walk += OnWalk;
            _model.Idle += OnIdle;
        }


        private void OnDestroy()
        {
            _model.Stomp -= OnStomp;
            _model.Kick -= OnKick;
            _model.Walk -= OnWalk;
            _model.Idle -= OnIdle;
        }


        private void OnWalk(float velocity)
        {
            _animator.Play(_walkAnimation);
            _animator.speed = Mathf.Abs(velocity) / _model.Speed;
            Vector3 flipping = Vector3.one;
            flipping.x = _model.Direction;
            _root.localScale = flipping;
        }


        private void OnIdle()
        {
            _animator.Play(_idleAnimation);
            _animator.speed = 1;
        }


        private void OnStomp(IStompable target, BobModel.FinishStomp callback)
        {
            Vector3 anchor = _stompAnchor.transform.position;
            anchor.x = target.Body.center.x;
            _stompAnchor.position = anchor;
            PlayAnimationWithCallback(_stompAnimation, () => callback());
        }


        private void OnKick(IKickable target, BobModel.FinishKick callback)
        {
            Vector3 anchor = _kickAnchor.transform.position;
            anchor.x = target.Body.center.x - target.Body.width / 2 * _model.Direction;
            _kickAnchor.position = anchor;
            PlayAnimationWithCallback(_kickAnimation, () => callback());
        }


        private void PlayAnimationWithCallback(string name, Action callback)
        {
            StartCoroutine(AnimationWithCallbackCoroutine(name, callback));
        }



        private IEnumerator AnimationWithCallbackCoroutine(string name,
            Action callback)
        {
            _animator.Play(_idleAnimation);
            yield return null;
            _animator.Play(name);
            yield return null;
            float duration = _animator.GetCurrentAnimatorClipInfo(0).Length;
            yield return new WaitForSeconds(duration);
            callback();
        }
    }

}

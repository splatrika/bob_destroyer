using UnityEngine;
using System.Collections;

namespace BobDestroyer.App
{

    public class Level : MonoBehaviour, ILevelData
    {
        public BobModel PlayerCharacter => _playerCharacter;
        public float GroundHeight => _groundHeight;
        public Rect ScreenBounds => new Rect()
        {
            width = 10,
            center = Vector2.zero
        };


        [SerializeField] private float _groundHeight;
        [SerializeField] private BobModel _playerCharacter;


        private void OnDrawGizmos()
        {
            Vector3 groundGizmosStart = new Vector2(-100, _groundHeight);
            Vector3 groundGizmosEnd = new Vector2(100, _groundHeight);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(groundGizmosStart, groundGizmosEnd);
        }
    }

}
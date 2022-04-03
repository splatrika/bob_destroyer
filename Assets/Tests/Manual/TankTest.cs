using UnityEngine;
using System.Collections;
using BobDestroyer.App;

namespace BobDestroyer.Tests
{

    public class TankTest : MonoBehaviour
    {
        [SerializeField] private Tank _prefab;
        [SerializeField] private Level _level;
        [SerializeField] private Side side;
        [SerializeField] private float _distance;


        private void Start()
        {
            Instantiate(_prefab)
                .Init(_level, _distance, side);
        }
    }

}
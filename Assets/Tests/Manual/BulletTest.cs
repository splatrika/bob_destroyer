using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BobDestroyer.App;

namespace BobDestroyer.Tests
{

    public class BulletTest : MonoBehaviour
    {
        [SerializeField] private Bullet _prefab;
        [SerializeField] private Transform _target;


        private void Start()
        {
            var instance = Instantiate(_prefab);
            instance.Init(_target, transform);
        }
    }

}
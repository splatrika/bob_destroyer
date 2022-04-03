using System;
using System.Collections;
using UnityEngine;

namespace BobDestroyer.App
{

    public static class FrequencyAction
    {
        public static Coroutine Run(MonoBehaviour sender, Action action, float frequency)
        {
            return sender.StartCoroutine(ActionCoroutine(action, frequency));
        }


        private static IEnumerator ActionCoroutine(Action action, float frequency)
        {
            yield return new WaitForSeconds(frequency);
            action();
        }
    }

}
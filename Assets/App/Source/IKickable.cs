using System;
using UnityEngine;

namespace BobDestroyer.App
{

    public interface IKickable
    {
        public Rect Body { get; }


        public void Kick();
    }

}

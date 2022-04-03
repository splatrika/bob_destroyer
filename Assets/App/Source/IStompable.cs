using System;
using UnityEngine;

namespace BobDestroyer.App
{

    public interface IStompable
    {
        public Rect Body { get; }


        public void Stomp();
    }

}


using System;

namespace BobDestroyer.App
{

    public interface IDirected
    {
        public event Action<Side> DirectionChanged;
        public Side Direction { get; }
    }

}

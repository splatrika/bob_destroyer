using UnityEngine;

namespace BobDestroyer.App
{

    public interface ILevelData
    {
        public BobModel PlayerCharacter { get; }
        public float GroundHeight { get; }
        public Rect ScreenBounds { get; }
    }

}
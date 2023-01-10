using System;
using Unity.Mathematics;

namespace Data.ValueObjects
{
    [Serializable]
    public struct InputData
    {
        public float HorizontalInputSpeed;
        public float ClampSpeed;
        public float2 ClampValues;

        public InputData(float horizontalInputSpeed, float clampSpeed, float2 clampValues)
        {
            HorizontalInputSpeed = horizontalInputSpeed;
            ClampSpeed = clampSpeed;
            ClampValues = clampValues;
        }
    }
}
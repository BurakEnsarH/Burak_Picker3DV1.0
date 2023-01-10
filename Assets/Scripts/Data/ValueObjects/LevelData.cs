using System;
using System.Collections.Generic;

namespace Data.ValueObjects
{
    [Serializable]
    public struct LevelData
    {
        public List<PoolData> PoolList;

        public LevelData(List<PoolData> poolList)
        {
            PoolList = poolList;
        }
    }

    [Serializable]
    public struct PoolData
    {
        public byte RequiredObjectCount;
    }
}
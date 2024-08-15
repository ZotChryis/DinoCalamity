using System;
using Schemas;
using UnityEngine;

namespace Schemas
{
    [Serializable]
    public class TileSearchParameters
    {
        public TileSchema.TileType? TileType;
        public int SearchRadius = 1;
        public bool InfiniteSearchRadius = false;
    }
}

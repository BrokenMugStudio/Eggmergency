using System;
using Eggmergency.Scripts.Enums;
using UnityEngine;

namespace Eggmergency.Scripts.Data
{
    [Serializable]
    public class TimelineEvent
    {
        public float SpawnTime;  
        public eObjectType Type;  
        public int LaneX;    
    }
}

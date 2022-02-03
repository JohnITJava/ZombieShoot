using UnityEngine;


namespace ZombieQuest
{
    public sealed class ZombieMovingStats
    {
        public Vector3 NextPatrolPoint { get; set; }
        public bool IsGoingToPoint { get; set; }
        public bool IsAchieveDestination { get; set; }
        public bool IsPlayerInVision { get; set; }
        public bool IsPlayerInHitBox { get; set; }
        public bool IsSmthOnTheWay { get; set; }
        public bool IsTriggered { get; set; }
        public bool WasAlreadyTriggered { get; set; }
        
    }
}
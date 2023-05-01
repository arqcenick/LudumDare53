using System;

public partial class LevelManager
{
    public enum DeathReason
    {
        Collision,
        OOB,
        OrderFail,
    }
    public struct LevelEvents
    {
        public Action OnDeliveryComplete;
        public Action OnDeliveryFailed;
        public Action<float> OnDayProgressed;
        public Action<int> OnDayPassed;
        public Action<DeathReason> OnPlayerDeath;


        public Action<OrderComponent> OnOrderCreated;
        public Action<OrderComponent> OnOrderCompleted;


    }
}

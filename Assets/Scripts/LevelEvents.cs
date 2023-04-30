using System;

public partial class LevelManager
{
    public struct LevelEvents
    {
        public Action OnDeliveryComplete;
        public Action OnDeliveryFailed;
        public Action<float> OnDayProgressed;

        public Action<OrderComponent> OnOrderCreated;
        public Action<OrderComponent> OnOrderCompleted;


    }
}

using System;

public struct PlayerEvents
{
    public Action<Cargo> OnCargoCollected;
    public Action<CargoHolder> OnCargoHolderAdded;

}

using System;

public interface IStackable
{
    public void OnInteractEnter(Action<Muzzle> callback);
}
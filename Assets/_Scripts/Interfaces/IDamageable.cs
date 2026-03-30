using System;

public interface IDamageable
{
    public enum DamageableType
    {
        None,
        Obstacle,
        Multiplier,
        Moving
    }

    public DamageableType Type 
    {
        get;
    }

    public bool IsDead
    {
        get;
    }

    public void OnDamaged(int damage)
    {

    }
    public void OnBlockCrashed(Action callback);
}

public interface IPooledObject
{
    void OnObjectInstantiated();

    void OnObjectReturnedToPool();

    void OnObjectGetFromPool();
}
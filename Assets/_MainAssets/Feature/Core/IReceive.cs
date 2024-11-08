public interface IReceive<in T>
{
    bool TryReceive(T item);
    bool CanReceive(T item);
}

public interface ISend
{
    void Send();
}
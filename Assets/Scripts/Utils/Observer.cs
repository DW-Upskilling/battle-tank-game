using System;

public abstract class Observer<T, S> : Singleton<T> where T : Observer<T, S>
{
    static event Action<S> ObserverQueue;

    public void AddListener(Action<S> listener)
    {
        ObserverQueue += listener;
    }

    public void RemoveListener(Action<S> listener)
    {
        ObserverQueue -= listener;
    }

    protected void TriggerEvent(S s)
    {
        ObserverQueue?.Invoke(s);
    }
}
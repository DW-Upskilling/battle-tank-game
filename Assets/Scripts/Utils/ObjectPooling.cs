using System.Collections.Generic;

public abstract class ObjectPooling<T, S> : Singleton<T> where T : ObjectPooling<T, S>
{
    List<ObjectPoolingItem<S>> poolItems;

    protected override void Initialize()
    {
        poolItems = new List<ObjectPoolingItem<S>>();
    }

    public virtual S GetItem()
    {
        if (poolItems.Count < 0)
            createNewPoolItem();

        ObjectPoolingItem<S> poolItem = poolItems.Find(e => !e.IsUsed);
        if (poolItem == null)
            poolItem = createNewPoolItem();

        poolItem.IsUsed = true;

        return poolItem.Item;
    }

    public virtual bool ReturnItem(S item)
    {
        ObjectPoolingItem<S> poolItem = poolItems.Find(e => e.Item.Equals(item));
        if (poolItem != null)
        {
            poolItem.IsUsed = false;
            return true;
        }

        return false;
    }

    protected abstract S CreateItem();

    ObjectPoolingItem<S> createNewPoolItem()
    {
        ObjectPoolingItem<S> item = new ObjectPoolingItem<S>(CreateItem());
        poolItems.Add(item);

        return item;
    }
}

public class ObjectPoolingItem<T>
{
    public T Item { get; private set; }
    public bool IsUsed { get; set; }

    public ObjectPoolingItem(T item)
    {
        Item = item;
        IsUsed = false;
    }
};
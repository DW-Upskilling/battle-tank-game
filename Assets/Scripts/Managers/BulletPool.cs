using UnityEngine;

class BulletPool : ObjectPooling<BulletPool, BulletController>
{
    [SerializeField]
    BulletScriptableObject bulletScriptableObject;

    protected override BulletController CreateItem()
    {
        return new BulletController(bulletScriptableObject);
    }
}
using UnityEngine;

public class TankController
{
    public TankModel TankModel { get; protected set; }
    public TankView TankView { get; protected set; }

    protected float horizontal, vertical;

    public TankController(TankScriptableObject tankScriptableObject)
    {
        // Override this constuctor is approriate derived class
    }

    public virtual void Update()
    {
        // movement senstivity threshold
        if (horizontal >= .2f || horizontal <= -.2f || vertical >= .2f || vertical <= -.2f)
            HandleMovement(horizontal, vertical, Time.deltaTime);
    }

    protected virtual void HandleMovement(float horizontal, float vertical, float timeVariance)
    {
        Vector3 position = TankView.Position;
        position.x += horizontal * TankModel.Speed * timeVariance;
        position.z += vertical * TankModel.Speed * timeVariance;

        Vector3 rotation = Vector3.zero;
        rotation.x = horizontal;
        rotation.y = position.y;
        rotation.z = vertical;

        if (rotation != Vector3.zero)
            TankView.Rotation = Quaternion.LookRotation(rotation);

        TankView.Position = position;
        TankView.ApplyTranform = true;
    }

    public void Shoot()
    {
        AmmoScriptableObject ammoScriptableObject = TankModel.AmmoScriptableObject;

        switch (ammoScriptableObject.AmmoType)
        {
            case AmmoType.Bullet:
                BulletPool bulletPool = BulletPool.Instance;
                if (bulletPool != null)
                {
                    BulletController bulletController = bulletPool.GetItem();
                    bulletController.Reset(this, TankView.BulletSpawnPosition);
                }
                // new BulletController((BulletScriptableObject)ammoScriptableObject, this, TankView.BulletSpawnPosition);
                break;
        }
    }

    public void TakeDamage(float damage)
    {
        TankModel.CurrentHealth -= damage;

        // Debug.Log("Damage: " + damage);
        // Debug.Log("Tank Health: " + TankModel.CurrentHealth);

        if (TankModel.CurrentHealth <= 0)
        {
            TankModel.IsAlive = false;
        }
    }
}
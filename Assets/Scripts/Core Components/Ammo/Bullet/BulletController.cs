using UnityEngine;

public class BulletController
{
    public TankController ParentTankContoller { get; set; }

    public Transform SpawnPoint { get; set; }

    public BulletModel BulletModel { get; }
    public BulletView BulletView { get; }

    float timeLeft, collisions;

    public BulletController(BulletScriptableObject bulletScriptableObject)
    {

        BulletModel = new BulletModel(bulletScriptableObject);
        BulletView = GameObject.Instantiate<BulletView>(bulletScriptableObject.BulletViewPrefab, Vector3.zero, Quaternion.identity);

        PhysicMaterial physicMaterial = new PhysicMaterial();
        physicMaterial.bounciness = BulletModel.Bounciness;
        physicMaterial.frictionCombine = PhysicMaterialCombine.Minimum;
        physicMaterial.bounceCombine = PhysicMaterialCombine.Maximum;

        BulletView.BulletController = this;

        BulletView.MeshColliderComponent.material = physicMaterial;

        BulletView.RigidbodyComponent.useGravity = BulletModel.UseGravity;

        timeLeft = 0;
        collisions = 0;
    }

    public BulletController(BulletScriptableObject bulletScriptableObject, TankController parentTankContoller, Transform spawnPoint)
    {
        ParentTankContoller = parentTankContoller;
        SpawnPoint = spawnPoint;

        BulletModel = new BulletModel(bulletScriptableObject);
        BulletView = GameObject.Instantiate<BulletView>(bulletScriptableObject.BulletViewPrefab, SpawnPoint.position, Quaternion.identity);

        PhysicMaterial physicMaterial = new PhysicMaterial();
        physicMaterial.bounciness = BulletModel.Bounciness;
        physicMaterial.frictionCombine = PhysicMaterialCombine.Minimum;
        physicMaterial.bounceCombine = PhysicMaterialCombine.Maximum;

        BulletView.BulletController = this;

        BulletView.MeshColliderComponent.material = physicMaterial;

        BulletView.RigidbodyComponent.useGravity = BulletModel.UseGravity;

        timeLeft = BulletModel.LifeTime;
        collisions = BulletModel.MaxCollisions;

        HandleFireMovement();
    }

    public void Update()
    {
        timeLeft -= Time.deltaTime;

        if (collisions <= 0 || timeLeft <= 0)
        {
            BulletView.gameObject.SetActive(false);
            if (BulletPool.Instance != null)
                BulletPool.Instance.ReturnItem(this);
        }
    }

    public void OnCollisionEnter(EnemyTankController enemyTankController)
    {
        if (enemyTankController != ParentTankContoller)
        {
            enemyTankController.TakeDamage(GetDamage());
            collisions--;
        }
    }
    public void OnCollisionEnter(PlayerTankController playerTankController)
    {
        if (playerTankController != ParentTankContoller)
        {
            playerTankController.TakeDamage(GetDamage());
            collisions--;
        }
    }

    public void HandleFireMovement()
    {
        Vector3 direction = SpawnPoint.forward * BulletModel.Speed;

        BulletView.gameObject.transform.forward = direction.normalized;
        BulletView.RigidbodyComponent.AddForce(direction.normalized, ForceMode.Impulse);
        BulletView.RigidbodyComponent.velocity = direction.normalized;
    }

    public float GetDamage()
    {
        return BulletModel.Damage * ParentTankContoller.TankModel.Damage;
    }

    public void Reset(TankController parentTankContoller, Transform spawnPoint)
    {
        timeLeft = BulletModel.LifeTime;
        collisions = BulletModel.MaxCollisions;

        ParentTankContoller = parentTankContoller;
        SpawnPoint = spawnPoint;

        BulletView.Position = spawnPoint.position;
        BulletView.Rotation = spawnPoint.rotation;

        BulletView.gameObject.SetActive(true);
    }
}
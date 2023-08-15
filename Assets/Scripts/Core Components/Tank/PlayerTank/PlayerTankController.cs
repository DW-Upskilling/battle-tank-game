using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTankController : TankController
{

    PlayerTankModel PlayerTankModel;
    PlayerTankView PlayerTankView;

    LevelManager LevelManager;

    Joystick joystick;
    Button shootButton;

    bool triggerShoot;
    bool DestroyFlag;

    public PlayerTankController(PlayerTankScriptableObject playerTankScriptableObject, Joystick _joystick, Button _shootButton) : base((TankScriptableObject)playerTankScriptableObject)
    {
        if (_joystick == null)
            throw new NullReferenceException("joystick object isn't available");
        if (_shootButton == null)
            throw new NullReferenceException("shootButton object isn't available");

        joystick = _joystick;
        shootButton = _shootButton;

        PlayerTankModel = new PlayerTankModel(playerTankScriptableObject);
        TankModel = (TankModel)PlayerTankModel;

        PlayerTankView = GameObject.Instantiate<PlayerTankView>(PlayerTankModel.PlayerTankViewPrefab);
        TankView = (TankView)PlayerTankView;

        PlayerTankView.PlayerTankController = this;
        TankView.TankController = (TankController)this;

        LevelManager = LevelManager.Instance;
        if(LevelManager == null)
            throw new NullReferenceException("LevelManager isn't available");

        triggerShoot = false;
        DestroyFlag = false;

        shootButton.onClick.AddListener(ShootButtonAction);
    }

    public override void Update()
    {
        if (DestroyFlag)
            return;

        if (!PlayerTankModel.IsAlive)
        {
            DeathEvent.Instance.TriggerEvent(PlayerTankView.gameObject);
            DestroyFlag = true;
            LevelManager.PlayerLost(PlayerTankView);
            return;
        }

        horizontal = joystick.Horizontal;
        vertical = joystick.Vertical;

        base.Update();
    }

    public void FixedUpdate()
    {
        PlayerTankModel.TimeLeftForNextShot -= Time.fixedDeltaTime;

        if (triggerShoot)
        {
            if (PlayerTankModel.TimeLeftForNextShot <= 0)
            {
                PlayerTankModel.TimeLeftForNextShot = PlayerTankModel.FireRate;
                AmmoUsageEvent.Instance.TriggerEvent(PlayerTankView.gameObject);
                Shoot();
            }
            triggerShoot = false;
        }
    }

    void ShootButtonAction()
    {
        triggerShoot = true;
    }

}
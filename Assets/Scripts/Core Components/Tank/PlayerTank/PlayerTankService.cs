using UnityEngine;
using UnityEngine.UI;

public class PlayerTankService : Singleton<PlayerTankService>
{

    [SerializeField]
    PlayerTankScriptableObject PlayerTankScriptableObject;

    [SerializeField]
    Joystick Joystick;

    [SerializeField]
    Button ShootButton;

    protected override void Initialize()
    {
        
    }

   void Start()
    {
        if (PlayerTankScriptableObject != null)
        {
            PlayerTankController playerTankController = new PlayerTankController(PlayerTankScriptableObject, Joystick, ShootButton);
        }
    }
}
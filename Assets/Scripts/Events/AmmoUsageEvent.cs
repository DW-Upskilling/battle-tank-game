using UnityEngine;

public class AmmoUsageEvent : Observer<AmmoUsageEvent, int>
{
    int ammoUsageCount;

    protected override void Initialize()
    {
        ammoUsageCount = PlayerPrefs.GetInt("PlayerAmmoUsageCount", 0);
    }

    public void TriggerEvent(GameObject gameobject)
    {
        if (gameobject.GetComponent<PlayerTankView>() != null)
        {
            ammoUsageCount += 1;
            PlayerPrefs.SetInt("PlayerAmmoUsageCount", ammoUsageCount);

            base.TriggerEvent(ammoUsageCount);
        }
    }
}
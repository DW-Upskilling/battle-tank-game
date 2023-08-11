using UnityEngine;

public class DeathEvent : Observer<DeathEvent, int>
{
    int deathCount;

    protected override void Initialize()
    {
        deathCount = PlayerPrefs.GetInt("PlayerDeathCount", 0);
    }

    public void TriggerEvent(GameObject gameobject)
    {
        if (gameobject.GetComponent<PlayerTankView>() != null)
        {
            deathCount += 1;
            PlayerPrefs.SetInt("PlayerDeathCount", deathCount);

            base.TriggerEvent(deathCount);
        }
    }
}
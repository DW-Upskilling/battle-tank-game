using UnityEngine;

public class KillEvent : Observer<KillEvent, int>
{
    int killCount;

    protected override void Initialize()
    {
        killCount = PlayerPrefs.GetInt("PlayerKillCount", 0);
    }
    public void TriggerEvent(GameObject gameobject)
    {
        if (gameobject.GetComponent<EnemyTankView>() != null)
        {
            killCount += 1;
            PlayerPrefs.SetInt("PlayerKillCount", killCount);

            base.TriggerEvent(killCount);
        }
    }
}
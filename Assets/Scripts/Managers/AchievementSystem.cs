using UnityEngine;

public class AchievementSystem : Singleton<AchievementSystem>
{
    protected override void Initialize(){}

    void Start()
    {
        AmmoUsageEvent.Instance.AddListener(BulletsFiredAchievement);
        DeathEvent.Instance.AddListener(DeathAchievement);
        KillEvent.Instance.AddListener(KillAchievement);
    }

    void BulletsFiredAchievement(int count)
    {
        BulletsFired bulletsFired = (BulletsFired)count;

        switch (bulletsFired)
        {
            case BulletsFired.A:
                Debug.Log("Achievement Ammo → Noob");
                break;
            case BulletsFired.B:
                Debug.Log("Achievement Ammo → Non");
                break;
            case BulletsFired.C:
                Debug.Log("Achievement Ammo → Master");
                break;
        }
    }

    void DeathAchievement(int count)
    {
        Deaths deaths = (Deaths)count;

        switch (deaths)
        {
            case Deaths.A:
                Debug.Log("Achievement Death → Noob");
                break;
            case Deaths.B:
                Debug.Log("Achievement Death → Non");
                break;
            case Deaths.C:
                Debug.Log("Achievement Death → Master");
                break;
        }
    }

    void KillAchievement(int count)
    {
        Kills kills = (Kills)count;

        switch (kills)
        {
            case Kills.A:
                Debug.Log("Achievement Kills → Noob");
                break;
            case Kills.B:
                Debug.Log("Achievement Kills → Non");
                break;
            case Kills.C:
                Debug.Log("Achievement Kills → Master");
                break;
        }
    }
}

public enum BulletsFired
{
    A = 10,
    B = 25,
    C = 50
}

public enum Deaths
{
    A = 1,
    B = 5,
    C = 10
}

public enum Kills
{
    A = 5,
    B = 10,
    C = 15
}
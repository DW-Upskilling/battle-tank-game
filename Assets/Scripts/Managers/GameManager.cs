using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    List<SceneBlock> Scenes;

    protected override void Initialize()
    {
        if (Scenes == null)
            throw new MissingReferenceException("Scenes information isn't provided");
    }

    public SceneBlock findScene(string name) {
        return Scenes.Find(e => e.Name == name);
    }
}
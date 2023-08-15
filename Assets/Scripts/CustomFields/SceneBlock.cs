using System;
using UnityEngine;

[Serializable]
public class SceneBlock
{

    [SerializeField]
    string name;
    [SerializeField]
    int buildIndex;

    public string Name { get { return name; } }
    public int BuildIndex { get { return buildIndex; } }

}

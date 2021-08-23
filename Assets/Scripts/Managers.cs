using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Managers
{
    public static DirectionManager Direction
    {
        get
        {
            return GameObject.Find("DirectionManager").GetComponent<DirectionManager>();
        }
    }

    public static LoadingManager Loading
    {
        get
        {
            return GameObject.Find("LoadingManager").GetComponent<LoadingManager>();
        }
    }

    public static MusicManager Music
    {
        get
        {
            return GameObject.Find("MusicManager").GetComponent<MusicManager>();
        }
    }
}
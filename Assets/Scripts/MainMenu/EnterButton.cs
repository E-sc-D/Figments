using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterButton : MonoBehaviour
{   
    public void onClick()
    {
        Managers.Direction.Direct(Managers.Direction.SwitchMainMenuToFigmentChooser());
    }
}

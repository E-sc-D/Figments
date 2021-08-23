using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JamesButton : MonoBehaviour
{
    public void onClick()
    {
        Managers.Direction.Direct(Managers.Direction.SwitchFigmentChooserToJamesStart());
    }
}

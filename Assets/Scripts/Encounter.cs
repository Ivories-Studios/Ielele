using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Encounter : MonoBehaviour
{
    public int done;

    public abstract void StartFight();
}

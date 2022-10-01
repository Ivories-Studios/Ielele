using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Encounter : MonoBehaviour
{
    public int done;
    protected float timer;

    public abstract void StartFight();
}

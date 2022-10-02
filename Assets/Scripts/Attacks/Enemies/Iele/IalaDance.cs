using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IalaDance : Attack
{
    [SerializeField] float dance;

    public override void Cast(UnitObject unit, float multiplier = 1, float? stepAheadOverride = null)
    {
        base.Cast(unit, multiplier);
        PlayerObject.Instance.Dance(dance);
    }
}

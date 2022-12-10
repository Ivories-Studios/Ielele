using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class UnitAnimWrapper : MonoBehaviour
{
    [SerializeField] private UnitObject _unitObject;
    public float multiplier = 1;
    public float? stepAheadOverride;

    public void CastAbility(int index)
    {
        _unitObject.attacks[index].Cast(_unitObject, multiplier, stepAheadOverride);
    }
}

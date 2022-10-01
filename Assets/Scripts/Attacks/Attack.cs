using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] protected int _energyCost;
    [SerializeField] public int _power;
    [SerializeField] protected float _blockTime;
    [SerializeField] protected float _stepAhead;
    [SerializeField] protected float _knockback;
    [SerializeField] protected float _comboIncrease;
    [SerializeField] protected bool _instantiateSelf;
    [SerializeField] protected List<AudioClip> audioClips = new List<AudioClip>();
    [SerializeField] protected List<AudioClip> audioClips2 = new List<AudioClip>();
    [SerializeField, Tooltip("-1: never; 0: next frame; >0: after delay")] protected float _destroyAfter = -1;

    [HideInInspector] public UnitObject _caster;

    // Start is called before the first frame update
    public virtual void Start()
    {
        StartCoroutine(DestroyAfter(_destroyAfter));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Cast(UnitObject unit, float multiplier = 1f)
    {
        unit.IncreaseEnergy(-_energyCost);
    }

    public virtual bool CanCast(UnitObject unit)
    {
        return _energyCost <= unit.energy;
    }

    IEnumerator DestroyAfter(float delay)
    {
        if(delay < 0)
        {
            yield break;
        }
        else if(delay == 0)
        {
            yield return new WaitForEndOfFrame();
        }
        else
        {
            yield return new WaitForSeconds(delay);
        }
        Destroy(gameObject);
    }
}

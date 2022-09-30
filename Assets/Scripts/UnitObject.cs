using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitObject : MonoBehaviour
{
    [SerializeField] protected int _maxHealth;
    
    protected int _health;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void TakeDamage(int amount)
    {
        _health -= amount;
        if(_health < 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {

    }
}

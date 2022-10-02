using UnityEngine;

public class EnemyDeathScript : MonoBehaviour
{
    [SerializeField] UnitObject _unitObject;

    public void Die()
    {
        _unitObject.Die();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectScript : MonoBehaviour
{
    [Header("Counter")]
    [SerializeField] Vector3 _counterOffset;
    [SerializeField] GameObject _counterEffect;
    [SerializeField] float _counterTimer;

    public void Counter()
    {
        GameObject counter = Instantiate(_counterEffect, transform.position + _counterOffset, Quaternion.identity);

        StartCoroutine(DestroyAfterUse(counter, _counterTimer));
    }

    IEnumerator DestroyAfterUse(GameObject obj, float timer)
    {
        yield return new WaitForSeconds(timer);

        Destroy(obj);
    }
}

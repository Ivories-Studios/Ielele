using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectScript : MonoBehaviour
{
    public PlayerGraphicsTurnScript gameTurnScript;
    public UnitAnimWrapper unitAnimWrapper;

    [Header("Counter")]
    [SerializeField] Vector3 _counterOffset;
    [SerializeField] GameObject _counterEffect;
    [SerializeField] float _counterTimer;

    [Header("Punch 1")]
    [SerializeField] Vector3 _punchOffset;
    [SerializeField] GameObject _punchEffect;
    [SerializeField] float _punchTimer;

    [Header("Punch 2")]
    [SerializeField] Vector3 _punch2Offset;
    [SerializeField] GameObject _punch2Effect;
    [SerializeField] float _punch2Timer;

    [Header("Punch 3")]
    [SerializeField] Vector3 _punch3Offset;
    [SerializeField] GameObject _punch3Effect;
    [SerializeField] float _punch3Timer;

    [Header("Axe")]
    [SerializeField] Vector3 _axeOffset;
    [SerializeField] GameObject _axeEffect;
    [SerializeField] float _axeTimer;

    IEnumerator DestroyAfterUse(GameObject obj, float timer)
    {
        yield return new WaitForSeconds(timer);

        Destroy(obj);
    }

    public void Counter()
    {
        GameObject counter = Instantiate(_counterEffect, transform.position + _counterOffset, Quaternion.identity);

        StartCoroutine(DestroyAfterUse(counter, _counterTimer));
    }

    public void Punch1()
    {
        Vector3 mult = _punchOffset;
        if (!gameTurnScript.Right)
            mult = new Vector3(-1.0f * _punchOffset.x, _punchOffset.y, _punchOffset.z);

        GameObject punch = Instantiate(_punchEffect, transform.position + mult, Quaternion.identity);
        

        if (!gameTurnScript.Right)
            punch.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

        StartCoroutine(DestroyAfterUse(punch, _punchTimer));
    }

    public void Punch2()
    {
        Vector3 mult = _punch2Offset;
        if (!gameTurnScript.Right)
            mult = new Vector3(-1.0f * _punch2Offset.x, _punch2Offset.y, _punch2Offset.z);

        GameObject punch = Instantiate(_punch2Effect, transform.position + mult, Quaternion.identity);

        if (!gameTurnScript.Right)
            punch.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

        StartCoroutine(DestroyAfterUse(punch, _punch2Timer));
    }


    public void Punch3()
    {
        Vector3 mult = _punch3Offset;
        if (!gameTurnScript.Right)
            mult = new Vector3(-1.0f * _punch3Offset.x, _punch3Offset.y, _punch3Offset.z);

        GameObject punch = Instantiate(_punch3Effect, transform.position + mult, Quaternion.identity);

        if (!gameTurnScript.Right)
            punch.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

        StartCoroutine(DestroyAfterUse(punch, _punch3Timer));
    }

    public void Axe()
    {
        Vector3 mult = _axeOffset;
        if (!gameTurnScript.Right)
            mult = new Vector3(-1.0f * _axeOffset.x, _axeOffset.y, _axeOffset.z);

        GameObject punch = Instantiate(_axeEffect, transform.position + mult, Quaternion.identity);

        if (!gameTurnScript.Right)
            punch.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

        StartCoroutine(DestroyAfterUse(punch, _axeTimer));
    }

}

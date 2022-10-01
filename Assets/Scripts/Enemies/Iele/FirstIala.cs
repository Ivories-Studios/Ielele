
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstIala : UnitObject
{
    [SerializeField] Encounter encounter;
    [SerializeField] Transform[] projectileLocations;
    [SerializeField] Transform[] stunLocations;
    [SerializeField] Transform[] idleLocations;
    [SerializeField] Transform[] spawnLocations;
    [SerializeField] Transform safeLocation;

    [SerializeField] UnitObject[] basicEnemyPrefabs;
    [SerializeField] Slider healthBar;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        healthBar.maxValue = _maxHealth;
        healthBar.value = health;
    }

    // Update is called once per frame
    public override void Update()
    {
        if(health <= 0)
        {
            return;
        }
        base.Update();
        blockTime = 0;
        stunTime -= Time.deltaTime;
    }

    public IEnumerator StartProjectileAttack()
    {
        if (health <= 0)
        {
            encounter.done++;
            yield break;
        }
        Transform dest = projectileLocations[Random.Range(0, projectileLocations.Length)];
        for (int i = 0; i < Random.Range(3, 6); i++)
        {
            yield return StartCoroutine(MoveCoroutine(dest.position + new Vector3(0, 0, Random.Range(-3, 3))));
            yield return new WaitForSeconds(0.5f);
            CastAttack(0);
            yield return new WaitForSeconds(0.5f);
        }
        encounter.done++;
    }

    public IEnumerator StartSpawnEnemiesAttack()
    {
        if (health <= 0)
        {
            encounter.done++;
            yield break;
        }
        yield return StartCoroutine(MoveCoroutine(safeLocation.position));
        yield return new WaitForSeconds(1f);
        for(int i = 0; i < Random.Range(spawnLocations.Length / 2, spawnLocations.Length); i++)
        {
            Instantiate(basicEnemyPrefabs[Random.Range(0, basicEnemyPrefabs.Length)], spawnLocations[i].position, Quaternion.identity);
        }
        yield return new WaitForSeconds(5f);
        CastAttack(1);
        yield return new WaitForSeconds(10f);

        encounter.done++;
        yield return null;
    }

    public IEnumerator StartStunAttack()
    {
        if (health <= 0)
        {
            encounter.done++;

            yield break;
        }
        Transform dest = stunLocations[Random.Range(0, stunLocations.Length)];
        yield return StartCoroutine(MoveCoroutine(dest.position));
        yield return new WaitForSeconds(1f);
        CastAttack(1);
        yield return new WaitForSeconds(0.5f);
        encounter.done++;
    }

    public IEnumerator StartIdle()
    {
        if (health <= 0)
        {
            yield break;
        }
        Transform dest = idleLocations[Random.Range(0, idleLocations.Length)];
        yield return StartCoroutine(MoveCoroutine(dest.position + new Vector3(0, 0, Random.Range(-3, 3))));
    }

    IEnumerator MoveCoroutine(Vector3 dest)
    {
        if (health <= 0)
        {
            yield break;
        }
        float time = 0.9f;
        Vector3 startingPos = rb.position;
        Vector3 finalPos = dest;
        float elapsedTime = 0;

        while (elapsedTime < time)
        {
            rb.MovePosition(Vector3.Lerp(startingPos, finalPos, elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        rb.MovePosition(dest);
    }

    public override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);
        healthBar.value = health;
    }
}

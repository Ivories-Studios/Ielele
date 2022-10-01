using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAiManager : MonoBehaviour
{

    public List<Enemy> enemies = new List<Enemy>();

    public static int NumberOfEnemiesThatShouldAttackPlayer = 3;
    public int NumberOfEnemiesAttackingPlayer = 0;

    public List<Transform> enemyIdleLocations;
    public List<Transform> enemyFleeLocations;

    public static bool debug = false;

    public void StartFight()
    {
        fighting = true;
        allFleeing = false;
    }

    public void EndFight()
    {
        fighting = false;
        allFleeing = true;
        timer = 0;
    }

    public GameObject CreateEnemy(GameObject prefab, Vector3 position) 
    {
        if (fighting)
        {
            GameObject obj = Instantiate(prefab);
            obj.transform.position = position;
            enemies.Add(obj.GetComponent<Enemy>());
            return obj;

        }
        else
        {
            Debug.LogError("Trying to create enemy while not in a fight");
            return null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    static float timer = 0;
    public static bool fighting;
    static bool allFleeing = false;
    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            RefreshAIActions();
            timer = Random.Range(3.5f, 5.5f);
        }


        if (debug)
        {
            foreach (Enemy e in enemies)
            {
                Debug.DrawLine(e.transform.position, e.movementTarget);
            }
        }

        if (!fighting)
        {
            if (!allFleeing)
            {
                allFleeing = true;
                foreach(Enemy enemy in enemies)
                {
                    enemy.state = AIState.Flee;
                }
            }
        }

    }

    void RefreshAIActions()
    {
        if (fighting)
        {
            int numberOfAttackingEnemies = 0;
            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                if (enemies[i].Health <= 0)
                {
                    enemies.Remove(enemies[i]);
                }
            }

            foreach (Enemy enemy in enemies)
            {

                if (enemy.state == AIState.Attacking)
                {
                    numberOfAttackingEnemies++;
                }
            }
            NumberOfEnemiesAttackingPlayer = numberOfAttackingEnemies;
            while (numberOfAttackingEnemies < NumberOfEnemiesThatShouldAttackPlayer && numberOfAttackingEnemies < enemies.Count)
            {
                numberOfAttackingEnemies++;
                Enemy enemy = enemies[Random.Range(0, enemies.Count)];
                enemy.state = AIState.Attacking;
                enemy.targetDisplacement = Random.onUnitSphere;
                enemy.targetDisplacement.y /= 1.41f;
                enemy.targetDisplacement = enemy.targetDisplacement.normalized;
                enemy.targetDisplacement*= enemy.range;
            }

            foreach (Enemy enemy in enemies)
            {
                if (enemy.state == AIState.Flee)
                {
                    enemy.movementTarget = GetClosestPosition(enemy.transform.position, enemyFleeLocations).position;
                }

                if (enemy.state == AIState.Idle)
                {
                    if (Random.Range(1, 100) < 50)
                    {
                        enemy.movementTarget = GetClosestPosition(enemy.transform.position, enemyIdleLocations).position;
                        Vector3 randSphere = Random.onUnitSphere;
                        randSphere = randSphere.normalized;
                        enemy.targetDisplacement = new Vector3(randSphere.x, 0, randSphere.z);
                        enemy.state = AIState.RoamingAround;
                    }
                }
                if (enemy.state == AIState.RoamingAround)
                {
                    if (Random.Range(1, 100) < 75)
                    {
                        enemy.movementTarget = GetClosestPosition(enemy.transform.position, enemyIdleLocations).position;
                        Vector3 randSphere = Random.onUnitSphere;
                        randSphere = randSphere.normalized;
                        enemy.targetDisplacement = new Vector3(randSphere.x, 0, randSphere.z);
                    }
                }

            }

        }
        else
        {
            foreach (Enemy enemy in enemies)
            {
                enemy.state = AIState.Flee;
                enemy.movementTarget = GetClosestPosition(enemy.transform.position, enemyFleeLocations).position;
            }
        }
    }

    Transform GetClosestPosition(Vector3 from, List<Transform> transforms)
    {
        float dist;
        Transform best;
        best = transforms[0];
        dist = Vector3.Distance(from, best.position);

        foreach(Transform t in transforms)
        {
            float currentDist = Vector3.Distance(from, t.position);
            if (currentDist < dist)
            {
                dist = currentDist;
                best = t;
            }
        }

        return best;
    }


}

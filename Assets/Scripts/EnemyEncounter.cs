using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EnemyEncounter : Encounter
{
    [SerializeField] List<GameObject> enemyGroups = new List<GameObject>();
    [SerializeField] List<int> enemyCount = new List<int>();
    [SerializeField] List<float> enemyDelay = new List<float>();
    [SerializeField] List<Transform> spawnPoints = new List<Transform>();
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] Collider[] marginCollider;

    EnemyAiManager aiManager;

    private void Awake()
    {
        aiManager = GetComponent<EnemyAiManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    IEnumerator SpawnWaves()
    {
        aiManager.StartFight();
        for (int i = 0; i < enemyGroups.Count; i++)
        {
            yield return new WaitForSeconds(enemyDelay[i]);
            for (int j = 0; j < enemyCount[i]; j++)
            {
                aiManager.CreateEnemy(enemyGroups[i], spawnPoints[Random.Range(0, spawnPoints.Count)].position);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void StartFight()
    {
        virtualCamera.Priority = 20;
        foreach(Collider col in marginCollider)
        {
            col.gameObject.SetActive(true);
        }
        StartCoroutine(SpawnWaves());
    }

    public void StopFight()
    {
        virtualCamera.Priority = 0;
        foreach (Collider col in marginCollider)
        {
            col.gameObject.SetActive(false);
        }
        aiManager.EndFight();
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.CompareTag("Player"))
        {
            StartFight();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        /*if (other.transform.parent.CompareTag("Player"))
        {
            StopFight();
        }*/
    }
}

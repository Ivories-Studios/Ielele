using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EnemyEncounter : Encounter
{
    [SerializeField] List<GameObject> enemyGroups = new List<GameObject>();
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] Collider[] marginCollider;

    // Start is called before the first frame update
    void Start()
    {
        
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
    }

    public void StopFight()
    {
        virtualCamera.Priority = 0;
        foreach (Collider col in marginCollider)
        {
            col.gameObject.SetActive(false);
        }
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
        if (other.transform.parent.CompareTag("Player"))
        {
            StopFight();
        }
    }
}

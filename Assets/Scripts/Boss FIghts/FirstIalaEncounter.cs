using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FirstIalaEncounter : Encounter
{
    [SerializeField] FirstIala iala;
    Phases currentPhase = Phases.Idle;
    [SerializeField] GameObject healthBars;
    [SerializeField] CinemachineVirtualCamera virtualCamera;

    // Update is called once per frame
    void Update()
    {
        if (iala == null)
        {
            MenuManager.Instance.ShowVictoryScreen();
            virtualCamera.Priority = 0;
            Destroy(gameObject);
        }
        timer += Time.deltaTime;
        switch (currentPhase)
        {
            case Phases.Idle:
                if (timer > 1)
                {
                    int random = Random.Range(0, 2);
                    if (random == 0)
                    {
                        switch (Random.Range(0, 10))
                        {
                            case 0:
                            case 1:
                                if (iala != null)
                                {
                                    currentPhase = Phases.SpawnEnemies;
                                    done = 0;
                                    StartCoroutine(iala.StartSpawnEnemiesAttack());
                                }
                                break;
                            case 2:
                            case 3:
                            case 4:
                            case 5:
                            case 6:
                                if (iala != null)
                                {
                                    currentPhase = Phases.Projectiles;
                                    done = 0;
                                    StartCoroutine(iala.StartProjectileAttack());
                                }
                                break;
                            case 7:
                            case 8:
                            case 9:
                                if (iala != null)
                                {
                                    currentPhase = Phases.Stun;
                                    done = 0;
                                    StartCoroutine(iala.StartStunAttack());
                                }
                                break;
                        }
                    }
                    else if(random == 1)
                    {
                        StartCoroutine(iala.StartIdle());
                    }
                    timer = 0;
                }
                break;
            case Phases.SpawnEnemies:
                if(done == 1)
                {
                    currentPhase = Phases.Idle;
                }
                break;
            case Phases.Projectiles:
                if(done == 1)
                {
                    currentPhase = Phases.Idle;
                }
                break;
            case Phases.Stun:
                if (done == 1)
                {
                    currentPhase = Phases.Idle;
                }
                break;
        }
    }

    public override void StartFight()
    {
        gameObject.SetActive(true);
        healthBars.SetActive(true);
        virtualCamera.Priority = 20;
    }
}

enum Phases
{
    Idle,
    SpawnEnemies,
    Projectiles,
    Stun
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ThirdIalaEncounter : Encounter
{
    [SerializeField] FirstIala firstIala;
    [SerializeField] SecondIala secondIala;
    [SerializeField] ThirdIala thirdIala;
    Phases3 phases;
    [SerializeField] GameObject healthBars;
    [SerializeField] CinemachineVirtualCamera virtualCamera;

    public override void StartFight()
    {
        gameObject.SetActive(true);
        healthBars.SetActive(true);
        virtualCamera.Priority = 20;
    }

    // Update is called once per frame
    void Update()
    {
        if (firstIala == null && secondIala == null && thirdIala == null)
        {
            MenuManager.Instance.ShowVictoryScreen();
            virtualCamera.Priority = 0;
            Destroy(gameObject);
        }
        timer += Time.deltaTime;
        switch (phases)
        {
            case Phases3.Idle:
                if(timer > 1)
                {
                    int random = Random.Range(0, 4);
                    if(random == 0)
                    {
                        phases = Phases3.DoinStuff;
                        done = 0;
                        switch(Random.Range(0, 10))
                        {
                            case 0:
                            case 7:
                                if(firstIala != null)
                                {
                                    StartCoroutine(firstIala.StartProjectileAttack());
                                }
                                else
                                {
                                    done++;
                                }
                                if(secondIala != null)
                                {
                                    StartCoroutine(secondIala.StartCharmAttack());
                                }
                                else
                                {
                                    done++;
                                }
                                break;
                            case 1:
                            case 8:
                            case 3:
                                if (thirdIala != null)
                                {
                                    StartCoroutine(thirdIala.StartOlogAttack());
                                }
                                else
                                {
                                    done++;
                                }
                                if (secondIala != null)
                                {
                                    StartCoroutine(secondIala.StartCharmAttack());
                                }
                                else
                                {
                                    done++;
                                }
                                break;
                            case 2:
                            case 9:
                                if (thirdIala != null)
                                {
                                    StartCoroutine(thirdIala.StartOlogAttack());
                                }
                                else
                                {
                                    done++;
                                }
                                if (firstIala != null)
                                {
                                    StartCoroutine(firstIala.StartProjectileAttack());
                                }
                                else
                                {
                                    done++;
                                }
                                break;
                            case 4:
                            case 10:
                                if (secondIala != null)
                                {
                                    StartCoroutine(secondIala.StartSpawnEnemiesAttack());
                                }
                                else
                                {
                                    done++;
                                }
                                if (firstIala != null)
                                {
                                    StartCoroutine(firstIala.StartProjectileAttack());
                                }
                                else
                                {
                                    done++;
                                }
                                break;
                            case 5:
                                if (firstIala != null)
                                {
                                    StartCoroutine(firstIala.StartSpawnEnemiesAttack());
                                }
                                else
                                {
                                    done++;
                                }
                                done++;
                                break;
                            case 6:
                                if (thirdIala != null)
                                {
                                    StartCoroutine(thirdIala.StartSpawnEnemiesAttack());
                                }
                                else
                                {
                                    done++;
                                }
                                done++;
                                break;
                        }
                    }
                    if (random == 1)
                    {
                        StartCoroutine(firstIala.StartIdle());
                        if (Random.Range(0, 2) == 0)
                        {
                            StartCoroutine(thirdIala.StartIdle());
                        }
                        if (Random.Range(0, 2) == 0)
                        {
                            StartCoroutine(secondIala.StartIdle());
                        }
                    }
                    timer = 0;
                }
                break;
            case Phases3.DoinStuff:
                if (done == 2)
                {
                    phases = Phases3.Idle;
                }
                break;
        }
    }
}

enum Phases3
{
    Idle,
    DoinStuff
}
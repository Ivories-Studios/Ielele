using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SecondIalaEncounter : Encounter
{
    [SerializeField] SecondIala iala2;
    [SerializeField] ThirdIala iala3;
    Phases2 currentPhase;
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
        if(iala2 == null && iala3 == null)
        {
            MenuManager.Instance.ShowVictoryScreen();
            virtualCamera.Priority = 0;
            Destroy(gameObject);
        }
        timer += Time.deltaTime;
        switch (currentPhase)
        {
            case Phases2.Idle:
                if (timer > 1)
                {
                    int random = Random.Range(0, 4);
                    if (random == 0)
                    {
                        currentPhase = Phases2.DoinStuff;
                        done = 0;
                        switch (Random.Range(0, 10))
                        {
                            case 0:
                                if (iala2 != null)
                                {
                                    StartCoroutine(iala2.StartSpawnEnemiesAttack());
                                }
                                else
                                {
                                    done++;
                                }
                                if (iala3 != null)
                                {
                                    StartCoroutine(iala3.StartOlogAttack());
                                }
                                else
                                {
                                    done++;
                                }
                                break;
                            case 1:
                            case 6:
                            case 10:
                                if (iala2 != null)
                                {
                                    StartCoroutine(iala2.StartCharmAttack());
                                }
                                else
                                {
                                    done++;
                                }
                                if (iala3 != null)
                                {
                                    StartCoroutine(iala3.StartOlogAttack());
                                }
                                else
                                {
                                    done++;
                                }
                                break;
                            case 2:
                                if (iala2 != null)
                                {
                                    StartCoroutine(iala2.StartCharmAttack());
                                }
                                else
                                {
                                    done++;
                                }
                                if (iala3 != null)
                                {
                                    StartCoroutine(iala3.StartSpawnEnemiesAttack());
                                }
                                else
                                {
                                    done++;
                                }
                                break;
                            case 3:
                            case 7:
                            case 8:
                                if (iala2 != null)
                                {
                                    StartCoroutine(iala2.StartIdle());
                                }
                                done++;
                                if (iala3 != null)
                                {
                                    StartCoroutine(iala3.StartOlogAttack());
                                }
                                else
                                {
                                    done++;
                                    timer = 1;
                                }
                                break;
                            case 4:
                            case 9:
                                if (iala2 != null)
                                {
                                    StartCoroutine(iala2.StartIdle());
                                }
                                done++;
                                if (iala3 != null)
                                {
                                    StartCoroutine(iala3.StartOlogAttack());
                                }
                                else
                                {
                                    done++;
                                    timer = 1;
                                }
                                break;
                            case 5:
                                if (iala2 != null)
                                {
                                    StartCoroutine(iala2.StartSpawnEnemiesAttack());
                                }
                                else
                                {
                                    done++;
                                    timer = 1;
                                }
                                done++;
                                break;
                        }
                    }
                    else if (random == 1)
                    {
                        StartCoroutine(iala2.StartIdle());
                        StartCoroutine(iala3.StartIdle());
                    }
                    timer = 0;
                }
                break;
            case Phases2.DoinStuff:
                if (done == 2)
                {
                    currentPhase = Phases2.Idle;
                }
                break;
        }
    }
}

enum Phases2
{
    Idle,
    DoinStuff
}
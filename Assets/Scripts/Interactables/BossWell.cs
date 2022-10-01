using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWell : MonoBehaviour
{
    [SerializeField] Encounter bossEncounter;
    bool onlyOnce;

    public void Interact(PlayerObject player)
    {
        bossEncounter.StartFight();
    }
}

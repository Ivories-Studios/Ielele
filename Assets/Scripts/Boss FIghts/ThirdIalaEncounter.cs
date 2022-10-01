using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdIalaEncounter : Encounter
{
    [SerializeField] FirstIala firstIala;
    [SerializeField] SecondIala secondIala;
    [SerializeField] ThirdIala thirdIala;

    public override void StartFight()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

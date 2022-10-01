using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AITest : MonoBehaviour
{

    public TextMeshProUGUI text;
    public GameObject enemyPrefab;
    // Start is called before the first frame update
    void Start()
    {
        EnemyAiManager.debug = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CreateEnemy();
        }
        text.text = "Enemies: " + EnemyAiManager.enemies.Count+"\n " +
            "Enemies attacking player: " + EnemyAiManager.NumberOfEnemiesAttackingPlayer +"\n" +
            "In fight: " + EnemyAiManager.fighting;
    }
    bool inFight = false;
    public void ChangeFight()
    {
        if (!inFight)
        {
            EnemyAiManager.StartFight();
            inFight = true;
        }
        else
        {
            EnemyAiManager.EndFight();
            inFight = false;
        }
    }

    public void CreateEnemy()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.y = 0;
        pos.z += 11;
        GameObject obj = EnemyAiManager.CreateEnemy(enemyPrefab, pos);
        obj.transform.position = pos;
    }

}

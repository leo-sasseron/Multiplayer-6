using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemySpawner : NetworkBehaviour {
    public GameObject enemyPrefab;
    public int numberOfEnemies = 4;

    public override void OnStartServer()
    {
        for (int i = 0; i < numberOfEnemies; ++i)
        {
            var pos = new Vector3(UnityEngine.Random.Range(-8.0F, 8.0F), 0, UnityEngine.Random.Range(-8.0F, 8.0F));

            var rot = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0);

            var enemy = Instantiate(enemyPrefab, pos, rot) as GameObject;

            NetworkServer.Spawn(enemy);
        }
    }
}

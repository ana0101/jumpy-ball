using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawnerScript : MonoBehaviour
{
    [SerializeField]
    private GameObject obstacle;

    private LogicScript logicScript;

    private float timer = 0;
    private float spawnRate;
    private float lowSpawnRate;
    private float highSpawnRate;
    private bool ballIsAlive = true;

    // Start is called before the first frame update
    private void Start()
    {
        logicScript = GameObject.FindGameObjectWithTag("logic").GetComponent<LogicScript>();
        SpawnObstacle();
    }

    // Update is called once per frame
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnRate)
        {
            timer = 0;
            SpawnObstacle();
        }
    }

    private void SpawnObstacle()
    {
        if (ballIsAlive)
        {
            Instantiate(obstacle, new Vector3(transform.position.x, transform.position.y, 0), transform.rotation);
            lowSpawnRate = logicScript.GetLowSpawnRate();
            highSpawnRate = logicScript.GetHighSpawnRate();
            spawnRate = Random.Range(lowSpawnRate, highSpawnRate);
        }
    }

    public void SetBallIsAlive(bool alive)
    {
        ballIsAlive = alive;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ObstacleScript : MonoBehaviour
{
    private float speed = 4;
    private LogicScript logicScript;

    private void Start()
    {
        logicScript = GameObject.FindGameObjectWithTag("logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    private void Update()
    {
        speed = logicScript.GetObstacleSpeed();

        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (transform.position.x < -9.5)
        {
            Destroy(gameObject);
        }
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
}

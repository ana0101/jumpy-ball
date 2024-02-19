using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rbBall;
    [SerializeField]
    private AudioSource jumpSFX;

    private LogicScript logicScript;
    private bool isOnGround = true;
    private bool isAlive = true;

    // Start is called before the first frame update
    private void Start()
    {
        logicScript = GameObject.FindGameObjectWithTag("logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && isAlive)
        {
            Jump();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("ground"))
        {
            isOnGround = true;
        }
        else
        {
            isOnGround = false;
        }

        if (collision.collider.CompareTag("obstacle"))
        {
            isAlive = false;
            logicScript.GameOver();
        }
    }

    private void Jump()
    {
        float jumpAmount = 8;
        rbBall.AddForce(Vector2.up * jumpAmount, ForceMode2D.Impulse);
        isOnGround = false;
        AudioScript.Instance.PlaySFX(jumpSFX);
    }
}

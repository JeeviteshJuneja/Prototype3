using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 20.0f;
    private PlayerController playerControllerScript;
    private float leftBound = -15f;
    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.gameStart && !playerControllerScript.gameOver)
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed * playerControllerScript.dash);
        }
        if(transform.position.x< leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}

    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Transform transformPlayer;
    public bool stickToPlayer;
    private Transform playerBallPosition;
    private Player scriptPlayer;
    private Vector2 previousLocation;
    private float speed;


    // Start is called before the first frame update
    void Start()
    {
        playerBallPosition = transformPlayer.Find("Geometry").Find("BallLocation");
        scriptPlayer = transformPlayer.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!stickToPlayer)
        {
            float distanceToPlayer = Vector3.Distance(transformPlayer.position, transform.position);
            if (distanceToPlayer < 0.5)
            {
                stickToPlayer = true;
                scriptPlayer.BallAttachedToPlayer = this;
            }
        }
        else
        {
            Vector2 currentLocation = new Vector2(transform.position.x, transform.position.z);
            speed = Vector2.Distance(currentLocation, previousLocation) / Time.deltaTime;
            transform.position = playerBallPosition.position;
            transform.Rotate(new Vector3(transformPlayer.right.x, 0, transformPlayer.right.z), speed, Space.World);
            previousLocation = currentLocation;
        }

        if (transform.position.y < -15)
        {
            transform.position = new Vector3(-2.5f, -13.327f, 22.4f);
            Rigidbody rigidbody = GetComponent<Rigidbody>();
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }
    }

}
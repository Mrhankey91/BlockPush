using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private GameController gameController;
    private IngameMenu ingameMenu;

    public float speed = 1;

    private float movementX;
    private float movementY;

    private CharacterController characterController;

    private Vector3 playerVelocity;
    private bool groundedPlayer;

    private float moveSpeed = 5.0f;
    private float rotationSpeed = 90f;
    private Vector3 rotation;

    private float jumpHeight = 1.0f;
    private float gravityValue = 9.81f; 
    private float forceMagnitude = 1200f;

    void Awake()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        gameController.onStart += OnStart;
        characterController = GetComponent<CharacterController>();
    }

    private void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void OnMenu()
    {
        gameController.ToggleIngameMenu();
    }

    private void Update()
    {
        //rotation = new Vector3(0, movementX * rotationSpeed * Time.deltaTime, 0);

        Vector3 move = new Vector3(movementX * moveSpeed * Time.deltaTime, 0, (1f + (movementY + 0.5f)) * moveSpeed * Time.deltaTime);
        //move = transform.TransformDirection(move);

        if (!characterController.isGrounded)
        {
            move.y -= gravityValue * Time.deltaTime;
        }

        characterController.Move(move);
        //transform.Rotate(this.rotation);

        speed = characterController.velocity.magnitude * -movementY;

        if (transform.position.y < -3f)
            gameController.GameOver();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Obstacle obstacle = hit.gameObject.GetComponent<Obstacle>();

        if (obstacle)
        {
            if (obstacle.type == Obstacle.ObstacleType.Hazardous)
                gameController.GameOver();

            Rigidbody rigidBody = hit.gameObject.GetComponent<Rigidbody>();
            if (obstacle.isPushable)
            {
                if (rigidBody != null)
                {
                    Vector3 forceDirection = hit.gameObject.transform.position - transform.position;
                    forceDirection.y = 0;
                    forceDirection.Normalize();

                    rigidBody.AddForceAtPosition(forceDirection * forceMagnitude * Time.deltaTime, transform.position, ForceMode.Impulse);
                }
            }
            else
            {
                rigidBody.velocity = Vector3.zero;
            }
        }
    }

    private void OnStart(int level)
    {
        characterController.enabled = false;
        transform.position = Vector3.zero;
        characterController.enabled = true;
    }
}

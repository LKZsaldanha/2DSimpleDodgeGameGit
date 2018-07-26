using UnityEngine;
using UnityEngine.UI;

public class PlayerFixedMovement : MonoBehaviour
{

    public float speed = 10f;
    public int numberOfLanes = 5;
    public float laneWidth = 2f;


    private float[] laneX;
    private int desiredLaneIndex = 0;


    private Rigidbody2D rb;
    private GameManager gm;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gm = FindObjectOfType<GameManager>();

        laneX = new float[numberOfLanes];
        int middleLaneIndex;

        if (numberOfLanes % 2 == 0)
        {
            middleLaneIndex = numberOfLanes / 2;
        }
        else
        {
            middleLaneIndex = (numberOfLanes + 1) / 2;
        }

        for (int i = 0; i < laneX.Length; i++)
        {
            laneX[i] = (i + 1 - middleLaneIndex) * laneWidth;
        }
        desiredLaneIndex = middleLaneIndex -1;

    }
  

    private void FixedUpdate()
    {
        if (gm.allowGameplayInputs)
        {
            MoveToLane();
        }
    }

    private void MoveToLane()
    {
        Vector2 targetPosition = rb.position;
        targetPosition = laneX[desiredLaneIndex] * Vector2.right;

        Vector2 moveVector = Vector2.zero;
        moveVector.x = (targetPosition - rb.position).x * speed;

        rb.MovePosition(rb.position + moveVector * Time.deltaTime);
    }

    private void ChangeLane(bool goingRight)
    {
        desiredLaneIndex += (goingRight) ? 1 : -1;
        desiredLaneIndex = Mathf.Clamp(desiredLaneIndex, 0, numberOfLanes - 1);
    }

    public void PlayerGoRight()
    {
        if (gm.allowGameplayInputs)
        {
            if(desiredLaneIndex < numberOfLanes - 1)
            {
                desiredLaneIndex += 1;
            }
        }
    }

    public void PlayerGoLeft()
    {
        if (gm.allowGameplayInputs)
        {
            if (desiredLaneIndex > 0)
            {
                desiredLaneIndex += -1;
            }
        }
    }
}

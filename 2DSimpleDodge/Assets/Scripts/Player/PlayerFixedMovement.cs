using UnityEngine;

public class PlayerFixedMovement : MonoBehaviour
{

    public float speed = 10f;
    public int numberOfLanes = 5;
    public float laneWidth = 2f;


    private float[] laneX;
    private int desiredlaneIndex = 0;

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
        desiredlaneIndex = middleLaneIndex;
    }

    private void Update()
    {
        if (gm.allowGameplayInputs)
        {
            if (!gm.mobileVersion)
            {
                GetInput();
            }
            MoveToLane();
        }
    }

    private void GetInput()
    {
        if (Input.GetButtonDown("Left"))
        {
            ChangeLane(false);
        }
        else if (Input.GetButtonDown("Right"))
        {
            ChangeLane(true);
        }
    }

    private void MoveToLane()
    {
        Vector2 targetPosition = rb.position;
        targetPosition = laneX[desiredlaneIndex] * Vector2.right;

        Vector2 moveVector = Vector2.zero;
        moveVector.x = (targetPosition - rb.position).x * speed;

        rb.MovePosition(rb.position + moveVector * Time.deltaTime);
    }

    private void ChangeLane(bool goingRight)
    {
        desiredlaneIndex += (goingRight) ? 1 : -1;
        desiredlaneIndex = Mathf.Clamp(desiredlaneIndex, 0, numberOfLanes - 1);
    }

    public void PlayerGoRight()
    {
        if (gm.allowGameplayInputs)
        {
            desiredlaneIndex += 1;
        }
    }
    public void PlayerGoLeft()
    {
        if (gm.allowGameplayInputs)
        {
            desiredlaneIndex += -1;
        }
    }

}

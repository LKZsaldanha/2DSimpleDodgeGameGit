using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour {

    public float speed = 10f;
    public float widthLimit = 5f;

    public bool fixedMovement = false;
    public float firstLaneX = 5;
    public float lastLaneX = 5;
    public float laneWidth = 2f;

    public bool allowInput = true;


    private bool mobileControl = false;

    private Rigidbody2D rb;

    private GameManager gm;

    private float inputUIValue = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gm = FindObjectOfType<GameManager>();
        mobileControl = gm.mobileVersion;
    }

    private void FixedUpdate()
    {
        float xValue = 0f;
        if (gm.allowGameplayInputs)
        {
            if (mobileControl)
            {
                //xValue = GetMobileTouchInput();
                xValue = inputUIValue;
            }
            else
            {
                xValue = GetPCInput();
            }

            if (fixedMovement)
            {
                Vector2 newFixedPosition = new Vector2(laneWidth * xValue, 0);
                Vector2 fixedMove = rb.position + newFixedPosition;
                if (fixedMove.x >= firstLaneX && fixedMove.x <= lastLaneX)
                {
                    rb.MovePosition(rb.position + newFixedPosition);
                }
                inputUIValue = 0;
            }
            else
            {
                xValue *= Time.fixedDeltaTime * speed;

                Vector2 newPosition = rb.position + Vector2.right * xValue;

                newPosition.x = Mathf.Clamp(newPosition.x, -widthLimit, widthLimit);

                rb.MovePosition(newPosition);
            }
        }
    }

    private float GetMobileTouchInput()
    {
        float inputValue = 0f;

        int i = 0;
        while (i < Input.touchCount)
        {
            if (Input.GetTouch(i).position.x > Screen.width / 2)
            {
                inputValue = 1f;
            }
            if (Input.GetTouch(i).position.x < Screen.width / 2)
            {
                inputValue = -1f;
            }
            ++i;
        }
    #if UNITY_EDITOR
        inputValue = Input.GetAxis("Horizontal");
    #endif

        return inputValue;
    }

    private float GetPCInput()
    {
        float inputValue = 0f;
        if (fixedMovement)
        {
            if (Input.GetButtonDown("Left"))
            {
                inputValue = -1f;
            }
            else if (Input.GetButtonDown("Right"))
            {
                inputValue = 1f;
            }
        }
        else
        {
            inputValue = Input.GetAxis("Horizontal");
        }


        return inputValue;
    }

    public void PlayerGoRight()
    {
        if (gm.allowGameplayInputs)
        {
            inputUIValue = 1f;
        }
    }

    public void PlayerGoLeft()
    {
        if (gm.allowGameplayInputs)
        {
            inputUIValue = -1f;
        }
    }
}

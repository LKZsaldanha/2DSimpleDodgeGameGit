using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour {

    private const float SWIPEDEADZONE = 100;


    private PlayerFixedMovement pMove;
    private GameManager gm;
    private Camera cam;
    private Rigidbody2D rb;

    private Vector2 swipeDelta;
    private Vector2 startTouch;


    private void Start () {
        pMove = GetComponent<PlayerFixedMovement>();
        rb = GetComponent<Rigidbody2D>();
        gm = FindObjectOfType<GameManager>();
        cam = Camera.main;
    }
	
	private void Update () {
        if (gm.allowGameplayInputs)
        {
            if (gm.mobileVersion)
            {
                if (gm.mobileInputType == MobileInputType.TAP)
                {
                    GetTapMobileInput();
                }
                if (gm.mobileInputType == MobileInputType.SWIPE)
                {
                    GetSwipeMobileInput();
                }
            }
            else
            {
                GetKeyboardInput();
            }
        }

    }
    private void GetSwipeMobileInput()
    {
        if (Input.touches.Length!=0)
        {
            if(Input.touches[0].phase == TouchPhase.Began)
            {
                startTouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                startTouch = swipeDelta = Vector2.zero;
            }
        }

        swipeDelta = Vector2.zero;

        if(startTouch != Vector2.zero)
        {
            if (Input.touches.Length !=0)
            {
                swipeDelta = Input.touches[0].position - startTouch;
            }
        }

        if(swipeDelta.magnitude > SWIPEDEADZONE)
        {
            if (swipeDelta.x > 0)
            {
                pMove.PlayerGoRight();
            }
            else if(swipeDelta.x < 0)
            {
                pMove.PlayerGoLeft();
            }

            startTouch = swipeDelta = Vector2.zero;
        }
    }

    private void GetTapMobileInput()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);
            if (touch.phase == TouchPhase.Began)
            {
                float touchPoint = cam.ScreenToWorldPoint(touch.position).x;
                //if (touch.position.x > Screen.width / 2)
                if (touchPoint > rb.position.x)
                {
                    pMove.PlayerGoRight();
                }
                //if (touch.position.x < Screen.width / 2)
                if (touchPoint < rb.position.x)
                {
                    pMove.PlayerGoLeft();
                }
            }
        }
    }

    private void GetKeyboardInput()
    {
        if (Input.GetButtonDown("Left"))
        {
            pMove.PlayerGoLeft();
        }
        else if (Input.GetButtonDown("Right"))
        {
            pMove.PlayerGoRight();
        }
    }
}

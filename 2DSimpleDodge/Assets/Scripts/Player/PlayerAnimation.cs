using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    public float stoppingAnimDistance = .1f;

    private Rigidbody2D rb;

    private Vector2 lastPosition = Vector2.zero;
    private float deltaPosition = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lastPosition = rb.position;
    }


    private void Update()
    {
        deltaPosition = (lastPosition - rb.position).magnitude;

        if (deltaPosition < stoppingAnimDistance)
        {
            //Idle
        }
        else
        {
            //Moving
            if (lastPosition.x - rb.position.x < 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (lastPosition.x - rb.position.x > 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }            
        }


        lastPosition = rb.position;

    }

    public void Hit(bool harmful, bool stillAlive)
    {
        if (stillAlive)
        {
            if (harmful)
            {
                //Get hit
            }
            else
            {
                //Collect
            }
        }
        else
        {
            //Die
        }
    }

}

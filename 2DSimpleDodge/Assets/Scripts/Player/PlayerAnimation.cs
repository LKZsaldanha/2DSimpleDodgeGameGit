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
            GetComponent<Animator>().SetBool("Walking", false);
        }
        else
        {
            GetComponent<Animator>().SetBool("Walking", true);

            //Moving
            if (lastPosition.x - rb.position.x < 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (lastPosition.x - rb.position.x > 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
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
                GetComponent<Animator>().SetTrigger("Died");
            }
            else
            {
                //Collect
                GetComponent<Animator>().SetTrigger("Collect");
            }
        }
        else
        {
            //Die
            GetComponent<Animator>().SetTrigger("Died");
        }
    }

}

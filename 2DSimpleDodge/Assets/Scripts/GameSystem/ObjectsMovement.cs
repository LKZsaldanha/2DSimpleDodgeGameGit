using UnityEngine;

public class ObjectsMovement : MonoBehaviour {


    private Rigidbody2D rb;    
    private GameManager gm;

    private float speed = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gm = FindObjectOfType<GameManager>();
        speed = gm.gameSpeed;
    }

    public void FixedUpdate()
    {        
        Vector2 gravityVector = Vector2.zero;
        gravityVector = speed * Vector2.up * -1;

        rb.MovePosition(rb.position + gravityVector * Time.fixedDeltaTime);
    }
}

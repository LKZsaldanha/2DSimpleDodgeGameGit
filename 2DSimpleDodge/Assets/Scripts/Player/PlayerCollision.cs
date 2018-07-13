using UnityEngine;

public class PlayerCollision : MonoBehaviour {
    public int life = 1;

    public string obstacleTag = "Harmful";
    public Transform hitEffect;
    public int obstaclePointReduction = 0;

    public string collectableTag = "Collectable";
    public Transform collectEffect;
    public int collectablePointValue = 1;

    public Score scoreObject;

    private GameManager gm;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(obstacleTag))
        {
            Vector3 hitPosition = new Vector3(collision.contacts[0].point.x, collision.contacts[0].point.y, 0);
            CreateHitEffect(hitPosition);
            scoreObject.RemovePoints(obstaclePointReduction);

            Destroy(collision.gameObject);

            life--;
            if (life > 0)
            {

            }
            else
            {
                Die();
            }

        }
        else if (collision.gameObject.CompareTag(collectableTag))
        {
            Vector3 collectPosition = new Vector3(collision.contacts[0].point.x, collision.contacts[0].point.y, 0);
            CreateCollectEffect(collectPosition);
            scoreObject.AddPoints(collectablePointValue);

            Destroy(collision.gameObject);
        }
    }

    private void CreateHitEffect(Vector3 _position)
    {        
        Instantiate(hitEffect, _position, Quaternion.identity);
    }

    private void CreateCollectEffect(Vector3 _position)
    {
        Instantiate(collectEffect, _position, Quaternion.identity);
    }

    private void Die()
    {
        gm.EndGame();
    }
}

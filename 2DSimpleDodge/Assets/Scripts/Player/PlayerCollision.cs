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
    private PlayerAnimation pAnim;

    public float posY;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        pAnim = FindObjectOfType<PlayerAnimation>();
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
                pAnim.Hit(true, true);
            }
            else
            {
                pAnim.Hit(true, false);

                Die();
            }

        }
        else if (collision.gameObject.CompareTag(collectableTag))
        {
            Vector3 collectPosition = new Vector3(collision.contacts[0].point.x, collision.contacts[0].point.y, 0);
            CreateCollectEffect(collectPosition);

            pAnim.Hit(false, true);

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
        Instantiate(collectEffect, new Vector3(_position.x, _position.y + posY, _position.z), Quaternion.identity);
    }

    private void Die()
    {
        gm.EndGame();
    }
}

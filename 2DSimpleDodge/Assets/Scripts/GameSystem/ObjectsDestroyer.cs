using UnityEngine;

public class ObjectsDestroyer : MonoBehaviour
{


    public string[] destructableTags;

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        for (int i = 0; i < destructableTags.Length; i++)
        {
            if (collision.gameObject.CompareTag(destructableTags[i]))
            {
                Destroy(collision.gameObject);
            }
        }        
    }
}

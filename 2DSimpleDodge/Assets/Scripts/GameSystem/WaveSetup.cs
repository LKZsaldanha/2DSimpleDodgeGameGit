using UnityEngine;

public class WaveSetup : MonoBehaviour {

    public Transform collectablePrefab;
    public Transform[] collectableInstances;

    public Transform harmfulPrefab;
    public Transform[] harmfulInstances;

    private void Awake()
    {
        InstantiateCollectables();
        InstantiateHarmfull();
        Destroy(gameObject);
    }

    private void InstantiateCollectables()
    {
        for (int i = 0; i < collectableInstances.Length; i++)
        {
            Instantiate(collectablePrefab, collectableInstances[i].position,Quaternion.identity);
            Destroy(collectableInstances[i].gameObject);
        }
    }

    private void InstantiateHarmfull()
    {
        for (int i = 0; i < harmfulInstances.Length; i++)
        {
            Instantiate(harmfulPrefab, harmfulInstances[i].position, Quaternion.identity);
            Destroy(harmfulInstances[i].gameObject);
        }
    }
}

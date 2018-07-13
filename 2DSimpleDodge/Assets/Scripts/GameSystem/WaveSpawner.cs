using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WavesDifficulty
{
    public string waveDifficulty;
    public Transform[] waves;
}

public class WaveSpawner : MonoBehaviour {

    public Transform spawnPoint;

    public WavesDifficulty[] wavesDifficulty;
    

    public float timeBetweenWaves = 2f;

    private List<Transform> waves = new List<Transform>();

    private float lastwaveTime = 0f;
    public int nextDifficulty = 1;

    private void Start()
    {
        lastwaveTime = Time.time;
        ResetList();
    }

    private void Update()
    {
        if(Time.time - lastwaveTime > timeBetweenWaves)
        {
            SpawnWave();
        }
        else
        {
            lastwaveTime -= Time.deltaTime;
        }


        if (Input.GetKeyDown(KeyCode.A))
        {
            RaiseDifficulty(nextDifficulty);
        }
    }

    void RaiseDifficulty(int _difficulty)
    {
        if (wavesDifficulty.Length <= _difficulty)
        {
            return;
        }

        WavesDifficulty newDif = wavesDifficulty[_difficulty];

        for (int i = 0; i < newDif.waves.Length; i++)
        {
            waves.Add(newDif.waves[i]);
        }

        nextDifficulty++;
    }

    private void ResetList()
    {
        Debug.Log("Resetting waves array");
        waves.Clear();
        WavesDifficulty zeroDif = wavesDifficulty[0];
        for (int i = 0; i < zeroDif.waves.Length; i++)
        {
            waves.Add(zeroDif.waves[i]);
        }
        nextDifficulty = 1;
    }

    private void SpawnWave()
    {
        Transform newWave = waves[Random.Range(0, waves.Count)];

        Instantiate(newWave, spawnPoint.position, spawnPoint.rotation);

        lastwaveTime = Time.time;
    }

}

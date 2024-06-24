using System.Collections;
using UnityEngine;

public class PawnManager : Singleton<PawnManager>
{
    public GameObject passenger;
    private Vector3 passengerSpawnPosition = new Vector3(-0.5f, 1.5f, -19);
    public GameObject sentinel;
    [SerializeField] private Transform[] sentinelSpawns;

    public void SpawnPassenger()
    {
        StartCoroutine("DelayPassengerSpawn");
    }
    IEnumerator DelayPassengerSpawn()
    {
        yield return new WaitForSeconds(Random.Range(7,12));
        Instantiate(passenger, passengerSpawnPosition, Quaternion.identity);
        Debug.Log("new Passenger Spawned");
    }


    public void SentinelCanSpawn()
    {
        StartCoroutine("DelaySentinelSpawn");   
    }
    IEnumerator DelaySentinelSpawn()
    {
        yield return new WaitForSeconds(Random.Range(2, 5));
        Instantiate(sentinel, sentinelSpawns[Random.Range(0, sentinelSpawns.Length)]);
        Debug.Log("Spawned");

    }
}


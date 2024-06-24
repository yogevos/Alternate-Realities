using UnityEngine;

public class BulletHoleManager : Singleton<BulletHoleManager>
{
    public GameObject bulletHolePrefab;
    public GameObject[] bulletHoles;

    private void Start()
    {
    }
    public void RandomDecalPrefab()
    {
        bulletHolePrefab = bulletHoles[Random.Range(0, bulletHoles.Length)];
    }
}

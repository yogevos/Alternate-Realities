using Unity.VisualScripting;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    private int hitChance;
    public float damage;
    //Particles
    public GameObject FireVFX;
    public GameObject HitVFX;
    [SerializeField] LayerMask playerLayer;

    [SerializeField] private float fireRate;
    private float lastfired;

    [SerializeField] bool canfire;

    #region Parents
    private GameObject hitParent;
    #endregion

    [Header("Audio")]
    [SerializeField] AudioClip gunShot;
    public void Start()
    {
        hitParent = new GameObject("Hit Parent");
    }

    private void Update()
    {
        if (canfire)
        {
            EnemyRay();
        }
    }
    public void EnemyRay()
    {

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out RaycastHit hitInfo, 100))
        {
            if (hitInfo.collider.gameObject.CompareTag("Player"))
            {
                if (Time.time - lastfired > 1 / fireRate)
                {
                    lastfired = Time.time;

                    hitChance = Random.Range(0, 3);
                    if (hitChance != 0)
                    {
                        Instantiate(HitVFX, hitInfo.point, Quaternion.identity, hitParent.transform);
                        IDamageable damageable = hitInfo.collider.GetComponentInParent<IDamageable>();
                        if (damageable != null && hitInfo.collider.gameObject.CompareTag("Player"))
                        {
                            damageable.Damage(damage);
                        }
                    }
                    else
                    {
                        Debug.Log("Hit Failed");
                    }

                    // inside " " is the name of the sound we want to play
                    //AudioSystem.Instance.Play(" ");
                    Instantiate(FireVFX, transform.position, Quaternion.identity);
                }

            }

        }
    }
}

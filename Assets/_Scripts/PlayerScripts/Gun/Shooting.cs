
using UnityEngine;
using UnityEngine.VFX;

public class Shooting : MonoBehaviour
{
    [Header("Bullet Related")]
    [SerializeField] private Transform bulletOrigin;
    [SerializeField] float bulletSpeed;
    [SerializeField] public float fireRate;
    private int bulletDamage = 10;
    private float lastfired;

    #region Parents
    private GameObject hitParent;
    #endregion

    #region Components
    [SerializeField] Animator anim;
    [SerializeField] Camera cam;
    #endregion

    #region Effects
    //Particles
    public GameObject FireVFX;
    public GameObject HitVFX;
    #endregion
    
    
    void Start()
    {
           
        anim = GetComponent<Animator>();
        hitParent = new GameObject("Hit Parent");
        hitParent.transform.localScale = new Vector3(5, 5, 5);
        
        
    }

    
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        //add shoot delay

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 100))
        {
   
            Instantiate(HitVFX, hitInfo.point, Quaternion.identity,hitParent.transform);
            BulletHoleManager.Instance.RandomDecalPrefab();
            GameObject spawnedDecal = Instantiate(BulletHoleManager.Instance.bulletHolePrefab, hitInfo.point, Quaternion.identity);
            Quaternion targetRotation = Quaternion.LookRotation(hitInfo.normal);
            spawnedDecal.transform.rotation = targetRotation;
            spawnedDecal.gameObject.transform.SetParent(hitInfo.transform);
            Destroy(spawnedDecal, 5);
            //Debug.Log(hit.point);
            

            IDamageable damageable = hitInfo.collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.Damage(bulletDamage);
            }
        }
        Instantiate(FireVFX, bulletOrigin.transform.position, Quaternion.identity, bulletOrigin);
        anim.SetTrigger("Shoot");
        
    }
    
    
}

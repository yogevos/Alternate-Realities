using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float rotationSpeed;
    public Transform player;


    private void Start()
    {
    }
    void Update()
    {
        player = GameManager.Instance.player1.transform;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(transform.position - player.transform.position), rotationSpeed * Time.deltaTime);
    }
}

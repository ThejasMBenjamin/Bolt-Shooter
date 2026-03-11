using UnityEngine;

public class bullet : MonoBehaviour
{

    [SerializeField] private float bulletSpeed;
    [HideInInspector] public float CurrentWeaponDamage;
    private Vector2 bulletDirection;

    private void Start() {Destroy(gameObject, 3f);}
    private void Update() 
    {transform.position += (Vector3)(bulletDirection * bulletSpeed * Time.deltaTime);}
    public void bulletPosition(Vector2 pos) {bulletDirection = pos.normalized;}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemey"))
        {
            GameObject Enemey = collision.gameObject;
            Enemey.GetComponent<EnemeyDamageSystem>().Damage(CurrentWeaponDamage);
            Destroy(gameObject);
        }
    }
}

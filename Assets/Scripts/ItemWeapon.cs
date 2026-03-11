using UnityEngine;

public class ItemWeapon : MonoBehaviour
{
    [SerializeField] GameObject weaponPreFab;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")){
         
            weaponPreFab.GetComponent<itemVisual>().enabled = false;
            
            WeaponPickUp weaponPickUp = collision.GetComponent<WeaponPickUp>();
            weaponPickUp.WeaponEquipe(weaponPreFab);
            Destroy(gameObject);
        }
    }
}

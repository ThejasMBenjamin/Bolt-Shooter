using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    [HideInInspector] public GameObject currentWeapon;
    [SerializeField] Transform weaponHolder;
    [SerializeField] AudioClip PickUpClip;
    private AudioSource ad;

    private void Start()
    {
        ad = GetComponent<AudioSource>();
        //ad.priority = 130;
    }
    public void WeaponEquipe(GameObject weaponPreFab)
    {
        if(currentWeapon != null) Destroy(currentWeapon.gameObject);
        ad.clip = PickUpClip;
        ad.Play();
        GameObject weaponObj = Instantiate(weaponPreFab, weaponHolder.position,Quaternion.identity);


        weaponObj.transform.SetParent(weaponHolder);

        currentWeapon = weaponObj; 

    }
}

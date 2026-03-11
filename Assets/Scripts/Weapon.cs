using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject bulletPreFab;
    [SerializeField] WeaponHold wph;
    [Header("Settings")]
    [SerializeField] private float defualtNextFireTime = 0.12f;
    [SerializeField] private float shotGunNextFireTime = 0.6f;
    [Header("Weapon Damage Setting")]
    [SerializeField] private float PistolDmg;
    [SerializeField] private float RiffleDmg;
    [SerializeField] private float ShotGunDmg;

    [Header("Others")]
    private GameObject bullet;
    private Vector2 lastPos;
    private float nextFireTime;
    private float currentWeaponDmg;
    private WeaponSoundEffect wps;

    private void Awake() { wps = GetComponent<WeaponSoundEffect>(); }
    private void Update()
    {
        if(wph.transform.childCount == 0) return;
        if (wph.MouseInDeadZone)
        {
            if (wps != null)
            {
                wps.ad.loop = false; wps.ad.Stop();
                return;
            }
        }

        if (!wph.currentWeaponBlockedAngles) {lastPos = wph.direction;}
        else {wph.direction = lastPos; }

        //Taking Child Object AudioScript
        wps = wph.wphChild.GetComponent<WeaponSoundEffect>();

        //
            if (wph.weaponLayer == WeaponHold.ShotGun)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame && Time.time > nextFireTime)
            {
                nextFireTime = Time.time + shotGunNextFireTime;
                ShotGunShoot(); wps.ShotGunAudioPlay();
            }
        }
        else
        {
            if (Mouse.current.leftButton.isPressed && Time.time > nextFireTime)
            {
                nextFireTime = Time.time + defualtNextFireTime;
                SpawnBullet(lastPos); wps.WeaponAudioPlay();
            }
            else if (Mouse.current.leftButton.wasReleasedThisFrame || wph.MouseInDeadZone){
                wps.ad.loop = false;
                wps.ad.Stop();
            }
        } 

        switch(wph.weaponLayer)
        {
            case WeaponHold.Pistol: currentWeaponDmg = PistolDmg; break;
            case WeaponHold.AssaultRifle: currentWeaponDmg = RiffleDmg; break;
            case WeaponHold.ShotGun: currentWeaponDmg = ShotGunDmg; break;
        }
    }

    private void ShotGunShoot()
    {
        float spreadAngle = 15f;

        //Mid
        SpawnBullet(lastPos);

        //top
        Vector2 topDir = RotateVector(lastPos, spreadAngle);
        SpawnBullet(topDir);

        //bottom
        Vector2 botDir = RotateVector(lastPos, -spreadAngle);
        SpawnBullet(botDir);
    }

    // Calculating Angles 
    private Vector2 RotateVector(Vector2 v,float degree)
    {
        float rad = degree * Mathf.Deg2Rad;

        float sin = Mathf.Sin(rad);
        float cos = Mathf.Cos(rad);

        return new Vector2(
            v.x * cos - v.y * sin,
            v.x * sin + v.y * cos //This Line Need To ReChek xD
        ).normalized;
    }



    private void SpawnBullet(Vector2 dir)
    {


        bullet = Instantiate(bulletPreFab, wph.shootPoint.position, Quaternion.identity);
        bullet.GetComponent<bullet>().CurrentWeaponDamage = currentWeaponDmg;
        bullet.GetComponent<bullet>().bulletPosition(dir);
        wph.vs.MuzzleFlashPlay();
        wph.impulseSource.GenerateImpulseWithForce(wph.Recoil);
    }

}

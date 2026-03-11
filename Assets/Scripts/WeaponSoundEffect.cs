using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
public class WeaponSoundEffect : MonoBehaviour
{
    [SerializeField] private AudioClip AudioClip;
    [HideInInspector] public AudioSource ad;

   
    //private 
    private void Awake(){ad  = GetComponent<AudioSource>();}

    public void  WeaponAudioPlay()
    {
        ad.clip = AudioClip;
        ad.loop = true;
        ad.Play();
    }

    public void ShotGunAudioPlay()
    {
        ad.clip = AudioClip;
        ad.loop = false;
        ad.Play();
    }



}

using UnityEngine;

public class EnemeySoundEffect : MonoBehaviour
{
    [SerializeField] AudioClip roar;
    [SerializeField] AudioClip hit;
    AudioSource ad;

    private void Awake()
    {
        ad = GetComponent<AudioSource>();
        ad.clip = roar;
        ad.volume = .20f;
        ad.Play();
    }

    public void HitSoundPlay()
    {
        if (!ad.isPlaying)
        {
            ad.clip = hit;
            ad.Play();
        }
    }

}

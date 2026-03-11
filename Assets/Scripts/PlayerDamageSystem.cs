using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDamageSystem : MonoBehaviour
{
    [SerializeField] private GameObject WeaponHold;
    [SerializeField] AudioClip DeathClip;
    [SerializeField] AudioClip HitClip;
    [SerializeField] private WeaponHold wph;
    [SerializeField] private PanelManager pm;
    [SerializeField] HealthBar hbr;
    [SerializeField] private int maxHealth;
    [HideInInspector] public static bool isDeath = false;
    private AudioSource ad;
    private Animator anim;
    private SpriteRenderer sr;
    private EntityDamageVisual eds;
    private int currentHealth;
    private bool OnlyOneTime = true;
    private bool Death = false; //
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        eds = GetComponent<EntityDamageVisual>();
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        hbr.SetMaxHealth(maxHealth);
        ad = GetComponent<AudioSource>();
        isDeath = false;
    }


    public void OnHit(int d)
    {
        currentHealth -= d;
        anim.SetTrigger("isHit");
        hbr.SetHealth(currentHealth);
        ad.clip = HitClip;
        if(!ad.isPlaying) ad.Play();
        eds.DamageFlashPlay();
    }
    private void Update()
    {
        if (currentHealth <= 0 && !isDeath)
        { anim.SetTrigger("OnDeath"); isDeath = true; WeaponHold.SetActive(false);
        Death = true; eds.StopDamageFlash();
        }
        anim.SetBool("isDeath",isDeath);
        if (Death && OnlyOneTime) { OnlyOneTime = false; ad.clip = DeathClip; ad.Play();}
    }

    private void OnDeath()
    {
        Death= false;
        pm.OnDeathScreen();
        Destroy(gameObject);
    }
}

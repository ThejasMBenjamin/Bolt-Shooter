using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponHold : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] PolygonCollider2D playerCollider;
    [SerializeField] SpriteRenderer playerSprite;
    [SerializeField] Animator playerAnimator;

    [Header("Scripts")]
    [SerializeField] PlayerMoment plym;
    [SerializeField] public Visuals vs;
    [SerializeField] private Weapon wp;

    [Header("Settings")]
    [SerializeField] public float mouseDeadZone;
    [SerializeField] public float Recoil;
    [SerializeField] private float angleGap;

    [Header("Others")]
    [HideInInspector] public CinemachineImpulseSource impulseSource;
    [HideInInspector] public Transform muzzle;
    [HideInInspector] public Transform shootPoint;
    [HideInInspector] public Transform wphChild;
    [HideInInspector] public SpriteRenderer muzzleSprite;
    [HideInInspector] public Vector3 aimOrigin;
    [HideInInspector] public Vector2 direction;
    [HideInInspector] public float angle;
    [HideInInspector] public bool PistolBlockedAngles;
    [HideInInspector] public bool ShotGunBlockedAngles;
    [HideInInspector] public bool AssaultRifleBlockedAngles;
    [HideInInspector] public bool currentWeaponBlockedAngles;
    [HideInInspector] public bool isGunHold;
    [HideInInspector] public bool blockedAngles;
    [HideInInspector] public bool rightScreen;
    [HideInInspector] public bool leftScreen;
    [HideInInspector] public int weaponLayer;
    [HideInInspector] public int currentWeaponDamage;
    [HideInInspector] public bool MouseInDeadZone;
    [HideInInspector] public const int  Pistol = 10, 
    ShotGun = 11, AssaultRifle = 12;
    private SpriteRenderer weaponSprite;
    private int spriteOrder;
    private int currentZone = -1;

    private void Start() {impulseSource = GetComponent<CinemachineImpulseSource>();    }
    private void Update()
    {

        if (transform.childCount == 0) { isGunHold = false;  return; }
        else { isGunHold = true; }


        // Declaring and Initialising Components & Objects
        wphChild = transform.GetChild(0);
        weaponSprite = wphChild.GetComponent<SpriteRenderer>();
        shootPoint = wphChild.GetChild(0);
        muzzle = wphChild.GetChild(1);
        muzzleSprite = muzzle.GetComponent<SpriteRenderer>();
        //Disable Shadow
        Transform shadow =  wphChild.GetChild(2);
        shadow.GetComponent<SpriteRenderer>().enabled = false;

        //  Rotation Logic New Old one Not Worked as Expected Uhhh..
        Vector2 pos = Camera.main.WorldToScreenPoint(wphChild.position);
        direction = Mouse.current.position.ReadValue() - pos;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        //
        ////Mouse DeadZone
        if (direction.sqrMagnitude <= mouseDeadZone * mouseDeadZone) { MouseInDeadZone = true; return; }//? <- uh i need to re-learn
        else MouseInDeadZone = false;
            //

            rightScreen = angle > -90f && angle < 90f;
        leftScreen = !rightScreen;


        // Rotation Logic Old
        //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        //mousePos.z = 0f;
        //direction = mousePos - shootPoint.position;
        //angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        ////wphChild.right = direction;
        //transform.rotation = Quaternion.Euler(0, 0, angle);


        /*Blocked Angles*/
        PistolBlockedAngles = (angle > 50f && angle < 130f) || (angle > -120 && angle < -60f);
        ShotGunBlockedAngles = (angle < 120f && angle > 60f) || (angle > -130f && angle < -50f);
        AssaultRifleBlockedAngles = (angle > 70f && angle < 110f) || (angle > -115f && angle < -60f);


        switch (weaponLayer)
        {
            case Pistol: currentWeaponBlockedAngles = PistolBlockedAngles; break;
            case ShotGun: currentWeaponBlockedAngles = ShotGunBlockedAngles; break;
            case AssaultRifle: currentWeaponBlockedAngles = AssaultRifleBlockedAngles; break;
        }

        if (currentWeaponBlockedAngles) return;

        //Gun Rotation
        wphChild.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        //For Testing
        //if (Mouse.current.leftButton.wasPressedThisFrame) { Debug.Log(angle); }

        // Weapon Hold Animation Check 

        playerAnimator.SetBool("isGunHold", isGunHold);


        //Weapon Flip 
        if (rightScreen) { weaponSprite.flipY = false; }
        else if (leftScreen) { weaponSprite.flipY = true; }

        weaponLayer = wphChild.gameObject.layer;



        // All Weapon Pos / Shoot Pos / Muzzle Pos Cng + Buffer Solution etc..

        //Followed + in Left Side and - in Right Side


        //For Mid
        if (angle > -40 + angleGap && angle < 20 - angleGap) { currentZone = 1; } 
        else if (angle > 160 + angleGap || angle < -140 - angleGap) { currentZone = 2; } 
        //For Mid Top
        else if (angle > 20 + angleGap && angle < 70 - angleGap) { currentZone = 3; } 
        else if (angle > 110 + angleGap && angle < 160 - angleGap) { currentZone = 4; }
        //For Bottom Mid
        else if (angle > -90 + angleGap && angle < -40 - angleGap) { currentZone = 5; } 
        else if (angle > -140 + angleGap && angle < -90 - angleGap) { currentZone = 6; }

        if (weaponLayer == Pistol)
        {
            currentWeaponDamage = 2;

            if (rightScreen)
            { muzzle.localPosition = new Vector2(6.8f, 1.16f);
              shootPoint.localPosition = new Vector2(11.5f, 1f);}
            else if (leftScreen)
            {muzzle.localPosition = new Vector2(6.8f, -1f);
             shootPoint.localPosition = new Vector2(11.81f, -0.95f);}

            switch (currentZone)
            {
                //For Mid
                case 1: transform.localPosition = new Vector2(2.02f, -1.80f); spriteOrder = 5; break;
                case 2: transform.localPosition = new Vector2(-2.21f, -1.80f); spriteOrder = 5; break;
                //For Mid Top
                case 3: transform.localPosition = new Vector2(1.76f, -1.13f); spriteOrder = 1; break;
                case 4: transform.localPosition = new Vector2(-1.76f, -1.13f); spriteOrder = 1; break;
                //For Bottom Mid
                case 5: transform.localPosition = new Vector2(0.92f, -2.75f); spriteOrder = 5; break;
                case 6: transform.localPosition = new Vector2(-0.92f, -2.75f); spriteOrder = 5; break;

            }
        }
        else if (weaponLayer == ShotGun)
        {
            currentWeaponDamage = 4;

            if (rightScreen)
            { muzzle.localPosition = new Vector2(8.74f, 1.26f);
             shootPoint.localPosition = new Vector2(11.59f, 0.95f);}
            else if (leftScreen)
            { muzzle.localPosition = new Vector2(9.8f, -1.3f);
              shootPoint.localPosition = new Vector2(13.5f, -1.27f);}

            switch (currentZone)
            {
                //For Mid
                case 1: transform.localPosition = new Vector2(1.44f, -2.26f); spriteOrder = 5; break;
                case 2: transform.localPosition = new Vector2(-1.21f, -2.26f); spriteOrder = 5; break;
                //For Mid Top
                case 3: transform.localPosition = new Vector2(2.36f, -1.4f); spriteOrder = 1; break;
                case 4: transform.localPosition = new Vector2(-2.3f, -1.67f); spriteOrder = 1; break;
                ////For Bottom Mid
                case 5: transform.localPosition = new Vector2(0.76f, -2.29f); spriteOrder = 5; break;
                case 6: transform.localPosition = new Vector2(-0.63f, -2.52f); spriteOrder = 5; break;

            }
        }
        else if (weaponLayer == AssaultRifle)
        {
            currentWeaponDamage = 6;

            if (rightScreen)
            {muzzle.localPosition = new Vector2(8.7f, 0.8f);
                shootPoint.localPosition = new Vector2(12.4f, 0.6f);}
            else if (leftScreen)
            { muzzle.localPosition = new Vector2(9f, -.5f);
              shootPoint.localPosition = new Vector2(13f, -0.4f);}

            switch (currentZone)
            {
                //For Mid
                case 1: transform.localPosition = new Vector2(1.54f, -1.18f); spriteOrder = 5; break;
                case 2: transform.localPosition = new Vector2(-1.54f, -1.18f); spriteOrder = 5; break;
                //For Mid Top
                case 3: transform.localPosition = new Vector2(2.21f, -0.33f); spriteOrder = 1; break;
                case 4: transform.localPosition = new Vector2(-2.21f, -0.33f); spriteOrder = 1; break;
                //For Bottom Mid
                case 5: transform.localPosition = new Vector2(-0.87F, -1.95f); spriteOrder = 5; break;
                case 6: transform.localPosition = new Vector2(0.5f, -2.3f); spriteOrder = 5; break;

            }
        }

        //Changing Weapon Sprite Order Layer
        weaponSprite.sortingOrder = spriteOrder;


    }
}


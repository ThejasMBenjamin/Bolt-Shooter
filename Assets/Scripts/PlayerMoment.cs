using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMoment : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private WeaponHold wph;
    [SerializeField] private float moveSpeed;

    [Header("Others")]
    [HideInInspector] public float lastMoveBtn;
    [HideInInspector] public Vector3 playerPos;
    private float buttonInputX, buttonInputY;

    Animator anim;
    SpriteRenderer sp;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        playerPos = transform.position;
    }

    private void Update()
    {
        // Basic Moment
        buttonInputX = Keyboard.current.dKey.isPressed ? 1 :
                       Keyboard.current.aKey.isPressed ? -1 : 0;

        buttonInputY = Keyboard.current.wKey.isPressed ? 1 :
                       Keyboard.current.sKey.isPressed ? -1 : 0;

        bool isWalking = buttonInputX != 0 || buttonInputY != 0;
        anim.SetBool("isMoving", isWalking);

        if (PlayerDamageSystem.isDeath == true) return;

        Vector2 move = new Vector2(buttonInputX, buttonInputY).normalized;
        if (isWalking)
        {
            transform.position += (Vector3)move * moveSpeed * Time.deltaTime;
            lastMoveBtn = buttonInputX;
        }

        // Character Flip

        Vector2 pos = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 dir = Mouse.current.position.ReadValue() - pos;
        float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;

        // - Without Gun
        if (wph.transform.childCount == 0) {
            if (angle > -90f && angle < 90f) { sp.flipX = false; }
            else { sp.flipX = true; }
        } else // - With Gun
         {
        if (wph.currentWeaponBlockedAngles) return;
        else if (wph.rightScreen) { sp.flipX = false; }
        else { sp.flipX = true; }
        }



    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
public class Crosshair : MonoBehaviour
{
    [HideInInspector] public bool changeCursor = false;
    [SerializeField] Texture2D AimCursor;
    [SerializeField] Texture2D defualtCursor;
    private Texture2D cursor;
    private Vector2 hotspot = Vector2.zero;


    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "SampleScene") changeCursor = false;
        else changeCursor = true;
    }
    private void Update()
    {

        cursor = changeCursor ? defualtCursor : AimCursor;
        Cursor.SetCursor(cursor, hotspot, CursorMode.Auto);

    }
}

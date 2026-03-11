using UnityEngine;

public class itemVisual : MonoBehaviour
{
    [SerializeField] private float floatSpeed = 2f;     
    [SerializeField] private float floatHeight = 0.3f;  

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        transform.position = startPos + new Vector3(0, Mathf.Sin(Time.time * floatSpeed) * floatHeight, 0);
    }
}

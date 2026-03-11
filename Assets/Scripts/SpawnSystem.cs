using UnityEngine;

public class SpawnSystem : MonoBehaviour
{
    [SerializeField] private GameObject EPurple;
    [SerializeField] private GameObject EOrange;
    [SerializeField] private GameObject EGreen;
    [SerializeField] private GameObject ERed;
    [SerializeField] private int maxEnemeySpawn = 2;
    public int enemyCount = 0;

    [SerializeField] private float spawnInterval = 2f;
    private float spawnDistance = 2f; 

    private Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;
        InvokeRepeating(nameof(SpawnEnemey),1f,spawnInterval);
    }

    private void SpawnEnemey()
    {
        //Debug.Log("Enemeny Count Before" + "" + enemyCount);
        if (enemyCount >= maxEnemeySpawn) return;
        GameObject[] EnObjs = {EPurple,EOrange,ERed,EGreen };
        Vector2 spawnPos = GetOffScreenPosition();
        int randomSpawnCount = Random.Range(0, 4);
        Instantiate(EnObjs[randomSpawnCount], spawnPos, Quaternion.identity);
        enemyCount++;

    }

    public void OnEneDie()
    {
        //enemyCount = Mathf.Max(0, enemyCount -1);
        enemyCount--;
    }

    private Vector2 GetOffScreenPosition()
    {
        float ScreenHeight = mainCam.orthographicSize;
        float ScreenWidth =  ScreenHeight * mainCam.aspect;

        int randomSide = Random.Range(0, 4);

        Vector2 spawnPos = Vector2.zero;

        switch (randomSide)
        {
            // Left
            case 0:
                spawnPos = new Vector2(
                    mainCam.transform.position.x - ScreenWidth - spawnDistance,
                    Random.Range(mainCam.transform.position.y - ScreenHeight, 
                                 mainCam.transform.position.y + ScreenHeight));
                break;

            // Right
            case 1:
                spawnPos = new Vector2(
                    mainCam.transform.position.x + ScreenWidth + spawnDistance,
                    Random.Range(mainCam.transform.position.y - ScreenHeight,
                                 mainCam.transform.position.y + ScreenHeight));
                break;

            // Top
            case 2:
                spawnPos = new Vector2(
                    Random.Range(mainCam.transform.position.x - ScreenHeight,
                                 mainCam.transform.position.x + ScreenHeight),
                                 mainCam.transform.position.y + ScreenHeight + spawnDistance);
                break;

            // Bottom
            case 3:
                spawnPos = new Vector2(
                    Random.Range(mainCam.transform.position.x - ScreenWidth,
                                 mainCam.transform.position.x + ScreenWidth),
                                 mainCam.transform.position.y - ScreenHeight - spawnDistance);
                break;
        }

        return spawnPos;
    }

}

using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public GameObject cubePrefab; 
    public Transform spawnPoint; 
    public float spawnInterval = 0.5f; 

    private float timer = 0f; 

    void Update()
    {
        
        if (Input.GetMouseButton(0))
        {
            
            timer += Time.deltaTime;

            
            if (timer >= spawnInterval)
            {
                
                timer = 0f;

                
                SpawnCube();
            }
        }
    }

    void SpawnCube()
    {
        
        Instantiate(cubePrefab, spawnPoint.position, Quaternion.identity);

        
    }
}

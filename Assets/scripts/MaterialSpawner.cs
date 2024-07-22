using UnityEngine;

public class MaterialSpawner : MonoBehaviour
{
    public GameObject[] materialPrefabs; 
    public int numberOfMaterials = 100; 
    public float spawnRadius = 50f; 
    public float clumpingRadius = 10f; 
    public LayerMask groundLayer; 
    public float spawnHeight = 10f; 

    void Start()
    {
        SpawnMaterials();
    }

    void SpawnMaterials()
    {
        for (int i = 0; i < numberOfMaterials; i++)
        {
            Vector3 spawnPosition = GetValidSpawnPosition();
            if (spawnPosition != Vector3.zero)
            {
                
                int randomMaterialIndex = Random.Range(0, materialPrefabs.Length);
                GameObject materialPrefab = materialPrefabs[randomMaterialIndex];
                Instantiate(materialPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }

    Vector3 GetValidSpawnPosition()
    {
        for (int attempt = 0; attempt < 10; attempt++) 
        {
            
            Vector3 randomPosition = Random.insideUnitSphere * spawnRadius;
            randomPosition.y = spawnHeight; 

            
            if (randomPosition.magnitude < 5f)
            {
                continue; 
            }

            
            if (Physics.Raycast(randomPosition + Vector3.up * spawnHeight, Vector3.down, out RaycastHit hit, Mathf.Infinity, groundLayer))
            {
                Vector3 groundPosition = hit.point;

                
                if (!IsClumped(groundPosition))
                {
                    return groundPosition;
                }
            }
        }

        return Vector3.zero; 
    }

    bool IsClumped(Vector3 position)
    {
        
        Collider[] colliders = Physics.OverlapSphere(position, clumpingRadius);
        foreach (Collider collider in colliders)
        {
            
            if (collider.CompareTag("Material"))
            {
                return true;
            }
        }
        return false; 
    }
}

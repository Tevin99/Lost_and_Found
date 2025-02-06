using UnityEngine;

public class PlayerBounds : MonoBehaviour
{
    private float minX, maxX, minZ, maxZ;
    private Vector3 respawnPoint;

    void Start()
    {
        Terrain terrain = Terrain.activeTerrain; // 현재 Terrain 가져오기
        if (terrain != null)
        {
            Vector3 terrainPos = terrain.transform.position; // Terrain 중심 좌표
            Vector3 terrainSize = terrain.terrainData.size;  // Terrain 크기

            minX = terrainPos.x;
            maxX = terrainPos.x + terrainSize.x;
            minZ = terrainPos.z;
            maxZ = terrainPos.z + terrainSize.z;
        }

        respawnPoint = transform.position; // 플레이어 초기 위치 저장
    }

    void Update()
    {
        // X, Z 범위를 벗어나면 리스폰
        if (transform.position.x < minX || transform.position.x > maxX || 
            transform.position.z < minZ || transform.position.z > maxZ)
        {
            transform.position = respawnPoint;
        }
    }
}

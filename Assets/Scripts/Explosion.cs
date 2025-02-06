using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour
{
    public GameObject explosionEffect; // 폭발 이펙트 프리팹
    private bool hasExploded = false;  // 폭발 여부 체크

    private Rigidbody rb;
    
    private void Start(){
        // Rigidbody가 없으면 추가하고 Is Kinematic을 설정
        // if (GetComponent<Rigidbody>() == null)
        // {
        //     Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        //     rb.isKinematic = true;
        // }

        //// 폭발 전 1초 대기
        // StartCoroutine(DelayExplosion());

        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.useGravity = false;  // 처음에는 중력 끄기
            rb.constraints = RigidbodyConstraints.FreezePositionY;  // Y축 고정
        }

        StartCoroutine(DelayExplosion());
    }

    private IEnumerator DelayExplosion(){
        yield return new WaitForSeconds(1f); // 1초 대기 후 폭발
        hasExploded = false; // 대기 후 폭발 시작
    }

    private void OnTriggerEnter(Collider other){


        Debug.Log($"Trigger detected with: {other.name}, Layer: {other.gameObject.layer}");

        // Terrain이 Layer 6이면 무시
        if (other.gameObject.layer == 6) 
        {
            Debug.Log("Ignored Terrain Collision");
            return;
        }

        // "Player" 태그가 있는 오브젝트와만 충돌
        if (other.CompareTag("Player") && !hasExploded)
        {
            hasExploded = true;
            Explode();
        }



        // Debug.Log($"Trigger detected with: {other.name}, Layer: {other.gameObject.layer}");

        // // Terrain Layer 확인
        // if (other.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        // {
        //     Debug.Log("Ignored Terrain Collision");
        //     return; // Terrain과 충돌 시 리턴
        // }

        // // Player 충돌만 감지
        // if (other.CompareTag("Player") && !hasExploded)
        // {
        //     hasExploded = true;
        //     Explode();
        // }



        // Debug.Log("Trigger detected with: " + other.name); // 디버그 로그로 충돌 감지 확인
    
        // // Terrain과의 충돌을 무시하려면, 해당 Layer를 체크
        // if (other.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        // {
        //     return; // Terrain과의 충돌은 무시
        // }
    
        // // "Terrain" Layer를 제외하고, "Player"와 충돌하는 경우만 처리
        // if (other.CompareTag("Player") && other.gameObject.layer != LayerMask.NameToLayer("Terrain") && !hasExploded)
        // {
        //     hasExploded = true;
        //     Explode();
        // }
    }


    void Explode()
    {
        // 폭발 이펙트 생성
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity); // 폭발 생성
        }
        Destroy(gameObject); // 폭탄 삭제 
    }

   
}

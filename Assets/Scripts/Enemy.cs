using Photon.Pun;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5f;
    private Transform targetPlayer;

    public float damage = 0.10f;
    public GameObject hitEffect;

    public float health;

    public float rotationSpeed = 30f;

    void Start()
    {
        FindNearestPlayer();
    }

    void FindNearestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        float nearestDistance = float.MaxValue;

        foreach (GameObject player in players)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
            if (distanceToPlayer < nearestDistance)
            {
                nearestDistance = distanceToPlayer;
                targetPlayer = player.transform;
            }
        }

        // 가장 가까운 플레이어를 찾았다면 적을 해당 플레이어에게 바로 따라가도록 설정
        if (targetPlayer != null)
        {
            StartChasingPlayer();
        }
    }

    // 플레이어가 생성될 때마다 호출되는 함수
    void OnPlayerSpawned(GameObject newPlayer)
    {
        // 새로운 플레이어와의 거리를 계산
        float distanceToNewPlayer = Vector2.Distance(transform.position, newPlayer.transform.position);

        // 새로운 플레이어가 현재 타겟 플레이어보다 가깝다면 새로운 플레이어를 타겟으로 설정
        if (distanceToNewPlayer < Vector2.Distance(transform.position, targetPlayer.position))
        {
            targetPlayer = newPlayer.transform;
        }
    }

    void StartChasingPlayer()
    {
        // 플레이어 오브젝트를 찾은 경우 적을 따라가는 기능 활성화
        enabled = true;
    }

    void Update()
    {
        // 타겟 플레이어 오브젝트가 null이 아닌 경우에만 실행
        if (targetPlayer != null)
        {
            // 적 오브젝트가 타겟 플레이어를 향해 이동
            transform.position = Vector2.MoveTowards(transform.position, targetPlayer.position, speed * Time.deltaTime);
        }

        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        PhotonView target = other.gameObject.GetComponent<PhotonView>();


        if (target != null && (!target.IsMine || target.IsSceneView))
        {
            if (target.tag == "Player")
            {
                GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
                Destroy(effect, 1f);
                target.RPC("HealthUpdate", RpcTarget.AllBuffered, damage);
            }
        }

        if(other.tag == "Bullet")
        {
            TakeDamage(other.GetComponent<Bullet>().bulletDamage);
        }
    }

    void TakeDamage(float damageAmount)
    {
        health -= damageAmount;
        if(health < 0)
        {
            Destroy(gameObject);
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        }
    }



}
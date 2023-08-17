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

        // ���� ����� �÷��̾ ã�Ҵٸ� ���� �ش� �÷��̾�� �ٷ� ���󰡵��� ����
        if (targetPlayer != null)
        {
            StartChasingPlayer();
        }
    }

    // �÷��̾ ������ ������ ȣ��Ǵ� �Լ�
    void OnPlayerSpawned(GameObject newPlayer)
    {
        // ���ο� �÷��̾���� �Ÿ��� ���
        float distanceToNewPlayer = Vector2.Distance(transform.position, newPlayer.transform.position);

        // ���ο� �÷��̾ ���� Ÿ�� �÷��̾�� �����ٸ� ���ο� �÷��̾ Ÿ������ ����
        if (distanceToNewPlayer < Vector2.Distance(transform.position, targetPlayer.position))
        {
            targetPlayer = newPlayer.transform;
        }
    }

    void StartChasingPlayer()
    {
        // �÷��̾� ������Ʈ�� ã�� ��� ���� ���󰡴� ��� Ȱ��ȭ
        enabled = true;
    }

    void Update()
    {
        // Ÿ�� �÷��̾� ������Ʈ�� null�� �ƴ� ��쿡�� ����
        if (targetPlayer != null)
        {
            // �� ������Ʈ�� Ÿ�� �÷��̾ ���� �̵�
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
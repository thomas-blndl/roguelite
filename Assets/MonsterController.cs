using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    public float range = 10.0f;
    private bool canSpawn = true;
    private float _spawncooldown = 1.0f;
    [SerializeField]
    private GameObject _ghoulPrefab;
    private GameObject _player;

    private void Awake() {
        _player = GameObject.FindWithTag("Player");
    }

    void Update()
    {


        if (canSpawn)
        {
            Vector3 point;
            if (RandomPointOnNavmesh(_player.transform.position, range, out point))
            {
                canSpawn = false;
                Instantiate(_ghoulPrefab, point, Quaternion.identity);
                StartCoroutine(ResetCanSpawn());
            }
        }
    }


    public bool RandomPointOnNavmesh(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }


    public IEnumerator ResetCanSpawn()
    {
        yield return new WaitForSeconds(_spawncooldown);
        canSpawn = true;
    }
}

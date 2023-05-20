using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Projectile> _projectiles;

    [SerializeField] private float _timer = 1f;

    private float _currentTime = 0f;

    void Update()
    {
        _currentTime += Time.deltaTime;

        if (_currentTime < _timer) return;

        
        Spawn(new Vector2(Random.Range(-5,5),5));

        _currentTime = 0;
    }

    private void Spawn(Vector2 location)
    {
        _projectiles[Random.Range(0,_projectiles.Count)].Clone(location);
    }

}

using System.Collections.Generic;
using Microsoft.Win32.SafeHandles;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField]private float _timeBetweenShots = 2f;
    [SerializeField]private float _shootTimer = 2f;
    private GameObject _player;
    
    private Vector3 _playerPos;
    public Vector3 PlayerPos { get{return _playerPos; } set{ _playerPos = value; } }
    private float _degreeRotation;
    public float DegreeRotation { get{return _degreeRotation; } set{_degreeRotation = value; } }

    [SerializeField] private List<GameObject> _bullets = new List<GameObject>();
    [SerializeField] private Transform _spawnpoint;
    [SerializeField] private Transform _pivotPoint;
    
    private void Start()
    {
        _player = GameObject.Find("Player");
        if (_player==null)
        {
            Debug.LogError("Player: "+ _player.name + " not found ");
        }
    }

    
    private void Update()
    {
        SearchPlayerPos();
        TrackPlayer();
        if (_shootTimer>0)
        {
            _shootTimer -= Time.deltaTime;
        }
        else
        {
            Shoot();
        }
    }
    public void Shoot()
    {
        int rand = Random.Range(0, _bullets.Count);
        GameObject proj = Instantiate(_bullets[rand], _spawnpoint.position, Quaternion.identity);
        proj.GetComponent<Bullet>().SetDirection(_playerPos-_spawnpoint.position);
        _shootTimer = _timeBetweenShots;

    }

    public void TrackPlayer()
    {
        Rotation(_playerPos, _spawnpoint);
        _pivotPoint.rotation = Quaternion.Euler(0, 0, _degreeRotation);
    }

    private void Rotation(Vector3 target, Transform origin)
    {
        Vector3 originVector = origin.transform.position;
        _degreeRotation = Mathf.Atan2(originVector.y - target.y, originVector.x - target.x) * Mathf.Rad2Deg;
    }
    public void SearchPlayerPos()
    {
        _playerPos = _player.transform.position;
    }
}

using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAimAndShoot : MonoBehaviour
{

    [SerializeField] private GameObject _gun;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _bulletSpawnPoint;

    private GameObject _bulletInst;

    private Vector2 _worldMousePosition;
    private Vector2 _direction;

    // Update is called once per frame
    void Update()
    {
        GunRotation();
        GunShooting();
    }

    private void GunRotation()
    {
        //Rotate the gun towards the mouse position
        _worldMousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        _direction = (_worldMousePosition - (Vector2)_gun.transform.position).normalized;
        _gun.transform.right = _direction;
    }

    private void GunShooting()
    {
        if (InputManager.attack)
        {
            _bulletInst = Instantiate(_bullet, _bulletSpawnPoint.position, _gun.transform.rotation);
        }    
    }
}

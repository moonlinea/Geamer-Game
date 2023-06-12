using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private Animator _Anim;

    [SerializeField] private float _playerSpeed;

    private void Awake()
    {
        gemStackSystem = GetComponent<StackSystem>();
    }
    private void FixedUpdate()// Character Positions and Rotations Controller With Joystick
    {


        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            _rigidbody.velocity = new Vector3(_joystick.Horizontal * _playerSpeed, _rigidbody.velocity.y, _joystick.Vertical * _playerSpeed);
            transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
            _Anim.SetBool("walking", true);
        }
        else _Anim.SetBool("walking", false);

    }

    private StackSystem gemStackSystem;

    private void OnTriggerEnter(Collider other)
    {
        GameObject gem = other.GetComponent<GameObject>();
        if (gem != null)
        {
            gemStackSystem.AddGem(gem.gameObject);
        }
    }
}

    //---------------------------------------------------------------------------------------- other Joystick
    //private Rigidbody _rigidbody;
    //private Vector3 _moveVector;
    //[SerializeField] private FloatingJoystick _joystick;



    //[SerializeField] private Animator _Anim;

    //[SerializeField] private float _playerMoveSpeed;
    //[SerializeField] private float _playerRotateSpeed;

    //private void Awake()
    //{
    //    _rigidbody = GetComponent<Rigidbody>();
    //}
    //private void Update()
    //{
    //    Move();
    //}
    //private void Move()
    //{
    //    _moveVector = Vector3.zero;
    //    _moveVector.x = _joystick.Horizontal * _playerMoveSpeed * Time.deltaTime;
    //    _moveVector.z = _joystick.Vertical * _playerMoveSpeed * Time.deltaTime;

    //    if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
    //    {
    //        Vector3 direction = Vector3.RotateTowards(transform.forward, _moveVector, _playerRotateSpeed * Time.deltaTime, 0.0f);
    //        _Anim.SetBool("walking", true);
    //    }
    //    else if(_joystick.Horizontal == 0 || _joystick.Vertical == 0)
    //    {
    //        _Anim.SetBool("walking", false);
    //    }
    //    _rigidbody.MovePosition(_rigidbody.position + _moveVector);
    //}



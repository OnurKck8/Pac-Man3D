using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    public Rigidbody _rb;
    public LayerMask _walls;
    public Text _scoreText;

    GameObject[] _enemies;
    GameObject[] _baits;

    int _baitCount;
    int _score=0;

    float _speed = 5.0f;

    bool _left, _right, _up, _down = false;

    void Start()
    {
        _enemies = GameObject.FindGameObjectsWithTag("Enemy");
        _baits = GameObject.FindGameObjectsWithTag("Bait");
        _baitCount = _baits.Length;

        _right = true;
        _scoreText.text = "Score: " + _score;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Bait")
        {
            Destroy(other.gameObject);
            _score += 10;
            _scoreText.text = "Score: " + _score;
            _baitCount--;

            if(_baitCount==0)
            {
                Debug.Log("Winner");
            }
        }

        if (other.gameObject.tag == "BigBait")
        {
            Destroy(other.gameObject);
           
            foreach(GameObject enemy in _enemies)
            {
                enemy.GetComponent<AI>().BeBlue();
            }
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow) && Controller(Vector3.right)==false)
        {
            DirectionStatus(true, false, false, false);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && Controller(-Vector3.right) == false)
        {
            DirectionStatus(false, true, false, false);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && Controller(Vector3.forward) == false)
        {
            DirectionStatus(true, false, true, false);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && Controller(-Vector3.forward) == false)
        {
            DirectionStatus(true, false, false, true);
        }
    }

    bool Controller(Vector3 _lightStatus)
    {
        RaycastHit _contact;
        if(Physics.Raycast(transform.position,_lightStatus,out _contact,2.0f,_walls))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void DirectionStatus(bool _rightDirection, bool _leftDirection, bool _upDirection, bool downDirection)
    {
        _right = _rightDirection;
        _left = _leftDirection;
        _up = _upDirection;
        _down = downDirection;
    }

    void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        if(_right)
        {
            _rb.velocity = Vector3.right * _speed;
        }
        if (_left)
        {
            _rb.velocity = -Vector3.right * _speed;
        }
        if (_up)
        {
            _rb.velocity = Vector3.forward * _speed;
        }
        if (_down)
        {
            _rb.velocity = -Vector3.forward * _speed;
        }

    }
}

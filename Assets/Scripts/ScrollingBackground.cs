using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    // Create references to two background sprites and float variable for movement speed
    [SerializeField] Transform _first, _second;
    [SerializeField] private float _scrollSpeed, _zOffset;

    private float _horizontalDifference = 37.46f, _minimumDistance = -37.46f;

    private Vector3 newPosition;

    // Start is called before the first frame update
    void Start()
    {
        _zOffset += _first.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Wrap();
    }

    void Movement()
    {
        _first.position = new Vector3(_first.position.x + Time.deltaTime * _scrollSpeed, _first.position.y, _zOffset);
        _second.position = new Vector3(_second.position.x + Time.deltaTime * _scrollSpeed, _second.position.y, _zOffset);
    }

    void Wrap()
    {
        if (_first.localPosition.x < _minimumDistance)
        {
            newPosition = new Vector3(_second.position.x * _horizontalDifference * 2, _second.position.y, _zOffset);
            _first.position = newPosition;
        }

        if (_second.localPosition.x < _minimumDistance)
        {
            newPosition = new Vector3(_first.position.x * _horizontalDifference * 2, _first.position.y, _zOffset);
            _second.position = newPosition;
        }
    }


}

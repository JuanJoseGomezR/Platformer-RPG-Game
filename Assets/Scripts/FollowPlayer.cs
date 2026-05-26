using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]
    private Transform _player;

    [SerializeField]
    private float _smoothTime = 0.2f;

    [SerializeField]
    private Vector3 _velocity = Vector3.zero;

    void LateUpdate()
    {
        Vector3 targetPosition = new Vector3(
            _player.position.x,
            _player.position.y + 5f,
            transform.position.z
        );

        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref _velocity,
            _smoothTime
        );
    }
}

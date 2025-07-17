using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] Transform _player;
    void LateUpdate()
    {
        transform.position = new Vector3(_player.position.x, _player.position.y, transform.position.z);
    }
}
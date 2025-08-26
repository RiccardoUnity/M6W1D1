using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float _x;
    private float _z;
    private Vector3 _move;
    private float _lenght;
    [SerializeField] private float _speed = 4f;
    
    void Update()
    {
        transform.rotation *= Quaternion.Euler(0f, Input.GetAxis("Mouse X"), 0f);

        _x = Input.GetAxis("Horizontal");
        _z = Input.GetAxis("Vertical");
        _move = new Vector3(_x, 0f, _z);

        _lenght = _move.sqrMagnitude;

        if (_lenght > 1f)
        {
            _lenght = Mathf.Sqrt(_lenght);
            _x /= _lenght;
            _z /= _lenght;
        }

        transform.position += transform.rotation * (_move * (_speed * Time.deltaTime));
    }
}

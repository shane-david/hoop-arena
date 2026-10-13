using UnityEngine;

public class SidewaysTranslator : MonoBehaviour
{
    [Header("Movement Data")]
    [SerializeField] private float _speed;
    [SerializeField] private float _distance; 
    [SerializeField] private MovementType _movementType; 


    private Vector3 _direction; 
    private Vector3 _startPosition; 

    // get hte position of the hoop when it is instantiated 
    void Start()
    {
        _startPosition = transform.position; 
        if (_movementType == MovementType.upAndDown)
        {
            _direction = Vector3.up; 
        } else
        {
            _direction = Vector3.forward; 
        }
    }

    // translate the hoop back and forth 
    void Update()
    {
        float offset = Mathf.Sin(Time.time * _speed) * _distance; 
        transform.position = _startPosition + _direction.normalized * offset; 
    }
}

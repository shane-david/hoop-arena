using UnityEngine;

public class Rotator : MonoBehaviour, IHoopLevel
{
    [Header("Movement Data")]
    [SerializeField] private float _speed;
    [SerializeField] private MovementType _movementType; 
    private Vector3 _centerPosition; // the center to translate the hoop around 
    private Vector3 _direction; // the direction to rotate

    // get hte position of the hoop when it is instantiated 
    void Start()
    {   
        // set center position 
        _centerPosition = transform.localPosition; 

        // set direction 
        if (_movementType == MovementType.upAndDown)
        {
            _direction = Vector3.forward; 
        } else
        {
            _direction = Vector3.up; 
        }
    }

    // rotate the hoop in a circle 
    public void Move()
    {   
        transform.RotateAround(transform.parent.position, _direction, _speed*Time.deltaTime); 
    }
}

using UnityEngine;

public class Rotator : MonoBehaviour, IHoopLevel
{
    [Header("Movement Data")]
    [SerializeField] private float _speed;
    [SerializeField] private MovementType _movementType; 
    private Vector3 _direction; // the direction to rotate
    private Vector3 _axis; // the wall axis so we can move it around the wall instead of the world

    // get hte position of the hoop when it is instantiated 
    void Start()
    {   
        // get the wall Normal from our anchor, and get the wall horizontal 
        Vector3 wallNormal = transform.parent.up; 
        Vector3 wallHor = Vector3.Cross(Vector3.up, wallNormal).normalized; 

        // set the direction 
        _direction = (_movementType == MovementType.upAndDown) ? wallHor : wallNormal; 
    }

    // rotate the hoop in a circle 
    public void Move()
    {   
        transform.RotateAround(transform.parent.position, _direction, _speed*Time.deltaTime); 
    }
}

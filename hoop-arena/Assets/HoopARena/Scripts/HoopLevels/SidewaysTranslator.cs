using UnityEngine;

public class SidewaysTranslator : MonoBehaviour, IHoopLevel
{
    [Header("Movement Data")]
    [SerializeField] private float _speed;
    [SerializeField] private float _distance; 
    [SerializeField] private MovementType _movementType; 


    private Vector3 _startDirection; 
    private Vector3 _startPosition; 

    // get the position of the hoop when it is instantiated     
    void Start()
    {   
        // get the start position 
        _startPosition = transform.localPosition; 

        // get directions according to the wall instead of the world 
        Vector3 wallNormal = transform.parent.up; 
        Vector3 wallHor = Vector3.Cross(Vector3.up, wallNormal).normalized; 
        Vector3 wallVert = Vector3.Cross(wallNormal, wallHor).normalized; 

        // get this in regards to the world direction 
        Vector3 worldDirection = (_movementType == MovementType.upAndDown) ? wallVert : wallHor; 

        // convert the local space so movement stays attached to the anchor
        _startDirection = transform.parent.InverseTransformDirection(worldDirection); 
    }

    // translate the hoop back and forth 
    public void Move()
    {
        float offset = Mathf.Sin(Time.time * _speed) * _distance; 
        transform.localPosition = _startPosition + _startDirection.normalized * offset; 
    }
}

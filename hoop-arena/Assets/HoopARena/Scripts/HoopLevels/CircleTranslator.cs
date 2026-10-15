using UnityEngine;

public class CircleTranslator : MonoBehaviour, IHoopLevel
{
    [Header("Movement Data")]
    [SerializeField] private float _speed;
    [SerializeField] private float _radius; 

    private float _angle; // keep track of time in order to use it with circle formula
    private Vector3 _startLocalPos; 
    private Vector3 _localHorizontal;
    private Vector3 _localUp;

    private void Start()
    {   
        // make sure the anchor exists
        if (transform.parent == null) return; 

        // get the starting local position 
        _startLocalPos = transform.localPosition; 

        // set the normal, horizontal, and vertical based off the anchor, not the world space
        Vector3 wallNormal = transform.parent.up; 
        Vector3 wallHor = Vector3.Cross(Vector3.up, wallNormal).normalized; 
        Vector3 wallVert = Vector3.Cross(wallNormal, wallHor).normalized; 

        // conver the parent's local space 
        _localHorizontal = transform.parent.InverseTransformDirection(wallHor); 
        _localUp = transform.parent.InverseTransformDirection(wallVert); 
    }

    // translate the hoop in a circle
    public void Move()
    {   
        // make sure the parent anchor exists
        if (transform.parent == null) return; 

        // advance the angle
        _angle += _speed * Time.deltaTime; 

        // calculate the x and y offsets with sin and cos
        float offsetUp = Mathf.Cos(_angle) * _radius;
        float offsetHorizontal = Mathf.Sin(_angle) * _radius;

        transform.localPosition = _startLocalPos + _localUp * offsetUp + _localHorizontal * offsetHorizontal;   
    }
}

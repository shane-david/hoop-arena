using UnityEngine;

public class CircleTranslator : MonoBehaviour
{
    [Header("Movement Data")]
    [SerializeField] private float _speed;
    [SerializeField] private float _radius; 
    private float _angle; // keep track of time in order to use it with circle formula

    // translate the hoop in a circle
    void Update()
    {   
        // make sure the parent anchor exists
        if (transform.parent == null) return; 

        // advance the angle
        _angle += _speed * Time.deltaTime; 

        // calculate the x and y offsets with sin and cos
        float offsetY = Mathf.Cos(_angle) * _radius;
        float offsetZ = Mathf.Sin(_angle) * _radius;

        // apply the changes
        Vector3 centerPos = transform.parent.position; 
        transform.position = new Vector3(transform.position.x, centerPos.y + offsetY, centerPos.z + offsetZ); 

    }
}

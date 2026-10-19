using UnityEngine;
using System.Collections.Generic; 

// script to handle switching between hoops movement types after every score
[RequireComponent(typeof(CircleTranslator))]
[RequireComponent(typeof(SidewaysTranslator))]
[RequireComponent(typeof(Rotator))]
public class HoopLevelChanger : MonoBehaviour
{
    // movement scripts - should be components of of the book 
    private List<IHoopLevel> _levels = new(); 

    // keeping track 
    private int _currentType; 
    private Vector3 _spawnLocalPos;
    private Quaternion _spawnLocalRot; 
 
    private void Awake()
    {
        // cache all the movement scripts and put them in list 
        var circleTranslator = GetComponent<CircleTranslator>();
        _levels.Add(circleTranslator);
        var sidewaysTranslator = GetComponent<SidewaysTranslator>();
        _levels.Add(sidewaysTranslator);
        var rotator = GetComponent<Rotator>(); 
        _levels.Add(rotator); 
        
        // start at no movement scripts
        _currentType = _levels.Count; 
    }

    // subscribe and unsubscribe to the score event
    private void OnEnable() {
        EventHandler.OnScore += ChangeLevel;
        UIHandler.ResetGame += ResetHoop;
    }
    private void OnDisable() {
        EventHandler.OnScore -= ChangeLevel;
        UIHandler.ResetGame -= ResetHoop;
    }

    // do movement logic
    private void Update()
    {
        // if we are at or above the count do nothing
        if (_currentType >= _levels.Count) return; 

        // otherwise call move of the current level
        _levels[_currentType].Move();
    }

    public void CapturePose()
    {
        // keep track of spawn
        _spawnLocalPos = transform.localPosition;
        _spawnLocalRot = transform.localRotation; 
    }

    private void ChangeLevel()
    {   
        // reset to start transform 
        transform.localPosition = _spawnLocalPos;
        transform.localRotation = _spawnLocalRot; 

        // determine new level
        _currentType = Random.Range(0, _levels.Count+1); 
        Debug.Log($"Changed level to level {_currentType}");
    }

    private void ResetHoop()
    {
        // stop whatever level is running 
        _currentType = _levels.Count; 

        // reset the transfrom 
        transform.localPosition = _spawnLocalPos;
        transform.localRotation = _spawnLocalRot; 
    }
}

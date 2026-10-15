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
    private void OnEnable() => EventHandler.OnScore += ChangeLevel;
    private void OnDisable() => EventHandler.OnScore -= ChangeLevel;

    // do movement logic
    private void Update()
    {
        // if we are at or above the count do nothing
        if (_currentType >= _levels.Count) return; 

        // otherwise call move of the current level
        _levels[_currentType].Move();
    }

    private void ChangeLevel()
    {   
        // reset to start transform 
        this.gameObject.transform.localPosition = Vector3.up * 0.2f; 
        this.gameObject.transform.localRotation = Quaternion.Euler(-90, 0, 90);

        // determine new level
        _currentType = Random.Range(0, _levels.Count+1); 
        Debug.Log($"Changed level to level {_currentType}");
    }
}

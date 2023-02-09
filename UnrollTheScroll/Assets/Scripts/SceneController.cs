using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject _wanderingAIPrefab;
    private GameObject _wanderingAIInstance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_wanderingAIInstance == null)
        {
            _wanderingAIInstance = Instantiate(_wanderingAIPrefab) as GameObject;
            _wanderingAIInstance.transform.position = new Vector3(0, 0, 0);
            float angle = Random.Range(0, 360);
            _wanderingAIInstance.transform.Rotate(0, angle, 0);
        }
    }
}

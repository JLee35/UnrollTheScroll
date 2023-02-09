
using UnityEngine;

public class WanderingAI : MonoBehaviour
{
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private float obstacleRange = 5.0f;

    [SerializeField] private GameObject fireballPrefab;
    private GameObject _fireball;

    private Animator _animator;
    private bool _hasAnimator;

    // animation IDs
    private int _animIDSpeed;
    private int _animIDMotionSpeed;

    private bool _alive;

    private void AssignAnimationIDs()
    {
        _animIDSpeed = Animator.StringToHash("Speed");
        _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
    }

    // Start is called before the first frame update
    void Start()
    {
        _alive = true;
        
        // Get the Animator attached to the GameObject.
        _hasAnimator = TryGetComponent(out _animator);
        AssignAnimationIDs();
    }

    // Update is called once per frame
    void Update()
    {
        if (_alive)
        {
            // Trigger the Animator's Walk animation.
            if (_hasAnimator)
            {
                _animator.SetFloat(_animIDSpeed, 2);
                _animator.SetFloat(_animIDMotionSpeed, 1);
            }
        
            transform.Translate(0, 0, speed * Time.deltaTime);

            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.SphereCast(ray, 0.75f, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                if (hitObject.GetComponent<PlayerCharacter>())
                {
                    if (_fireball == null)
                    {
                        _fireball = Instantiate(fireballPrefab) as GameObject;
                        _fireball.transform.position = new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z);
                        _fireball.transform.position = _fireball.transform.TransformPoint(Vector3.forward * 1.5f);
                        _fireball.transform.rotation = transform.rotation;
                    }
                }
                else if (hit.distance < obstacleRange)
                {
                    float angle = Random.Range(-110, 110);
                    transform.Rotate(0, angle, 0);
                }
            }    
        }
    }

    public void SetAlive(bool alive)
    {
        _alive = alive;
    }

    void OnFootstep()
    {
        // This function is called by the animator class, and if it is missing
        // an exception is thrown.
    }
}

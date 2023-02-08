using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour
{
    public float speed = 3.0f;
    public float obstacleRange = 5.0f;
    
    private Animator _animator;
    private bool _hasAnimator;

    // animation IDs
    private int _animIDSpeed;
    private int _animIDGrounded;
    private int _animIDJump;
    private int _animIDFreeFall;
    private int _animIDMotionSpeed;

    private void AssignAnimationIDs()
    {
        _animIDSpeed = Animator.StringToHash("Speed");
        _animIDGrounded = Animator.StringToHash("Grounded");
        _animIDJump = Animator.StringToHash("Jump");
        _animIDFreeFall = Animator.StringToHash("FreeFall");
        _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
    }

    // Start is called before the first frame update
    void Start()
    {
        // Get the Animator attached to the GameObject.
        _hasAnimator = TryGetComponent(out _animator);
        AssignAnimationIDs();
    }

    // Update is called once per frame
    void Update()
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
            if (hit.distance < obstacleRange)
            {
                float angle = Random.Range(-110, 110);
                transform.Rotate(0, angle, 0);
            }
        }

    }
}

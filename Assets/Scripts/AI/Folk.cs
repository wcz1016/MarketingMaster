using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Folk : MonoBehaviour
{
    enum FolkState { idle, gather, leave }

    public float IdleSpeed;
    public float GatherSpeed;

    public Transform LeftShop;
    public Transform RightShop;

    public static int ActiveFolksCount = 0;

    private int _currentTargetIndex = 0;
    private Vector2 _currentTarget;
    private Rigidbody2D rigidBody;
    private FolkState _state;
    private float _deceleration, _gatherTime = 0;
    private Animator _animator;
    private List<Vector2> _targets;

    private void Awake()
    {
        ActiveFolksCount++;
        rigidBody = GetComponent<Rigidbody2D>();
        GameControl.onShowTimeStart += OnShowTimeStart;
        GameControl.onShowTimeEnd += OnShowTimeEnd;
        _animator = transform.GetChild(0).GetComponent<Animator>();
        
    }

    private void Start()
    {
        _currentTarget = _targets[0];
    }

    //IEnumerator Start()
    //{
    //    yield return new WaitForSeconds(0.5f);
    //_currentTarget = _targets[0];
    //}

    private void FixedUpdate()
    {
        switch (_state) {
            case FolkState.idle:
                MoveInPath();
                break;

            case FolkState.gather:
                GatherToShop();
                break;

            case FolkState.leave:
                MoveInPath();
                break;
        }
    }

    void MoveInPath()
    {
        Vector2 toTarget = _currentTarget - (Vector2)gameObject.transform.position;
        Vector2 velocity = Vector3.Normalize(toTarget) * IdleSpeed;

        if (((int)toTarget.x ^ (int)rigidBody.velocity.x) < 0)
        {
            _animator.SetTrigger("IsTurningAroud");
        }

        rigidBody.velocity = velocity;

        if (toTarget.magnitude < 0.1f)
        {
            //Debug.Log(_currentTargetIndex);
            if (_currentTargetIndex >= _targets.Count - 1)
            {
                ActiveFolksCount--;
                Destroy(gameObject);
                GameControl.onShowTimeEnd -= OnShowTimeEnd;
                GameControl.onShowTimeStart -= OnShowTimeStart;
            }
            else
            {
                _currentTargetIndex++;
                _currentTarget = _targets[_currentTargetIndex];
            }
        }
    }

    void GatherToShop()
    {
        Vector2 toTarget = _currentTarget - (Vector2)gameObject.transform.position;
        
        if (toTarget.magnitude > 0.1f)
        {
            float speed = GatherSpeed + _gatherTime * _deceleration;
            _animator.speed = 1 + speed;
            Vector2 velocity = Vector3.Normalize(toTarget) * speed;
            rigidBody.velocity = velocity;
        }
        else
        {
            rigidBody.velocity = Vector2.zero;
            _animator.speed = 1;
        }

        _gatherTime += Time.deltaTime;
    }

    void OnShowTimeStart(float leftProbability)
    {
        if (_state == FolkState.idle)
        {
            _state = FolkState.gather;
            _currentTarget = Random.Range(0, 1f) < leftProbability ? LeftShop.position : RightShop.position;
            float distance = (_currentTarget - (Vector2)gameObject.transform.position).magnitude;
            float duration = GameControl.Instance.ShowTimeDuration - 1f;
            _deceleration = 2 * (distance - GatherSpeed * duration) / (duration * duration);
            // 还没处理好避撞，暂时只在向商店移动时进行碰撞检测，其他时候可以相互穿过或者重合
            GetComponent<CapsuleCollider2D>().enabled = true;
        }
    }

    void OnShowTimeEnd()
    {
        if (_state == FolkState.gather)
        {
            ActiveFolksCount--;
            _state = FolkState.leave;
            _currentTarget = _targets[_currentTargetIndex];
            GetComponent<CapsuleCollider2D>().enabled = false;
            _animator.speed = 1;
        }
    }

    public void SetPath(List<Vector2> targets)
    {
        _targets = targets;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
    public class PlayerController : MonoBehaviour
    {
        // ReSharper disable once InconsistentNaming
        public string PlayerID;
        public float MinGroundNormalY = .65f;
        public float GravityModifier = 1f;
        public float JumpTakeOffSpeed = 10;
        public float MaxSpeed = 7;
        public Rigidbody2D Rb2D;
        public event Action<string, bool> PlayerToggledReady;

        private Vector2 _targetVelocity;
        private Vector2 _groundNormal;
        private Vector2 _velocity;
        private bool _grounded;
        private bool _ready;
        private ContactFilter2D _contactFilter;
        private RaycastHit2D[] _hitBuffer = new RaycastHit2D[16];
        private List<RaycastHit2D> _hitBufferList = new List<RaycastHit2D>(16);
        private SpriteRenderer _spriteRenderer;
        private Animator _animator;

        private const float MinMoveDistance = 0.001f;
        private const float ShellRadius = 0.01f;

        void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
            Rb2D = GetComponent<Rigidbody2D>();
            Rb2D.freezeRotation = true;
            _ready = false;
        }

        private void OnEnable()
        {
            StartCoroutine(Charge());
        }
        
        void Update()
        {
            _targetVelocity = Vector2.zero;
            ComputeVelocity();
            Fire();
        }

        void FixedUpdate()
        {
            _velocity += GravityModifier * Physics2D.gravity * Time.deltaTime;
            _velocity.x = _targetVelocity.x;

            _grounded = false;

            Vector2 deltaPosition = _velocity * Time.deltaTime;

            Vector2 moveAlongGround = new Vector2(_groundNormal.y, -_groundNormal.x);

            Vector2 move = moveAlongGround * deltaPosition.x;

            Movement(move, false);

            move = Vector2.up * deltaPosition.y;

            Movement(move, true);
        }

        void Movement(Vector2 move, bool yMovement)
        {
            float distance = move.magnitude;

            if (distance > MinMoveDistance)
            {
                int count = Rb2D.Cast(move, _contactFilter, _hitBuffer, distance + ShellRadius);
                _hitBufferList.Clear();

                for (int i = 0; i < count; i++)
                {
                    _hitBufferList.Add(_hitBuffer[i]);
                }

                for (int i = 0; i < _hitBufferList.Count; i++)
                {
                    Vector2 currentNormal = _hitBufferList[i].normal;
                    if (currentNormal.y > MinGroundNormalY)
                    {
                        _grounded = true;
                        if (yMovement)
                        {
                            _groundNormal = currentNormal;
                            currentNormal.x = 0;
                        }
                    }

                    float projection = Vector2.Dot(_velocity, currentNormal);
                    if (projection < 0)
                    {
                        _velocity = _velocity - projection * currentNormal;
                    }

                    float modifiedDistance = _hitBufferList[i].distance - ShellRadius;
                    distance = modifiedDistance < distance ? modifiedDistance : distance;
                }
            }

            Rb2D.position = Rb2D.position + move.normalized * distance;
        }

        private void ComputeVelocity()
        {
            Vector2 move = Vector2.zero;

            move.x = Input.GetAxis("Horizontal" + PlayerID);

            if (Input.GetButtonDown("Jump" + PlayerID) && _grounded)
            {
                _velocity.y = JumpTakeOffSpeed;
            }

            //Below's for sprite and animator controls, no idea what they do.
            /*
            //bool flipSprite = (_spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0.01f));
            //if (flipSprite)
            //{
            //    _spriteRenderer.flipX = !_spriteRenderer.flipX;
            //}

            //_animator.SetBool("grounded", _grounded);
            //_animator.SetFloat("velocityX", Mathf.Abs(_velocity.x) / MaxSpeed);
            */

            _targetVelocity = move * MaxSpeed;
        }

        private void Fire()
        {
            if (Input.GetButtonDown("Fire" + PlayerID))
            {
                _ready = !_ready;
                if (PlayerToggledReady != null)
                {
                    Debug.Log("Ready! " + PlayerID);
                    PlayerToggledReady.Invoke(PlayerID, _ready);
                }
            }
        }

        public ushort Health = 0;
        
        private bool _charge;
        public ushort ChargeAmount = 0;
        public float ChargeRate = 0.5f;
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.CompareTag("Charging"))
            {
                _charge = true;
            }
            else if (other.collider.CompareTag("Hand"))
            {
                if (other.transform.childCount > 0)
                {
                    other.transform.GetComponentInChildren<BaseSpell>();
                }
                else
                {
                    GetElectrocuted();
                }
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.collider.CompareTag("Charging"))
            {
                _charge = false;
            }
        }

        public void GetElectrocuted()
        {

        }

        private IEnumerator Charge()
        {
            yield return new WaitUntil(() => _charge);
            ChargeAmount++;
            yield return new WaitForSecondsRealtime(ChargeRate);
            StartCoroutine(Charge());
        }

        private void OnTriggerEnter2D(Collider2D obj)
        {
            if (obj.CompareTag("SpellItem"))
            {
                SpellItem spell = obj.gameObject.GetComponent<SpellItem>();

                if(spell == null)
                {
                    Debug.LogError("SpellItem component is null");
                    return;
                }
                spell.SpellCollected(PlayerID, spell.SpellType);
            }
        }
    }
}

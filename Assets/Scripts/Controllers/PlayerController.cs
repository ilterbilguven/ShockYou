using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    [Serializable]
    public enum PlayerTypes
    {
        Birdie = 1,
        Paladin = 2,
        Ghost = 3,
        Otherthing = 4
    }
        
    public class PlayerController : MonoBehaviour
    {
        public Transform CanvasContainer;
        public Transform PhysicsContainer;
        // ReSharper disable once InconsistentNaming
        public string PlayerID;
        public float MinGroundNormalY = .65f;
        public float GravityModifier = 1f;
        public float JumpTakeOffSpeed = 10;
        public float MaxSpeed = 7;
        public Rigidbody2D Rb2D;
        public event Action<string, bool> PlayerToggledReady;
        public PlayerTypes PlayerType;
        [HideInInspector]
        public ushort Score = 0;
        public Animator Animator;
        public SpriteRenderer _spriteRenderer;

        [SerializeField] private Vector2 _targetVelocity;
        private Vector2 _groundNormal;
        [SerializeField] private Vector2 _velocity;
        private bool _grounded;
        private bool _ready;
        private bool _go;
        private ContactFilter2D _contactFilter;
        private RaycastHit2D[] _hitBuffer = new RaycastHit2D[16];
        private List<RaycastHit2D> _hitBufferList = new List<RaycastHit2D>(16);

        public Text ChargeText;

        private const float MinMoveDistance = 0.001f;
        private const float ShellRadius = 0.01f;

        public bool Flip;

        public HandController PlayerHand;
        public ushort Health = 0;
        [SerializeField] private bool _charge;
        public ushort ChargeAmount = 0;
        public ushort ChargeLimit = 100;
        public float ChargeRate = 0.5f;

        void Awake()
        {
            //Animator = CanvasContainer.GetComponent<Animator>();
            Rb2D.constraints = RigidbodyConstraints2D.FreezeAll;
            _ready = false;
            //Animator.Play("Idle");
            GameController.Instance.LevelStarted += LevelStarted;
        }

        private void LevelStarted()
        {
            if (_ready)
            {
                Rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            }

            _go = _ready;
        }

        void Update()
        {
            Fire();

            if (!_go) return;
            _targetVelocity = Vector2.zero;
            ComputeVelocity();

            ChargeText.text = ChargeAmount.ToString();
        }

        private void OnEnable()
        {
            StartCoroutine(Charge());
        }

        void FixedUpdate()
        {
            if (!_go) return;
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

            if (Animator != null)
            {
                SetCurrentState(Mathf.Abs(_velocity.x) > Mathf.Epsilon ? MovementType.Walking : MovementType.Idle);

                Animator.SetBool("Grounded", _grounded);
            }

            if (move.x != 0)
            {
                Flip = move.x < Mathf.Epsilon;
                PhysicsContainer.localScale = new Vector3(Flip ? -1 : 1,1,1);
            }

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

        private void OnCollisionEnter2D(Collision2D other)
        {
            

        }

        private void OnCollisionStay2D(Collision2D other)
        {
            _charge = other.collider.CompareTag("Charging") && Mathf.Abs(_velocity.x) > Mathf.Epsilon;
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.collider.CompareTag("Charging"))
            {
                _charge = false;
            }
        }

        public void GetElectrocuted(ushort damage)
        {
            ChargeAmount -= damage;
        }

        private IEnumerator Charge()
        {
            yield return new WaitUntil(() => _charge);
            AddCharge(1);
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
                spell.SpellCollected(this, spell.SpellType);
            }
            else if (obj.CompareTag("Hand"))
            {
                if (obj.transform.childCount > 0)
                {
                    GameController.Instance.SpellController.StaticSpell(this, obj.gameObject.GetComponentInParent<PlayerController>());
                }
                else
                {
                    Debug.LogError("It shouldnt enter here");
                }
            }
            else if (obj.CompareTag("Spell"))
            {
                PlayerController sender = obj.gameObject.GetComponentInParent<PlayerController>();
                PlayerController receiver = this;

                BaseSpell usedSpell = obj.gameObject.GetComponentInParent<BaseSpell>();
                if (usedSpell.SpellType == SpellType.Static)
                {
                    StaticSpell asd = (StaticSpell)usedSpell;
                    asd.UseSpell();
                }

                if (usedSpell.SpellType == SpellType.Battery)
                {
                    BatterySpell asd = (BatterySpell)usedSpell;
                    asd.UseSpell();
                }

                if (usedSpell.SpellType == SpellType.Taser)
                {
                    TaserSpell asd = (TaserSpell)usedSpell;
                    asd.UseSpell(sender, receiver);
                }

                if (usedSpell.SpellType == SpellType.Random)
                {
                    RandomSpell asd = (RandomSpell)usedSpell;
                    asd.UseSpell();
                }

                //Destroy(other.gameObject);
            }
        }

        public Sprite GetPlayerSpriteByType()
        {
            Sprite sprite; 
            if(!GameController.Instance.playerPics.TryGetValue(PlayerType, out sprite))
            {
                Debug.LogError("Player sprite not found.");
                return null;
            }

            return sprite;
        }

        public void SetCurrentState(MovementType type)
        {
            Animator.SetInteger("State", (int)type);
        }

        public void AddCharge(ushort amount)
        {
            ChargeAmount += amount;
            ChargeAmount = (ushort)Mathf.Clamp(ChargeAmount, 0, ChargeLimit);
        }

        public void RemoveSpell()
        {
            PlayerHand.RemoveSpell();
        }

        public void Knockback(ushort amount)
        {
            Rb2D.AddForce(new Vector2(Flip ? amount : -amount,0), ForceMode2D.Impulse);
        }
    }
}

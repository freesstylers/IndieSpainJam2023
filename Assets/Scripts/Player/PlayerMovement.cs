using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

namespace Player
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class PlayerMovement : MonoBehaviour
    {
        private Camera cam;
        private NavMeshAgent player;
        private Animator playerAnim;

        [SerializeField]
        private bool canMove = true;
        [SerializeField]
        private bool isMoving = false;
        [SerializeField]
        private bool isInteracting = false;

        public Vector3 initPosition;
        public bool initPosSet = false;

        void Start()
        {
            cam = Camera.main;
            player = GetComponent<NavMeshAgent>();
            playerAnim = GetComponent<Animator>();
            player.updateRotation = false;
        }
        private void OnEnable()
        {
            ResetPosition();
        }
        void Update()
        {
            if (!isInteracting && canMove && !isMoving)
            {
                if (Input.GetMouseButton(0))
                {
                    Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hitPoint;

                    if (!CheckUIHit() && Physics.Raycast(ray, out hitPoint))
                    {
                        player.SetDestination(hitPoint.point);
                        timeCount = 0.0f;
                        isMoving = true;
                        StartCoroutine(movementAnimation());
                    }
                }
            }

            Animation();
        }

        bool CheckUIHit()
        {
            var pointerEventData = new PointerEventData(EventSystem.current);
            
            return pointerEventData.selectedObject != null;
        }

        private void Animation()
        {
            if (playerAnim == null)
                return;

            if (player.velocity != Vector3.zero)
            {
                //Que se mueva y tal
            }
            else
            {
                //Idle
            }

        }

        public void SetMove(bool set)
        {
            canMove = set;
        }

        public bool GetCanMove() { return canMove; }

        public void SetInteracting(bool set)
        {
            isInteracting = set;
        }

        public bool GetInteracting() { return isInteracting; }

        bool rotateLeft = true;
        
        [SerializeField]
        float timeCount = 0.0f;

        [SerializeField]
        float MaxAngleDeflection = 10.0f;
        [SerializeField]
        float SpeedOfPendulum = 50.0f;

        IEnumerator movementAnimation()
        {
            while (player.remainingDistance > player.stoppingDistance)
            {
                float angle = MaxAngleDeflection * Mathf.Sin(timeCount * SpeedOfPendulum);
                transform.localRotation = Quaternion.Euler(0, 0, angle);

                timeCount += Time.deltaTime;

                yield return null;
            }
            isMoving = false;
        }

        private void OnMouseEnter()
        {
            canMove = false;
        }

        private void OnMouseExit()
        {
            canMove = true;
        }

        public void ResetPosition()
        {
            if(player !=null)
                player.isStopped = true;

            if (initPosSet)
                transform.position = initPosition;
            else
            {
                initPosition = transform.position;
                initPosSet = true;
            }

            isMoving = false;
            if (player != null)
                player.isStopped = false;

            DialogueManager.instance_.player = gameObject;
        }
    }
}
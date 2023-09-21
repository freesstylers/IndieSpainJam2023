using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Player
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class PlayerMovement : MonoBehaviour
    {
        private Camera cam;
        private NavMeshAgent player;
        private Animator playerAnim;

        private bool canMove = true;
        private bool isInteracting = false;

        void Start()
        {
            cam = Camera.main;
            player = GetComponent<NavMeshAgent>();
            playerAnim = GetComponent<Animator>();
            player.updateRotation = false;
        }

        void Update()
        {
            if (!isInteracting && canMove)
            {
                if (Input.GetMouseButton(0))
                {
                    timeCount = 0.0f;
                    Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hitPoint;

                    if (Physics.Raycast(ray, out hitPoint))
                    {
                        player.SetDestination(hitPoint.point);
                        StartCoroutine(movementAnimation());
                    }
                }
            }

            Animation();
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
        float SpeedOfPendulum = 0.5f;

        IEnumerator movementAnimation()
        {
            while (player.remainingDistance > player.stoppingDistance)
            {
                float angle = MaxAngleDeflection * Mathf.Sin(timeCount * SpeedOfPendulum);
                transform.localRotation = Quaternion.Euler(0, 0, angle);

                timeCount += Time.deltaTime;

                yield return null;
            }
        }
    }
}
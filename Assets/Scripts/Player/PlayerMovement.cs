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

        void Start()
        {
            cam = Camera.main;
            player = GetComponent<NavMeshAgent>();
            playerAnim = GetComponent<Animator>();
            player.updateRotation = false;
        }

        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitPoint;

                if (Physics.Raycast(ray, out hitPoint))
                    player.SetDestination(hitPoint.point);
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
    }
}
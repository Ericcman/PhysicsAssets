using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controller
{
    [RequireComponent(typeof(CreatureMover))]
    public class MoveCreatureAI : MonoBehaviour
    {
        [Header("Wandering Settings")]
        [SerializeField]
        private float directionChangeInterval = 3f;
        [SerializeField]
        private float wanderRadius = 1f;
        [SerializeField]
        private bool run = false;

        private CreatureMover m_Mover;
        private float timer;
        private Vector2 currentDirection;
        private Vector3 target;

        private void Awake()
        {
            m_Mover = GetComponent<CreatureMover>();
            PickNewDirection();
        }

        private void Update()
        {
            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                PickNewDirection();
            }

            target = transform.position + new Vector3(currentDirection.x, 0f, currentDirection.y);
            m_Mover.SetInput(currentDirection, target, run, false);
        }

        private void PickNewDirection()
        {
            timer = directionChangeInterval;

            Vector2 randomDirection = Random.insideUnitCircle.normalized * Random.Range(0.5f, 1f);
            currentDirection = Vector2.ClampMagnitude(randomDirection, 1f);
        }
    }
}


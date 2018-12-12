using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

namespace Complete
{
    public class TankAgent : Agent
    {
        TankMovement tankMovement;
        TankShooting tankShooting;

        //public GameObject myAcademyObj;
        //BananaAcademy myAcademy;

        //public GameObject area;
        //BananaArea myArea;

        Rigidbody agentRb;
        
        public bool contribute;
        private RayPerception rayPer;
        public bool useVectorObs;

        public override void InitializeAgent()
        {
            tankMovement = GetComponent<TankMovement>();
            tankShooting = GetComponent<TankShooting>();
            base.InitializeAgent();
            agentRb = GetComponent<Rigidbody>();
            Monitor.verticalOffset = 1f;
            //myArea = area.GetComponent<BananaArea>();
            rayPer = GetComponent<RayPerception>();
            //myAcademy = myAcademyObj.GetComponent<BananaAcademy>();
        }

        public override void CollectObservations()
        {
            AddVectorObs(0);
            if (useVectorObs)
            {
                float rayDistance = 50f;
                float[] rayAngles = { 20f, 90f, 160f, 45f, 135f, 70f, 110f };
                string[] detectableObjects = { "tank", "boundarie", "ground", "obstacle" };
                AddVectorObs(rayPer.Perceive(rayDistance, rayAngles, detectableObjects, 0f, 0f));
                Vector3 localVelocity = transform.InverseTransformDirection(agentRb.velocity);
                AddVectorObs(localVelocity.x);
                AddVectorObs(localVelocity.z);
            }
        }

        public override void AgentAction(float[] vectorAction, string textAction)
        {
            var Vertical = (int)vectorAction[0];
            var Horizontal = (int)vectorAction[1];
            var FireButton = (int)vectorAction[2];

            switch (Vertical)
            {
                case 0:
                    tankMovement.m_MovementInputValue = 0;
                    break;
                case 1:
                    tankMovement.m_MovementInputValue = 1;
                    break;
                case 2:
                    tankMovement.m_MovementInputValue = -1;
                    break;
            }

            switch (Horizontal)
            {
                case 0:
                    tankMovement.m_TurnInputValue = 0;
                    break;
                case 1:
                    tankMovement.m_TurnInputValue = 1;
                    break;
                case 2:
                    tankMovement.m_TurnInputValue = -1;
                    break;
            }

            switch (FireButton)
            {
                case 0:
                    tankShooting.m_GetFireButton = false;
                    break;
                case 1:
                    tankShooting.m_GetFireButton = true;
                    break;
            }
        }

        public override void AgentOnDone()
        {

        }
    }
}
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
        public Vector2 relativePos;
        public GameObject opponent;

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
            if (useVectorObs)
            {
                //float rayDistance = 80f;
                //float[] rayAngles = { 20f, 90f, 160f, 45f, 135f, 70f, 110f };
                //string[] detectableObjects = { "tank", "boundarie", "ground", "obstacle" };
                //AddVectorObs(rayPer.Perceive(rayDistance, rayAngles, detectableObjects, 1f, 1f));
                //AddVectorObs(rayPer.Perceive(rayDistance, rayAngles, detectableObjects, 1f, -10f));
                //Vector3 localVelocity = transform.InverseTransformDirection(agentRb.velocity);
                //AddVectorObs(localVelocity.x);
                //AddVectorObs(localVelocity.z);

                Vector3 opponent_position = transform.InverseTransformPoint(opponent.transform.position);
                relativePos = new Vector2(opponent_position.x, opponent_position.z);
                AddVectorObs(relativePos);
                //AddVectorObs(tankShooting.CurrentLaunchForce);

                //AddVectorObs(opponent_position.x);
                //AddVectorObs(opponent_position.z);
            }
        }

        private void FixedUpdate()
        {
            //AddReward(-0.01f);
        }

        public override void AgentReset()
        {
            base.AgentReset();
        }

        public override void AgentAction(float[] vectorAction, string textAction)
        {
            //tankMovement.m_MovementInputValue = vectorAction[0];
            //tankMovement.m_TurnInputValue = vectorAction[0];
            //tankShooting.m_GetFireButton = (vectorAction[1] > 0);

            //var Vertical = (int)vectorAction[0];
            var Horizontal = (int)vectorAction[0];
            var FireButton = (int)vectorAction[1];
            //Time penalty
           
            //switch (Vertical)
            //{
            //    case 0:
            //        tankMovement.m_MovementInputValue = 0;
            //        break;
            //    case 1:
            //        tankMovement.m_MovementInputValue = 1;
            //        break;
            //    case 2:
            //        tankMovement.m_MovementInputValue = -1;
            //        break;
            //}

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
            Debug.Log("Random rotation set");
            GetComponent<TankMovement>().TankRigidbody.rotation = Quaternion.Euler(0f, Random.Range(0.0f,360.0f), 0f);
        }
    }
}

//activate ml-agents
//mlagents-learn config/tank_trainer_config.yaml --run-id=tankRun --train
//tensorboard --logdir=summaries
//Decision Frequency = 5
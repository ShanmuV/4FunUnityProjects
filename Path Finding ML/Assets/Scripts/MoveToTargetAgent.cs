using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class MoveToTargetAgent : Agent
{

    [SerializeField] private Transform target;
    [SerializeField] private SpriteRenderer Background;



    public override void OnEpisodeBegin()
    {
        target.localPosition = new Vector3(Random.Range(-4f,4f), Random.Range(-3f,3f));
        transform.localPosition = new Vector3(Random.Range(-4f,4f), Random.Range(-3f,3f));

    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation((Vector2) transform.localPosition);
        sensor.AddObservation((Vector2) target.localPosition);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveY = actions.ContinuousActions[1];

        float movementSpeed = 5f;

        transform.localPosition += new Vector3(moveX, moveY) * Time.deltaTime * movementSpeed;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Target"))
        {
            AddReward(10f);
            Background.color = Color.green;
            EndEpisode();
        }
        else if (collision.CompareTag("Wall"))
        {
            AddReward(-2f);
            Background.color = Color.red;
            EndEpisode();
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
    }

}

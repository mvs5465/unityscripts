using UnityEngine;
using System.Collections;

namespace Bunker
{
	public class FollowCamera : MonoBehaviour
	{

		public float interpVelocity;
		private float minDistance;
		private float followDistance;
		private GameObject target;
		private Vector3 offset;
		Vector3 targetPos;
		// Use this for initialization
		void Start()
		{
			target = FindAnyObjectByType<Player>().gameObject;
			targetPos = transform.position;
		}

		// Update is called once per frame
		void FixedUpdate()
		{
			if (target)
			{
				Vector3 posNoZ = transform.position;
				posNoZ.z = target.transform.position.z;

				Vector3 targetDirection = (target.transform.position - posNoZ);

				interpVelocity = targetDirection.magnitude * 5f;

				targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime);

				transform.position = Vector3.Lerp(transform.position, targetPos + offset, 0.25f);

			}
		}
	}
}
using UnityEngine;

public class SwingAxe : MonoBehaviour
{
	public AnimationCurve swingPattern;
	public float halfArcPerSecond = 25f;
	public float angleForAudio = 2f;
	float timeModifier;
	float elapsedTime;
	float swingSize;
	int direction;


	void Start ()
	{

		float currentAngle = transform.rotation.eulerAngles.z;

		timeModifier = Mathf.Abs(currentAngle) / halfArcPerSecond;
		swingSize = Mathf.Abs(currentAngle);

		if (currentAngle > 0f)
			direction = 1;
		else
			direction = -1;
	}

	void Update()
	{
		elapsedTime += Time.deltaTime / timeModifier;

		if (elapsedTime >= 1f)
			elapsedTime -= 1f;

		float angle = swingPattern.Evaluate(elapsedTime) * swingSize * direction;


		Vector3 rot = transform.rotation.eulerAngles;
		rot.z = angle;
		transform.rotation = Quaternion.Euler(rot);
	}
}

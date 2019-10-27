
using System.Collections;
using UnityEngine;

public class Present : MonoBehaviour
{
	public string PresentText;

	float SecondsSinceLastShake;
	float SecondsUntilNextShakeAnimation = 2f;

	void Start()
	{
		SecondsSinceLastShake = Random.Range(0, SecondsUntilNextShakeAnimation);
	}

	void Update()
	{
		SecondsSinceLastShake += Time.deltaTime;

		if (SecondsSinceLastShake >= SecondsUntilNextShakeAnimation)
		{
			StartCoroutine(Shake());
			SecondsSinceLastShake = 0;
			SecondsUntilNextShakeAnimation = Random.Range(0.5f, 2f);
		}
	}

	Vector3 GetNextShakeRotationDirection()
	{
		
		int axisToShake = Random.Range(0, 3);
		Vector3 shakeRotationVector = Vector3.zero;
		

		switch (axisToShake)
		{
			case 0:
				shakeRotationVector.x = 1;
				break;
			case 1:
				shakeRotationVector.y = 1;
				break;
			case 2:
				shakeRotationVector.z = 1;
				break;
		}

		return shakeRotationVector;
	}

	IEnumerator Shake()
	{
		var initialRotation = transform.rotation;
		Vector3 shakeRotationVector = GetNextShakeRotationDirection();

		float angleMultiplier = Random.Range(5f, 20f);
		float rotationAngle = Random.Range(0, 2) == 0 ? -angleMultiplier : angleMultiplier;

		float framesToShake = 10;
		float rotationPerFrame = rotationAngle / framesToShake;

		for (int i = 0; i < framesToShake; i++)
		{
			transform.Rotate(shakeRotationVector, rotationPerFrame);
			yield return null;
		}

		for (int i = 0; i < framesToShake; i++)
		{
			transform.Rotate(shakeRotationVector, -rotationPerFrame);
			yield return null;
		}

		transform.SetPositionAndRotation(transform.position, initialRotation);
	}
}

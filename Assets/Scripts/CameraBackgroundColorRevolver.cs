
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraBackgroundColorRevolver : MonoBehaviour
{
	Camera Camera;
	Color CurrentColor = Color.white;
	float ColorFrequencyMultiplier = 0;

	float SecondsSinceColorUpdate = 0;

	const float SecondsPerColorUpdate = 0.02f;

    void Start()
    {
		Camera = gameObject.GetComponent<Camera>();
    }

	void Update()
	{
		SecondsSinceColorUpdate += Time.deltaTime;

		if (SecondsSinceColorUpdate >= SecondsPerColorUpdate)
		{
			UpdateNextCameraColor();
			SecondsSinceColorUpdate = 0;
		}
	}

	void UpdateNextCameraColor()
	{
		const float multiplierIncrememntAmount = 0.02f;

		ColorFrequencyMultiplier = ColorFrequencyMultiplier + multiplierIncrememntAmount;

		CurrentColor.r = GetNextColor(offset: 0);
		CurrentColor.g = GetNextColor(offset: 2);
		CurrentColor.b = GetNextColor(offset: 4);

		Camera.backgroundColor = CurrentColor;
	}

	float GetNextColor(int offset)
	{
		const float frequency = 0.3f;
		return (Mathf.Sin(frequency * ColorFrequencyMultiplier + offset) * 127f + 128f) / 255f;
	}
}

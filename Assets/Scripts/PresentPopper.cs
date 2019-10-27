using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PresentPopper : MonoBehaviour
{
	ParticleSystem ParticleSystem;

	public Image PresentTextPanel;
	public TextMeshProUGUI PresentText;

	static bool PresentPopAnimationLock = false;

	void Start()
    {
		if (ParticleSystem == null)
		{
			ParticleSystem = FindObjectOfType<ParticleSystem>();
		}
	}

    void Update()
    {
	    if (PresentPopAnimationLock)
	    {
			return;
	    }

	    if (Input.GetMouseButtonUp(0))
	    {
		    TryPoppingPresentFromScreenPosition(Input.mousePosition);
		}

	    foreach (var touch in Input.touches)
	    {
			TryPoppingPresentFromScreenPosition(touch.position);
		}
    }

	void TryPoppingPresentFromScreenPosition(Vector2 position)
	{
		Ray ray = Camera.main.ScreenPointToRay(position);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit))
		{
			var present = hit.collider.GetComponent<Present>();
			if (present != null)
			{
				StartCoroutine(PopPresent(present));
			}
		}
	}

	IEnumerator PopPresent(Present present)
	{
		PresentPopAnimationLock = true;
		var particleSystemPosition = present.transform.position;
		ParticleSystem.transform.position = particleSystemPosition;

		ParticleSystem.Play();

		while (ParticleSystem.isPlaying)
		{
			yield return null;
		}

		PresentTextPanel.transform.localScale = Vector2.zero;
		PresentTextPanel.gameObject.SetActive(true);
		PresentText.text = present.PresentText;

		int textAnimationFrames = 60;
		Vector3 scaleIncrement = Vector3.one / textAnimationFrames;

		for (int i = 0; i < textAnimationFrames; i++)
		{
			PresentTextPanel.transform.localScale += scaleIncrement;
			yield return null;
		}

		PresentTextPanel.transform.localScale = Vector3.one;
	}

	public void DismissPresent()
	{
		PresentTextPanel.gameObject.SetActive(false);
		PresentPopAnimationLock = false;
	}
}

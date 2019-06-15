using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Float : MonoBehaviour {

	[SerializeField] protected float distance;
	[SerializeField] protected float speed;

	protected Vector3 startPoint;
	protected bool up;

	private void Awake() {
		startPoint = transform.position;	
	}

	private void Update() {
		transform.position += (up ? Vector3.up : Vector3.down) * speed * Time.deltaTime;

		if (Vector3.Distance(startPoint, transform.position) >= distance / 2f) {
			up = !up;
		}
	}
}

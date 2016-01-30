using UnityEngine;
using System.Collections;

public class LightCheck : MonoBehaviour {
	public GameObject previousMirror = null;
	public GameObject nextMirror = null;
	public double angleVariance = 10;

	private double minAngle, maxAngle;

	// Use this for initialization
	void Start () {
		if (nextMirror == null) {
			// if this object is the target/light sink.
			transform.rotation = getAngle(transform.position, previousMirror.transform.position);

			minAngle = transform.rotation.eulerAngles.z - angleVariance;
			maxAngle = transform.rotation.eulerAngles.z + angleVariance;
		} else if (previousMirror == null) {
			// if this object is the light source.
			transform.rotation = getAngle(transform.position, nextMirror.transform.position);

			minAngle = transform.rotation.eulerAngles.z - angleVariance;
			maxAngle = transform.rotation.eulerAngles.z + angleVariance;
		}
		else {
			// if this object is not the light source or sink.
			Vector2 toPrev = previousMirror.transform.position - transform.position;
			Vector2 toNext = nextMirror.transform.position - transform.position;
			double angle = Vector2.Angle(toPrev, toNext);
			double angleToPrev = Vector2.Angle(transform.up, toPrev);
			double angleToNext = Vector2.Angle(transform.up, toNext);

			if(angleToPrev <= angleToNext) angle = angleToPrev + angle/2;
			else angle = angleToPrev - angle/2;

			minAngle = angle - angleVariance;
			maxAngle = angle + angleVariance;

			//Debug.Log(angle);
			//Debug.Log(angleToPrev);
			//Debug.Log(angleToNext);
		}
	}
	
	// Update is called once per frame
	void Update () {
	}

	/*
	 * check that this mirror is within tolerance.
	 */
	public bool inTolerance() { return transform.rotation.eulerAngles.z >= minAngle && transform.rotation.eulerAngles.z <= maxAngle; }

	/*
	 * check that this and all prier mirrors are within tolerance angle to reflect light from source.
	 */
	public bool inToleranceFromSource() {
		if (previousMirror != null)
			return inTolerance () && previousMirror.GetComponent<LightCheck> ().inToleranceFromSource();
		else return inTolerance();
	}

	private Quaternion getAngle(Vector3 sourcePos, Vector3 targetPos) {
		Vector3 dir = targetPos - sourcePos;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		return Quaternion.Euler(0f, 0f, angle - 90);
	}
}

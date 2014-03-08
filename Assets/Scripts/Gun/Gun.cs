using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {
	
	public GameObject Proj;
	public float RotationSpeed = 1;

	public float FireRate = 0.05f;
	private float fireTimer = 0.0f;

	private string pNumber;
	
	// Use this for initialization
	void Start () {
		
		pNumber = transform.parent.GetComponent<PlayerController>().PNumber;
	}
	
	// Update is called once per frame
	void Update () {

		RotateGun();
		Fire();
	}

	void RotateGun()
	{
		float axisHorizontal = Input.GetAxis("RightAnalogX_"+pNumber);
		float axisVertical = Input.GetAxis("RightAnalogY_"+pNumber);
		float angle = (Mathf.Atan2(axisHorizontal, axisVertical) * Time.deltaTime) *Mathf.Rad2Deg;

		if (angle != 0)
		{

			transform.localEulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, angle);
			transform.localRotation = Quaternion.Euler(0.0f,0, (Mathf.Atan2(axisHorizontal, axisVertical) * Mathf.Rad2Deg));
			transform.RotateAround(transform.parent.position, new Vector3(0, 0, 1.0f), angle);
		}
	}

	void Fire()
	{
		fireTimer += Time.deltaTime;

		if (Input.GetAxis("RightTrigger_"+ pNumber) < 0 && fireTimer >= FireRate) {
			// Instantiate the projectile at the position and rotation of this transform
			GameObject proj = (GameObject) Instantiate(Proj, transform.position, Quaternion.identity);
			
			// Lol roatation :D
			proj.transform.rotation = transform.rotation;
			fireTimer = 0.0f;
		}
	}
}

using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {
	
	private GameObject Proj;
	public GameObject NormalProj;
	public GameObject SmallStunningProj;
	public GameObject BigStunningProj;
	public float RotationSpeed = 1;

	public float FireRate = 0.05f;
	private float fireTimer = 0.0f;

	private float projectileTimer = 0.0f;

	private float fireRateTimer = 0.0f;
	private float baseFireRate;

	private string pNumber;

	public PlayerController ParentPlayerController;

	// C'est tellemet hot des variables globales
	private float prevAngle = 0;
	private bool rotating = false;
	

	// Use this for initialization
	void Start () {
		ParentPlayerController = transform.parent.gameObject.GetComponent<PlayerController>();
		pNumber = transform.parent.GetComponent<PlayerController>().PNumber;
		Proj = NormalProj;
		baseFireRate = FireRate;
	}
	
	// Update is called once per frame
	void Update () {
		if(!ParentPlayerController.IsStunned()){

			RotateGun();
			Fire();
		}
		VerifyProjectileTimer();
		VerifyFireRateTimer();

	}

	public void ChangeProjectile(int projNb)
	{
		switch(projNb)
		{
		case 0:
			Proj = NormalProj;
			break;
		case 1:
			Proj = SmallStunningProj;
			SetProjectileTimer(4.0f);
			break;
		case 2:
			Proj = BigStunningProj;
			SetProjectileTimer(6.0f);
			break;
		}
	}

	void SetProjectileTimer(float time)
	{
		projectileTimer = time;
	}

	void SetFireRateTimer(float time)
	{
		fireRateTimer = time;
	}

	void VerifyProjectileTimer()
	{
		if(projectileTimer > 0.0f){
			projectileTimer -= Time.deltaTime;
			if(projectileTimer <= 0)
			{
				Proj = NormalProj;
				projectileTimer = 0.0f;
			}

		}
	}
	void VerifyFireRateTimer()
	{
		if(fireRateTimer > 0.0f){
			fireRateTimer -= Time.deltaTime;
			if(fireRateTimer <= 0)
			{
				FireRate = baseFireRate;
				fireRateTimer = 0.0f;
			}
			
		}
	}
	
	public void ApplyBonusFireRate(float bonus, float time)
    {
        FireRate *= bonus;
        SetFireRateTimer(time);
    }
    
    void RotateGun()
    {
        float axisHorizontal = Input.GetAxisRaw("RightAnalogX_"+pNumber);
		//Debug.Log("X: "+axisHorizontal);
		float axisVertical = Input.GetAxisRaw("RightAnalogY_"+pNumber);
		//Debug.Log("Y: "+axisVertical);



		if (rotating)
		{

			if (Mathf.Abs(axisHorizontal) < 0.01 || Mathf.Abs(axisVertical) < 0.01)
			{
				rotating = false;
				return;
			}
			float angle = Mathf.Atan2(axisVertical, axisHorizontal) * Mathf.Rad2Deg;
			//Debug.Log("Angle: " + angle);

			transform.RotateAround(transform.parent.position, Vector3.up, -(angle - prevAngle));

			transform.localRotation = Quaternion.Euler(0.0f,0, -angle);
			prevAngle = angle;

		}
		else
		{
			if (Mathf.Abs(axisHorizontal) > 0.1 || Mathf.Abs(axisVertical) > 0.1)
			{
				prevAngle = Mathf.Atan2(axisVertical, axisHorizontal) * Mathf.Rad2Deg;
				rotating = true;
			}
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

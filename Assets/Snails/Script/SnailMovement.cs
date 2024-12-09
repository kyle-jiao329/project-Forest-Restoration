using UnityEngine;
using System.Collections;

public class SnailMovement : MonoBehaviour {

	public float SnailSpeed;
	public GameObject SnailROOT_OBJ;
	public Animator SnailAnimatorController;

	public SkinnedMeshRenderer SnailMeshRenderer;
	public Material[] SnailMaterials;
	private int CurrentSnailMaterial;

	public ParticleSystem SnailSmokeParticles;
	private bool ParticleEmissionIsON;
	

	
	void Start(){
		ParticleEmissionIsON = false;
		SnailSmokeParticles.emissionRate = 0;

	}
	
	void Update(){

		if (Input.GetKeyDown (KeyCode.Space)) {
			SwitchMaterial();	

		}
	}

	void FixedUpdate(){

		this.GetComponent<Rigidbody>().AddForce (new Vector3 (Input.GetAxis ("Horizontal"), 0f, Input.GetAxis ("Vertical")).normalized * Time.deltaTime * SnailSpeed);

		if (this.GetComponent<Rigidbody>().velocity.sqrMagnitude > 0.1f) {
			SnailROOT_OBJ.transform.rotation = Quaternion.LookRotation (this.GetComponent<Rigidbody>().velocity);

			if (ParticleEmissionIsON == false) {
					ParticleEmissionIsON = true;
					SnailSmokeParticles.emissionRate = 6;
			}
		} else {
			if (ParticleEmissionIsON == true) {
				ParticleEmissionIsON = false;
				SnailSmokeParticles.emissionRate = 0;
			}
		}

		SnailAnimatorController.SetFloat ("SnailSpeed", this.GetComponent<Rigidbody>().velocity.sqrMagnitude);

	}

	public void SwitchMaterial(){

		CurrentSnailMaterial += 1;
		if (CurrentSnailMaterial >= SnailMaterials.Length) {
			CurrentSnailMaterial = 0;		
		}

		SnailMeshRenderer.material = SnailMaterials [CurrentSnailMaterial];

	}


	
}

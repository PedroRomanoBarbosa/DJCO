using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {
	private UnityStandardAssets.ImageEffects.Vortex vortex;
	private bool effect;
	private float effectTime;
    private GameGlobals game;


    public float effectSpeed;
	public float effectDuration;
	public float effectFadeSpeed;

	void Start ()
    {
        game = GameObject.Find("GameController").GetComponent<GameGlobals>();
        vortex = GameObject.Find("Main Camera").GetComponent<UnityStandardAssets.ImageEffects.Vortex>();
		effect = false;
	}

	void Update() {
        if(game.isMoving)
            if (effect) {
			    effectTime += Time.deltaTime;
			    if (effectTime >= effectDuration) {
				    effect = false;
				    effectTime = 0;
			    } else {
				    vortex.angle += effectSpeed * Time.deltaTime;
			    }
		    } else {
			    if (vortex.angle > 0) {
				    vortex.angle -= effectFadeSpeed * Time.deltaTime;
			    } else {
				    vortex.angle = 0;
			    }
		    }
	}
	
	void OnTriggerEnter (Collider other) {
		if (other.gameObject.CompareTag("Beer")) {
			//TODO change this. SoundScript.Instance.MakeCoinSound();
			Destroy(other.gameObject);
			effect = true;
			effectTime = 0;
		}
	}
}

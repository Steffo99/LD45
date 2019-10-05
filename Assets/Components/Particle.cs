using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Gravitation))]
public class Particle : MonoBehaviour {
    protected int _tier = 0;

    public new Rigidbody2D rigidbody;
    public Gravitation gravitation;
    public GameController gameController;
    public Merger merger;
    public Collider2D particleCollider;
    public Collider2D mergeCollider;

    public int Tier {
        get {
            return _tier;
        }
        set {
            _tier += 1;
            Scale *= gameController.scaleMultiplier;
        }
    }

    public float Scale {
        get {
            return transform.localScale.x;
        }
        set {
            transform.localScale = new Vector3(value, value, 1);
        }
    }

    public float Mass {
        get {
            return Mathf.Pow(gameController.particlesToMerge, Tier);
        }
    }

    protected void Awake() {
        rigidbody = GetComponent<Rigidbody2D>(); 
        gravitation = GetComponent<Gravitation>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        merger = GetComponentInChildren<Merger>();
        particleCollider = GetComponent<Collider2D>();
        mergeCollider = merger.GetComponent<Collider2D>();
    }
}
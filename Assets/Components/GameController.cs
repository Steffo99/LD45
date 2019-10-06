﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class GameController : MonoBehaviour
{
    [Header("Constants")]
    public float gravitationConstant = 2;
    public int particlesToMerge = 5;
    public int scaleMultiplier = 3;
    public int particleDurationConstant = 5;

    [Header("Black Hole")]
    public GameObject blackHolePrefab;
    public BlackHole blackHole;

    [Header("Particles")]
    public GameObject particlePrefab;
    public Gradient[] tierGradients;
    public RuntimeAnimatorController[] tierAnimation;
    public List<Gravitation> simulatedObjects;
    public int maxTierPresent;

    [Header("Upgrades")]
    public float[] upgradePushForce;
    public float[] upgradePushRadius;
    public int[] upgradeParticleCount;
    public int[] upgradeParticleTiers;

    [Header("Bought Upgrades")]
    public int _levelAntig = 0;
    public int _levelMatter = 0;
    public int _levelFission = 0;

    public int LevelAntig {
        get {
            return _levelAntig;
        }
        set {
            _levelAntig = value;
            pusher.pushForce = upgradePushForce[_levelAntig];
            pusher.pushRadius = upgradePushRadius[_levelAntig];
        }
    }

    public int LevelMatter {
        get {
            return _levelMatter;
        }
        set {
            _levelMatter = value;
            spawner.spawnedTier = upgradeParticleTiers[_levelMatter];
        }
    }

    public int LevelFission {
        get {
            return _levelFission;
        }
        set {
            _levelFission = value;
            spawner.spawnCount = upgradeParticleCount[_levelFission];
        }
    }

    [Header("References")]
    public SpawnOnMouseClick spawner;
    public PushOnMouseClick pusher;
    public CameraPan panner;
    public MusicManager musicManager;
    public Canvas canvas;

    protected void Awake() {
        spawner = Camera.main.GetComponent<SpawnOnMouseClick>();
        pusher = Camera.main.GetComponent<PushOnMouseClick>();
        panner = Camera.main.GetComponent<CameraPan>();
        musicManager = GetComponent<MusicManager>();
        simulatedObjects = new List<Gravitation>();
        blackHole = GameObject.FindGameObjectWithTag("BlackHole").GetComponent<BlackHole>();
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
    }

    protected void Start() {
        maxTierPresent = -1;
        LevelAntig = 0;
        LevelFission = 0;
        LevelMatter = 0;
    }

    public void CheckNewMaxTier(int tier) {
        if(tier > maxTierPresent) {
            maxTierPresent = tier;
            musicManager.UpdateLayers(maxTierPresent);
        }
    }

    public void RecalculateMaxTier() {
        maxTierPresent = -1;
        foreach(GameObject particleObject in GameObject.FindGameObjectsWithTag("Particle")) {
            Particle particle = particleObject.GetComponent<Particle>();
            if(particle.Tier > maxTierPresent) {
                maxTierPresent = particle.Tier;
            }
        };
        musicManager.UpdateLayers(maxTierPresent);
    }
}

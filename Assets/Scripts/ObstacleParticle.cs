﻿using UnityEngine;

public class ObstacleParticle : MonoBehaviour {

    [SerializeField]
    ParticleSystem m_particleSystem;

    [SerializeField]
    ParticleSystemRenderer m_particleSystemRenderer;

    [SerializeField]
    Material[] m_stateMaterials;

    public void PlayPfxWithColor(ColorState colorState, Transform obstacleTransform)
    {
        m_particleSystemRenderer.material = m_stateMaterials[(int)colorState];

        GameObject obstacleDestructionPFXGO = SimplePool.Spawn(this.gameObject, obstacleTransform.position, Quaternion.identity) as GameObject;
        obstacleDestructionPFXGO.transform.SetParent(obstacleTransform.parent);

        m_particleSystem.Play();
    }
}

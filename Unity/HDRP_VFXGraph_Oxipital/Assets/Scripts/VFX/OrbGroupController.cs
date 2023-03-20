using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbGroupController : MonoBehaviour
{
    public int idControlled;
    public int orbCount;

    [Header("PS Parameters")]
    [Range(0, 4000)]
    public float rate;
    [Range(0, 200)]
    public float life;
    [ColorUsage(true, true)]
    public Color color;
    public int colorIntensity;
    [Range(0, 1)]
    public float alpha;
    [Range(0, 100)]
    public float size;
    public float drag;
    [Range(0, 1)]
    public float velocityDrag;

    [Header("Emitter Parameters")]
    public OrbGroup.EmitterShape emitterShape;
    public OrbGroup.EmitterPlacementMode emitterPlacementMode;
    public float emitterSize;
    public bool emitFromInside;
    public bool activateCollision;

    private OrbsManager _orbsManager;
    private bool _isLinked;
    private int _idControlled = -1;

    // Start is called before the first frame update
    void Start()
    {
        _orbsManager = transform.parent.GetComponent<OrbsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Our id was updated then we need to update controller according to values
        if(idControlled != _idControlled)
		{
            foreach (OrbGroup oG in _orbsManager.orbs)
            {
                if (oG.orbGroupId == idControlled)
                {
                    rate = oG.rate;
                    life = oG.life;
                    color = oG.color;
                    colorIntensity = oG.colorIntensity;
                    alpha = oG.alpha;
                    size = oG.size;
                    drag = oG.drag;
                    velocityDrag = oG.velocityDrag;
                    emitterShape = oG.emitterShape;
                    emitterPlacementMode = oG.emitterPlacementMode;
                    emitterSize = oG.emitterSize;
                    emitFromInside = oG.emitFromInside;
                    activateCollision = oG.activateCollision;
                }

                _idControlled = idControlled;
            }
        }
        else
		{
            foreach (OrbGroup oG in _orbsManager.orbs)
            {
                if (oG.orbGroupId == idControlled)
                {
                    oG.rate = rate;
                    oG.life = life;
                    oG.color = color;
                    oG.colorIntensity = colorIntensity;
                    oG.alpha = alpha;
                    oG.size = size;
                    oG.drag = drag;
                    oG.velocityDrag = velocityDrag;
                    oG.emitterShape = emitterShape;
                    oG.emitterPlacementMode = emitterPlacementMode;
                    oG.emitterSize = emitterSize;
                    oG.emitFromInside = emitFromInside;
                    oG.activateCollision = activateCollision;
                }
            }
        }
    }
}

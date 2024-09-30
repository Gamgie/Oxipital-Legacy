using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HabiteAirPrefab : HabitePrefab
{
    public Chimera3D.FluidEmitter3D fluidEmitter;
    public Chimera3D.SmokeFluidSim smokeFluidSim;
    public Light spotLight;

    public override void Init(habiteMngr m)
    {
        id = 0;

        m_mngr = m;

        spotLight.DOIntensity(0, transitionDuration).From();
    }

    public override void ToBeDestroy()
    {
        // Turn down temperature to make particle move backward.
        DOTween.To(() => fluidEmitter.temperatureAmount,
                    x => fluidEmitter.temperatureAmount = x,
                    0,
                    transitionDuration*0.3f);

        // Wait for 20% and then dim light to dark in 78% of total time
        spotLight.DOIntensity(0, transitionDuration*0.78f).SetDelay(transitionDuration * 0.2f);

        // Destroy when we are sure transition is finished
        Destroy(this.gameObject, transitionDuration*1.2f);
    }

    public override void UpdatePrefab()
    {
        fluidEmitter.temperatureAmount = m_mngr.temperatureAmount;
        fluidEmitter.radius = m_mngr.emitterRadius;
        smokeFluidSim.m_densityBuoyancy = m_mngr.densityBuoyancy;
        smokeFluidSim.m_densityWeight = m_mngr.densityWeight;
    }

}

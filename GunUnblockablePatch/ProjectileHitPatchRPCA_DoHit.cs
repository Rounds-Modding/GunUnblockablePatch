using System;
using System.Collections;
using System.Text;
using System.Runtime.CompilerServices;
using HarmonyLib;
using UnityEngine;
using Photon.Pun;
using System.Reflection;

namespace GunUnblockablePatch
{
    [Serializable]
    [HarmonyPatch(typeof(ProjectileHit), "RPCA_DoHit")]
    class ProjectileHitPatchRPCA_DotHit
    {
		// prefix to fix gun.unblockable
		private static void Prefix(ProjectileHit __instance, Vector2 hitPoint, Vector2 hitNormal, Vector2 vel, int viewID, int colliderID, ref bool wasBlocked)
        {
			if (__instance.ownPlayer != null && __instance.ownPlayer.GetComponent<Holding>().holdable.GetComponent<Gun>() != null && __instance.ownPlayer.GetComponent<Holding>().holdable.GetComponent<Gun>().unblockable)
            {
				wasBlocked = false;
            }
            
        }
	}
}

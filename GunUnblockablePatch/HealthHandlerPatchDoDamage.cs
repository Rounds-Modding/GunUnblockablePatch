using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using HarmonyLib;
using UnityEngine;


namespace GunUnblockablePatch
{
    [Serializable]
    [HarmonyPatch(typeof(HealthHandler), "DoDamage")]
    class HealtHandlerPatchDoDamage
    {
        // patch for gun.unblockable
        private static void Prefix(HealthHandler __instance, Vector2 damage, Vector2 position, Color blinkColor, GameObject damagingWeapon, Player damagingPlayer, bool healthRemoval, bool lethal, ref bool ignoreBlock)
        {

            CharacterData data = (CharacterData)Traverse.Create(__instance).Field("data").GetValue();
			Player player = data.player;
			if (!data.isPlaying)
			{
				return;
			}
			if (data.dead)
			{
				return;
			}
			if (__instance.isRespawning)
			{
				return;
			}

			if (damagingPlayer != null && damagingPlayer.GetComponent<Holding>().holdable.GetComponent<Gun>() != null && damagingPlayer.GetComponent<Holding>().holdable.GetComponent<Gun>().unblockable)
            {
				ignoreBlock = true;
            }
        }
    }
}

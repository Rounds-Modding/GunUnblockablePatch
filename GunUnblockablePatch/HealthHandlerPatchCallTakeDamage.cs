using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;
using HarmonyLib;
using UnityEngine;
using Photon.Pun;


namespace GunUnblockablePatch
{
    [Serializable]
    [HarmonyPatch(typeof(HealthHandler), "CallTakeDamage")]
    class HealthHandlerPatchCallTakeDamage
    {
        // fix bug in base game method that caused CallTakeDamage to ignore Gun.unblockable
        private static void Postfix(HealthHandler __instance, Vector2 damage, Vector2 position, GameObject damagingWeapon, Player damagingPlayer, bool lethal)
        {
            CharacterData data = (CharacterData)Traverse.Create(__instance).Field("data").GetValue();
            // check if the original method skipped sending damage
            if (data.block.IsBlocking())
            {
                if (damagingPlayer != null && damagingPlayer.GetComponent<Holding>().holdable.GetComponent<Gun>() != null && damagingPlayer.GetComponent<Holding>().holdable.GetComponent<Gun>().unblockable)
                {
                    // if the opponent's gun had the unblockable tag, then this needs to call SendTakeDamage
                    data.view.RPC("RPCA_SendTakeDamage", RpcTarget.All, new object[]
                    {
                        damage,
                        position,
                        lethal,
                        (damagingPlayer != null) ? damagingPlayer.playerID : -1
                    });
                }
            }
        }
    }
}

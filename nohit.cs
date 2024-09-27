using Microsoft.CodeAnalysis.CSharp.Syntax;
using ReLogic.OS.Windows;
using System.Security.Cryptography.X509Certificates;
using Terraria;
using Terraria.Chat.Commands;
using Terraria.Graphics;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI.Chat;
using Terraria.ModLoader.Config;
using System.ComponentModel;
using Humanizer;
using Terraria.DataStructures;
using System.Collections.Generic;
using Terraria.Map;


namespace nohit
{
    public class nohitConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Header("Nohit")]
        [Label("Enable nohit")]
        [DefaultValue(true)]
        public bool EnableFeature;

    }
    public class nohit : Mod
    {
        public override void Load()
        {
            base.Load();
            ModContent.GetInstance<nohitConfig>();
        }

    }
    public class ExampleGlobalNPC : GlobalItem
    {
        public override void OnSpawn(Item item, IEntitySource source)
        {


            if (item.type is ItemID.KingSlimeBossBag or ItemID.EyeOfCthulhuBossBag)
            {
                item.value = Item.sellPrice(10, 0, 0, 0);
                item.stack = 3;
            }

            base.OnSpawn(item, source);
        }
    }


    public class nohitplayer : ModPlayer
    {
            public bool AnyBossAlive()
        {
            bool EnableFeature = ModContent.GetInstance<nohitConfig>().EnableFeature;
            if (EnableFeature == true)
            {


                foreach (NPC npc in Main.npc)
                {
                    if (npc.active && npc.boss)
                    {
                        return true;
                    }
                }
            }
                return false;
        }

        public override void ModifyHitByNPC(NPC npc, ref Player.HurtModifiers modifiers)
        {
            //checks if boss is alive
            if (AnyBossAlive() == true)
            {
                
                modifiers.FinalDamage += 1000;
            }
        }
        public override void ModifyHitByProjectile(Projectile proj, ref Player.HurtModifiers modifiers)
        {
            if (AnyBossAlive() == true)
            {
                modifiers.FinalDamage += 1000;

            }
        }
    }
}



using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;

namespace ExcuseMe.Items
{
	public class KnockbackTownNPC : GlobalItem
	{
		public Rectangle meleeHitbox = new();

		public override bool InstancePerEntity => true;

		public override bool? CanHitNPC(Item item, Player player, NPC target)
		{
			if (ValidItemForKnockback(item) && target.townNPC && meleeHitbox.Intersects(target.Hitbox)) return true;
			return null;
		}

		public override void ModifyHitNPC(Item item, Player player, NPC target, ref int damage, ref float knockBack, ref bool crit)
		{
			if (ValidItemForKnockback(item) && target.townNPC && meleeHitbox.Intersects(target.Hitbox))
			{
				damage = 0;
				crit = false;
				knockBack *= 3;
            }
        }

		public override void UseItemHitbox(Item item, Player player, ref Rectangle hitbox, ref bool noHitbox)
		{
			if (ValidItemForKnockback(item))
			{
				meleeHitbox = hitbox;
			}
        }

		private bool ValidItemForKnockback(Item item)
		{
			return !item.noMelee && item.knockBack > 0f && item.damage > 0 &&
				   item.useStyle == ItemUseStyleID.Swing || item.useStyle == ItemUseStyleID.Thrust || item.useStyle == ItemUseStyleID.Rapier;
		}
    }
}
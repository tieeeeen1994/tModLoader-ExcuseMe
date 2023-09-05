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

        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
			return ValidItemForKnockback(entity);
        }

        public override bool? CanHitNPC(Item item, Player player, NPC target)
		{
			if (ValidItemForKnockback(item) && target.townNPC && IsHitValid(player, target)) return true;
			return null;
		}

        public override void ModifyHitNPC(Item item, Player player, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (ValidItemForKnockback(item) && target.townNPC && IsHitValid(player, target))
            {
                modifiers.DisableCrit();
				modifiers.FinalDamage *= 0;
				modifiers.Knockback *= 3;
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
			return !item.noMelee && item.knockBack > 0f && item.damage > 0;
		}

		private bool IsHitValid(Player source, NPC target)
        {
            return meleeHitbox.Intersects(target.Hitbox) && Collision.CanHit(source, target);
        }
    }
}
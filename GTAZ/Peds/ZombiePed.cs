using System;
using System.Windows.Forms;
using GTA;
using GTA.Native;
using GTAZ.Controllable;
using GTAZ.Menus;

namespace GTAZ.Peds {

    public class ZombiePed : ControllablePed {

		private bool isExploded;

        public ZombiePed(int uid) : base(uid, "ZOMBIE_PED", 100f,
            new PedProperties {

                IsFriendly = false,
                IsZombie = true,

                SpawnRandomWeapons = true,
                RandomWeapons =
                    new[] {
                        WeaponHash.Unarmed, WeaponHash.Knife, WeaponHash.Dagger, WeaponHash.Crowbar, WeaponHash.Bat,
                        WeaponHash.Hammer, WeaponHash.Hatchet, WeaponHash.GolfClub
                    },

                Weapons = null,
                PreferredWeapon = WeaponHash.Unarmed,

                Armor = 0,
                Accuracy = 5,
                MaxHealth = 74,
                Health = 74,

                AttachBlip = true,
                BlipColor = BlipColor.Red,

                Teleport = false,
                HasMenu = false

            }) {
            
            Initialize += OnInitialize;
			isExploded = false;

        }

        private void OnInitialize(object sender, EventArgs eventArgs) {

            Ped.AlwaysKeepTask = true;
            Ped.Task.FightAgainst(Main.Player.Character);

        }

		protected override void OnUpdate(int tick)
		{
			base.OnUpdate(tick);

			if (Main.IsExplosionsToggled && !isExploded && Entity.IsDead)
			{
				Random rand = new Random(Game.GameTime);
				var prob = rand.Next(1, 101);
				if (prob <= 30)
				{
					World.AddExplosion(Entity.Position, ExplosionType.Valkyrie, 1.5f, 1.5f, true, false);
				}
				isExploded = true;
			}
		}

    }

}

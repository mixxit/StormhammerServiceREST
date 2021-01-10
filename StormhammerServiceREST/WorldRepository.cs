using StormhammerLibrary.Models;
using System;
using System.Collections.Generic;

namespace StormhammerServiceREST
{
    public class WorldRepository
    {
        public List<MobClass> MobClass { get;set; }
        public List<MobRace> MobRace { get; set; }

        public WorldRepository()
        {
            LoadMobClass();
            LoadMobRace();
        }

        private void LoadMobClass()
        {
            MobClass = new List<MobClass>();
            MobClass.Add(new MobClass() { Id = 1, Name = "Warrior" });
            MobClass.Add(new MobClass() { Id = 2, Name = "Cleric" });
            MobClass.Add(new MobClass() { Id = 3, Name = "Ranger" });
            MobClass.Add(new MobClass() { Id = 4, Name = "Rogue" });
            MobClass.Add(new MobClass() { Id = 5, Name = "Wizard" });
            MobClass.Add(new MobClass() { Id = 6, Name = "Paladin" });
            MobClass.Add(new MobClass() { Id = 7, Name = "Shadowknight" });
            MobClass.Add(new MobClass() { Id = 8, Name = "Shaman" });
            MobClass.Add(new MobClass() { Id = 9, Name = "Druid" });
            MobClass.Add(new MobClass() { Id = 10, Name = "Bard" });
            MobClass.Add(new MobClass() { Id = 11, Name = "Monk" });
            MobClass.Add(new MobClass() { Id = 12, Name = "Magician" });
            MobClass.Add(new MobClass() { Id = 13, Name = "Necromancer" });
            MobClass.Add(new MobClass() { Id = 14, Name = "Enchanter" });
        }
        private void LoadMobRace()
        {
            MobRace = new List<MobRace>();
            MobRace.Add(new MobRace() { Id = 1, Name = "Human" });
            MobRace.Add(new MobRace() { Id = 2, Name = "Barbarian" });
            MobRace.Add(new MobRace() { Id = 3, Name = "Erudite" });
            MobRace.Add(new MobRace() { Id = 4, Name = "Wood Elf" });
            MobRace.Add(new MobRace() { Id = 5, Name = "High Elf" });
        }
    }
}
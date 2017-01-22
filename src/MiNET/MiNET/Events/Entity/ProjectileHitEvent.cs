using MiNET.Plugins;
using MiNET.Utils;
using MiNET.Worlds;
using System;

namespace MiNET.Events.Entity
{
    /*public class ProjectileHitEvent : Plugin
    {
        public event EventHandler<ProjectileHitEventArgs> HitEvent;
        public virtual void OnProjectileHitEvent(ProjectileHitEventArgs e)
        {
            EventHandler<ProjectileHitEventArgs> handler = HitEvent;
            if (handler != null) handler(this, e);
        }

    }*/

    public class ProjectileHitEventArgs : EventArgs
    {
        public long EntityID { get; set; }
        public Level Level { get; set; }
        public Player Shooter { get; set; }

        public PlayerLocation KnownPosition { get; set; }

        public ProjectileHitEventArgs(long entityID, Level level, PlayerLocation knownPosition, Player shooter)
        {
            EntityID = entityID;
            Shooter = shooter;
            KnownPosition = knownPosition;
            Level = level;
        }
    }
}

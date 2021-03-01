﻿using System;
namespace EntityComponentSystemClassLibrary.ECS
{
    public interface IComponent
    {
        public string Name
        {
            get { return GetType().Name; }
        }

        public Entity OwningEntity
        {
            get { return OwningEntity; }
        }
    }
}

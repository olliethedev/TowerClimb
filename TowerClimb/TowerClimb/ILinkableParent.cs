using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TowerClimb
{
    interface ILinkableParent
    {
        void addChild(ILinkableChild cha);
        void onLinkableUpdate();
        Vector2 getParentPosChange();
    }
}

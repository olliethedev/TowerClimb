using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TowerClimb
{
    interface ILinkableChild
    {
        void onParentUpdate(ILinkableParent per);
    }
}

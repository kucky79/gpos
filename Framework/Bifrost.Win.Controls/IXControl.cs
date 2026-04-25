using System;
using System.Collections.Generic;
using System.Text;
using NF.Framework;

namespace NF.Framework.Win.Controls
{
    public interface IXControl
    {
        void OnSubSystemTypeChanged(SubSystemType subSysType, bool Header);
    }
}

﻿using OOO_StroiMaterial.DbEnti;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOO_StroiMaterial.DbEnti
{
    public static class AppData
    {
        public static TradeEntities1 db { get; set; } = new TradeEntities1();
    }
}

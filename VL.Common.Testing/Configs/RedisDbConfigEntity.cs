﻿using System;
using System.Collections.Generic;
using System.Xml.Linq;
using VL.Common.Core.DAS;

namespace VL.Common.Testing.Configs
{
    class RedisDbConfigEntity : DbConfigEntity
    {
        public RedisDbConfigEntity(string fileName = "RedisDbConnections") : base(fileName)
        {
        }

        public override IEnumerable<XElement> ToXElements()
        {
            throw new NotImplementedException();
        }

        protected override List<DbConfigItem> GetDbConfigItems()
        {
            throw new NotImplementedException();
        }
    }
}

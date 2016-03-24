﻿using System.Collections.Generic;
using System.Data;

namespace VL.Common.ORM.Objects
{
    /// <summary>
    /// 表基类 
    /// 它定义了复合PDM模型生成的表 
    /// 需有TableName,Properties
    /// 同时他定义了基于会话DbSession的操作基本规范
    /// </summary>
    public abstract class IPDMTBase
    {
        public IPDMTBase()
        {
        }
        public IPDMTBase(IDataReader reader)
        {
            Init(reader);
        }

        /// <summary>
        /// 通过DataReader初始化数据
        /// </summary>
        /// <param name="reader"></param>
        public abstract void Init(IDataReader reader);
        /// <summary>
        /// 通过DataReader初始化数据
        /// </summary>
        /// <param name="reader"></param>
        public abstract void Init(IDataReader reader, List<string> fields);
        /// <summary>
        /// 获取表名
        /// </summary>
        public abstract string GetTableName();
    }
}

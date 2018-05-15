using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;

namespace VL.Common.Core.ORM
{
    /// <summary>
    /// 表基类 
    /// 它定义了复合PDM模型生成的表 
    /// 需有TableName,Properties
    /// 同时他定义了基于会话IDbSession的操作基本规范
    /// </summary>
    [DataContract]
    public abstract class VLModel_DB
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public VLModel_DB()
        {
            PreInit();
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public VLModel_DB(IDataReader reader)
        {
            PreInit();
            Init(reader);
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public VLModel_DB(IDataReader reader, List<string> fields)
        {
            PreInit();
            Init(reader, fields);
        }
        /// <summary>
        /// 初始化扩展
        /// </summary>
        public virtual void PreInit()
        {
        }
        /// <summary>
        /// 通过DataReader初始化数据
        /// </summary>
        /// <param name="reader"></param>
        public virtual void Init(IDataReader reader)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 通过DataReader初始化数据
        /// </summary>
        public virtual void Init(IDataReader reader, List<string> fields)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 获取表名
        /// </summary>
        public virtual string TableName { get; }
    }
}

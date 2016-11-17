//using System.Collections.Generic;
//using System.Data;
//using System.Runtime.Serialization;

//namespace VL.Common.ORM
//{
//    /// <summary>
//    /// 表基类 
//    /// 它定义了复合PDM模型生成的表 
//    /// 需有TableName,Properties
//    /// 同时他定义了基于会话DbSession的操作基本规范
//    /// </summary>
//    [DataContract]
//    public abstract class IPDMTBase
//    {
//        /// <summary>
//        /// 构造函数
//        /// </summary>
//        public IPDMTBase()
//        {
//        }
//        /// <summary>
//        /// 构造函数
//        /// </summary>
//        /// <param name="reader"></param>
//        public IPDMTBase(IDataReader reader)
//        {
//            Init(reader);
//        }

//        /// <summary>
//        /// 通过DataReader初始化数据
//        /// </summary>
//        /// <param name="reader"></param>
//        public abstract void Init(IDataReader reader);
//        /// <summary>
//        /// 通过DataReader初始化数据
//        /// </summary>
//        public abstract void Init(IDataReader reader, List<string> fields);
//        /// <summary>
//        /// 获取表名
//        /// </summary>
//        public abstract string TableName { get; }
//    }
//}

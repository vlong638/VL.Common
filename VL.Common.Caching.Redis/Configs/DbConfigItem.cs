using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace VL.Common.Caching.Redis.Configs
{
    public class DbConfigItem
    {
        public string DbName { set; get; }
        public string Host { set; get; }
        public int Port { set; get; }
        public int DbNumber { set; get; }

        public DbConfigItem(string dbName)
        {
            DbName = dbName;
            Host = "";
            Port = 0;
            DbNumber = 0;
        }

        public void Init(XElement element)
        {
            DbName = element.Attribute(nameof(DbName)).Value;
            Host = element.Attribute(nameof(Host)).Value;
            Port = Convert.ToInt32(element.Attribute(nameof(Port)).Value);
            DbNumber = Convert.ToInt32(element.Attribute(nameof(DbNumber)).Value);
        }
        public XElement ToXElement()
        {
            return new XElement(nameof(DbConfigItem)
                , new XAttribute(nameof(DbName), DbName)
                , new XAttribute(nameof(Host), Host)
                , new XAttribute(nameof(Port), Port)
                , new XAttribute(nameof(DbNumber), DbNumber));
        }
        public RedisEndpoint ToRedisEndpoint()
        {
            RedisEndpoint endpoint = new RedisEndpoint();
            endpoint.Host = Host;
            endpoint.Port = Port;
            endpoint.Db = DbNumber;
            return endpoint;
        }
    }
}

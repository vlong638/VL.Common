using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;
using VL.Common.Object.ORM;

namespace VL.Common.Object.VL.Blog
{
    [DataContract]
    public partial class TTag : IPDMTBase
    {
        #region Properties
        [DataMember]
        public String UserName { get; set; }
        [DataMember]
        public Guid TagId { get; set; }
        [DataMember]
        public String Name { get; set; }
        #endregion

        #region Constructors
        public TTag()
        {
        }
        public TTag(Guid tagId)
        {
            TagId = tagId;
        }
        public TTag(IDataReader reader) : base(reader)
        {
        }
        #endregion

        #region Methods
        public override void Init(IDataReader reader)
        {
            this.UserName = Convert.ToString(reader[nameof(this.UserName)]);
            this.TagId = new Guid(reader[nameof(this.TagId)].ToString());
            this.Name = Convert.ToString(reader[nameof(this.Name)]);
        }
        public override void Init(IDataReader reader, List<string> fields)
        {
            if (fields.Contains(nameof(UserName)))
            {
                this.UserName = Convert.ToString(reader[nameof(this.UserName)]);
            }
            if (fields.Contains(nameof(TagId)))
            {
                this.TagId = new Guid(reader[nameof(this.TagId)].ToString());
            }
            if (fields.Contains(nameof(Name)))
            {
                this.Name = Convert.ToString(reader[nameof(this.Name)]);
            }
        }
        [DataMember]
        public override string TableName
        {
            get
            {
                return nameof(TTag);
            }
        }
        #endregion
    }
}

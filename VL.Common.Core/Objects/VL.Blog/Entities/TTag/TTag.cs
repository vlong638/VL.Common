using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;
using VL.Common.Core.ORM;

namespace VL.Common.Core.Object.VL.Blog
{
    [DataContract]
    public partial class TTag : VLModel_DB
    {
        #region Properties
        [DataMember]
        public String UserName { get; set; }
        [DataMember]
        public String TagName { get; set; }
        #endregion

        #region Constructors
        public TTag()
        {
        }
        public TTag(String userName, String tagName)
        {
            UserName = userName;
            TagName = tagName;
        }
        public TTag(IDataReader reader) : base(reader)
        {
        }
        #endregion

        #region Methods
        public override void Init(IDataReader reader)
        {
            this.UserName = Convert.ToString(reader[nameof(this.UserName)]);
            this.TagName = Convert.ToString(reader[nameof(this.TagName)]);
        }
        public override void Init(IDataReader reader, List<string> fields)
        {
            if (fields.Contains(nameof(UserName)))
            {
                this.UserName = Convert.ToString(reader[nameof(this.UserName)]);
            }
            if (fields.Contains(nameof(TagName)))
            {
                this.TagName = Convert.ToString(reader[nameof(this.TagName)]);
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

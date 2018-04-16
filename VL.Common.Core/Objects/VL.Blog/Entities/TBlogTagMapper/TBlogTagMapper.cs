using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;
using VL.Common.Core.ORM;

namespace VL.Common.Core.Object.VL.Blog
{
    [DataContract]
    public partial class TBlogTagMapper : VLModel_DB
    {
        #region Properties
        [DataMember]
        public Guid BlogId { get; set; }
        [DataMember]
        public String TagName { get; set; }
        #endregion

        #region Constructors
        public TBlogTagMapper()
        {
        }
        public TBlogTagMapper(Guid blogId, String tagName)
        {
            BlogId = blogId;
            TagName = tagName;
        }
        public TBlogTagMapper(IDataReader reader) : base(reader)
        {
        }
        #endregion

        #region Methods
        public override void Init(IDataReader reader)
        {
            this.BlogId = new Guid(reader[nameof(this.BlogId)].ToString());
            this.TagName = Convert.ToString(reader[nameof(this.TagName)]);
        }
        public override void Init(IDataReader reader, List<string> fields)
        {
            if (fields.Contains(nameof(BlogId)))
            {
                this.BlogId = new Guid(reader[nameof(this.BlogId)].ToString());
            }
            if (fields.Contains(nameof(TagName)))
            {
                this.TagName = Convert.ToString(reader[nameof(this.TagName)]);
            }
        }

        public override void PreInit()
        {
        }

        [DataMember]
        public override string TableName
        {
            get
            {
                return nameof(TBlogTagMapper);
            }
        }
        #endregion
    }
}

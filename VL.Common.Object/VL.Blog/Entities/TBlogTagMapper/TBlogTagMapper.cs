using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Data;
using VL.Common.Object.ORM;

namespace VL.Common.Object.VL.Blog
{
    [DataContract]
    public partial class TBlogTagMapper : IPDMTBase
    {
        #region Properties
        [DataMember]
        public Guid TagId { get; set; }
        [DataMember]
        public Guid BlogId { get; set; }
        #endregion

        #region Constructors
        public TBlogTagMapper()
        {
        }
        public TBlogTagMapper(Guid tagId, Guid blogId)
        {
            TagId = tagId;
            BlogId = blogId;
        }
        public TBlogTagMapper(IDataReader reader) : base(reader)
        {
        }
        #endregion

        #region Methods
        public override void Init(IDataReader reader)
        {
            this.TagId = new Guid(reader[nameof(this.TagId)].ToString());
            this.BlogId = new Guid(reader[nameof(this.BlogId)].ToString());
        }
        public override void Init(IDataReader reader, List<string> fields)
        {
            if (fields.Contains(nameof(TagId)))
            {
                this.TagId = new Guid(reader[nameof(this.TagId)].ToString());
            }
            if (fields.Contains(nameof(BlogId)))
            {
                this.BlogId = new Guid(reader[nameof(this.BlogId)].ToString());
            }
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

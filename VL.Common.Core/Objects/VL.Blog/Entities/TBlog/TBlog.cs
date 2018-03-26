using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;
using VL.Common.Core.ORM;

namespace VL.Common.Core.Object.VL.Blog
{
    [DataContract]
    public partial class TBlog : VLModel_DB
    {
        #region Properties
        [DataMember]
        public String UserName { get; set; }
        [DataMember]
        public Guid BlogId { get; set; }
        [DataMember]
        public String Title { get; set; }
        [DataMember]
        public String BreviaryContent { get; set; }
        [DataMember]
        public DateTime CreatedTime { get; set; }
        [DataMember]
        public DateTime LastEditTime { get; set; }
        [DataMember]
        public Boolean IsVisible { get; set; }
        #endregion

        #region Constructors
        public TBlog()
        {
        }
        public TBlog(Guid blogId)
        {
            BlogId = blogId;
        }
        public TBlog(IDataReader reader) : base(reader)
        {
        }
        #endregion

        #region Methods
        public override void Init(IDataReader reader)
        {
            this.UserName = Convert.ToString(reader[nameof(this.UserName)]);
            this.BlogId = new Guid(reader[nameof(this.BlogId)].ToString());
            this.Title = Convert.ToString(reader[nameof(this.Title)]);
            this.BreviaryContent = Convert.ToString(reader[nameof(this.BreviaryContent)]);
            this.CreatedTime = Convert.ToDateTime(reader[nameof(this.CreatedTime)]);
            this.LastEditTime = Convert.ToDateTime(reader[nameof(this.LastEditTime)]);
            this.IsVisible = Convert.ToBoolean(reader[nameof(this.IsVisible)]);
        }
        public override void Init(IDataReader reader, List<string> fields)
        {
            if (fields.Contains(nameof(UserName)))
            {
                this.UserName = Convert.ToString(reader[nameof(this.UserName)]);
            }
            if (fields.Contains(nameof(BlogId)))
            {
                this.BlogId = new Guid(reader[nameof(this.BlogId)].ToString());
            }
            if (fields.Contains(nameof(Title)))
            {
                this.Title = Convert.ToString(reader[nameof(this.Title)]);
            }
            if (fields.Contains(nameof(BreviaryContent)))
            {
                this.BreviaryContent = Convert.ToString(reader[nameof(this.BreviaryContent)]);
            }
            if (fields.Contains(nameof(CreatedTime)))
            {
                this.CreatedTime = Convert.ToDateTime(reader[nameof(this.CreatedTime)]);
            }
            if (fields.Contains(nameof(LastEditTime)))
            {
                this.LastEditTime = Convert.ToDateTime(reader[nameof(this.LastEditTime)]);
            }
            if (fields.Contains(nameof(IsVisible)))
            {
                this.IsVisible = Convert.ToBoolean(reader[nameof(this.IsVisible)]);
            }
        }
        [DataMember]
        public override string TableName
        {
            get
            {
                return nameof(TBlog);
            }
        }
        #endregion
    }
}

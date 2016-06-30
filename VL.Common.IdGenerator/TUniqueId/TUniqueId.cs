using System;
using System.Collections.Generic;
using System.Data;
using VL.Common.ORM.Objects;

namespace VL.Common.Entities
{
    public partial class TUniqueId : IPDMTBase
    {
        #region Properties
        public String IdentityName { get; set; }
        public String IdentityValue { get; set; }
        #endregion

        #region Constructors
        public TUniqueId()
        {
        }
        public TUniqueId(IDataReader reader) : base(reader)
        {
        }
        #endregion

        #region Methods
        public override void Init(IDataReader reader)
        {
            this.IdentityName = Convert.ToString(reader[nameof(this.IdentityName)]);
            this.IdentityValue = Convert.ToString(reader[nameof(this.IdentityValue)]);
        }
        public override void Init(IDataReader reader, List<string> fields)
        {
            if (fields.Contains(nameof(IdentityName)))
            {
                this.IdentityName = Convert.ToString(reader[nameof(this.IdentityName)]);
            }
            if (fields.Contains(nameof(IdentityValue)))
            {
                this.IdentityValue = Convert.ToString(reader[nameof(this.IdentityValue)]);
            }
        }
        public override string TableName
        {
            get
            {
                return nameof(TUniqueId);
            }
        }
        #endregion
    }
}

using VL.Common.ORM.Objects;

namespace VL.Common.Entities
{
    public class TUniqueIdProperties
    {
        #region Properties
        public static PDMDbProperty IdentityName { get; set; } = new PDMDbProperty(nameof(IdentityName), "IdentityName", "标识名", true, PDMDataType.varchar, 20, 0, true, null);
        public static PDMDbProperty IdentityValue { get; set; } = new PDMDbProperty(nameof(IdentityValue), "IdentityValue", "标志量", false, PDMDataType.varchar, 40, 0, true, null);
        #endregion
    }
}

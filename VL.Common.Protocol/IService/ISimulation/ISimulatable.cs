using System;
using VL.Common.DAS.Objects;

namespace VL.Common.Protocol.IService
{
    /// <summary>
    /// 模拟三要素
    /// 空间:模拟落实的物理位置
    /// 时间:时间总是在变化的,我们总是试图去模拟某一个时间点的行为.
    /// 对象:模拟的目标内容
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISimulatable
    {
        Result SimulateCreate(DbSession session, DateTime simulateTime);
    }
}

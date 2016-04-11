using System;
using VL.Common.DAS.Objects;
using VL.Common.Protocol.IResult;

namespace VL.Common.Protocol.ISimulation
{
    /// <summary>
    /// 模拟三要素
    /// 空间:模拟落实的物理位置
    /// 对象:模拟的目标内容
    /// 时间:时间总是在变化的,我们总是试图去模拟某一个时间点的行为.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISimulatable<T>
    {
        Result SimulateCreate(DbSession session, T t, DateTime simulateTime);
    }
}
